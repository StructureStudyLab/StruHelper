using System;
using System.Collections.Generic;
using System.Text;

namespace Satwe_tool
{
	internal class Test
	{
		public static void TestWmassParser()
		{
			string path = @"H:\公司项目\Velo\PKPM样例\20140306  刘亚辉\WMASS.OUT";
			ParserWmass parser = ParserWmass.InstanceLeft;
			if (parser.ReadFile(path)) {				
				DataSummary sumInfo=parser.ParseSummaryInfo();
				parser.ParseSesmicInfo();
			}
		}
	}
}
