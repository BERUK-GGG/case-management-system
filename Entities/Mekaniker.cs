using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB_Ärendesystem.Entities
{
    public class Mekaniker
    {

        [Key] public int Id { get; set; }
        public string Namn { get; set; }
        public string Roll { get; set; }
        public string AnvändarNamn { get; set; }
        public string lösenord { get; set; }
        public string specialisering { get; set; }

        //public virtual List<Besök> Besöks { get; set; }

        public override string ToString()
        {
            return $"{Namn}";
        }

    }
}
