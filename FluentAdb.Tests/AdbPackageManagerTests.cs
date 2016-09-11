using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAdb.Enums;
using FluentAdb.Tests.Util;
using NUnit.Framework;

namespace FluentAdb.Tests
{
    [TestFixture]
    public class AdbPackageManagerTests
    {
        [Test]
        public async Task GetPackages()
        {
            //arrange
            var processManager = new TestProcessManager();
            processManager.AddProcess(new[]
            {
                "package:com.google.android.youtube",
                "package:com.android.browser.provider",
                "package:com.android.pacprocessor"
            }, "list packages");

            var adb = new Adb(processManager);

            //act
            var result = await adb.SingleDevice.Shell.PackageManager.GetPackages();

            //assert
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("com.google.android.youtube", result.ElementAt(0));
            Assert.AreEqual("com.android.browser.provider", result.ElementAt(1));
            Assert.AreEqual("com.android.pacprocessor", result.ElementAt(2));
        }

        [Test]
        public async Task GetUsers()
        {
            //arrange
            var processManager = new TestProcessManager();
            processManager.AddProcess(new[]
            {
                "Users:",
                "           UserInfo{0:Owner:13} running",
                "           UserInfo{1:Guest:2}"
            }, "list users");

            var adb = new Adb(processManager);

            //act
            var result = await adb.SingleDevice.Shell.PackageManager.GetUsers();

            //assert
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("0", result.ElementAt(0).Id);
            Assert.AreEqual("Owner", result.ElementAt(0).Name);
            Assert.AreEqual("1", result.ElementAt(1).Id);
            Assert.AreEqual("Guest", result.ElementAt(1).Name);
        }

        [Test]
        public async Task CreateUser_Error()
        {
            //arrange
            var processManager = new TestProcessManager();
            processManager.AddProcess(new[]
            {
                "Error: couldn't create User."
            }, "create-user");

            var adb = new Adb(processManager);

            //act
            try
            {
                await adb.SingleDevice.Shell.PackageManager.CreateUser("Test");
                //assert
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOf<AdbException>(ex);
            }
        }

        [Test]
        public async Task GetInstallLocation()
        {
            //arrange
            var processManager = new TestProcessManager();
            processManager.AddProcess(new[]
            {
                "1[internal]",
            }, "get-install-location");

            var adb = new Adb(processManager);

            //act
            var result = await adb.SingleDevice.Shell.PackageManager.GetInstallLocation();

            //assert
            Assert.AreEqual(InstallLocation.Internal, result);
        }

    }
}
