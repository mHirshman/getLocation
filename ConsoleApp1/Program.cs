using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WcfService1;

namespace ConsoleApp1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            ServiceHost serviceHost = new ServiceHost(typeof(IService1)) ;
            serviceHost.Open();

        }
    }
}
