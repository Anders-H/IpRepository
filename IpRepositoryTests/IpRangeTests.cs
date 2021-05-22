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
            //Assert.IsTrue(new IpRange(new IpAddress(1, 2, 3, 4), );
        }
    }
}