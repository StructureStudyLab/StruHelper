using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;

namespace Satwe_tool
{
	public partial class InfoControl : UserControl
	{
		static OpenFileDialog openFileDialog = new OpenFileDialog();
		static List<string> searchTextList;
		static List<string> deletedPath=new List<string>();
		RichTextBoxControl rtb_wmass = new RichTextBoxControl();
		RichTextBoxControl rtb_Summary = new RichTextBoxControl();
		RichTextBoxControl rtb_wzq = new RichTextBoxControl();
		RichTextBoxControl rtb_wdisp = new RichTextBoxControl();
		RichTextBoxControl rtb_wgcpj = new RichTextBoxControl();
		RichTextBoxControl rtb_wdcnl = new RichTextBoxControl();
		RichTextBoxControl rtb_satk = new RichTextBoxControl();
		RichTextBoxControl rtb_wv02q = new RichTextBoxControl();
		RichTextBoxControl rtb_satbmb = new RichTextBoxControl();
		InfoControl _otherControl;
		bool _synLeftRight;
        int _controlMarker;
        int _showType;//0 simple show 1 fullshow 2 indexShow
        bool _isShowNumber;
        int _currentSearchIndex;

		public RichTextBoxEx RtbMass
		{
			get { return rtb_wmass.TextPanel; }
		}

		public static List<string> SearchHistory
		{
			get { return searchTextList; }
			set { searchTextList = value; }
		}

		public bool SynLeftRight
		{
			get { return _synLeftRight; }
			set
			{
				_synLeftRight = value;
				rtb_wmass.TextPanel.NeedSync = value;
				rtb_Summary.TextPanel.NeedSync = value;
				rtb_wzq.TextPanel.NeedSync = value;
				rtb_wdisp.TextPanel.NeedSync = value;
				rtb_wgcpj.TextPanel.NeedSync = value;
				rtb_wdcnl.TextPanel.NeedSync = value;
				rtb_satk.TextPanel.NeedSync = value;
				rtb_wv02q.TextPanel.NeedSync = value;
				rtb_satbmb.TextPanel.NeedSync = value;
				if (value == true && this.tabControl.SelectedIndex != _otherControl.tabControl.SelectedIndex) {
					_otherControl.tabControl.SelectedIndex = this.tabControl.SelectedIndex;
				}
			}
		}

        public RichTextBoxEx CurrentRichTextBox
        {
            get
            {
                RichTextBoxControl control=tabControl.SelectedTab.Controls[0] as RichTextBoxControl;
                return control.TextPanel;
            }
        }

		static InfoControl()
		{
			openFileDialog.Filter ="Pkpm、Yjk文件|*.jws;*.JWS;*.yjk;*.YJK";
			openFileDialog.RestoreDirectory = true;			
		}

		public InfoControl(InfoControl otherControl,int marker)
		{			
			InitializeComponent();
			SetOtherInfoControl(otherControl);
            _controlMarker = marker;
			_synLeftRight = false;
            _showType = 0;
            _isShowNumber = true;
            _currentSearchIndex = 0;
            rtb_Summary.Parent = tabPage_summary;           
            rtb_Summary.Dock = DockStyle.Fill;          

			rtb_wmass.Parent = tabPage_wmass;
			rtb_wmass.Dock = DockStyle.Fill;           
			rtb_wzq.Parent = tabPage_wzq;
			rtb_wzq.Dock = DockStyle.Fill;
			rtb_wdisp.Parent = tabPage_wdisp;
			rtb_wdisp.Dock = DockStyle.Fill;
			rtb_wgcpj.Parent = tabPage_wgcpj;
			rtb_wgcpj.Dock = DockStyle.Fill;
			rtb_wdcnl.Parent = tabPage_wdcnl;
			rtb_wdcnl.Dock = DockStyle.Fill;
			rtb_satk.Parent = tabPage_stak;
			rtb_satk.Dock = DockStyle.Fill;
			rtb_wv02q.Parent = tabPage_wv02q;
			rtb_wv02q.Dock = DockStyle.Fill;
			rtb_satbmb.Parent = tabPage_satbmb;
			rtb_satbmb.Dock = DockStyle.Fill;
            InitPkpmPath();
			InitSearchHistory();
		}

        public void InitProject()
        {
            if (cb_path.Items.Count>0)
            {
                cb_path.SelectedIndex = 0;
            }
        }

        public void SavePathToConfig()
        {            
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            AppSettingsSection appSec = config.AppSettings;
            string title;
            if (_controlMarker==1)
            {
                title = "LeftPath";
            }
            else
            {
                title = "RightPath";
            }
            foreach (var item in appSec.Settings.AllKeys)
            {
                if (item.Contains(title))
                {
                    appSec.Settings.Remove(item);   
                }				    
            }
            for (int i = 0; i < cb_path.Items.Count; i++)
            {
                string key = string.Format("{0}{1}",title,i);
                appSec.Settings.Add(key,cb_path.Items[i] as string);
            }			

            config.Save(ConfigurationSaveMode.Modified);
        }

		public void SetOtherInfoControl(InfoControl otherControl)
		{
			if (otherControl != null) {
				_otherControl = otherControl;
				rtb_wmass.TextPanel.OtherRichTextBox = otherControl.rtb_wmass.TextPanel;
				rtb_Summary.TextPanel.OtherRichTextBox = otherControl.rtb_Summary.TextPanel;
				rtb_wzq.TextPanel.OtherRichTextBox = otherControl.rtb_wzq.TextPanel;
				rtb_wdisp.TextPanel.OtherRichTextBox = otherControl.rtb_wdisp.TextPanel;
				rtb_wgcpj.TextPanel.OtherRichTextBox = otherControl.rtb_wgcpj.TextPanel;
				rtb_wdcnl.TextPanel.OtherRichTextBox = otherControl.rtb_wdcnl.TextPanel;
				rtb_satk.TextPanel.OtherRichTextBox = otherControl.rtb_satk.TextPanel;
				rtb_wv02q.TextPanel.OtherRichTextBox = otherControl.rtb_wv02q.TextPanel;
				rtb_satbmb.TextPanel.OtherRichTextBox = otherControl.rtb_satbmb.TextPanel;
			}
		}
		
		
		private void bt_selectPath_Click(object sender, EventArgs e)
		{
			
			if (cb_path.Items.Count>0) {
				int index = cb_path.SelectedIndex >= 0 ? cb_path.SelectedIndex : 0;
				string fileName=cb_path.Items[index] as string;
				openFileDialog.InitialDirectory = fileName.Substring(0, fileName.LastIndexOf('\\'));
			}
			if (openFileDialog.ShowDialog() == DialogResult.OK) {
				int selectedIndex = -1;
				for (int i = 0; i < cb_path.Items.Count; i++) {
					if ((string)cb_path.Items[i] == openFileDialog.FileName) {
						selectedIndex = i;
					}
				}
				if (selectedIndex==-1) {
					selectedIndex = cb_path.Items.Add(openFileDialog.FileName);
				}
				this.cb_path.SelectedIndex = selectedIndex;
			}		    
		}

		private void cb_path_SelectedIndexChanged(object sender, EventArgs e)
		{
            LoadData();
		}

        private void LoadData()
        {
            try
            {               
				string selectPath = (string)cb_path.SelectedItem as string;
				FileInfo fileInfo = new FileInfo(selectPath);
				int lastIndex=selectPath.LastIndexOf('\\');
				string sourceName=null;
				if (fileInfo.Extension==".yjk"||fileInfo.Extension==".YJK") {
					sourceName = PathFinder.YJK;
				} else if (fileInfo.Extension==".jws"||fileInfo.Extension==".JWS") {
					sourceName = PathFinder.PKPM;
				}
				string dir = selectPath.Substring(0, lastIndex);
				if (sourceName==PathFinder.YJK) {
					dir += @"\设计结果";
				}

				string wzqFile = dir + @"\WZQ.OUT";
                ParserWzq parserWzq = new ParserWzq();
                if (parserWzq.ReadFile(wzqFile))
                {
                    this.rtb_wzq.TextPanel.Text = parserWzq.FullContent(_isShowNumber);
                }
                else
                {
                    MessageBox.Show("无法解析WZQ.OUT文件，请确认文件正确", "解析错误");
					this.rtb_wzq.TextPanel.Text = "";
                }

				string wgcpjFile = dir + @"\WGCPJ.OUT";
                ParserWgcpj parserWgcpj = new ParserWgcpj();
                if (parserWgcpj.ReadFile(wgcpjFile))
                {
                    this.rtb_wgcpj.TextPanel.Text = parserWgcpj.FullContent(_isShowNumber);
                }
                else 
                {
                    MessageBox.Show("无法解析WGCPJ.OUT文件，请确认文件正确", "解析错误");
					this.rtb_wgcpj.TextPanel.Text = "";
                }

				string wdcnlFile = dir + @"\WDCNL.OUT";
                ParserWdcnl parserWdcnl = new ParserWdcnl();
                if (parserWdcnl.ReadFile(wdcnlFile))
                {
                    this.rtb_wdcnl.TextPanel.Text = parserWdcnl.FullContent(_isShowNumber);
                }
                else
                {
                    MessageBox.Show("无法解析WDCNL.OUT文件，请确认文件正确", "解析错误");
					this.rtb_wdcnl.TextPanel.Text = "";
                }


				string wv02qFile = dir + @"\WV02Q.OUT";
                ParserWv02q parserWv02q = new ParserWv02q();
                if (parserWv02q.ReadFile(wv02qFile))
                {
                    this.rtb_wv02q.TextPanel.Text = parserWv02q.FullContent(_isShowNumber);
                }
                else 
                {
                    MessageBox.Show("无法解析WV02Q.OUT文件，请确认文件正确", "解析错误");
					this.rtb_wv02q.TextPanel.Text = "";
                }

				string satkFile = dir + @"\SAT-K.OUT";
                ParserSatk parserSatk = new ParserSatk();
                if (parserSatk.ReadFile(satkFile))
                {
                    this.rtb_satk.TextPanel.Text = parserSatk.FullContent(_isShowNumber);
                }


				string satbmbFile = dir + @"\SATBMB.OUT";
                ParserSatbmb parserSatbmb = new ParserSatbmb();
                if (parserSatbmb.ReadFile(satbmbFile))
                {
                    this.rtb_satbmb.TextPanel.Text = parserSatbmb.FullContent(_isShowNumber);
                }

				string wdispFile = dir + @"\WDISP.OUT";
				ParserWdisp parserWdisp = new ParserWdisp();
				if (parserWdisp.ReadFile(wdispFile)) {
					parserWdisp.ParseDisp();
					this.rtb_wdisp.TextPanel.Text = parserWdisp.FullContent(_isShowNumber);
				} else{
					MessageBox.Show("无法解析WDISP.OUT文件，请确认文件正确", "解析错误");
					this.rtb_wdisp.TextPanel.Text="";
				}

				string wmassFile = dir + @"\WMASS.OUT";
                ParserWmass parserWmass;
                if (_controlMarker == 1)
                {
                    parserWmass = ParserWmass.InstanceLeft;
                }
                else
                {
                    parserWmass = ParserWmass.InstanceRight;
                }
				parserWmass.SourceName = sourceName;
                if (parserWmass.ReadFile(wmassFile))
                {
                    parserWmass.ParseTInfo(parserWzq);
                    parserWmass.ParseDispInfo(parserWdisp);
                    this.rtb_Summary.TextPanel.Text = parserWmass.m_SumInfo.GetData(_showType);
                    this.rtb_wmass.TextPanel.Text = parserWmass.FullContent(_isShowNumber);
                    
                }
                else
                {
                    MessageBox.Show("无法解析WMASS.OUT文件，请确认文件正确", "解析错误");
					this.rtb_wmass.TextPanel.Text = "";
                }
                formatContent();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void formatContent()
        {
            RichTextBoxEx rtb = this.rtb_Summary.TextPanel;
            KeyFactorIndexer indexer=new KeyFactorIndexer();
            foreach (string item in indexer.KeyFactors.Values)
            {
                int index=rtb.Find(item,RichTextBoxFinds.NoHighlight);
                if (index>=0)
                {
                    rtb.Select(index, item.Length);
                    rtb.SelectionColor = Color.Green;
                }
            }
           
            rtb.DeselectAll();
        }

		private void cb_path_Leave(object sender, EventArgs e)
		{
            if (cb_path.Text==string.Empty)
            {
                return;
            }
			int index = cb_path.Items.IndexOf(cb_path.Text);
			if (index < 0) {
				index = cb_path.Items.Add(cb_path.Text);
			}
			cb_path.SelectedIndex = index;
		}

		private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
		{
            _currentSearchIndex = 0;
			if (_otherControl!=null&&_synLeftRight) {
				_otherControl.tabControl.SelectedIndex = this.tabControl.SelectedIndex;
			}
			if (this.tabControl.SelectedIndex==0) {
				tsb_fullSimpleShow.Enabled = true;
				tsb_showHideNumber.Enabled = false;
			} else {
				tsb_fullSimpleShow.Enabled = false;
				tsb_showHideNumber.Enabled = true;
			}
		}

        private void InitPkpmPath()
        {
			if (_controlMarker==1) {
				List<string> pkpmPaths = PathFinder.GetSatwePath();
				for (int i = 0; i < pkpmPaths.Count; i++) {
					cb_path.Items.Add(pkpmPaths[i]);
				}
			} else if (_controlMarker==2){
				List<string> yjkPaths = PathFinder.GetYjkPath();
				for (int i = 0; i < yjkPaths.Count; i++) {
					cb_path.Items.Add(yjkPaths[i]);
				}
			}
            
        }

		private void InitSearchHistory()
		{
			if (searchTextList!=null) {
				for (int i = 0; i < searchTextList.Count; i++) {
					tsb_searchText.Items.Add(searchTextList[i]);
				}
			}			
		}
       

        private void tsb_fullSimpleShow_Click(object sender, EventArgs e)
        {
            if (tsb_fullSimpleShow.Text == "详细显示")
            {
                tsb_fullSimpleShow.Text = "简略显示";
                _showType = 1;
            }
            else
            {
                tsb_fullSimpleShow.Text = "详细显示";
                _showType = 0;
            }
            LoadData();
        }
        private void tsb_explain_Click(object sender, EventArgs e)
        {
            if (_showType==2)
            {
                _showType = 0;
                tsb_fullSimpleShow.Text = "详细显示";
            }
            else
            {
                _showType = 2;
            }            
            LoadData();
        }


        private void tsb_showHideNumber_Click(object sender, EventArgs e)
        {
            if (_isShowNumber)
            {
                tsb_showHideNumber.Text = "显示行号";
                _isShowNumber = false;
            }
            else
            {
                tsb_showHideNumber.Text = "隐藏行号";
                _isShowNumber = true;
            }
            LoadData();
        }

        private void tabPage_summary_Enter(object sender, EventArgs e)
        {
            tsb_fullSimpleShow.Enabled = true;
            tsb_showHideNumber.Enabled = false;
        }

        private void tabPage_summary_Leave(object sender, EventArgs e)
        {
            tsb_fullSimpleShow.Enabled = false;
            tsb_showHideNumber.Enabled = true;
        }

        private void tsb_searchDown_Click(object sender, EventArgs e)
        {
            if (tsb_searchText.Text==string.Empty)
            {
                return;
            }
            string Findstring = tsb_searchText.Text;
			if (!searchTextList.Contains(Findstring)) {
				searchTextList.Insert(0, Findstring);
			}
            RichTextBoxControl rtb=tabControl.SelectedTab.Controls[0] as RichTextBoxControl;
            if (rtb!=null)
            {
                _currentSearchIndex = rtb.TextPanel.Find(tsb_searchText.Text, _currentSearchIndex, RichTextBoxFinds.None);
                if (_currentSearchIndex == -1)
                {
                    MessageBox.Show("搜索完毕，再无法找到：" + "\"" + Findstring + "\"","搜索完毕");
                    _currentSearchIndex = 0;
                }
                else
                {
                    rtb.Focus();
                    _currentSearchIndex += Findstring.Length;
                }
            }
        }

        private void tsb_searchUp_Click(object sender, EventArgs e)
        {
            if (tsb_searchText.Text==string.Empty)
            {
                return;
            }
            string Findstring = tsb_searchText.Text;
			if (!searchTextList.Contains(Findstring)) {
				searchTextList.Insert(0, Findstring);
			}
            RichTextBoxControl rtb = tabControl.SelectedTab.Controls[0] as RichTextBoxControl;
            if (rtb != null)
            {
                if (_currentSearchIndex==0)
                {
                    _currentSearchIndex = rtb.TextPanel.Text.Length;
                }
                _currentSearchIndex = rtb.TextPanel.Find(tsb_searchText.Text,1,_currentSearchIndex, RichTextBoxFinds.Reverse);
                if (_currentSearchIndex <=0)
                {
                    MessageBox.Show("无法解析" + "\"" + Findstring + "\"","搜索完毕");
                    _currentSearchIndex = rtb.TextPanel.Text.Length;
                }
                else
                {
                    rtb.Focus();
                    _currentSearchIndex -= Findstring.Length;
                }
               
            }
        }

		private void tsb_searchText_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar=='\r') {
				tsb_searchDown_Click(sender, e);
			}
		}

		private void tsb_searchText_MouseEnter(object sender, EventArgs e)
		{
			for (int i = 0; i < searchTextList.Count; i++) {
				if (!tsb_searchText.Items.Contains(searchTextList[i])) {
					tsb_searchText.Items.Insert(0, searchTextList[i]);
				}
			}
		}

		private void tsmi_delete_Click(object sender, EventArgs e)
		{
			if (cb_path.SelectedIndex >= 0) {
				deletedPath.Add(cb_path.SelectedItem as string);
				cb_path.Items.RemoveAt(cb_path.SelectedIndex);
				cb_path.SelectedIndex = 0;
			}
		}

        
		
		

	}
}
