using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kulala.Learning.IOC.Service
{
    public class GroupService : IGroupService
    {
       private readonly IDBL dBL;
        public GroupService(IDBL dBL)
        {
            this.dBL = dBL;
        }
        public void QueryGroup()
        {
            Console.WriteLine("Query Group info");
            dBL.Query();
        }
    }
}
