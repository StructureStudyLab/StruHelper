using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace Satwe_tool
{
	internal class ParserWzq:ParserBase
	{
        public double EffectiveMassFactor_X;
        public double EffectiveMassFactor_Y;
        public List<InfoT> TinfoList = new List<InfoT>();
        public List<InfoVG> VgList=new List<InfoVG>();
        public InfoVG minVg_X, minVg_Y;
        
        public List<InfoT> ParseTInfo(int floorCount,string SourceName)
        {
			if (SourceName == PathFinder.PKPM) {
				PkpmTInfo(floorCount);
			} else if (SourceName == PathFinder.YJK) {
				YjkTInfo(floorCount);
			}         
            return TinfoList;
        }

		private void PkpmTInfo(int floorCount)
		{
			Dictionary<int, Dictionary<int, InfoVG>> mapFloorNoToVgInfo = new Dictionary<int, Dictionary<int, InfoVG>>();
			for (int i = 0; i < m_contentArray.Length; i++) {
				if (m_contentArray[i].Contains("考虑扭转耦联时的振动周期")) {
					while (!m_contentArray[i].Contains("1")) {
						i++;
					}
					for (int j = i; ; j++) {						
						string lineData = m_contentArray[j];
						string[] dataArray = lineData.Trim().Split(new char[] { ' ', '(', ')', '+' }, StringSplitOptions.RemoveEmptyEntries);
						if (dataArray.Length == 0) {
							break;
						}
						InfoT tInfo = new InfoT();
						tInfo.LoadData(dataArray);
						TinfoList.Add(tInfo);
					}
				} else if (m_contentArray[i].Contains("X 方向的有效质量系数")) {
					string lineData = m_contentArray[i];
					string valueData = lineData.Substring(lineData.IndexOf(':') + 1).Trim(new char[] { '%', ' ' });
					EffectiveMassFactor_X = double.Parse(valueData);
				} else if (m_contentArray[i].Contains("Y 方向的有效质量系数")) {
					string lineData = m_contentArray[i];
					string valueData = lineData.Substring(lineData.IndexOf(':') + 1).Trim(new char[] { '%', ' ' });
					EffectiveMassFactor_Y = double.Parse(valueData);
				} else if (m_contentArray[i].Contains("Fx") && m_contentArray[i].Contains("Vx")) {
					string[] lastFoorArray = null;
					for (int j = 0; j < floorCount; j++) {
						int index = i + 5 + j;						
						string lineData = m_contentArray[index];
						string[] dataArray = lineData.Split(new char[] { ' ', '(', ')', '%' },StringSplitOptions.RemoveEmptyEntries);
						if (dataArray.Length==8) {
							lastFoorArray = dataArray;
						} else {
							List<string> dataList = new List<string>(dataArray);
							dataList.Insert(0, lastFoorArray[0]);
							dataList.Insert(5, lastFoorArray[5]);
							dataArray = dataList.ToArray();
						}
						InfoVG vgInfo = new InfoVG();						
						vgInfo.LoadData(dataArray, true);
						if (!mapFloorNoToVgInfo.ContainsKey(vgInfo.TowerNo)) {
							mapFloorNoToVgInfo.Add(vgInfo.TowerNo,new Dictionary<int, InfoVG>());
						}
						mapFloorNoToVgInfo[vgInfo.TowerNo].Add(vgInfo.FoorNo, vgInfo);
						VgList.Add(vgInfo);
					}
				} else if (m_contentArray[i].Contains("Fy") && m_contentArray[i].Contains("Vy")) {
					string[] lastFoorArray = null;
					for (int j = 0; j < floorCount; j++) {
						int index = i + 5 + j;						
						string lineData = m_contentArray[index];
						string[] dataArray = lineData.Split(new char[] { ' ', '(', ')', '%' },StringSplitOptions.RemoveEmptyEntries);
						if (dataArray.Length == 8) {
							lastFoorArray = dataArray;
						} else {
							List<string> dataList = new List<string>(dataArray);
							dataList.Insert(0, lastFoorArray[0]);
							dataList.Insert(5, lastFoorArray[5]);
							dataArray = dataList.ToArray();
						}						
						int floorNo = int.Parse(dataArray[0]);
						int towerNo = int.Parse(dataArray[1]);
						InfoVG vgInfo = mapFloorNoToVgInfo[towerNo][floorNo];
						vgInfo.LoadData(dataArray, false);
					}
				}
			}
			minVg_X = minVg_Y = VgList[0];
			for (int i = 1; i < VgList.Count; i++) {
				if (minVg_X.Vx_Ratio > VgList[i].Vx_Ratio) {
					minVg_X = VgList[i];
				}
				if (minVg_Y.Vy_Ratio > VgList[i].Vy_Ratio) {
					minVg_Y = VgList[i];
				}
			}
		}
		private void YjkTInfo(int floorCount)
		{
			Dictionary<int, Dictionary<int, InfoVG>> mapFloorNoToVgInfo = new Dictionary<int, Dictionary<int, InfoVG>>();
			for (int i = 0; i < m_contentArray.Length; i++) {
				if (m_contentArray[i].Contains("考虑扭转耦联时的振动周期")) {
					while (!m_contentArray[i].Contains("1")) {
						i++;
					}
					for (int j = i; ; j++) {
						string lineData = m_contentArray[j];
						string[] dataArray = lineData.Trim().Split(new char[] { ' ', '(', ')', '+' }, StringSplitOptions.RemoveEmptyEntries);
						if (dataArray.Length == 0) {
							break;
						}
						InfoT tInfo = new InfoT();
						tInfo.LoadData(dataArray);
						TinfoList.Add(tInfo);
					}
				} else if (m_contentArray[i].Contains("X向平动振型参与质量系数")) {
					string lineData = m_contentArray[i];
					string valueData = lineData.Substring(lineData.IndexOf(':') + 1).Trim(new char[] { '%', ' ' });
					EffectiveMassFactor_X = double.Parse(valueData);
				} else if (m_contentArray[i].Contains("Y向平动振型参与质量系数")) {
					string lineData = m_contentArray[i];
					string valueData = lineData.Substring(lineData.IndexOf(':') + 1).Trim(new char[] { '%', ' ' });
					EffectiveMassFactor_Y = double.Parse(valueData);
				} else if (m_contentArray[i].Contains("Fx") && m_contentArray[i].Contains("Vx")) {
					for (int j = 0; j < floorCount; j++) {
						int index = i + 2 + j;						
						string lineData = m_contentArray[index];
						string[] dataArray = lineData.Split(new char[] { ' ', '(', ')', '%' },StringSplitOptions.RemoveEmptyEntries);
  					    InfoVG vgInfo = new InfoVG();
						vgInfo.LoadData(dataArray, true);
						if (!mapFloorNoToVgInfo.ContainsKey(vgInfo.TowerNo)) {
							mapFloorNoToVgInfo.Add(vgInfo.TowerNo, new Dictionary<int, InfoVG>());
						}
						mapFloorNoToVgInfo[vgInfo.TowerNo].Add(vgInfo.FoorNo, vgInfo);
						VgList.Add(vgInfo);
					}
				} else if (m_contentArray[i].Contains("Fy") && m_contentArray[i].Contains("Vy")) {
					for (int j = 0; j < floorCount; j++) {
						int index = i + 2 + j;						
						string lineData = m_contentArray[index];
						string[] dataArray = lineData.Split(new char[] { ' ', '(', ')', '%' },StringSplitOptions.RemoveEmptyEntries);
						int floorNo = int.Parse(dataArray[0]);
						int towerNo = int.Parse(dataArray[1]);
						InfoVG vgInfo = mapFloorNoToVgInfo[towerNo][floorNo];
						vgInfo.LoadData(dataArray, false);
					}
				}
			}
			minVg_X = minVg_Y = VgList[0];
			for (int i = 1; i < VgList.Count; i++) {
				if (minVg_X.Vx_Ratio > VgList[i].Vx_Ratio) {
					minVg_X = VgList[i];
				}
				if (minVg_Y.Vy_Ratio > VgList[i].Vy_Ratio) {
					minVg_Y = VgList[i];
				}
			}
		}
	}
}
