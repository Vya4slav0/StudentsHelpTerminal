namespace StudentsHelpTerminal.Models.Other
{
    internal class SortDescription
    {
        public SortDescription() { }
        public SortDescription(string columnName, bool isDescending, bool enable)
        {
            ColumnName = columnName;
            IsDescending = isDescending;
            Enable = enable;
        }

        public string ColumnName { get; set; }
        public bool IsDescending { get; set; }
        public bool Enable { get; set; }
    }
}
