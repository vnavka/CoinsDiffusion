using CoinDiffusionVP.Models;
using System;

namespace CoinDiffusionVP
{
    class Program
    {
        static void Main(string[] args)
        {
            var map = new AreaMap(new Country[]{
                new Country("France", 1,4,4,6),
                new Country("Spain", 3,1,6,3),
                new Country("Portugal", 1,1,2,2)
            });

            while (!map.AreaReady)
            {
                map.RunTransaction();
            }

            map.GetFinalInfo();
            Console.ReadLine();
        }
    }
}
