using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CodeFirst.Models
{
    [Table("Kisi")]
    public class Kisi
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [StringLength(20), Required]
        public string Ad { get; set; }

        [StringLength(20), Required]
        public string Soyad { get; set; }

        [Required]
        public int Yas { get; set; }

        public virtual List<Adres> Adresler { get; set; }
    }
}