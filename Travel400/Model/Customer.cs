using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

using System;
using System.Collections.Generic;

namespace Travel400.Model
{
    [Table("Customer")]
    public class Customer : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }

        [Column("FullName")]
        public string FullName { get; set; }

        [Column("PassportNumber")]
        public string PassportNumber { get; set; }

        [Column("Nationality")]
        public string Nationality { get; set; }

        [Column("Phone")]
        public string Phone { get; set; }

        [Column("Email")]
        public string? Email { get; set; }

        [Column("Gender")]
        public string? Gender { get; set; }

        [Column("RegisteredOn")]
        public DateTime RegisteredOn { get; set; } = DateTime.Now;

        // ملاحظة: Supabase لا يدعم العلاقات التلقائية مثل EF
        // العلاقة مع Bookings تتم يدوياً إن لزم، لذلك نحذف ICollection هنا
    }
}
