namespace ACC.Common.Types
{
    public interface IIdentifiable<Tkey>
    {
        Tkey Id { get; }
    }

    public interface IIdentifiable : IIdentifiable<string>
    {
    }
}