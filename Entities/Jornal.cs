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
    public class Jornal
    {
        [Key]public int JornalID { get; set; }   
        public string Åtgärder {  get; set; }

        public int BesökId { get; set; }

        [ForeignKey("BesökId")]
        public virtual Besök Besök { get; set; }

        public int ReservdelID { get; set; }

        [ForeignKey("ReservdelID")]
        public virtual Reservdel reservdel {  get; set; }


    }
}
