using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB_Ärendesystem.Entities
{
    public class Besök
    {
        public int BesökID { get; set; }
        public Kund KundID { get; set; }
        public DateTime DateAndTime { get; set; }
        public string syfte { get; set; }
        public Mekaniker MekanikerID { get; set; }
        public ICollection<Reservdel> ReservdelID { get; set; }
        

    }
}
