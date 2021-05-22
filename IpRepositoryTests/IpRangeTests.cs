using System;
using IpRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IpRepositoryTests
{
    [TestClass]
    public class IpRangeTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CannotCreateIlligalRanges()
        {
            var _ = new IpRange(new IpAddress(5, 4, 3, 2), new IpAddress(5, 4, 2, 3));
        }

        [TestMethod]
        public void RangeSize()
        {
            Assert.IsTrue(new IpRange("1.1.1.1", "1.1.1.1").Size == 1);
            Assert.IsTrue(new IpRange("1.1.1.1", "1.1.1.2").Size == 2);
            Assert.IsTrue(new IpRange("1.1.1.1", "1.1.2.1").Size == 256);
            Assert.IsTrue(new IpRange("1.1.1.1", "1.1.255.0").Size == 64770);
            Assert.IsTrue(new IpRange("1.1.1.1", "1.1.255.1").Size == 64771);
            Assert.IsTrue(new IpRange("1.1.1.1", "1.1.255.2").Size == 64772);
        }
    }
}