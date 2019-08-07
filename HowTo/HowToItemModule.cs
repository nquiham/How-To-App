using System;
using SQLite;

namespace HowTo
{
    internal class HowToItemModule
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Module_Id { get; set; }

        public string Module_Type { get; set; }

        public string Module_Text { get; set; }

        public byte[] Module_Image { get; set; }

        public int Module_Order { get; set; }

        public int Module_HowToId { get; set; }
    }
}