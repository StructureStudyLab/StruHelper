using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace Satwe_tool
{
	struct InfoMaterial
	{
		public int StoryNo;
		public int StdStoryNo;
		public int TowerNo;
		public int BeamCount;
		public int BeamRc;
		public int BeamRf;
		public int ColumnCount;
		public int ColumnRc;
		public int ColumnRf;
		public int WallCount;
		public int WallRc;
		public int WallRf;
		public double StoryHeight;
		public double TotalHeight;
		public void LoadData(List<string> data)
		{
			Debug.Assert(data.Count == 14);
			try {
				StoryNo = int.Parse(data[0]);
				StdStoryNo = int.Parse(data[1]);
				TowerNo = int.Parse(data[2]);
				BeamCount = int.Parse(data[3]);
				BeamRc = int.Parse(data[4]);
				BeamRf = int.Parse(data[5]);
				ColumnCount = int.Parse(data[6]);
				ColumnRc = int.Parse(data[7]);
				ColumnRf = int.Parse(data[8]);
				WallCount = int.Parse(data[9]);
				WallRc = int.Parse(data[10]);
				WallRf = int.Parse(data[11]);
				StoryHeight = double.Parse(data[12]);
				TotalHeight = double.Parse(data[13]);
			} catch (Exception e) {
				MessageBox.Show(e.Message);
			}
		}
	}
}
