using System;
using System.Linq;
// ReSharper disable ReplaceWithSingleCallToFirst

namespace EFDemo
{
    class Program
    {
        static void Main()
        {
            //Simple where on one table
            using (var db = new ShopEntities())
            {
                var kunde = db.Kunden.Where(k => k.Id == 2004).First();
                Console.WriteLine($"Id: '{kunde.Id}', Name: {kunde.Name} ");
            }

            //Join & where
            using (var db = new ShopEntities())
            {
                db.Configuration.LazyLoadingEnabled = true;  //by default
                var qry = from bestellDetails in db.BestellDetails
                          join bestellung in db.Bestellungen on bestellDetails.BestellId equals bestellung.Id
                          join kunden in db.Kunden on bestellung.KundenId equals kunden.Id
                          where kunden.Id == 1003
                          select bestellDetails;
                foreach (var item in qry)
                {
                    Console.WriteLine($"{item.Bestellungen.Kunden.Name}, {item.Bestellungen.Bestelldatum}, {item.Artikel.Bezeichnung}, {item.Anzahl}");
                }
            }
            //Add
            using (var db = new ShopEntities())
            {
                var newKunde = new Kunden
                {
                    Firma = "EF Neu Firma",
                    Name = "EF Neu Name",
                    Straße = "EF Neu Straße",
                    PLZ = "EF NEu PLZ",
                    Ort = "EF Neu Ort"
                };
                db.Kunden.Add(newKunde);
                db.SaveChanges();
                Console.WriteLine($"Id: '{newKunde.Id}'");
            }
            //Update
            using (var db = new ShopEntities())
            {
                var kunde = db.Kunden.First(k => k.Id == 2004);
                Console.WriteLine($"kunde (before): '{kunde.Name}'");
                kunde.Name = "Name_" + Guid.NewGuid().ToString().Substring(0, 5);
                db.SaveChanges();
                kunde = db.Kunden.First(k => k.Id == 2004);
                Console.WriteLine($"kunde (after): '{kunde.Name}'");
            }

            //Update disconnected
            Kunden kunde2004Before;
            using (var db = new ShopEntities())
            {
                kunde2004Before = db.Kunden.First(k => k.Id == 2004);
                Console.WriteLine($"kunde (before): '{kunde2004Before.Name}'");
            }

            var kunde2004After = new Kunden
            {
                Id = kunde2004Before.Id,
                Firma = kunde2004Before.Firma,
                Name = kunde2004Before.Name,
                Straße = kunde2004Before.Straße,
                PLZ = kunde2004Before.PLZ,
                Ort = kunde2004Before.Ort,
                Geburtsdatum = kunde2004Before.Geburtsdatum,
            };
            using (var db = new ShopEntities())
            {
                kunde2004After.Name = "Name_" + Guid.NewGuid().ToString().Substring(0, 5);
                db.Kunden.Attach(kunde2004After);
                db.SaveChanges();
                var kunde2004FromDb = db.Kunden.First(k => k.Id == 2004);
                Console.WriteLine($"kunde (after): '{kunde2004FromDb.Name}'");
            }


            Console.WriteLine("Press any key to end");
            Console.Read();

        }
    }
}
