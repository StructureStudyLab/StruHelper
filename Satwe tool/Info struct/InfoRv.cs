using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace Satwe_tool
{
	class InfoRv
	{
		public int FoorNo;		
		public int TowerNo;
        public double Ratio_X;
        public double Ratio_Y;
        public double Rv_X;
        public double Rv_Y;
		public void LoadData(List<string> data)
		{			
            FoorNo = int.Parse(data[0]);
            TowerNo = int.Parse(data[1]);
            Rv_X = Convert.ToDouble(data[2]);
            Rv_Y = Convert.ToDouble(data[3]);	
            Ratio_X = double.Parse(data[4]);
            Ratio_Y = double.Parse(data[5]);
           		
		}
	}
}
