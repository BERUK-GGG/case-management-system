using Microsoft.EntityFrameworkCore.Proxies;
using RB_Ärendesystem.Datalayer;
using RB_Ärendesystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLayer
{
    public class TestData
    {
        public static void SeedData()
        {
            RB_context testDB = new RB_context();

            testDB.Database.EnsureDeleted();
            testDB.Database.EnsureCreated();


            Kund kund3 = new Kund { Namn = "us", PersonNr = 123456, Address = "Gatan 1", TeleNr = 0701234567, Epost = "me@gmail.com" };

            Kund kund2 = new Kund { Namn = "you", PersonNr = 123456, Address = "Gatan 1", TeleNr = 0701234567, Epost = "me@gmail.com" };

            Kund kund1 = new Kund { Namn = "me", PersonNr = 123456, Address = "Gatan 1", TeleNr = 0701234567, Epost = "me@gmail.com" };

            testDB.kunder.Add(kund1);
            testDB.kunder.Add(kund2);
            testDB.kunder.Add(kund3);
            testDB.SaveChanges();
            Mekaniker mekaniker1 = new Mekaniker { Namn = "you", Roll = "Mekaniker", specialisering = "Motor", AnvändarNamn = "you", lösenord = "password" };
            Mekaniker mekaniker2 = new Mekaniker { Namn = "Broooow", Roll = "Mekaniker3", specialisering = "däck", AnvändarNamn = "brush", lösenord = "password" };

            testDB.mekaniker.Add(mekaniker1);
            testDB.mekaniker.Add(mekaniker2);

            testDB.SaveChanges();
            Reservdel reservdel1 = new Reservdel { Namn = "Motor", Pris = 1000, };
            Reservdel reservdel2 = new Reservdel { Namn = "Däck", Pris = 500, };

            Besök besök1 = new Besök { KundId = kund1.ID, DateAndTime = new System.DateTime(2021, 12, 24, 12, 00, 00), syfte = "Service", Anställningsnummer = mekaniker1.Anställningsnummer };
            Besök besök2 = new Besök { KundId = kund2.ID, DateAndTime= new System.DateTime(2021, 11, 29, 12, 00, 00), syfte= "Däck", Anställningsnummer = mekaniker2.Anställningsnummer };
           

            


            testDB.reservdelar.Add(reservdel1);
            testDB.reservdelar.Add(reservdel2);
            testDB.besök.Add(besök1);
            testDB.besök.Add(besök2);

            testDB.SaveChanges();

            Console.WriteLine("klart");
        }
    }
}
