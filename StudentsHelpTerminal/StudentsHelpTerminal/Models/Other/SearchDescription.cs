namespace StudentsHelpTerminal.Models.Other
{
    internal class SearchDescription
    {
        public SearchDescription() { }
        public SearchDescription(string columnName, string query, bool notFullQuery)
        {
            ColumnName = columnName;
            Query = query;
            NotFullQuery = notFullQuery;
        }

        public string ColumnName { get; set; }
        public string Query { get; set; }
        public bool NotFullQuery { get; set; }
        public bool IsEmpty { get {return string.IsNullOrEmpty(Query?.Trim());} }
    }
}
