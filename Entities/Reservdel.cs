using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB_Ärendesystem.Entities
{
    public class Reservdel
    {   
        [Key]public int ReservID { get; set; }
        public string Namn { get; set; }
        public decimal Pris { get; set; }

        
    }
}
