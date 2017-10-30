using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoinDiffusionVP.Models
{
	class DataProcessor
	{
		private string PATH_INPUT;
		private string PATH_OUTPUT;
		private List<Country> fileInfoDataSet;
		public DataProcessor()
		{
			var rootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

			PATH_INPUT = rootPath + @"\resource\input.txt";
			PATH_OUTPUT = rootPath + @"\resource\output.txt";
			fileInfoDataSet = new List<Country>();
		}
		public Country[] GetInputDataFile()
		{
			using (StreamReader sr = new StreamReader(PATH_INPUT))
			{
				string line;
				while ((line = sr.ReadLine()) != null)
				{
					var _countryAmmount = Convert.ToInt32(line);
					for (var i = 0; i < _countryAmmount; i++)
					{
						if ((line = sr.ReadLine()) == null)
							throw new Exception(String.Format("Invalid Size of input FILE {0} data", PATH_INPUT));

						var lineItems = line.Split(' ');
						if (lineItems.Length != 5)
							throw new Exception(String.Format("Invalid Size of input line {0} data", line));

						var item = new Country(Convert.ToString(lineItems[0]),
												Convert.ToInt32(lineItems[1]),
												Convert.ToInt32(lineItems[2]),
												Convert.ToInt32(lineItems[3]),
												Convert.ToInt32(lineItems[4]));

						fileInfoDataSet.Add(item);
					}
				}
			}
			return fileInfoDataSet.ToArray();
		}
		public void GetOutputFile(AreaMap source)
		{
			using (var stream = new FileStream(PATH_OUTPUT, FileMode.Truncate, FileAccess.Write))
			{
				using (var writter = new StreamWriter(stream))
				{
					foreach (var item in source.innerCountries)
					{
						writter.WriteLine("Country{0,10}___steps{1,6}", item.name, item.readyStep);
					}
				}
			}
		}
	}
}
