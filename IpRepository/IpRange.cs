using System;

namespace IpRepository
{
    public class IpRange
    {
        public IpAddress StartAddress { get; }
        public IpAddress EndAddress { get; }

        public IpRange(IpAddress address)
        {
            StartAddress = address;
            EndAddress = address.Copy();
        }

        public IpRange(string address)
        {
            var a = new IpAddress(address);
            StartAddress = a;
            EndAddress = a.Copy();
        }

        public IpRange(IpAddress startAddress, IpAddress endAddress)
        {
            if (endAddress < startAddress)
                throw new ArgumentOutOfRangeException();

            StartAddress = startAddress;
            EndAddress = endAddress;
        }

        public IpRange(string startAddress, string endAddress)
        {
            var start = new IpAddress(startAddress);
            var end = new IpAddress(endAddress);

            if (start < end)
                throw new ArgumentOutOfRangeException();

            StartAddress = start;
            EndAddress = end;
        }

        public long Size
        {
            get
            {
                var size = 1L;

                return size;
            }
        }
    }
}