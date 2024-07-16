using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wordFinder.helpers
{
    public class WriteConsole
	{
        public static string centerText(string text) {
			return String.Format("{0," + ((Console.WindowWidth / 2) + (text.Length / 2)) + "}", text);
		}


		public static string NewLine(char text)
		{
			return "".PadRight(Console.WindowWidth,text) ;
		}
	}
}
