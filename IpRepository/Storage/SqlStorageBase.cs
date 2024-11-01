using System;
using Microsoft.Data.SqlClient;

namespace IpRepository.Storage;

public abstract class SqlStorageBase : IStorage, IDisposable
{
    protected SqlConnection Connection { get; }

    protected SqlStorageBase(string connectionString)
    {
        Connection = new SqlConnection(connectionString);
    }

    public void Add(IpRange range)
    {
        Connection.Open();
        AddIpRange(range);
        Connection.Close();
    }

    protected abstract void AddIpRange(IpRange range);

    public void Replace(IpRange oldRange, IpRange newRange)
    {
        Connection.Open();
        ReplaceIpRange(oldRange, newRange);
        Connection.Close();
    }

    protected abstract void ReplaceIpRange(IpRange oldRange, IpRange newRange);

    public bool IsFree(IpRange range)
    {
        Connection.Open();
        var free = IsIpRangeFree(range);
        Connection.Close();
        return free;
    }

    protected abstract bool IsIpRangeFree(IpRange range);

    public void Delete(IpRange range)
    {
        Connection.Open();
        DeleteIpRange(range);
        Connection.Close();
    }

    protected abstract void DeleteIpRange(IpRange range);

    public void DeleteAll()
    {
        Connection.Open();
        DeleteAllIpRanges();
        Connection.Close();
    }

    protected abstract void DeleteAllIpRanges();

    public void Dispose() =>
        Connection.Dispose();
}