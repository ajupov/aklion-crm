namespace Aklion.Infrastructure.Query
{
    public enum QueryType
    {
        None,
        SelectCount,
        SelectOne,
        SelectList,
        SelectPagedList,
        SelectForAutocompleteOrSelect,
        Insert,
        InsertList,
        Update,
        Delete
    }
}