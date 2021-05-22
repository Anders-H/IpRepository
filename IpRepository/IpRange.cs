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

            if (end < start)
                throw new ArgumentOutOfRangeException();

            StartAddress = start;
            EndAddress = end;
        }

        public long Size
        {
            get
            {
                var size = 1L;
                const long stepSize0 = 16777216L;
                const long stepSize1 = 65536L;
                const long stepSize2 = 255L;

                var diff1 = EndAddress[0] - StartAddress[0];
                var diff2 = EndAddress[1] - StartAddress[1];
                var diff3 = EndAddress[2] - StartAddress[2];
                var diff4 = EndAddress[3] - StartAddress[3];

                size += diff1 * stepSize0;
                size += diff2 * stepSize1;
                size += diff3 * stepSize2;
                size += diff4;
                
                return size;
            }
        }
    }
}