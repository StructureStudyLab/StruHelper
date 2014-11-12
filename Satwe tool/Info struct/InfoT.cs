using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace Satwe_tool
{
	class InfoT
	{
		public int ShakeNo;		
		public double T;
        public double Angle;
        public double MoveFactor;
        public double MoveFactor_X;
        public double MoveFactor_Y;
        public double TwistFactor;
		public void LoadData(string[] data)
		{			
            ShakeNo = int.Parse(data[0]);
			T = double.Parse(data[1]);
			Angle = double.Parse(data[2]);
			MoveFactor = double.Parse(data[3]);
            MoveFactor_X = double.Parse(data[4]);
            MoveFactor_Y=double.Parse(data[5]);
            TwistFactor = double.Parse(data[6]);
		}
	}
}
