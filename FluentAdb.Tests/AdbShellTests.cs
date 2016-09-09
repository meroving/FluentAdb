using System.Threading.Tasks;
using FluentAdb.Tests.Util;
using NUnit.Framework;

namespace FluentAdb.Tests
{
    [TestFixture]
    public class AdbShellTests
    {
        [Test]
        public async Task GetAllProperties()
        {
            // arrange
            var processManager = new FakeProcessManager();
            processManager.AddProcess(new[]
            {
                "[ro.product.brand]: [Philips]",
                "[ro.secure]: [1]",
                "[ro.wifi.channels]: []"
            }, "getprop");

            var adb = new Adb(processManager);

            // act
            var result = await adb.SingleDevice().Shell.GetAllProperties();

            // assert
            Assert.AreEqual("Philips", result["ro.product.brand"]);
            Assert.AreEqual("1", result["ro.secure"]);
            Assert.AreEqual("", result["ro.wifi.channels"]);
        }

        [Test]
        public async Task GetProperty()
        {
            // arrange
            var processManager = new FakeProcessManager();
            processManager.AddProcess(new[] { " ap0 " });
            var adb = new Adb(processManager);

            // act
            var result = await adb.SingleDevice().Shell.GetProperty("test.property");

            // assert
            Assert.AreEqual("ap0", result);
        }

        [Test]
        public async Task GetPropertyNull()
        {
            // arrange
            var processManager = new FakeProcessManager();
            processManager.AddProcess(new[] { "  " });
            var adb = new Adb(processManager);

            // act
            var result = await adb.SingleDevice().Shell.GetProperty("test.property");

            // assert
            Assert.IsNull(result);
        }
    }
}