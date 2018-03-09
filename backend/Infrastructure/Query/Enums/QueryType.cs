namespace Infrastructure.Query.Enums
{
    public enum QueryType : byte
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