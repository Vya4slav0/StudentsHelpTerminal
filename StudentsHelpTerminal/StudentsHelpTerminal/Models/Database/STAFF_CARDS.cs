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
    
    public partial class STAFF_CARDS
    {
        public int ID_CARD { get; set; }
        public int SYSTEM_TYPE { get; set; }
        public Nullable<int> STAFF_ID { get; set; }
        public System.DateTime DATE_BEGIN { get; set; }
        public System.DateTime DATE_END { get; set; }
        public int VALID { get; set; }
        public int VALID_TRANSFER { get; set; }
        public int TEMPORARY_ACC { get; set; }
        public Nullable<int> DOCUMENTS_ID { get; set; }
        public System.DateTime HISTORY_DATE { get; set; }
        public int PROHIBIT { get; set; }
        public Nullable<long> IDENTIFIER { get; set; }
        public int TYPE_IDENTIFIER { get; set; }
        public Nullable<long> IDENTIFIER_TRANSFORMED { get; set; }
        public int WITHDRAW_TO_STOP_LIST { get; set; }
        public System.DateTime LAST_TIMESTAMP { get; set; }
        public Nullable<int> USER_ID { get; set; }
        public int NPP_NUMBER { get; set; }
    }
}
