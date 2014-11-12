using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace Satwe_tool
{
	class InfoDisp
	{
		public int WorkNo;
        public string Description;
        public double XFactor;
        public string XFactor_description;
        public double YFactor;
        public string YFactor_description;
        public double XFactor_floor;
        public string XFactor_floor_description;
        public double YFactor_floor;
        public string YFactor_floor_description;
        public int XMaxDisp_floorAngle;
        public string XMaxDisp_floorAngle_description;
        public int YMaxDisp_floorAngle;
        public string YMaxDisp_floorAngle_description;

        public string GetDescription()
        {
           
            StringBuilder sb=new StringBuilder();
            if (XFactor > 0 || YFactor > 0 || XFactor_floor > 0 || YFactor_floor > 0 || XMaxDisp_floorAngle > 0 || YMaxDisp_floorAngle > 0)
            {
                sb.AppendFormat("         工况{0,-2}:", WorkNo);
                bool isFirst = true;
                if (XFactor > 0)
                {
                    sb.AppendFormat("{0}{1}\n", isFirst ? "" : "                ", XFactor_description);
                    isFirst = false;
                }
                if (YFactor > 0)
                {
                    sb.AppendFormat("{0}{1}\n", isFirst ? "" : "                ", YFactor_description);
                    isFirst = false;
                }
                if (XFactor_floor > 0)
                {
                    sb.AppendFormat("{0}{1}\n", isFirst ? "" : "                ", XFactor_floor_description);
                    isFirst = false;
                }
                if (YFactor_floor > 0)
                {
                    sb.AppendFormat("{0}{1}\n", isFirst ? "" : "                ", YFactor_floor_description);
                    isFirst = false;
                }
                if (XMaxDisp_floorAngle > 0)
                {
                    sb.AppendFormat("{0}{1}\n", isFirst ? "" : "                ", XMaxDisp_floorAngle_description);
                    isFirst = false;
                }
                if (YMaxDisp_floorAngle > 0)
                {
                    sb.AppendFormat("{0}{1}\n", isFirst ? "" : "                ", YMaxDisp_floorAngle_description);
                    isFirst = false;
                }
            }
            return sb.ToString();
        }
	}
}
