using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DeviceCommunicatorApp
{
    public partial class Form1 : Form
    {
        private TcpClient? tcpClient;
        private NetworkStream? stream;
        private CancellationTokenSource? cts;
        private readonly List<string> commandHistory = new();
        private int commandHistoryIndex = -1;

        public Form1()
        {
            InitializeComponent();
            txtCommand.KeyDown += txtCommand_KeyDown;
            txtCommand.Text = "PING";
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            if (tcpClient is { Connected: true })
            {
                Disconnect();
                return;
            }

            if (!TryGetHostAndPort(out string host, out int port))
            {
                return;
            }

            try
            {
                btnConnect.Enabled = false;
                tcpClient = new TcpClient();
                await tcpClient.ConnectAsync(host, port).WaitAsync(TimeSpan.FromSeconds(10));

                stream = tcpClient.GetStream();
                cts = new CancellationTokenSource();

                UpdateConnectionStatus(true);
                LogMessage("SYSTEM", $"Connected to {host}:{port}");

                _ = Task.Run(() => ReceiveLoop(cts.Token));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateConnectionStatus(false);
            }
            finally
            {
                btnConnect.Enabled = true;
            }
        }

        private async Task ReceiveLoop(CancellationToken token)
        {
            byte[] buffer = new byte[2048];

            try
            {
                while (!token.IsCancellationRequested && tcpClient is { Connected: true } && stream != null)
                {
                    int bytesRead = await stream.ReadAsync(buffer.AsMemory(0, buffer.Length), token).ConfigureAwait(false);
                    if (bytesRead == 0)
                    {
                        break;
                    }

                    byte[] receivedBytes = new byte[bytesRead];
                    Array.Copy(buffer, receivedBytes, bytesRead);

                    var packet = new PacketModel("RX", receivedBytes);

                    if (IsHandleCreated)
                    {
                        BeginInvoke(new Action(() => LogPacket(packet)));
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Socket shutdown requested by disconnect workflow.
            }
            catch (ObjectDisposedException)
            {
                // Ignore disposed stream during shutdown.
            }
            catch (IOException)
            {
                // Treat network disconnects as a normal closure.
            }
            catch (Exception ex)
            {
                if (IsHandleCreated)
                {
                    BeginInvoke(new Action(() => LogMessage("ERROR", ex.Message)));
                }
            }
            finally
            {
                if (IsHandleCreated)
                {
                    BeginInvoke(new Action(() =>
                    {
                        if (tcpClient is { Connected: true })
                        {
                            Disconnect();
                        }
                        else
                        {
                            UpdateConnectionStatus(false);
                        }
                    }));
                }
            }
        }

        private async void btnSend_Click(object sender, EventArgs e)
        {
            if (tcpClient == null || !tcpClient.Connected || stream == null)
            {
                MessageBox.Show("Not connected to a target device.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string commandText = txtCommand.Text.Trim();
            if (string.IsNullOrEmpty(commandText))
            {
                return;
            }

            try
            {
                byte[] sendBuffer = BuildPayload(commandText);
                await stream.WriteAsync(sendBuffer, 0, sendBuffer.Length);

                AddCommandToHistory(commandText);

                PacketModel packet = new PacketModel("TX", sendBuffer);
                LogPacket(packet);

                if (chkAutoClearAfterSend.Checked)
                {
                    txtCommand.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Transmission error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private byte[] BuildPayload(string commandText)
        {
            if (chkHexMode.Checked)
            {
                return ParseHexPayload(commandText);
            }

            string suffix = chkAppendCRLF.Checked ? "\r\n" : string.Empty;
            return Encoding.ASCII.GetBytes(commandText + suffix);
        }

        private static byte[] ParseHexPayload(string payload)
        {
            string normalized = Regex.Replace(payload, "\\s+", string.Empty);

            if (normalized.Length % 2 != 0)
            {
                throw new FormatException("Hex payload must contain an even number of digits.");
            }

            return Enumerable.Range(0, normalized.Length / 2)
                .Select(index => Convert.ToByte(normalized.Substring(index * 2, 2), 16))
                .ToArray();
        }

        private void Disconnect()
        {
            cts?.Cancel();
            stream?.Close();
            tcpClient?.Close();

            stream = null;
            tcpClient = null;
            cts = null;

            UpdateConnectionStatus(false);
            LogMessage("SYSTEM", "Disconnected from device.");
        }

        private void UpdateConnectionStatus(bool connected)
        {
            lblStatus.Text = connected ? "Status: Connected" : "Status: Disconnected";
            btnConnect.Text = connected ? "Disconnect" : "Connect";
            txtHost.Enabled = !connected;
            txtPort.Enabled = !connected;
            btnSend.Enabled = connected;
        }

        private void LogPacket(PacketModel packet)
        {
            string mode = chkHexView.Checked ? packet.HexData : packet.AsciiData.TrimEnd();
            txtLog.AppendText($"[{packet.Timestamp:HH:mm:ss.fff}] [{packet.Direction}] {mode}{Environment.NewLine}");
        }

        private void LogMessage(string tag, string text)
        {
            if (txtLog.IsDisposed)
            {
                return;
            }

            txtLog.AppendText($"[{DateTime.Now:HH:mm:ss.fff}] [{tag}] {text}{Environment.NewLine}");
        }

        private void AddCommandToHistory(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                return;
            }

            if (commandHistory.Count == 0 || !commandHistory[^1].Equals(command, StringComparison.Ordinal))
            {
                commandHistory.Add(command);
            }

            commandHistoryIndex = commandHistory.Count - 1;
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtLog.Clear();
        }

        private void txtCommand_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)
            {
                return;
            }

            if (commandHistory.Count == 0)
            {
                return;
            }

            if (e.KeyCode == Keys.Up)
            {
                commandHistoryIndex = Math.Max(0, commandHistoryIndex - 1);
            }
            else
            {
                commandHistoryIndex = Math.Min(commandHistory.Count - 1, commandHistoryIndex + 1);
            }

            txtCommand.Text = commandHistory[commandHistoryIndex];
            txtCommand.SelectionStart = txtCommand.TextLength;
            txtCommand.SelectionLength = 0;
            e.Handled = true;
        }

        private bool TryGetHostAndPort(out string host, out int port)
        {
            host = txtHost.Text.Trim();
            if (string.IsNullOrWhiteSpace(host))
            {
                MessageBox.Show("Please enter a valid host or IP address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                port = 0;
                return false;
            }

            if (!int.TryParse(txtPort.Text.Trim(), out port) || port <= 0 || port > 65535)
            {
                MessageBox.Show("Please enter a valid port number between 1 and 65535.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Disconnect();
        }
    }
}