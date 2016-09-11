using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAdb.Interfaces;
using FluentAdb.Tests.Util;
using NUnit.Framework;

namespace FluentAdb.Tests
{
    [TestFixture]
    public class BugTests
    {
        [Test]
        public async Task RestartServerMessagIgnoreAtDevicesList()
        {
            //Arrange
            var output = new List<string>
            {
                  "adb server is out of date.  killing...",
                  "* daemon started successfully *",
                  "List of devices attached", 
                  "YT910X72U3	device"
            };
            var processManager = new TestProcessManager();
            processManager.AddProcess(output);
            var adb = new Adb(processManager);

            //Act
            var devices = await adb.GetDevices();

            //Assert

            Assert.AreEqual(1, devices.Count());
            Assert.AreEqual("YT910X72U3", devices.First().SerialNumber);
            Assert.AreEqual(DeviceState.Online, devices.First().State);
        }
    }
}
