namespace SerialCommunicationUWP
{
    public class SerialDeviceItem
    {
        public SerialDeviceItem()
        {
            SerialMain = new SerialMain();
            ConnectDisconnectHandle = new SerialConnectDisconnect();
            ReadWriteHandle = new SerialReadWrite();
            ConfigDevice = new SerialConfigureDevice();
        }

        public SerialMain SerialMain { get; private set; }
        public SerialConfigureDevice ConfigDevice { get; private set; }
        public SerialConnectDisconnect ConnectDisconnectHandle { get; private set; }
        public SerialReadWrite ReadWriteHandle { get; private set; }
    }
}