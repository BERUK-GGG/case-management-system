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
        public Mekaniker MechanikernId { get; set; }
        public Reservdel ReservdelID { get; set; }
        public Mekaniker Mekaniker
        {
            get => default;
            set
            {
            }
        }

        public Kund Kund
        {
            get => default;
            set
            {
            }
        }
    }
}
