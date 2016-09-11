using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAdb.Enums;
using FluentAdb.Interfaces;
using FluentAdb.Tests.Util;
using NUnit.Framework;
using Rhino.Mocks;

namespace FluentAdb.Tests
{
    [TestFixture]
    public class AdbTests
    {

        [Test]
        public async Task GetDevicesAllStates()
        {
            // arrange
            var output = new List<string>
            {
                "List of devices attached",
                "PHILIPS W732\tdevice",
                "0123456789ABCDE\tunauthorized",
                "0123456789ABCDE\toffline"
            };
            var processManager = new TestProcessManager();
            processManager.AddProcess(output);
            var adb = new Adb(processManager);

            // act
            var devices = (await adb.GetDevices()).ToList();

            // assert
            processManager.Stub.AssertWasCalled(_ => _.CreateProcess(Arg<string>.Is.Anything, Arg<string>.Matches(commands => commands.Contains("devices"))));
            Assert.AreEqual(3, devices.Count);
            Assert.IsTrue(devices.ElementAt(0).State == DeviceState.Online);
            Assert.IsTrue(devices.ElementAt(1).State == DeviceState.Unauthorized);
            Assert.IsTrue(devices.ElementAt(2).State == DeviceState.Offline);
        }

        [Test]
        public async Task GetDevicesInvalidState()
        {
            // arrange
            var output = new List<string>
            {
                "List of devices attached",
                "0123456789ABCDE\tinvalidstate"
            };
            var processManager = new TestProcessManager();
            processManager.AddProcess(output);
            var adb = new Adb(processManager);

            // act + assert
            await AsyncAssert.Throws<UnexpectedOutputException>(async () => (await adb.GetDevices()).ToList());
        }

        // TODO: not work return statement in async methods
        [TestCase(new string[] {}, AdbState.Unknown)]
        [TestCase(new [] { "device" }, AdbState.Device)]
        [TestCase(new [] { "offline" }, AdbState.Offline)]
        [TestCase(new [] { "bootloader" }, AdbState.Bootloader)]
        [TestCase(new[] { "invelidstate" }, AdbState.Unknown)]
        public async Task GetState(string[] output, AdbState state)
        {
            // arrange
            var processManager = new TestProcessManager();
            processManager.AddProcess(output);
            var adb = new Adb(processManager);

            var result = await adb.GetState();

            processManager.Stub.AssertWasCalled(_ => _.CreateProcess(Arg<string>.Is.Anything, Arg<string>.Matches(commands => commands.Contains("get-state"))));
            Assert.IsTrue(result == state);
        }
    }
}