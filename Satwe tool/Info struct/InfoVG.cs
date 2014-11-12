using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace Satwe_tool
{
	class InfoVG
	{
		public int FoorNo;		
		public int TowerNo;
        public double Fx;
        public double Fy;
        public double Vx;
        public double Vy;
        public double Vx_Ratio;
        //public double Vx_Total_Ratio;
        public double Vy_Ratio;
        //public double Vy_Total_Ratio;
        public double Mx;
        public double My;
        public double Static_Fx;
        public double Static_Fy;
		public void LoadData(string[] data,bool isX)
		{
            if (isX)
            {
                FoorNo = int.Parse(data[0]);
                TowerNo = int.Parse(data[1]);
                Fx = double.Parse(data[2]);
                Vx = double.Parse(data[3]);
                Vx_Ratio = double.Parse(data[4]);
				if (data.Length == 8) {
					Mx = double.Parse(data[6]);
					Static_Fx = double.Parse(data[7]);
				} else {
					Mx = double.Parse(data[5]);
					Static_Fx = double.Parse(data[6]);
				}
                //Vx_Total_Ratio = double.Parse(data[5]);
               
            }
            else
            {
                Fy = double.Parse(data[2]);
                Vy = double.Parse(data[3]);
                Vy_Ratio = double.Parse(data[4]);
                //Vy_Total_Ratio = double.Parse(data[5]);
				if (data.Length == 8) {
					My = double.Parse(data[6]);
					Static_Fy = double.Parse(data[7]);
				} else {
					My = double.Parse(data[5]);
					Static_Fy = double.Parse(data[6]);
				}               
            }
		}
	}
}
