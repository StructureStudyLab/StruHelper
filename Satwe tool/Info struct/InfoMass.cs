using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace Satwe_tool
{
	class InfoMass
	{
		public int FoorNo;
		public int TowerNo;
        public double MassRate;
		public void LoadData(List<string> data)
		{
			
            FoorNo = int.Parse(data[0]);
			TowerNo = int.Parse(data[1]);
			MassRate = double.Parse(data[2]);
		}
	}
}
