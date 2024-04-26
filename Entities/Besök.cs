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
        public int ID { get; set; }
     
        public Kund Kund { get; set; }

        public DateTime DateAndTime { get; set; }
        public string Syfte { get; set; }
        
        public Mekaniker Mekaniker { get; set; }
    }
}
