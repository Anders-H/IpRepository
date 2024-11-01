namespace IpRepository.Storage;

public interface IStorage
{
    public void Add(IpRange range);
    public void Replace(IpRange oldRange, IpRange newRange);
    public bool IsFree(IpRange range);
    public void Delete(IpRange range);
}