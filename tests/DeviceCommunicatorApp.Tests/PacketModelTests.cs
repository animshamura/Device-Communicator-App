using DeviceCommunicatorApp;

namespace DeviceCommunicatorApp.Tests;

public class PacketModelTests
{
    [Fact]
    public void HexData_FormatsBytesAsSpaceSeparatedHex()
    {
        var packet = new PacketModel("TX", new byte[] { 0x0A, 0x1F, 0xFF });

        Assert.Equal("0A 1F FF", packet.HexData);
    }

    [Fact]
    public void AsciiData_ReturnsOriginalText()
    {
        var packet = new PacketModel("RX", "PING\r\n".Select(ch => (byte)ch).ToArray());

        Assert.Equal("PING\r\n", packet.AsciiData);
    }

    [Fact]
    public void PrintableData_ReplacesControlCharactersWithDots()
    {
        var packet = new PacketModel("RX", new byte[] { 0x50, 0x49, 0x4E, 0x47, 0x0D, 0x0A });

        Assert.Equal("PING..", packet.PrintableData);
    }
}
