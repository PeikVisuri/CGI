using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGI
{
    class MainC
    {
        public static void Main(string[] args)
        {
            //test
            while (true)
            {
                Console.WriteLine("Input Business ID: ");
                string idInput = Console.ReadLine();
                BusinessIdSpecification test = new BusinessIdSpecification();
                Console.WriteLine("Check result: "+test.IsSatisfiedBy(idInput));
                foreach(string i in test.ReasonsForDissatisfaction)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}
