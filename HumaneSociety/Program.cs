using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class Program
    {
        static void Main(string[] args)
        {
            USState[] states = Query.GetStates();

            foreach (var state in states)
            {
                Console.WriteLine(state.Abbreviation);
            }

            PointOfEntry.Run();
        }
    }
}
