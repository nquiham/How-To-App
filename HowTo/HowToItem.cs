using SQLite;

namespace HowTo
{
    public class HowToItem
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string CreationDate { get; set; }
    }
}