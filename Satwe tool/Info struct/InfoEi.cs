using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace Satwe_tool
{
	class InfoEi
	{
		public int FoorNo;		
		public int TowerNo;
        public double Ratx;
        public double Ratx1;
        public double Ratx2;
        public double Raty;
        public double Raty1;
        public double Raty2;
        public double WeekFloorFactor;
		public void LoadData(List<string> data)
		{
			
            FoorNo = int.Parse(data[0]);
            TowerNo = int.Parse(data[1]);
			Ratx = double.Parse(data[2]);
			Raty = double.Parse(data[3]);
			Ratx1 = double.Parse(data[4]);
            Raty1 = double.Parse(data[5]);
            if (data.Count==9)
            {
                Ratx2 = double.Parse(data[6]);
                Raty2 = double.Parse(data[7]);
                WeekFloorFactor = double.Parse(data[8]);
            }
            else if (data.Count == 7)
            {
                WeekFloorFactor = double.Parse(data[6]);
            }            
		}
	}
}
