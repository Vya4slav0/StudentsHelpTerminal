using StudentsHelpTerminal.Infrastructure.Services;
using System.Collections.Generic;
using System.Linq;
using System;

namespace StudentsHelpTerminal.Models.Other
{
    internal class Table
    {
        public Table (string name, SearchDescription searchDescription, SortDescription sortDescription)
        {
            Name = name; 
            SearchDescription = searchDescription;
            SortDescription = sortDescription;
        }

        public string Name { get; set; }
        public IEnumerable<Student> Items { get; private set; }

        #region Sort description

        private SortDescription _SortDescription;

        public SortDescription SortDescription 
        {
            get { return _SortDescription; } 
            set 
            { 
                _SortDescription = value;
                Items = SortItems(Items, _SortDescription);
            } 
        }

        #endregion

        #region Search description

        private SearchDescription _SearchDescription;
        public SearchDescription SearchDescription 
        { 
            get { return _SearchDescription; } 
            set
            {
                if (_SearchDescription is null && !(value is null) ||
                    !_SearchDescription.Equals(value) && _SearchDescription?.Query.Trim().Length > 0)
                    _SearchDescription = value;
                Items = DBHelper.FindStudents(_SearchDescription.Query, _SearchDescription.ColumnName, _SearchDescription.NotFullQuery);
            } 
        }

        #endregion

        private IEnumerable<Student> SortItems(IEnumerable<Student> toSort, SortDescription sortDescription)
        {
            if (toSort is null) return null;
            if (!sortDescription.Enable) return toSort.OrderBy(s => s.Id);
            
            Func<Func<Student, object>, IOrderedEnumerable<Student>> sortMethod = sortDescription.IsDescending ?
                (Func<Func<Student, object>, IOrderedEnumerable<Student>>)toSort.OrderByDescending : 
                toSort.OrderBy;
            
                
            switch (sortDescription.ColumnName)
            {
                case DBHelper.NAME:
                    return sortMethod(s => s.Name);
                case DBHelper.SURNAME:
                    return sortMethod(s => s.Surname);
                case DBHelper.PATRONYMIC:
                    return sortMethod(s => s.Patronymic);
                case DBHelper.ID:
                    return sortMethod(s => s.Id);
                case DBHelper.CARD_ID:
                    return sortMethod(s => s.CardID);
            }
            return toSort;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
