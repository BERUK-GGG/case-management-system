using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RB_Ärendesystem.Datalayer.Repositories.Base;
using RB_Ärendesystem.Datalayer.Repositories.interfaces;
using RB_Ärendesystem.Entities;

namespace RB_Ärendesystem.Datalayer.Repositories
{
    public class BesökRepository: Repository<Besök>, IBesökRepository

    {
        public BesökRepository(RB_context context): base(context) { }

        public override IEnumerable<Besök> GetAll()
        {
            return Context.besök.Include(x => x.Mekaniker);
        }
    }
}
