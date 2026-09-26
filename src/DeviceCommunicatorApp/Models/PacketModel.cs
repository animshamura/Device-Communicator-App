using System;
using System.Text;

namespace DeviceCommunicatorApp
{
    public class PacketModel
    {
        public DateTime Timestamp { get; set; }
        public string Direction { get; set; } // "TX" (Transmit) or "RX" (Receive)
        public byte[] RawBytes { get; set; }
        public string HexData => BitConverter.ToString(RawBytes).Replace("-", " ");
        public string AsciiData => Encoding.ASCII.GetString(RawBytes);
        public string PrintableData => string.Concat(AsciiData.Select(ch => char.IsControl(ch) ? '.' : ch));

        public PacketModel(string direction, byte[] bytes)
        {
            Timestamp = DateTime.Now;
            Direction = direction;
            RawBytes = bytes;
        }
    }
}