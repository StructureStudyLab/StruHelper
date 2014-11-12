using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace Satwe_tool
{
	internal class ParserWmass:ParserBase
	{
		public DataSummary m_SumInfo;
        public string SourceName;
		protected ParserWmass()
		{
	    }
		public static ParserWmass InstanceLeft
		{
			get
			{
				if (_instanceLeft == null) {
                    _instanceLeft = new ParserWmass();
				}
                return _instanceLeft as ParserWmass;
			}
		}

        public static ParserWmass InstanceRight
        {
            get
            {
                if (_instanceRight == null)
                {
                    _instanceRight = new ParserWmass();
                }
                return _instanceRight as ParserWmass;
            }
        }

		public override bool ReadFile(string filePath)
		{
            m_SumInfo = new DataSummary();
			if (!base.ReadFile(filePath))
			{
				 return false;
			}			
			m_key = new ConstSearchKey(1);
			this.ParseSummaryInfo();
			this.ParseSesmicInfo();
            this.ParseEiInfo();
            this.ParseMassInfo();
            this.ParseRvInfo();            
			return true;
		}

		/// <summary>
		/// 结构总信息
		/// </summary>
		/// <returns></returns>
		public DataSummary ParseSummaryInfo()
		{
			if (SourceName==PathFinder.PKPM) {
				PkpmSummaryInfo();
			} else if (SourceName==PathFinder.YJK) {
				YjkSummaryInfo();
			}
				
			return m_SumInfo;
		}

		private void PkpmSummaryInfo()
		{
			PkpmFloorMatInfo();
			for (int i = 0; i < m_contentArray.Length; i++) {
				if (m_contentArray[i].Contains(m_key.BaseCount)) {
					string lineData = m_contentArray[i];
					m_SumInfo.BaseCount = int.Parse(lineData.Substring(lineData.IndexOf('=') + 1).Trim());
				} else if (m_contentArray[i].Contains(m_key.StructureType)) {
					string lineData = m_contentArray[i];
					string[] splitData = lineData.Split(new char[]{' ',':','='},StringSplitOptions.RemoveEmptyEntries);
					m_SumInfo.StructureType = splitData[1];
					break;
				}
			}
		}

		private void YjkSummaryInfo()
		{
			YjkFloorMatInfo();
			for (int i = 0; i < m_contentArray.Length; i++) {
				if (m_contentArray[i].Contains(m_key.BaseCount)) {
					string lineData = m_contentArray[i];
					string[] dataArray = lineData.Split(new char[] { ':', '=' }, StringSplitOptions.RemoveEmptyEntries);
					m_SumInfo.BaseCount = int.Parse(dataArray[1]);
                    break;
				} else if (m_contentArray[i].Contains("结构体系")) {
					string lineData = m_contentArray[i];
					string[] splitData = lineData.Split(new char[] { ':', ' ' }, StringSplitOptions.RemoveEmptyEntries);
					m_SumInfo.StructureType = splitData[1];					
				}
			}
		}

		private List<InfoMaterial> PkpmFloorMatInfo()
		{
			int dataIndex = 0;
			for (int i = 0; i < m_contentArray.Length; i++) {
				if (m_contentArray[i].Contains(m_key.StoreyMatInfo)) {
					dataIndex = i + 6;
					break;
				}
			}
			string lastFloorNo = string.Empty;
			string lastStdNo = string.Empty;
			int floorCount = 0;
			for (int i = dataIndex; i < m_contentArray.Length; i++) {
				string lineData = m_contentArray[i];
				string[] splitData = lineData.Split(new char[] { ' ', '(', ')', '/' },StringSplitOptions.RemoveEmptyEntries);
				List<string> result = new List<string>(splitData);				
				if (result.Count ==0) {
					break;
				}
				if (result.Count==14) {
					lastFloorNo = result[0];
					lastStdNo = result[1];
					floorCount++;
				} else {
					result.Insert(0, lastStdNo);
					result.Insert(0, lastFloorNo);
				}
				InfoMaterial elemInfo = new InfoMaterial();
				elemInfo.LoadData(result);
				m_SumInfo.FloorElemMatInfo.Add(elemInfo);
			}
			m_SumInfo.FloorCount =floorCount;
			return m_SumInfo.FloorElemMatInfo;
		}

		private List<InfoMaterial> YjkFloorMatInfo()
		{
			int dataIndex = 0;
			for (int i = 0; i < m_contentArray.Length; i++) {
				if (m_contentArray[i].Contains(m_key.StoreyMatInfo)) {
					dataIndex = i + 5;
					break;
				}
			}
			string lastFloorNo = string.Empty;			
			int floorCount = 0;
			for (int i = dataIndex; i < m_contentArray.Length; i++) {
				string lineData = m_contentArray[i];
				string[] splitData = lineData.Split(new char[] { ' ', '(', ')', '/' },StringSplitOptions.RemoveEmptyEntries);
				if (splitData.Length != 8) {
					break;
				}
				if (lastFloorNo!=splitData[0]) {
					lastFloorNo = splitData[0];
					floorCount++;
				}
				InfoMaterial elemInfo = new InfoMaterial();
				m_SumInfo.FloorElemMatInfo.Add(elemInfo);
			}
			m_SumInfo.FloorCount = floorCount;
			return m_SumInfo.FloorElemMatInfo;
		}

		
		
		public InfoSesmic ParseSesmicInfo()
		{
			if (SourceName == PathFinder.PKPM) {
				PkpmSesmicInfo();
			} else if (SourceName == PathFinder.YJK) {
				YjkSesmicInfo();
			}
			
			return m_SumInfo.SesmicInfo;
		}
		private void PkpmSesmicInfo()
		{
			for (int i = 0; i < m_contentArray.Length; i++) {
				if (m_contentArray[i].Contains(m_key.WindInfo)) {
					List<string> result = new List<string>();
					string lineData = m_contentArray[i + 1];
					result.Add(lineData.Substring(lineData.LastIndexOf('=') + 1).Trim());
					lineData = m_contentArray[i + 4];
					result.Add(lineData.Substring(lineData.LastIndexOf('=') + 1).Trim());
					lineData = m_contentArray[i + 5];
					result.Add(lineData.Substring(lineData.LastIndexOf('=') + 1).Trim());
					lineData = m_contentArray[i + 3];
					result.Add(lineData.Substring(lineData.LastIndexOf(':') + 1).Trim());
					m_SumInfo.WindInfo.LoadData(result);
				}
				if (m_contentArray[i].Contains(m_key.SesmicInfo)) {
					List<string> result = new List<string>();
					string lineData = m_contentArray[i + 3];
					result.Add(lineData.Substring(lineData.LastIndexOf('=') + 1).Trim());
					lineData = m_contentArray[i + 4];
					result.Add(lineData.Substring(lineData.LastIndexOf('=') + 1).Trim());
					lineData = m_contentArray[i + 5];
					result.Add(lineData.Substring(lineData.LastIndexOf(':') + 1).Trim());
					lineData = m_contentArray[i + 6];
					result.Add(lineData.Substring(lineData.LastIndexOf('=') + 1).Trim());
					lineData = m_contentArray[i + 10];
					result.Add(lineData.Substring(lineData.LastIndexOf('=') + 1).Trim());
					lineData = m_contentArray[i + 11];
					result.Add(lineData.Substring(lineData.LastIndexOf('=') + 1).Trim());
					lineData = m_contentArray[i + 18];
					result.Add(lineData.Substring(lineData.LastIndexOf(':') + 1).Trim());
					lineData = m_contentArray[i + 19];
					result.Add(lineData.Substring(lineData.LastIndexOf(':') + 1).Trim());
					m_SumInfo.SesmicInfo.LoadData(result);
					break;
				}
			}
		}
		private void YjkSesmicInfo()
		{
			for (int i = 0; i < m_contentArray.Length; i++) {
				if (m_contentArray[i].Contains(m_key.WindInfo)) {
					List<string> result = new List<string>();
					string lineData = m_contentArray[i + 3];
					result.Add(lineData.Substring(lineData.LastIndexOf(':') + 1).Trim());
					lineData = m_contentArray[i + 4];
					result.Add(lineData.Substring(lineData.LastIndexOf(':') + 1).Trim());
					lineData = m_contentArray[i + 5];
					result.Add(lineData.Substring(lineData.LastIndexOf(':') + 1).Trim());
					lineData = m_contentArray[i + 2];
					result.Add(lineData.Substring(lineData.LastIndexOf(':') + 1).Trim());
					m_SumInfo.WindInfo.LoadData(result);
				}
				
				if (m_contentArray[i].Contains(m_key.SesmicInfo)) {
					for (int j = i+1; j <i+50; j++) {
						string lineData = m_contentArray[j];
						string[] dataArray = lineData.Split(new char[] { ':', '(', ' ' }, StringSplitOptions.RemoveEmptyEntries);			
						if (lineData.Contains("地震烈度:")) {
							m_SumInfo.SesmicInfo.Strength = double.Parse(dataArray[1]);
						} else if (lineData.Contains("场地类别:")) {
							m_SumInfo.SesmicInfo.SiteType = dataArray[1];
						} else if (lineData.Contains("设计地震分组:")) {
							m_SumInfo.SesmicInfo.SiteGroup = dataArray[1];
						} else if (lineData.Contains("特征周期:")) {
							m_SumInfo.SesmicInfo.Tg = double.Parse(dataArray[1]);
						} else if (lineData.Contains("框架的抗震等级:")) {
							m_SumInfo.SesmicInfo.FrameGrade =int.Parse(dataArray[1]);
						} else if (lineData.Contains("剪力墙的抗震等级:")) {
							m_SumInfo.SesmicInfo.WallGrade = int.Parse(dataArray[1]);
						} else if (lineData.Contains("是否考虑偶然偏心:")) {
							m_SumInfo.SesmicInfo.Consider_e = dataArray[1] == "是";
						} else if (lineData.Contains("是否考虑双向地震扭转效应:")) {
							m_SumInfo.SesmicInfo.Consider_doubleT = dataArray[1] == "是";
						}
					}
					break;
				}
			}
		}


        public List<InfoEi> ParseEiInfo()
        {
			if (SourceName == PathFinder.PKPM) {
				PkpmEiInfo();
			} else if (SourceName == PathFinder.YJK) {
				YjkEiInfo();
			}            
			return m_SumInfo.FloorEiInfo;            
        }
		private void PkpmEiInfo()
		{
			for (int i = 0; i < m_contentArray.Length; i++) {
				if (m_contentArray[i].Contains("Floor No") && m_contentArray[i + 1].Contains("Xstif")) {
					List<string> dataList = new List<string>();
					string lineData = m_contentArray[i];
					int indexOfTower = lineData.IndexOf("Tower No");
					if (indexOfTower < 0) {
						continue;
					}
					dataList.Add(lineData.Substring(11, indexOfTower - 11).Trim());
					dataList.Add(lineData.Substring(indexOfTower + 10).Trim());
					i = i + 4;
					lineData = m_contentArray[i];
					int indexOfRaty = lineData.IndexOf("Raty");
					dataList.Add(lineData.Substring(8, indexOfRaty - 8).Trim());
					dataList.Add(lineData.Substring(indexOfRaty + 6).Trim());
					i = i + 1;
					lineData = m_contentArray[i];
					indexOfRaty = lineData.IndexOf("Raty1");
					dataList.Add(lineData.Substring(8, indexOfRaty - 8).Trim());
					dataList.Add(lineData.Substring(indexOfRaty + 6).Trim());
					i = i + 1;
					lineData = m_contentArray[i];
					indexOfRaty = lineData.IndexOf("Raty2");
					string[] subStrArray=null; 
					if (indexOfRaty >= 0) {
						dataList.Add(lineData.Substring(8, indexOfRaty - 8).Trim());
						subStrArray = lineData.Substring(indexOfRaty + 6).Split(
							new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
						dataList.Add(subStrArray[0]);
						if (subStrArray.Length==3) {
							dataList.Add(subStrArray[2]);
						}
						i = i + 1;
					}
					if (subStrArray==null||subStrArray.Length==1) {
						lineData = m_contentArray[i];
						dataList.Add(lineData.Substring(lineData.IndexOf('=') + 1).Trim());
					}
					

					InfoEi EiInfo = new InfoEi();
					EiInfo.LoadData(dataList);
					m_SumInfo.FloorEiInfo.Add(EiInfo);
				}
			}
			for (int i = 0; i < m_SumInfo.FloorEiInfo.Count; i++) {
				InfoEi item = m_SumInfo.FloorEiInfo[i];
				if (m_SumInfo.minRtx == null || m_SumInfo.minRtx.Ratx < item.Ratx) {
					m_SumInfo.minRtx = item;
				}
				if (m_SumInfo.minRty == null || m_SumInfo.minRty.Raty < item.Raty) {
					m_SumInfo.minRty = item;
				}
				if (m_SumInfo.minRtx1 == null || m_SumInfo.minRtx1.Ratx1 > item.Ratx1) {
					m_SumInfo.minRtx1 = item;
				}
				if (m_SumInfo.minRty1 == null || m_SumInfo.minRty1.Raty1 > item.Raty1) {
					m_SumInfo.minRty1 = item;
				}
				if (m_SumInfo.minRtx2 == null || m_SumInfo.minRtx2.Ratx2 > item.Ratx2) {
					m_SumInfo.minRtx2 = item;
				}
				if (m_SumInfo.minRty2 == null || m_SumInfo.minRty2.Raty2 > item.Raty2) {
					m_SumInfo.minRty2 = item;
				}
			}
		}
		private void YjkEiInfo()
		{
			for (int i = 0; i < m_contentArray.Length; i++) {
				if (m_contentArray[i].Contains("Floor No") && m_contentArray[i + 1].Contains("Xstif")) {
					List<string> dataList = new List<string>();
					string lineData = m_contentArray[i];
					int indexOfTower = lineData.IndexOf("Tower No");
					if (indexOfTower < 0) {
						continue;
					}
					dataList.Add(lineData.Substring(11, indexOfTower - 11).Trim());
					dataList.Add(lineData.Substring(indexOfTower + 10).Trim());
					int index= i + 4;
					lineData = m_contentArray[index];
					int indexOfRaty = lineData.IndexOf("Raty");
					dataList.Add(lineData.Substring(8, indexOfRaty - 8).Trim());
					dataList.Add(lineData.Substring(indexOfRaty + 6).Trim());

					index = index + 2;
					lineData = m_contentArray[index];
					indexOfRaty = lineData.IndexOf("Raty1");
					if (indexOfRaty >= 0) {
						dataList.Add(lineData.Substring(8, indexOfRaty - 8).Trim());
						dataList.Add(lineData.Substring(indexOfRaty + 6).Trim());
						index = index + 1;
					}
					lineData = m_contentArray[index];
					indexOfRaty = lineData.IndexOf("Raty2");
					if (indexOfRaty >= 0) {
						dataList.Add(lineData.Substring(8, indexOfRaty - 8).Trim());
						dataList.Add(lineData.Substring(indexOfRaty + 6).Trim());
						index = index + 1;
					}

					lineData = m_contentArray[i+5];
					dataList.Add(lineData.Substring(lineData.IndexOf('=') + 1).Trim());
					

					InfoEi EiInfo = new InfoEi();
					EiInfo.LoadData(dataList);
					m_SumInfo.FloorEiInfo.Add(EiInfo);
				}
			}
			for (int i = 0; i < m_SumInfo.FloorEiInfo.Count - 1; i++) {
				InfoEi item = m_SumInfo.FloorEiInfo[i];
				if (m_SumInfo.minRtx == null || m_SumInfo.minRtx.Ratx < item.Ratx) {
					m_SumInfo.minRtx = item;
				}
				if (m_SumInfo.minRty == null || m_SumInfo.minRty.Raty < item.Raty) {
					m_SumInfo.minRty = item;
				}
				if (m_SumInfo.minRtx1 == null || m_SumInfo.minRtx1.Ratx1 > item.Ratx1) {
					m_SumInfo.minRtx1 = item;
				}
				if (m_SumInfo.minRty1 == null || m_SumInfo.minRty1.Raty1 > item.Raty1) {
					m_SumInfo.minRty1 = item;
				}
				if (m_SumInfo.minRtx2 == null || m_SumInfo.minRtx2.Ratx2 > item.Ratx2) {
					m_SumInfo.minRtx2 = item;
				}
				if (m_SumInfo.minRty2 == null || m_SumInfo.minRty2.Raty2 > item.Raty2) {
					m_SumInfo.minRty2 = item;
				}
			}
		}

        public List<InfoMass> ParseMassInfo()
        {
			if (SourceName == PathFinder.PKPM) {
				PkpmMassInfo();
			} else if (SourceName == PathFinder.YJK) {
				YjkMassInfo();
			}   
            return m_SumInfo.FloorMassInfo;
        }
		private void PkpmMassInfo()
		{
			for (int i = 0; i < m_contentArray.Length; i++) {
				if (m_contentArray[i].Contains("各层的质量、质心坐标信息")) {
					i = i + 5;
					string lastFloorNo=string.Empty;
					for (int j = 0; j < m_SumInfo.FloorElemMatInfo.Count; j++) {
						List<string> dataList = new List<string>();
						int rowIndex = i + j;
						string lineData = m_contentArray[rowIndex];		
						int indexOfBrack=lineData.IndexOf('(');
						if (indexOfBrack>=0)
						{
							lineData=lineData.Remove(indexOfBrack);
						}						
						string[] dataArray = lineData.Split(new char[]{' ','('},StringSplitOptions.RemoveEmptyEntries);
						if (dataArray.Length==9) {
							lastFloorNo = dataArray[0];
							dataList.Add(dataArray[0]);
							dataList.Add(dataArray[1]);
							dataList.Add(dataArray[8]);
						} else {
							dataList.Add(lastFloorNo);
							dataList.Add(dataArray[0]);
							dataList.Add(dataArray[7]);
						}						
						InfoMass massInfo = new InfoMass();
						massInfo.LoadData(dataList);
						m_SumInfo.FloorMassInfo.Add(massInfo);
					}

				}
			}

			if (m_SumInfo.FloorMassInfo.Count > 0) {
				m_SumInfo.maxMassInfo = m_SumInfo.FloorMassInfo[0];
				m_SumInfo.minMassInfo = m_SumInfo.FloorMassInfo[0];
				for (int i = 0; i < m_SumInfo.FloorMassInfo.Count; i++) {
					if (m_SumInfo.maxMassInfo.MassRate < m_SumInfo.FloorMassInfo[i].MassRate) {
						m_SumInfo.maxMassInfo = m_SumInfo.FloorMassInfo[i];
					}
					if (m_SumInfo.minMassInfo.MassRate > m_SumInfo.FloorMassInfo[i].MassRate) {
						m_SumInfo.minMassInfo = m_SumInfo.FloorMassInfo[i];
					}
				}
			}

		}
		private void YjkMassInfo()
		{
			for (int i = 0; i < m_contentArray.Length; i++) {
				if (m_contentArray[i].Contains("各层质量、质心坐标")) {
					i = i + 5;
					for (int j = 0; j < m_SumInfo.FloorElemMatInfo.Count; j++) {
						List<string> dataList = new List<string>();
						int rowIndex = i + j;
						string lineData = m_contentArray[rowIndex];						
						string[] dataArray = lineData.Split(new char[]{' '},StringSplitOptions.RemoveEmptyEntries);
						dataList.Add(dataArray[0]);
						dataList.Add(dataArray[1]);
						dataList.Add(dataArray[9]);
						InfoMass massInfo = new InfoMass();
						massInfo.LoadData(dataList);
						m_SumInfo.FloorMassInfo.Add(massInfo);
					}

				}
			}

			if (m_SumInfo.FloorMassInfo.Count > 0) {
				m_SumInfo.maxMassInfo = m_SumInfo.FloorMassInfo[0];
				m_SumInfo.minMassInfo = m_SumInfo.FloorMassInfo[0];
				for (int i = 0; i < m_SumInfo.FloorMassInfo.Count; i++) {
					if (m_SumInfo.maxMassInfo.MassRate < m_SumInfo.FloorMassInfo[i].MassRate) {
						m_SumInfo.maxMassInfo = m_SumInfo.FloorMassInfo[i];
					}
					if (m_SumInfo.minMassInfo.MassRate > m_SumInfo.FloorMassInfo[i].MassRate) {
						m_SumInfo.minMassInfo = m_SumInfo.FloorMassInfo[i];
					}
				}
			}

		}

		public List<InfoRv> ParseRvInfo()
		{
			if (SourceName == PathFinder.PKPM) {
				PkpmRvInfo();
			} else if (SourceName == PathFinder.YJK) {
				YjkRvInfo();
			}   
			
			return m_SumInfo.FloorRvInfo;
		}

		private void PkpmRvInfo()
		{
			for (int i = 0; i < m_contentArray.Length; i++) {
				if (m_contentArray[i].Contains("X向刚重比")) {
					string dataVal = m_contentArray[i].Substring(m_contentArray[i].IndexOf('=') + 1).Trim();
					m_SumInfo.EGFactor_X = double.Parse(dataVal);
				} else if (m_contentArray[i].Contains("Y向刚重比")) {
					string dataVal = m_contentArray[i].Substring(m_contentArray[i].IndexOf('=') + 1).Trim();
					m_SumInfo.EGFactor_Y = double.Parse(dataVal);
                }
                else if (m_contentArray[i].Contains("层号") && m_contentArray[i].Contains("X刚重比"))
				{
                    i = i + 1;
                    string[] dataArray = m_contentArray[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    m_SumInfo.EGFactor_X = m_SumInfo.EGFactor_Y = 1000;
                    while (dataArray.Length>5)
                    {
                        double xFactor = double.Parse(dataArray[dataArray.Length - 2]);
                        double yFactor = double.Parse(dataArray[dataArray.Length - 1]);
                        if (xFactor<m_SumInfo.EGFactor_X)
                        {
                            m_SumInfo.EGFactor_X = xFactor;
                        }
                        if (yFactor<m_SumInfo.EGFactor_Y)
                        {
                            m_SumInfo.EGFactor_Y = yFactor;
                        }
                        i++;
                        dataArray = m_contentArray[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);                    
                    }
				}

				if (m_contentArray[i].Contains("楼层抗剪承载力")) {
					i = i + 8;
					for (int j = 0; j < m_SumInfo.FloorElemMatInfo.Count; j++) {
						List<string> dataList = new List<string>();
						int rowIndex = i + j;
						string lineData = m_contentArray[rowIndex];
						string[] dataArray = lineData.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
						List<string> filtedResult = new List<string>();
						for (int k = 0; k < dataArray.Length; k++) {
							if (dataArray[k] != string.Empty) {
								filtedResult.Add(dataArray[k]);
							}
						}
						dataList.Add(filtedResult[0]);
						dataList.Add(filtedResult[1]);
						dataList.Add(filtedResult[2]);
						dataList.Add(filtedResult[3]);
						dataList.Add(filtedResult[filtedResult.Count - 2].Trim());
						dataList.Add(filtedResult[filtedResult.Count - 1].Trim());

						InfoRv rvInfo = new InfoRv();
						rvInfo.LoadData(dataList);
						m_SumInfo.FloorRvInfo.Add(rvInfo);
					}
					break;
				}
			}
			if (m_SumInfo.FloorRvInfo.Count > 0) {
				m_SumInfo.minRvInfo_X = m_SumInfo.FloorRvInfo[0];
				m_SumInfo.minRvInfo_Y = m_SumInfo.FloorRvInfo[0];
				for (int i = 0; i < m_SumInfo.FloorRvInfo.Count; i++) {
					if (m_SumInfo.minRvInfo_X.Ratio_X > m_SumInfo.FloorRvInfo[i].Ratio_X) {
						m_SumInfo.minRvInfo_X = m_SumInfo.FloorRvInfo[i];
					}
					if (m_SumInfo.minRvInfo_Y.Ratio_Y > m_SumInfo.FloorRvInfo[i].Ratio_Y) {
						m_SumInfo.minRvInfo_Y = m_SumInfo.FloorRvInfo[i];
					}
				}
			} else {
				MessageBox.Show("楼层抗剪承载力数据不存在，请进行稳定性计算");
			}
		}
		private void YjkRvInfo()
		{
			for (int i = 0; i < m_contentArray.Length; i++) {
				if (m_contentArray[i].Contains("X向刚重比")) {
					string dataVal = m_contentArray[i].Substring(m_contentArray[i].IndexOf('=') + 1).Trim();
					m_SumInfo.EGFactor_X = double.Parse(dataVal);
				} else if (m_contentArray[i].Contains("Y向刚重比")) {
					string dataVal = m_contentArray[i].Substring(m_contentArray[i].IndexOf('=') + 1).Trim();
					m_SumInfo.EGFactor_Y = double.Parse(dataVal);
                }
                else if (m_contentArray[i].Contains("层号") && m_contentArray[i].Contains("X刚重比"))
                {
                    i = i + 1;
                    string[] dataArray = m_contentArray[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    m_SumInfo.EGFactor_X = m_SumInfo.EGFactor_Y = 1000;
                    while (dataArray.Length > 5)
                    {
                        double xFactor = double.Parse(dataArray[dataArray.Length - 2]);
                        double yFactor = double.Parse(dataArray[dataArray.Length - 1]);
                        if (xFactor < m_SumInfo.EGFactor_X)
                        {
                            m_SumInfo.EGFactor_X = xFactor;
                        }
                        if (yFactor < m_SumInfo.EGFactor_Y)
                        {
                            m_SumInfo.EGFactor_Y = yFactor;
                        }
                        i++;
                        dataArray = m_contentArray[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    }
                }

				if (m_contentArray[i].Contains("楼层抗剪承载力")) {
					i = i + 6;
					for (int j = 0; j < m_SumInfo.FloorElemMatInfo.Count; j++) {
						List<string> dataList = new List<string>();
						int rowIndex = i + j;
						string lineData = m_contentArray[rowIndex];
						string[] dataArray = lineData.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
						dataList.Add(dataArray[0]);
						dataList.Add(dataArray[1]);
						dataList.Add(dataArray[2]);
						dataList.Add(dataArray[3]);
						dataList.Add(dataArray[4]);
						dataList.Add(dataArray[5]);

						InfoRv rvInfo = new InfoRv();
						rvInfo.LoadData(dataList);
						m_SumInfo.FloorRvInfo.Add(rvInfo);
					}
					break;
				}
			}
			if (m_SumInfo.FloorRvInfo.Count > 0) {
				m_SumInfo.minRvInfo_X = m_SumInfo.FloorRvInfo[0];
				m_SumInfo.minRvInfo_Y = m_SumInfo.FloorRvInfo[0];
				for (int i = 0; i < m_SumInfo.FloorRvInfo.Count; i++) {
					if (m_SumInfo.minRvInfo_X.Ratio_X > m_SumInfo.FloorRvInfo[i].Ratio_X) {
						m_SumInfo.minRvInfo_X = m_SumInfo.FloorRvInfo[i];
					}
					if (m_SumInfo.minRvInfo_Y.Ratio_Y > m_SumInfo.FloorRvInfo[i].Ratio_Y) {
						m_SumInfo.minRvInfo_Y = m_SumInfo.FloorRvInfo[i];
					}
				}
			} else {
				MessageBox.Show("楼层抗剪承载力数据不存在，请进行稳定性计算");
			}
		}


        public void ParseTInfo( ParserWzq wzqParser )
        {
			m_SumInfo.VibrationT = wzqParser.ParseTInfo(m_SumInfo.FloorElemMatInfo.Count, SourceName);
            m_SumInfo.FirstTq = m_SumInfo.VibrationT[m_SumInfo.VibrationT.Count-1];
            m_SumInfo.FirstTt = m_SumInfo.VibrationT[m_SumInfo.VibrationT.Count-1];
            foreach (var item in m_SumInfo.VibrationT)
            {
                if (item.MoveFactor > 0.5 && m_SumInfo.FirstTq.T < item.T)
                {
                    m_SumInfo.FirstTq = item;
                }
                if (item.TwistFactor > 0.5 && m_SumInfo.FirstTt.T < item.T)
                {
                    m_SumInfo.FirstTt = item;
                }
            }
            m_SumInfo.EffectiveMassFactor_X = wzqParser.EffectiveMassFactor_X;
            m_SumInfo.EffectiveMassFactor_Y = wzqParser.EffectiveMassFactor_Y;
            m_SumInfo.VgList = wzqParser.VgList;
            m_SumInfo.minVg_X = wzqParser.minVg_X;
            m_SumInfo.minVg_Y = wzqParser.minVg_Y;
        }

        public void ParseDispInfo(ParserWdisp dispParser)
        {
            m_SumInfo.maxDispX = dispParser.maxX;
            m_SumInfo.maxDispX_floor = dispParser.maxX_floor;
            m_SumInfo.maxDispY = dispParser.maxY;
            m_SumInfo.maxDispY_floor = dispParser.maxY_floor;
            m_SumInfo.maxX_FloorDispAngle = dispParser.maxX_FloorDispAngle;
            m_SumInfo.maxY_FloorDispAngle = dispParser.maxY_FloorDispAngle;
            m_SumInfo.DispInfoList = dispParser.dispList;
        }
       
		
	}
}
