using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinDiffusionVP.Models
{
    class City
    {
        public int[] _coins;
        public int countryId { get; set; }
        private const int definedCoinConstAmmmount = 1000000;

        public City(int counties, int id)
        {
            countryId = id;
            _coins = new int[counties];

            for (var i = 0; i < _coins.Length; i++)
                _coins[i] = 0;
            _coins[id] = definedCoinConstAmmmount;
        }
    }
}
