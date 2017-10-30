using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinDiffusionVP.Models
{
    class Country
    {
        public string name;
        public int xl, yl, xh, yh;
        public int readyStep { get; set; }
        public Country(string name, int xl, int yl, int xh, int yh)
        {
            this.name = name;
            this.yl = yl;
            this.xl = xl;
            this.xh = xh;
            this.yh = yh;
            readyStep = 0;
        }
    }
}
