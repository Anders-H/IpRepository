using IpRepository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Serialization;

namespace IpRepositoryTests
{
    [TestClass]
    public class IpAddressTests
    {
        [TestMethod]
        public void CreateFromNetType()
        {
            Assert.IsTrue(new IpAddress(System.Net.IPAddress.Parse("1.2.3.4")).ToString() == "1.2.3.4");
            Assert.IsTrue(new IpAddress(System.Net.IPAddress.Parse("1.2.3.4")).ToLong() == 1002003004);
            Assert.IsTrue(new IpAddress(System.Net.IPAddress.Parse("255.2.3.4")).ToString() == "255.2.3.4");
            Assert.IsTrue(new IpAddress(System.Net.IPAddress.Parse("255.2.3.4")).ToLong() == 255002003004);
        }

        [TestMethod]
        public void ToLong()
        {
            Assert.IsTrue(new IpAddress(1, 2, 3, 4).ToLong() == 1002003004L);
            Assert.IsTrue(new IpAddress(255, 254, 253, 252).ToLong() == 255254253252L);
            Assert.IsTrue(new IpAddress(255, 255, 255, 255).ToLong() == 255255255255L);
            Assert.IsTrue(new IpAddress(0, 0, 0, 0).ToLong() == 0L);
            Assert.IsTrue(new IpAddress(0, 1, 0, 0).ToLong() == 1000000L);
        }

        [TestMethod]
        public void Next()
        {
            Assert.IsTrue(new IpAddress(0, 0, 0, 0).Next() == new IpAddress(0, 0, 0, 1));
            Assert.IsTrue(new IpAddress(0, 0, 0, 255).Next() == new IpAddress(0, 0, 1, 0));
            Assert.IsTrue(new IpAddress(0, 0, 255, 0).Next() == new IpAddress(0, 0, 255, 1));
            Assert.IsTrue(new IpAddress(0, 0, 255, 255).Next() == new IpAddress(0, 1, 0, 0));
            Assert.IsTrue(new IpAddress(0, 255, 255, 255).Next() == new IpAddress(1, 0, 0, 0));
            Assert.IsTrue(new IpAddress(255, 255, 255, 254).Next() == new IpAddress(255, 255, 255, 255));
            Assert.IsTrue(new IpAddress(255, 255, 255, 255).Next() == new IpAddress(0, 0, 0, 0));
        }

        [TestMethod]
        public void Previous()
        {
            Assert.IsTrue(new IpAddress(0, 0, 0, 1).Previous() == new IpAddress(0, 0, 0, 0));
            Assert.IsTrue(new IpAddress(0, 0, 1, 0).Previous() == new IpAddress(0, 0, 0, 255));
            Assert.IsTrue(new IpAddress(0, 0, 255, 1).Previous() == new IpAddress(0, 0, 255, 0));
            Assert.IsTrue(new IpAddress(0, 1, 0, 0).Previous() == new IpAddress(0, 0, 255, 255));
            Assert.IsTrue(new IpAddress(1, 0, 0, 0).Previous() == new IpAddress(0, 255, 255, 255));
            Assert.IsTrue(new IpAddress(255, 255, 255, 255).Previous() == new IpAddress(255, 255, 255, 254));
            Assert.IsTrue(new IpAddress(0, 0, 0, 0).Previous() == new IpAddress(255, 255, 255, 255));
        }

        [TestMethod]
        public void Equals()
        {
            Assert.IsTrue(new IpAddress(5, 6, 5, 4) == new IpAddress(System.Net.IPAddress.Parse("5.6.5.4")));
            Assert.IsTrue(new IpAddress(5, 6, 5, 8) != new IpAddress(5, 6, 5, 4));
        }
    }
}