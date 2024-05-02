using Entities;
using Microsoft.EntityFrameworkCore.Proxies;
using RB_Ärendesystem.Datalayer;
using RB_Ärendesystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RB_Ärendesystem.Datalayer
{
    public class TestData
    {
        public static void SeedData()
        {
            RB_context testDB = new RB_context();

            
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
            Reservdel reservdel3 = new Reservdel { Namn = "dsgcv", Pris = 600, };
            Reservdel reservdel4 = new Reservdel { Namn = "avgasrör", Pris = 1500, };
            Reservdel reservdel5 = new Reservdel { Namn = "asdfhgj", Pris = 5500, };
            Reservdel reservdel6 = new Reservdel { Namn = "dsgcv", Pris = 6050, };

            testDB.reservdelar.Add(reservdel1);
            testDB.reservdelar.Add(reservdel2);
            testDB.reservdelar.Add(reservdel3);
            testDB.reservdelar.Add(reservdel4);
            testDB.reservdelar.Add(reservdel5);
            testDB.reservdelar.Add(reservdel6);

            testDB.SaveChanges();

            Besök besök1 = new Besök { Kund = kund1, DateAndTime = new System.DateTime(2021, 12, 24, 12, 00, 00), Syfte = "Service", Mekaniker = mekaniker1};
            Besök besök2 = new Besök { Kund = kund2, DateAndTime= new System.DateTime(2021, 11, 29, 12, 00, 00), Syfte= "Däck", Mekaniker = mekaniker1 };
           
           
            testDB.besök.Add(besök1);
            testDB.besök.Add(besök2);

            testDB.SaveChanges();

            List<Reservdel> delar =  new List<Reservdel>() { reservdel2, reservdel1 };
            List<Reservdel> delar2 = new List<Reservdel>() { reservdel2, reservdel3 };

            Journal journal1 = new Journal() { Åtgärder = "Fixat bilrutan", reservdelar = delar , Besök = besök1};
            //Journal journal2 = new Journal() { Åtgärder = "Byt Dörren", reservdelar = delar , Besök = besök2 };
            Journal journal3 = new Journal() { Åtgärder = "Byt asfvjh", reservdelar = delar2, Besök = besök2 };

            testDB.jornals.Add(journal1);
            testDB.jornals.Add(journal3);

            testDB.SaveChanges();

            Console.WriteLine("klart");
        }
    }
}
