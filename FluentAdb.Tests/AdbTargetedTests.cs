using System;
using System.Threading.Tasks;
using FluentAdb.Enums;
using FluentAdb.Tests.Util;
using NUnit.Framework;
using Rhino.Mocks;

namespace FluentAdb.Tests
{
    [TestFixture]
    public class AdbTargetedTests
    {
        [Test]
        public async Task SingleDevice()
        {
            // arrange
            var processManager = new TestProcessManager();
            processManager.AddProcess(new[] { "Some output" });
            var adb = new Adb(processManager);

            // act
            var deviceAdb = adb.SingleDevice;
            await deviceAdb.RunCommand("test");

            // assert
            processManager.Stub.AssertWasCalled(_ => _.CreateProcess(Arg<string>.Is.Anything, Arg<string>.Matches(commands => commands.Contains("-d test"))));
        }

        [Test]
        public async Task SingleEmulator()
        {
            // arrange
            var processManager = new TestProcessManager();
            processManager.AddProcess(new[] { "Some output" });
            var adb = new Adb(processManager);

            // act
            var deviceAdb = adb.SingleEmulator;
            await deviceAdb.RunCommand("test");

            // assert
            processManager.Stub.AssertWasCalled(_ => _.CreateProcess(Arg<string>.Is.Anything, Arg<string>.Matches(commands => commands.Contains("-e test"))));
        }

        [Test]
        public async Task TargetDevice()
        {
            // arrange
            const string id = "Device ID";
            var processManager = new TestProcessManager();
            processManager.AddProcess(new[] { "Some output" });
            var adb = new Adb(processManager);

            // act
            var deviceAdb = adb.Target(id);
            await deviceAdb.RunCommand("test");

            // assert
            processManager.Stub.AssertWasCalled(_ => _.CreateProcess(Arg<string>.Is.Anything, Arg<string>.Matches(commands => commands.Contains(String.Format("-s \"{0}\" test", id)))));
        }

        [TestCase(InstallOptions.WithForwardLock, "-l")]
        [TestCase(InstallOptions.ReinstallKeepingData, "-r")]
        [TestCase(InstallOptions.InstallToShared, "-s")]
        [TestCase(InstallOptions.InstallToInternal, "-f")]
        public async Task Install(InstallOptions options, string expected)
        {
            // arrange
            var processManager = new TestProcessManager();
            processManager.AddProcess(new[] { "Success" });
            var adb = new Adb(processManager);

            // act
            var deviceAdb = adb.SingleDevice;
            await deviceAdb.Install("test.apk", options);

            // assert
            processManager.Stub.AssertWasCalled(_ => _.CreateProcess(Arg<string>.Is.Anything, Arg<string>.Matches(c => c.Contains(expected))));
        }

        [TestCase("Success", "SUCCESS")]
        [TestCase("Failure", "")]
        [TestCase("Error:", "INSTALL_PARSE_FAILED_UNEXPECTED_EXCEPTION")]
        public async Task InstallResult(string output, string expected)
        {
            // arrange
            var processManager = new TestProcessManager();
            processManager.AddProcess(new[] { "3584 KB/s (69747 bytes in 0.019s)", "        pkg: /data/local/tmp/LocaleChanger.apk", output });
            var adb = new Adb(processManager);

            // act
            var deviceAdb = adb.SingleDevice;
            var result = await deviceAdb.Install("test.apk");

            // assert
            Assert.AreEqual(expected, result);
        }

        [TestCase(BackupOptions.Apk, "-apk")]
        [TestCase(BackupOptions.NoApk, "-noapk")]
        [TestCase(BackupOptions.Obb, "-obb")]
        [TestCase(BackupOptions.NoObb, "-noobb")]
        [TestCase(BackupOptions.Shared, "-share")]
        [TestCase(BackupOptions.NoShared, "-noshared")]
        [TestCase(BackupOptions.All, "-all")]
        [TestCase(BackupOptions.System, "-system")]
        [TestCase(BackupOptions.NoSystem, "-nosystem")]
        public async Task Backup(BackupOptions options, string expected)
        {
            // arrange
            var processManager = new TestProcessManager();
            processManager.AddProcess(new[] { "Success" });
            var adb = new Adb(processManager);

            // act
            var deviceAdb = adb.SingleDevice;
            await deviceAdb.Backup(options);

            // assert
            processManager.Stub.AssertWasCalled(_ => _.CreateProcess(Arg<string>.Is.Anything, Arg<string>.Matches(c => c.Contains(expected))));
        }

        [Test]
        public async Task SuccessPullResult()
        {
            // arrange
            var processManager = new TestProcessManager();
            processManager.AddProcess(new[] { "[100%] /mnt/sdcard/Log.txt" });
            var adb = new Adb(processManager);

            // act
            var deviceAdb = adb.SingleDevice;
            var result = await deviceAdb.Pull("/mnt/sdcard/", "Log.txt");

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Error);
        }

        [Test]
        public async Task FailPullResult()
        {
            // arrange
            var processManager = new TestProcessManager();
            processManager.AddProcess(new[] { "adb: error: remote object '/mnt/sdcard/Log2.txt' does not exist" });
            var adb = new Adb(processManager);

            // act
            var deviceAdb = adb.SingleDevice;
            var result = await deviceAdb.Pull("/mnt/sdcard/", "Log.txt");

            // assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("remote object '/mnt/sdcard/Log2.txt' does not exist", result.Error);
        }

        [Test]
        public async Task SuccessPushResult()
        {
            // arrange
            var processManager = new TestProcessManager();
            processManager.AddProcess(new[] { "[100%] /mnt/sdcard/Log.txt" });
            var adb = new Adb(processManager);

            // act
            var deviceAdb = adb.SingleDevice;
            var result = await deviceAdb.Push("Log.txt","/mnt/sdcard/");

            // assert
            Assert.IsTrue(result.Success);
            Assert.IsNull(result.Error);
        }

        [Test]
        public async Task FailPushResult()
        {
            // arrange
            var processManager = new TestProcessManager();
            processManager.AddProcess(new[] { "adb: error: failed to copy 'Log.txt' to '/mnt/sdcard2/Log.txt': Permission denied" });
            var adb = new Adb(processManager);

            // act
            var deviceAdb = adb.SingleDevice;
            var result = await deviceAdb.Pull("/mnt/sdcard/", "Log.txt");

            // assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Permission denied", result.Error);
        }

        [Test]
        public async Task FailPushResult2()
        {
            // arrange
            var processManager = new TestProcessManager();
            processManager.AddProcess(new[] { "adb: error: failed to copy 'Log.txt' to '/mnt/rtyrtyurt': Read-only file system" });
            var adb = new Adb(processManager);

            // act
            var deviceAdb = adb.SingleDevice;
            var result = await deviceAdb.Pull("/mnt/rtyrtyurt", "Log.txt");

            // assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual("Read-only file system", result.Error);
        }
    }
}