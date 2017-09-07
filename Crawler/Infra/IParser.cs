namespace Crawlers.Infra
{
    public interface IParser<TOut>
    {
        TOut Parse(string parseable);
        string ParseBack(TOut parsed);
    }
}