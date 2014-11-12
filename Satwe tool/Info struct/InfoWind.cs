using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace Satwe_tool
{
	struct InfoWind
	{
		public double W0;		
		public double Tx;
		public double Ty;
		public string SiteRawType;		
		public void LoadData(List<string> data)
		{
			Debug.Assert(data.Count == 4);
			W0 = double.Parse(data[0]);
			Tx = double.Parse(data[1]);
			Ty = double.Parse(data[2]);
			SiteRawType = data[3];
		}
	}
}
