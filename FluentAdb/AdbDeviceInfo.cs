using System;
using FluentAdb.Interfaces;

namespace FluentAdb
{
    public class AdbDeviceInfo : IDeviceInfo
    {
        internal AdbDeviceInfo(string deviceString)
        {
            var tokens = deviceString.Split(new [] {'\t'}, StringSplitOptions.RemoveEmptyEntries);
            SerialNumber = tokens[0].Trim();
            string state = tokens[1].Trim();
            if (state == "device")
            {
                IsDevice = true;
                State = DeviceState.Online;
            }
            else if (state == "unauthorized")
            {
                State = DeviceState.Unauthorized;
            }
            else if (state == "offline")
            {
                State = DeviceState.Offline;
            }
            else
            {
                throw new UnexpectedOutputException(deviceString);
            }
        }

        public bool? IsDevice { get; private set; }
        public string SerialNumber { get; private set; }
        public DeviceState State { get; private set; }
    }
}
