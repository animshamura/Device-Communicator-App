namespace DeviceCommunicatorApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblHost;
        private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TextBox txtCommand;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.CheckBox chkHexView;
        private System.Windows.Forms.CheckBox chkHexMode;
        private System.Windows.Forms.CheckBox chkAppendCRLF;
        private System.Windows.Forms.CheckBox chkAutoClearAfterSend;
        private System.Windows.Forms.Button btnClearLog;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblHost = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.txtCommand = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.chkHexView = new System.Windows.Forms.CheckBox();
            this.chkHexMode = new System.Windows.Forms.CheckBox();
            this.chkAppendCRLF = new System.Windows.Forms.CheckBox();
            this.chkAutoClearAfterSend = new System.Windows.Forms.CheckBox();
            this.btnClearLog = new System.Windows.Forms.Button();

            this.SuspendLayout();

            this.lblHost.Location = new System.Drawing.Point(12, 15);
            this.lblHost.Text = "Host/IP:";
            this.lblHost.Size = new System.Drawing.Size(55, 20);

            this.txtHost.Location = new System.Drawing.Point(70, 12);
            this.txtHost.Size = new System.Drawing.Size(120, 22);
            this.txtHost.Text = "127.0.0.1";

            this.lblPort.Location = new System.Drawing.Point(205, 15);
            this.lblPort.Text = "Port:";
            this.lblPort.Size = new System.Drawing.Size(35, 20);

            this.txtPort.Location = new System.Drawing.Point(245, 12);
            this.txtPort.Size = new System.Drawing.Size(60, 22);
            this.txtPort.Text = "8080";

            this.btnConnect.Location = new System.Drawing.Point(320, 10);
            this.btnConnect.Size = new System.Drawing.Size(90, 26);
            this.btnConnect.Text = "Connect";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);

            this.lblStatus.Location = new System.Drawing.Point(425, 15);
            this.lblStatus.Size = new System.Drawing.Size(150, 20);
            this.lblStatus.Text = "Status: Disconnected";

            this.txtLog.Location = new System.Drawing.Point(12, 50);
            this.txtLog.Size = new System.Drawing.Size(660, 300);
            this.txtLog.Multiline = true;
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtLog.WordWrap = false;

            this.txtCommand.Location = new System.Drawing.Point(12, 365);
            this.txtCommand.Size = new System.Drawing.Size(420, 22);
            this.txtCommand.Text = "PING";

            this.btnSend.Location = new System.Drawing.Point(442, 363);
            this.btnSend.Size = new System.Drawing.Size(90, 26);
            this.btnSend.Text = "Send TX";
            this.btnSend.Enabled = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);

            this.chkHexMode.Location = new System.Drawing.Point(540, 365);
            this.chkHexMode.Size = new System.Drawing.Size(95, 22);
            this.chkHexMode.Text = "Hex mode";

            this.chkAppendCRLF.Location = new System.Drawing.Point(540, 388);
            this.chkAppendCRLF.Size = new System.Drawing.Size(110, 22);
            this.chkAppendCRLF.Text = "Append CRLF";
            this.chkAppendCRLF.Checked = true;

            this.chkAutoClearAfterSend.Location = new System.Drawing.Point(540, 411);
            this.chkAutoClearAfterSend.Size = new System.Drawing.Size(120, 22);
            this.chkAutoClearAfterSend.Text = "Clear after send";

            this.chkHexView.Location = new System.Drawing.Point(12, 390);
            this.chkHexView.Size = new System.Drawing.Size(90, 22);
            this.chkHexView.Text = "Hex View";

            this.btnClearLog.Location = new System.Drawing.Point(580, 10);
            this.btnClearLog.Size = new System.Drawing.Size(90, 26);
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);

            this.ClientSize = new System.Drawing.Size(684, 445);
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.lblHost, this.txtHost, this.lblPort, this.txtPort, this.btnConnect,
                this.lblStatus, this.txtLog, this.txtCommand, this.btnSend, this.chkHexMode,
                this.chkAppendCRLF, this.chkAutoClearAfterSend, this.chkHexView, this.btnClearLog
            });
            this.Text = "Real-Time Device Communication & Protocol Analyzer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}