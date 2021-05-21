using System.ComponentModel;
using Microsoft.VisualBasic.CompilerServices;

namespace IpRepository
{
    public class IpAddress
    {
        public byte[] Bytes { get; }

        public IpAddress() : this(0, 0, 0, 0)
        {
        }

        public IpAddress(System.Net.IPAddress address)
        {
            var bytes = address.GetAddressBytes();
            Bytes = new[] {
                bytes[0], bytes[1], bytes[2], bytes[3]
            };
        }

        public IpAddress(byte n1, byte n2, byte n3, byte n4)
        {
            Bytes = new[] {
                n1, n2, n3, n4
            };
        }

        public override string ToString() =>
            $"{Bytes[0]}.{Bytes[1]}.{Bytes[2]}.{Bytes[3]}";

        public long ToLong() =>
            long.Parse($"{Bytes[0]}{Bytes[1]:000}{Bytes[2]:000}{Bytes[3]:000}");

        public IpAddress Next()
        {
            var bytes = new [] {Bytes[0], Bytes[1], Bytes[2], Bytes[3]};

            if (bytes[3] < 255)
            {
                bytes[3]++;
                return new IpAddress(bytes[0], bytes[1], bytes[2], bytes[3]);
            }

            if (bytes[2] < 255)
            {
                bytes[2]++;
                bytes[3] = 0;
                return new IpAddress(bytes[0], bytes[1], bytes[2], bytes[3]);
            }

            if (bytes[1] < 255)
            {
                bytes[1]++;
                bytes[2] = 0;
                bytes[3] = 0;
                return new IpAddress(bytes[0], bytes[1], bytes[2], bytes[3]);
            }

            if (bytes[0] < 255)
            {
                bytes[0]++;
                bytes[1] = 0;
                bytes[2] = 0;
                bytes[3] = 0;
                return new IpAddress(bytes[0], bytes[1], bytes[2], bytes[3]);
            }

            return new IpAddress(0, 0, 0, 0);
        }

        public IpAddress Previous()
        {
            var bytes = new[] { Bytes[0], Bytes[1], Bytes[2], Bytes[3] };

            if (bytes[3] > 0)
            {
                bytes[3]--;
                return new IpAddress(bytes[0], bytes[1], bytes[2], bytes[3]);
            }

            if (bytes[2] > 0)
            {
                bytes[2]--;
                bytes[3] = 255;
                return new IpAddress(bytes[0], bytes[1], bytes[2], bytes[3]);
            }

            if (bytes[1] > 0)
            {
                bytes[1]--;
                bytes[2] = 255;
                bytes[3] = 255;
                return new IpAddress(bytes[0], bytes[1], bytes[2], bytes[3]);
            }

            if (bytes[0] > 0)
            {
                bytes[0]--;
                bytes[1] = 255;
                bytes[2] = 255;
                bytes[3] = 255;
                return new IpAddress(bytes[0], bytes[1], bytes[2], bytes[3]);
            }

            return new IpAddress(255, 255, 255, 255);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool IsSameAs(IpAddress other) =>
            Bytes[0] == other.Bytes[0]
            && Bytes[1] == other.Bytes[1]
            && Bytes[2] == other.Bytes[2]
            && Bytes[3] == other.Bytes[3];

        public static bool operator ==(IpAddress a, IpAddress b)
        {
            
            if (a.Equals(null) && b.Equals(null))
                return true;

            if (a.Equals(null) || b.Equals(null))
                return false;

            return a.IsSameAs(b);
        }

        public static bool operator !=(IpAddress a, IpAddress b) =>
            !(a == b);
    }
}