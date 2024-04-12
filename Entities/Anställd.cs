using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB_Ärendesystem.Entities
{
    public abstract class Anställd
    {
        [Key] public int AnställningsNr {  get; set; }
        public string AnvändarNamn {  get; set; }
        public string lösenord { get; set;}

        public abstract string GetUsername();
    }
}
