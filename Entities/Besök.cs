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
        [Key]public int BesökID { get; set; }
        public int KundID { get; set; }
        public DateTime DateAndTime { get; set; }
        public string syfte { get; set; }
        public int Anställningsnummer { get; set; }
        

    }
}
