using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace Satwe_tool
{
	struct InfoSesmic
	{
		public double Strength;
		public string SiteType;
		public string SiteGroup;
		public double Tg;
		public int FrameGrade;
		public int WallGrade;
		public bool Consider_e;//是否考虑偶然偏心
		public bool Consider_doubleT;//是否考虑双向地震扭转效应
		public void LoadData(List<string> data)
		{
			Debug.Assert(data.Count == 8);
			Strength = double.Parse(data[0]);
			SiteType = data[1];
			SiteGroup = data[2];
			Tg = double.Parse(data[3]);
			FrameGrade = int.Parse(data[4]);
			WallGrade = int.Parse(data[5]);
			Consider_e = data[6] == "是";
			Consider_doubleT = data[7] == "是";
		}
	}
}
