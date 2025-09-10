using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

using System;


namespace Travel400.Model
{
    [Table("Booking")]
    public class Booking : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("CountryName")]
        public string CountryName { get; set; }

        [Column("TravelDate")]
        public DateTime TravelDate { get; set; }

        [Column("SeatsRequested")]
        public int SeatsRequested { get; set; }

        [Column("ContactPhone")]
        public string ContactPhone { get; set; }

        [Column("Notes")]
        public string? Notes { get; set; }

        [Column("IsCanceled")]
        public bool IsCanceled { get; set; } = false;

        [Column("CustomerId")]
        public int? CustomerId { get; set; }
    }
}
