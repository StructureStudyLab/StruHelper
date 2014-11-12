using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Microsoft.Office.Tools.Excel;
using ExcelApp=Microsoft.Office.Interop.Excel.Application;
using Microsoft.Office.Interop.Excel;
using System.IO;
using System.Configuration;


namespace Satwe_tool
{
    public partial class MainForm : Form
    {
        InfoControl left;
        InfoControl right;
        public MainForm()
        {
            InitializeComponent();
			InitSearchTextList();
            left = new InfoControl(null, 1);
            right = new InfoControl(left, 2);
            left.SetOtherInfoControl(right);
            left.Parent = this.splitContainer1.Panel1;
            right.Parent = this.splitContainer1.Panel2;
            left.Dock = DockStyle.Fill;
            right.Dock = DockStyle.Fill;
            InitIndexTree();
            InitProject();
        }
		private void InitSearchTextList()
		{
			if (InfoControl.SearchHistory == null) {
				InfoControl.SearchHistory = new List<string>();
				for (int i = 0; i < 20; i++) {
					string key = string.Format("searchHistory{0}", i);
					string path = ConfigurationManager.AppSettings[key];
					if (path != null) {
						InfoControl.SearchHistory.Add(path);
					} else {
						break;
					}
				}
			}
		}

        private void cb_LRSync_CheckedChanged(object sender, EventArgs e)
        {
            right.SynLeftRight = cb_LRSync.Checked;
            left.SynLeftRight = cb_LRSync.Checked;
			this.splitContainer1.Panel2Collapsed = !cb_LRSync.Checked;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ThanksForm form = new ThanksForm();
            form.ShowDialog();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            HelpForm form = new HelpForm();
            form.ShowDialog();
        }

        private void InitIndexTree()
        {
            KeyFactorIndexer keyFactorIndexer = new KeyFactorIndexer();
            Dictionary<string, string> keyFactors = keyFactorIndexer.KeyFactors;
            TreeNode compareNode = new TreeNode("重要比值控制");
            foreach (var item in keyFactors)
            {
                TreeNode node = new TreeNode(item.Key);
                compareNode.Nodes.Add(node);
                node.Tag = item.Value;
            }

            compareNode.ExpandAll();
            tv_keyFactors.Nodes.Add(compareNode);
        }


        private void keyFactorsClicked(TreeNode node)
        {
            if (node.Tag == null)
            {
                return;
            }
            int index = left.CurrentRichTextBox.Find(node.Tag as string);
            if (index >= 0)
            {
                int line = left.CurrentRichTextBox.GetLineFromCharIndex(index);
                int firstIndex = left.CurrentRichTextBox.GetFirstCharIndexFromLine(line);
                int countOfString = left.CurrentRichTextBox.Lines[line].Length;
                left.CurrentRichTextBox.Select(firstIndex, countOfString);
                left.CurrentRichTextBox.ScrollToCaret();
                left.CurrentRichTextBox.Focus();
            }
            if (this.cb_LRSync.Checked)
            {
                index = right.CurrentRichTextBox.Find(node.Tag as string);
                if (index >= 0)
                {
                    int line = right.CurrentRichTextBox.GetLineFromCharIndex(index);
                    int firstIndex = right.CurrentRichTextBox.GetFirstCharIndexFromLine(line);
                    right.CurrentRichTextBox.Select(firstIndex, right.CurrentRichTextBox.Lines[line].Length);
                    right.CurrentRichTextBox.ScrollToCaret();
                    right.CurrentRichTextBox.Focus();
                }
            }

        }

        private void tv_keyFactors_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            keyFactorsClicked(e.Node);
        }

        private void tv_keyFactors_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            keyFactorsClicked(e.Node);
        }

        private void InitProject()
        {
            left.InitProject();			
        }

        private void tsb_exportExcel_Click(object sender, EventArgs e)
        {
            if (ParserWmass.InstanceLeft.m_SumInfo==null)
            {
                MessageBox.Show("左边工程为空，请指定左边工程");
                return;
            }

            if (ParserWmass.InstanceRight.m_SumInfo == null&&this.cb_LRSync.Checked)
            {
                MessageBox.Show("右边工程为空，请指定右边工程");
                return;
            }
			ExcelApp app=null;
			Workbooks wbs=null;
			try {
				app = new ExcelApp();
				wbs = app.Workbooks;
			} catch (Exception ex) {
				string mes=ex.Message;
				MessageBox.Show("1. 您电脑上可能没有正确安装Excel软件，或者没有激活\n2. 目前有些盗版Office不稳定，各种问题，建议换一个版本的Office\n3.如果是Win7，请尝试右键小猪手，以管理员身份运行小猪手", "错误提示");
				return;
			}

            try
            {
                string curDir = Directory.GetCurrentDirectory();
                string templateFile = curDir + @"\Template.xls";
                if (!File.Exists(templateFile))
                {
					MessageBox.Show("模板文件丢失，请联系QQ:19458533");
                    return;
                }
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel 文件（*.xls）|*.xls";
                sfd.FilterIndex = 1;
                sfd.FileName = "指标分析";
                sfd.RestoreDirectory = true;
                AGAIN:
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string localFilePath = sfd.FileName.ToString(); //获得文件路径 
                    string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径
                    if (templateFile==localFilePath)
                    {
                        MessageBox.Show("不能覆盖模板文件，请重新指定文件名");
                        goto AGAIN;
                    }
                    File.Copy(templateFile, localFilePath,true);                           
                    Workbook wb=wbs.Open(localFilePath);
                    app.Visible = true;                    
                    //////////////////////////////////////////////////////////////////////////
                    //受剪承载力
                    //////////////////////////////////////////////////////////////////////////
                    Worksheet sheet = (Worksheet)wb.Sheets["Data-承载力"];
                    if (sheet!=null)
                    {                       
                        List<InfoRv> rvInfoLeft= new List<InfoRv>(ParserWmass.InstanceLeft.m_SumInfo.FloorRvInfo);
                        rvInfoLeft.Reverse();                       
                        object[,] val = new object[rvInfoLeft.Count,2];
                        for (int i = 0; i < rvInfoLeft.Count; i++)
                        {
                            val[i, 0] = rvInfoLeft[i].Ratio_X;
                            val[i, 1] = rvInfoLeft[i].Ratio_Y;
                        }
                        sheet.Range["B2:C200", System.Type.Missing].Value = val;
                        sheet.Range["B1", Type.Missing].Value = "抗剪承载力" + ParserWmass.InstanceLeft.SourceName + "-X(左)";
                        sheet.Range["C1", Type.Missing].Value = "抗剪承载力" + ParserWmass.InstanceLeft.SourceName + "-Y(左)";

						List<InfoRv> maxFoorRv = rvInfoLeft;
						if (this.cb_LRSync.Checked) {

							List<InfoRv> rvInfoRight = new List<InfoRv>(ParserWmass.InstanceRight.m_SumInfo.FloorRvInfo);
							rvInfoRight.Reverse();
							val = new object[rvInfoRight.Count, 2];
							for (int i = 0; i < rvInfoRight.Count; i++) {
								val[i, 0] = rvInfoRight[i].Ratio_X;
								val[i, 1] = rvInfoRight[i].Ratio_Y;
							}
							sheet.Range["D2:E200", System.Type.Missing].Value = val;
							sheet.Range["D1", Type.Missing].Value = "抗剪承载力" + ParserWmass.InstanceRight.SourceName + "-X(右)";
							sheet.Range["E1", Type.Missing].Value = "抗剪承载力" + ParserWmass.InstanceRight.SourceName + "-Y(右)";
							maxFoorRv = rvInfoLeft.Count > rvInfoRight.Count ? rvInfoLeft : rvInfoRight;
						}

						object[,] floorArray = new object[maxFoorRv.Count, 1];
						object[,] regularArray = new object[maxFoorRv.Count, 1];
						double regularVal = 0.8;
						for (int i = 0; i < maxFoorRv.Count; i++) {
							floorArray[i, 0] = maxFoorRv[i].FoorNo;
							regularArray[i, 0] = regularVal;
						}
						sheet.Range["A2:A200", Type.Missing].Value = floorArray;
						sheet.Range["F2:F200", Type.Missing].Value = regularArray;
						
                    }

                    //////////////////////////////////////////////////////////////////////////
                    //楼层剪力
                    //////////////////////////////////////////////////////////////////////////
                    sheet = (Worksheet)wb.Sheets["Data-剪力"];
                    if (sheet != null)
                    {
                        List<InfoRv> rvInfoLeft = new List<InfoRv>(ParserWmass.InstanceLeft.m_SumInfo.FloorRvInfo);
                        rvInfoLeft.Reverse();
                        object[,] val = new object[rvInfoLeft.Count, 2];
                        for (int i = 0; i < rvInfoLeft.Count; i++)
                        {
                            val[i, 0] = rvInfoLeft[i].Rv_X;
                            val[i, 1] = rvInfoLeft[i].Rv_Y;
                        }
                        sheet.Range["B2:C200", System.Type.Missing].Value = val;
                        sheet.Range["B1", Type.Missing].Value = "楼层剪力" + ParserWmass.InstanceLeft.SourceName + "-X(左)";
                        sheet.Range["C1", Type.Missing].Value = "楼层剪力" + ParserWmass.InstanceLeft.SourceName + "-Y(左)";

						List<InfoRv> maxFoorRv=rvInfoLeft;
						if (this.cb_LRSync.Checked) {
							List<InfoRv> rvInfoRight = new List<InfoRv>(ParserWmass.InstanceRight.m_SumInfo.FloorRvInfo);
							rvInfoRight.Reverse();
							val = new object[rvInfoRight.Count, 2];
							for (int i = 0; i < rvInfoRight.Count; i++) {
								val[i, 0] = rvInfoRight[i].Rv_X;
								val[i, 1] = rvInfoRight[i].Rv_Y;
							}
							sheet.Range["D2:E200", System.Type.Missing].Value = val;
							sheet.Range["D1", Type.Missing].Value = "楼层剪力" + ParserWmass.InstanceRight.SourceName + "-X(右)";
							sheet.Range["E1", Type.Missing].Value = "楼层剪力" + ParserWmass.InstanceRight.SourceName + "-Y(右)";
							maxFoorRv = rvInfoLeft.Count > rvInfoRight.Count ? rvInfoLeft : rvInfoRight;
						}
                        
                        object[,] floorArray = new object[maxFoorRv.Count, 1]; 
                        for (int i = 0; i < maxFoorRv.Count; i++)
                        {
                            floorArray[i, 0] = maxFoorRv[i].FoorNo;
                        }
                        sheet.Range["A2:A200", Type.Missing].Value = floorArray;
                    }

                    //////////////////////////////////////////////////////////////////////////
                    //剪重比
                    //////////////////////////////////////////////////////////////////////////
                    sheet = (Worksheet)wb.Sheets["Data-剪重比"];
                    if (sheet != null)
                    {
                        List<InfoVG> vgInfoLeft = new List<InfoVG>(ParserWmass.InstanceLeft.m_SumInfo.VgList);
                        vgInfoLeft.Reverse();
                        object[,] val = new object[vgInfoLeft.Count, 2];
                        for (int i = 0; i < vgInfoLeft.Count; i++)
                        {
                            val[i, 0] = vgInfoLeft[i].Vx_Ratio;
							val[i, 1] = vgInfoLeft[i].Vy_Ratio;
                        }
                        sheet.Range["B2:C200", System.Type.Missing].Value = val;
                        sheet.Range["B1", Type.Missing].Value = "剪重比" + ParserWmass.InstanceLeft.SourceName + "-X(左)";
                        sheet.Range["C1", Type.Missing].Value = "剪重比" + ParserWmass.InstanceLeft.SourceName + "-Y(左)";

						List<InfoVG> maxFoorVg = vgInfoLeft;
						if (this.cb_LRSync.Checked) {
							List<InfoVG> vgInfoRight = new List<InfoVG>(ParserWmass.InstanceRight.m_SumInfo.VgList);
							vgInfoRight.Reverse();
							val = new object[vgInfoRight.Count, 2];
							for (int i = 0; i < vgInfoRight.Count; i++) {
								val[i, 0] = vgInfoRight[i].Vx_Ratio;
								val[i, 1] = vgInfoRight[i].Vy_Ratio;
							}
							sheet.Range["D2:E200", System.Type.Missing].Value = val;
							sheet.Range["D1", Type.Missing].Value = "剪重比" + ParserWmass.InstanceRight.SourceName + "-X(右)";
							sheet.Range["E1", Type.Missing].Value = "剪重比" + ParserWmass.InstanceRight.SourceName + "-Y(右)";
							maxFoorVg = vgInfoLeft.Count > vgInfoRight.Count ? vgInfoLeft : vgInfoRight;
						}
                       
                        object[,] floorArray = new object[maxFoorVg.Count, 1];
                        object[,] regularArray = new object[maxFoorVg.Count, 2];
                        double regularValX = 1.4;
                        double regularValY = 1.5;
                        for (int i = 0; i < maxFoorVg.Count; i++)
                        {
                            floorArray[i, 0] = maxFoorVg[i].FoorNo;
                            regularArray[i, 0] = regularValX;
                            regularArray[i, 1] = regularValY;
                        }
                        sheet.Range["A2:A200", Type.Missing].Value = floorArray;
                        sheet.Range["F2:G200", Type.Missing].Value = regularArray;
                    }

                    //////////////////////////////////////////////////////////////////////////
                    //侧刚比
                    //////////////////////////////////////////////////////////////////////////
                    sheet = (Worksheet)wb.Sheets["Data-侧刚比"];
                    if (sheet != null)
                    {
                        List<InfoEi> eiInfoLeft = new List<InfoEi>(ParserWmass.InstanceLeft.m_SumInfo.FloorEiInfo);                       
                        object[,] val = new object[eiInfoLeft.Count, 2];
                        for (int i = 0; i < eiInfoLeft.Count; i++)
                        {
                            val[i, 0] = eiInfoLeft[i].Ratx2;
                            val[i, 1] = eiInfoLeft[i].Raty2;
                        }
                        sheet.Range["B2:C200", System.Type.Missing].Value = val;
                        sheet.Range["B1", Type.Missing].Value = "侧刚比" + ParserWmass.InstanceLeft.SourceName + "-X(左)";
                        sheet.Range["C1", Type.Missing].Value = "侧刚比" + ParserWmass.InstanceLeft.SourceName + "-Y(左)";

						List<InfoEi> maxFoorEi = eiInfoLeft;
						if (this.cb_LRSync.Checked) {
							List<InfoEi> eiInfoRight = new List<InfoEi>(ParserWmass.InstanceRight.m_SumInfo.FloorEiInfo);
							val = new object[eiInfoRight.Count, 2];
							for (int i = 0; i < eiInfoRight.Count; i++) {
								val[i, 0] = eiInfoRight[i].Ratx2;
								val[i, 1] = eiInfoRight[i].Raty2;
							}
							sheet.Range["D2:E200", System.Type.Missing].Value = val;
							sheet.Range["D1", Type.Missing].Value = "侧刚比" + ParserWmass.InstanceRight.SourceName + "-X(右)";
							sheet.Range["E1", Type.Missing].Value = "侧刚比" + ParserWmass.InstanceRight.SourceName + "-Y(右)";
							maxFoorEi = eiInfoLeft.Count > eiInfoRight.Count ? eiInfoLeft : eiInfoRight;
						}
                        
                        object[,] floorArray = new object[maxFoorEi.Count, 1];
                        object[,] regularArray = new object[maxFoorEi.Count, 3];
                        double regularVal1 = 0.9;
                        double regularVal2 = 1.1;
                        double regularVal3 = 1.5;
                        for (int i = 0; i < maxFoorEi.Count; i++)
                        {
                            floorArray[i, 0] = maxFoorEi[i].FoorNo;
                            regularArray[i, 0] = regularVal1;
                            regularArray[i, 1] = regularVal2;
                            regularArray[i, 2] = regularVal3;
                        }
                        sheet.Range["A2:A200", Type.Missing].Value = floorArray;
                        sheet.Range["F2:H200", Type.Missing].Value = regularArray;
                    }
                }
            }
            catch (Exception exc)
            {
                string msg=exc.Message;
                app.Quit();
                MessageBox.Show("打开文件失败，请确认文件没被使用",msg);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            left.SavePathToConfig();
            right.SavePathToConfig();
			SaveSearchHistory();
        }
		public void SaveSearchHistory()
		{
			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			AppSettingsSection appSec = config.AppSettings;			
			foreach (var item in appSec.Settings.AllKeys) {				
				if (item.Contains("searchHistory")) {
					appSec.Settings.Remove(item);
				}
			}			
			for (int i = 0; i <InfoControl.SearchHistory.Count && i < 20; i++) {
				string key = string.Format("searchHistory{0}", i);
				appSec.Settings.Add(key, InfoControl.SearchHistory[i]);
			}
			config.Save(ConfigurationSaveMode.Modified);
		}
    }
}
