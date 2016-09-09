namespace FluentAdb.Interfaces
{
    public enum DeviceState
    {
        Offline,
        Online, 
        Unauthorized
    }

    public interface IDeviceInfo
    {
        bool? IsDevice { get; }
        string SerialNumber { get; }
        DeviceState State { get; }
    }
}
