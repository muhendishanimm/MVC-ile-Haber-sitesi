using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodeFirst.Models
{
    [Table("Adres")]
    public class Adres
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(300)]
        public string AdresTanim { get; set; }

        [StringLength(100)]
        public string Sehir { get; set; }

        public virtual Kisi Kisiler { get; set; }
    }
}