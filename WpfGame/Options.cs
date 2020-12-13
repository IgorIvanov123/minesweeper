using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfGame
{
	[Serializable]
	public class Options
	{
		public int weight { get; set; }
		public int countOfMines { get; set; }
		public Options()
		{
			weight = 10;
			countOfMines = 2;
		}

	}
}
