using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using RB_Ärendesystem.Datalayer.Repositories.Base;
using RB_Ärendesystem.Datalayer.Repositories.interfaces;
using RB_Ärendesystem.Entities;

namespace RB_Ärendesystem.Datalayer.Repositories
{
    public class JournalRepository : Repository<Journal>, IJournalRepository

    {
        public JournalRepository(RB_context context) : base(context) { }
    }
}
