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
            EndAddress = address;
        }

        public IpRange(IpAddress startAddress, IpAddress endAddress)
        {
            if (endAddress < startAddress)
                throw new ArgumentOutOfRangeException();

            StartAddress = startAddress;
            EndAddress = endAddress;
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