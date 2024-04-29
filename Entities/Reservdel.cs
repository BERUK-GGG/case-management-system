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
        [Key]public int ID { get; set; }
        public string Namn { get; set; }
        public decimal Pris { get; set; }

        public override string ToString()
        {
            return Namn;
        }
    }
}
