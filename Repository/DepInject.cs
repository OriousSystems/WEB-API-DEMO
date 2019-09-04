using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DepInject : IDependencyInjection
    {
        public string GetSomething()
        {
            return "this is dependency injection";
        }
    }
}
