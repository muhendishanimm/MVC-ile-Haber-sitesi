using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CodeFirst.Models.Managers
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Kisi> Kisiler { get; set; }
        public DbSet<Adres> Adresler { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new VeritabaniOlusturucu());
        }

        public class VeritabaniOlusturucu : CreateDatabaseIfNotExists<DatabaseContext>
        {
            protected override void Seed(DatabaseContext context)
            {
                // Rasgele anlamlı kişi bilgisi Insert et
                for (int i = 0; i < 10; i++)
                {
                    Kisi kisi = new Kisi();
                    kisi.Ad = FakeData.NameData.GetFirstName();
                    kisi.Soyad = FakeData.NameData.GetSurname();
                    kisi.Yas = FakeData.NumberData.GetNumber(10, 90);

                    context.Kisiler.Add(kisi);
                }
                context.SaveChanges();

                // Rasgele anlamlı adres bilgileri ekle
                List<Kisi> Kisiler = context.Kisiler.ToList();
                foreach (Kisi kisi in Kisiler)
                {
                    for (int i = 0; i < FakeData.NumberData.GetNumber(1, 5); i++)
                    {
                        Adres adres = new Adres();
                        adres.AdresTanim = FakeData.PlaceData.GetAddress();
                        adres.Sehir = FakeData.PlaceData.GetCity();
                        adres.Kisiler = kisi;

                        context.Adresler.Add(adres);
                    }
                }
                context.SaveChanges();
            }
        }
    }
}