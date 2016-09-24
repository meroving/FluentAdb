using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAdb.Enums;
using NUnit.Framework;

namespace FluentAdb.Tests
{
    [TestFixture]
    public class IntentTests
    {

        [Test]
        public void FlagsTest_NoFlags_NoDescription()
        {
            //Arrange

            //Act
            var intent = Intent.New.Action("android.intent.action.SEARCH").ToString();

            //Assert
            Assert.IsFalse(intent.Contains("-f"));
        }

        [Test]
        public void FlagsTest_OneFlag_DescriptionContainsFlagValue()
        {
            //Arrange

            //Act
            var intent = Intent.New.Action("android.intent.action.SEARCH").Flags(IntentOptions.FLAG_ACTIVITY_TASK_ON_HOME).ToString();

            //Assert
            Assert.IsTrue(intent.Contains("-f 16384"));
        }

        [Test]
        public void FlagsTest_ManyFlags_DescriptionContainsFlagsValueSum()
        {
            //Arrange

            //Act
            var intent = Intent.New.Action("android.intent.action.SEARCH").Flags(IntentOptions.FLAG_ACTIVITY_TASK_ON_HOME | IntentOptions.FLAG_ACTIVITY_NO_ANIMATION | IntentOptions.FLAG_INCLUDE_STOPPED_PACKAGES).ToString();

            //Assert
            Assert.IsTrue(intent.Contains("-f 81952"));
        }
    }
}
