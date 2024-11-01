namespace IpRepository.Storage;

public class SqlExampleStorage : SqlStorageBase
{
    public SqlExampleStorage(string connectionString) : base(connectionString)
    {
    }

    protected override void AddIpRange(IpRange range)
    {
        throw new System.NotImplementedException();
    }

    protected override void ReplaceIpRange(IpRange oldRange, IpRange newRange)
    {
        throw new System.NotImplementedException();
    }

    protected override bool IsIpRangeFree(IpRange range)
    {
        throw new System.NotImplementedException();
    }

    protected override void DeleteIpRange(IpRange range)
    {
        throw new System.NotImplementedException();
    }

    protected override void DeleteAllIpRanges()
    {
        throw new System.NotImplementedException();
    }
}