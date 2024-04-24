using RB_Ärendesystem.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Journal
    {
        [Key]
        public int ID { get; set; }   
        public string Åtgärder {  get; set; }

        public virtual Besök Besök { get; set; }

        public virtual List<Reservdel> reservdelar {  get; set; }


    }
}
