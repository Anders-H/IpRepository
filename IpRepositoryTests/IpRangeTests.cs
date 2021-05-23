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

        [TestMethod]
        public void Collides()
        {
            var range = new IpRange("10.10.10.10", "10.11.10.10");
            Assert.IsTrue(range.Collides(new IpAddress("10.10.10.10")));
            Assert.IsTrue(range.Collides(new IpAddress("10.10.10.11")));
            Assert.IsTrue(range.Collides(new IpAddress("10.10.11.10")));
            Assert.IsFalse(range.Collides(new IpAddress("10.10.10.9")));
            Assert.IsFalse(range.Collides(new IpAddress("10.11.10.11")));
            Assert.IsTrue(range.Collides(new IpRange("9.10.10.10", "11.10.10.10")));
            Assert.IsTrue(range.Collides(new IpRange("10.10.20.10", "12.10.10.10")));
            Assert.IsTrue(range.Collides(new IpRange("10.10.10.9", "10.10.10.11")));
            Assert.IsFalse(range.Collides(new IpRange("9.10.10.10", "9.20.10.10")));
            Assert.IsFalse(range.Collides(new IpRange("20.10.10.10", "30.10.10.10")));
        }
    }
}