using System;
using System.Collections;
using System.Collections.Generic;

namespace IpRepository;

public class IpRange : IEnumerable<IpAddress>
{
    public class Enumerator : IEnumerator<IpAddress>
    {
        private IpAddress? _current;
        public IpAddress StartAddress { get; }
        public IpAddress EndAddress { get; }

        internal Enumerator(IpAddress startAddress, IpAddress endAddress)
        {
            StartAddress = startAddress;
            EndAddress = endAddress;
        }

        public IpAddress Current =>
            _current!;

        object IEnumerator.Current =>
            Current;

        public bool MoveNext()
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (Current == null)
            {
                _current = StartAddress;
                return true;
            }
            
            var next = Current.Next();
            
            if (next > EndAddress || next <= StartAddress)
                return false;
            
            _current = next;
            return true;
        }

        public void Reset() =>
            _current = null;

        public void Dispose() =>
            _current = null;
    }

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

    public IEnumerator<IpAddress> GetEnumerator() =>
        new Enumerator(StartAddress, EndAddress);

    public override string ToString() =>
        StartAddress == EndAddress
            ? StartAddress.ToString()
            : $"{StartAddress}-{EndAddress}";

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    public bool Collides(IpAddress ipAddress) =>
        ipAddress >= StartAddress
        && ipAddress <= EndAddress;

    public bool Collides(IpRange ipRange)
    {
        if (ipRange.StartAddress <= StartAddress && ipRange.EndAddress >= EndAddress)
            return true;

        return Collides(ipRange.StartAddress) || Collides(ipRange.EndAddress);
    }

    public bool Subset(IpRange ipRange) =>
        Collides(ipRange.StartAddress) && Collides(ipRange.EndAddress);
}