using System;
using System.ComponentModel;

namespace IpRepository;

public class IpAddress
{
    private byte[] Bytes { get; }

    public IpAddress() : this(0, 0, 0, 0)
    {
    }

    public IpAddress(System.Net.IPAddress address)
    {
        var bytes = address.GetAddressBytes();
        Bytes = [bytes[0], bytes[1], bytes[2], bytes[3]];
    }

    public IpAddress(byte n1, byte n2, byte n3, byte n4) =>
        Bytes = [n1, n2, n3, n4];

    public IpAddress(string address)
    {
        if (!TryParse(address, out var a))
            throw new SystemException();

        Bytes = [a!.Bytes[0], a.Bytes[1], a.Bytes[2], a.Bytes[3]];
    }

    public IpAddress(long address)
    {
        if (!TryParse(address, out var a))
            throw new SystemException();

        Bytes = [a!.Bytes[0], a.Bytes[1], a.Bytes[2], a.Bytes[3]];
    }

    public IpAddress Copy() =>
        new(Bytes[0], Bytes[1], Bytes[2], Bytes[3]);

    public byte this[int i] =>
        Bytes[i];

    public override int GetHashCode() =>
        Bytes.GetHashCode();

    protected bool Equals(IpAddress other) =>
        Bytes.Equals(other.Bytes);

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        if (obj.GetType() != GetType())
            return false;

        return Equals((IpAddress)obj);
    }

    public static bool TryParse(string address, out IpAddress? ipAddress)
    {
        ipAddress = null;

        try
        {
            var stringBytes = address.Split('.');
            if (stringBytes.Length != 4)
                return false;

            var n1 = byte.Parse(stringBytes[0]);
            var n2 = byte.Parse(stringBytes[1]);
            var n3 = byte.Parse(stringBytes[2]);
            var n4 = byte.Parse(stringBytes[3]);
            ipAddress = new IpAddress(n1, n2, n3, n4);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool TryParse(long address, out IpAddress? ipAddress)
    {
        ipAddress = null;

        try
        {
            var s = address.ToString("000000000000");
            var n1 = byte.Parse(s.Substring(0, 3));
            var n2 = byte.Parse(s.Substring(3, 3));
            var n3 = byte.Parse(s.Substring(6, 3));
            var n4 = byte.Parse(s.Substring(9, 3));
            ipAddress = new IpAddress(n1, n2, n3, n4);
            return true;
        }
        catch
        {
            return false;
        }
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

    public static bool operator ==(IpAddress? a, IpAddress? b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.IsSameAs(b);
    }

    public static bool operator !=(IpAddress? a, IpAddress? b) =>
        !(a == b);

    public static bool operator >(IpAddress a, IpAddress b)
    {
        if (a.Bytes[0] > b.Bytes[0])
            return true;

        if (a.Bytes[0] < b.Bytes[0])
            return false;
        
        if (a.Bytes[1] > b.Bytes[1])
            return true;
        
        if (a.Bytes[1] < b.Bytes[1])
            return false;
        
        if (a.Bytes[2] > b.Bytes[2])
            return true;
        
        if (a.Bytes[2] < b.Bytes[2])
            return false;
        
        return a.Bytes[3] > b.Bytes[3];
    }

    public static bool operator <(IpAddress a, IpAddress b)
    {
        if (a.Bytes[0] < b.Bytes[0])
            return true;
        
        if (a.Bytes[0] > b.Bytes[0])
            return false;
        
        if (a.Bytes[1] < b.Bytes[1])
            return true;
        
        if (a.Bytes[1] > b.Bytes[1])
            return false;
        
        if (a.Bytes[2] < b.Bytes[2])
            return true;
        
        if (a.Bytes[2] > b.Bytes[2])
            return false;
        
        return a.Bytes[3] < b.Bytes[3];
    }

    public static bool operator >=(IpAddress a, IpAddress b) =>
        a > b || a == b;

    public static bool operator <=(IpAddress a, IpAddress b) =>
        a < b || a == b;
}