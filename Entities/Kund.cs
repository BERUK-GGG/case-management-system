using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB_Ärendesystem.Entities
{
    public class Kund
    {
        [Key]public int ID { get; set; }
        public string Namn { get; set; }
        public int PersonNr { get; set; }
        public string Address { get; set; }
        public int TeleNr { get; set; }
        public string Epost { get; set; }


        public string HämtaSträng()
        {

            return $"KundID = {ID}, Namn = {Namn}, PersonNr = {PersonNr}, Address = {TeleNr},TeleNr = {TeleNr} ,Epost = {Epost} ";

        }

        public override string ToString()
        {
            return $"{Namn}";
        }
    }
}
