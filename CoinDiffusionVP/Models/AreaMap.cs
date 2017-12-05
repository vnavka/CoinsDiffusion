using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinDiffusionVP.Models
{
    class AreaMap
    {
        public City[][] map;
        public Country[] innerCountries;
        public int countries;
        private int step { get; set; }
        public AreaMap(Country[] countries)
        {
            ContryValidation(countries);
            this.countries = countries.Length;
            innerCountries = countries;
            step = 0;

            int _max_x = 0, _max_y = 0;
            InitMapSize(ref _max_x, ref _max_y);
            InitMap(_max_x, _max_y);
        }
        public void RunTransaction()
        {
            for (var i = 0; i < map.Length; i++)
            {
                for (var j = 0; j < map[i].Length; j++)
                {
                    MakeTransaction(i, j);
                }
            }
        }

        public bool AreaReady
        {
            get
            {
                step++;
                var ready = 1;
                foreach (var country in innerCountries)
                {
                    if (country.readyStep == 0)
                    {
                        if (RunCheckCountryReady(country))
                        {
                            country.readyStep = step;
                        }
                        else
                        {
                            ready *= 0;
                        }
                    }
                }
                return ready == 1;
            }
        }
        public bool RunCheckCountryReady(Country item)
        {
            for (var j = item.xl - 1; j < item.xh; j++)
            {
                for (var k = item.yl - 1; k < item.yh; k++)
                {
                    if (!CheckCityReady(map[j][k]))
                        return false;
                }
            }
            return true;
        }
        public bool CheckCityReady(City item)
        {
            for (var i = 0; i < countries; i++)
            {
                if (item._coins[i] == 0)
                    return false;
            }
            return true;
        }
        public void GetFinalInfo()
        {
            int i = 1;
            foreach (var country in innerCountries)
            {
                Console.WriteLine("id{0,2} Step{1,5}", i, country.readyStep);
                i++;
            }
        }
        #region private methods
        private void ContryValidation(Country[] countries)
        {
            foreach (var item in countries)
            {
                if (item.xl < 1 || item.xl >= item.xh || item.xh >= 10)
                    throw new Exception(String.Format("Not valid x input data in {0} Country", item.name));
                if (item.yl < 1 || item.yl >= item.yh || item.yh >= 10)
                    throw new Exception(String.Format("Not valid y input data in {0} Country", item.name));
            }
        }
        private void MakeTransaction(int x, int y)
        {
            if (x > 0) //left trans
            {
                MakeTransfer(map[x][y], map[x - 1][y]);
            }
            if (x < map[0].Length - 1) //Right trans
            {
                MakeTransfer(map[x][y], map[x + 1][y]);
            }
            if (y > 0) //top trans
            {
                MakeTransfer(map[x][y], map[x][y - 1]);
            }
            if (y < map.Length - 1) //bot trans
            {
                MakeTransfer(map[x][y], map[x][y + 1]);
            }
        }
        private void MakeTransfer(City source, City destination)
        {
            if (source == null || destination == null)
                return;
			
			for (var i = 0; i < countries; i++)
			{
				var _t_coins = source._coins[i] / 1000;
				source._coins[i] -= _t_coins;
				destination._coins[i] += _t_coins;
			}
		}
        private void TestOutput()
        {
            for (var i = 0; i < map.Length; i++)
            {
                for (var j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == null)
                    {
                        Console.Write("_");
                        continue;
                    }
                    Console.Write(map[i][j].countryId);
                }
                Console.WriteLine();
            }
        }
        public void TestOutputCoins()
        {
            for (var i = 0; i < map.Length; i++)
            {
                for (var j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == null)
                    {
                        continue;
                    }
                    var t = map[i][j];
                    Console.Write(String.Format("C_{0,2}=", t.countryId));
                    for (var k = 0; k < countries; k++)
                    {
                        Console.Write(String.Format("B{0}_{1,9}| ", k + 1, t._coins[k]));
                    }
                    Console.WriteLine();
                }
            }
        }
        private void InitMap(int size_x, int size_y)
        {
            this.map = new City[size_x][];
            for (var i = 0; i < map.Length; i++)
            {
                this.map[i] = new City[size_y];
            }
            MapMemorySet();
        }
        private void MapMemorySet()
        {
            for (var i = 0; i < innerCountries.Length; i++)
            {
                for (var j = innerCountries[i].xl - 1; j < innerCountries[i].xh; j++)
                {
                    for (var k = innerCountries[i].yl - 1; k < innerCountries[i].yh; k++)
                    {
                        map[j][k] = new City(innerCountries.Length, i);
                    }
                }
            }
        }
        private void InitMapSize(ref int x, ref int y)
        {
            foreach (var item in innerCountries)
            {
                if (x < item.xh)
                    x = item.xh;

                if (y < item.yh)
                    y = item.yh;
            }
        }
        #endregion

    }
}
