//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudentsHelpTerminal.Models.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class SUBDIV_REF
    {
        public int ID_REF { get; set; }
        public int N_LEFT { get; set; }
        public int N_RIGHT { get; set; }
        public Nullable<int> N_LEVEL { get; set; }
        public string DISPLAY_NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public int VISIBLE { get; set; }
        public string ID_FROM_1C { get; set; }
        public int SUBDIV_STATE { get; set; }
        public string SUBDIV_OFFICIES { get; set; }
        public Nullable<int> STAFF_ID_CONTANT_PERSON { get; set; }
        public Nullable<int> STAFF_ID_ACCOMPANY { get; set; }
        public string SUBDIV_DESC { get; set; }
        public string PHONES { get; set; }
        public Nullable<int> STAFF_ACCESS_PATTERN_ID { get; set; }
        public Nullable<int> GUEST_ACCESS_PATTERN_ID { get; set; }
    }
}
