using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB_Ärendesystem.Entities
{
    public class Mekaniker : Anställd
    {
        public Anställd AnställningsNr { get; set; }
        public int MekanikerID { get; set; }
        public string Namn { get; set; }
        public string Roll { get; set; }
        public string specialisering { get; set; }

        // Method to override abstract method from base class
        public override string GetUsername()
        {
            return AnvändarNamn;
        }

        public string HämtaSträng()
        {
           
            return $"ID = {MekanikerID}, Namn = {Namn}, Roll = {Roll}, Specialisering = {specialisering} ";

        }
    }
}
