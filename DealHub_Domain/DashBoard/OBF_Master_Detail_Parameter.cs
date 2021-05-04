using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealHub_Domain.DashBoard
{
    



        public class SolutionCategory:Serviceslist
        {
            public string Solutioncategory { get; set; }
            public List<SolutionServices> Solutionservices { get; set; }
        }

        public class SolutionServices 
        {
            public string Solutioncategory { get; set; }

            public string value { get; set; }
            public List<Serviceslist> Serviceslist { get; set; }
        }

        public class Serviceslist
        {
            public string value { get; set; }
            public string viewValue { get; set; }
        }

    






}
