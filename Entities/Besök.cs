using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB_Ärendesystem.Entities
{
    public class Besök
    {
        [Key]
        public int BesökID { get; set; }

        public int KundId { get; set; } // Renamed from Kund_ID

        [ForeignKey("KundId")] // Updated ForeignKey attribute
        public virtual Kund Kund { get; set; }

        public DateTime DateAndTime { get; set; }
        public string syfte { get; set; }

        public int Anställningsnummer { get; set; }

        [ForeignKey("Anställningsnummer")]
        public virtual Mekaniker Mekaniker { get; set; }
    }
}
