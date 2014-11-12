namespace Satwe_tool
{
	partial class InfoControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoControl));
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPage_summary = new System.Windows.Forms.TabPage();
			this.tabPage_wmass = new System.Windows.Forms.TabPage();
			this.tabPage_wzq = new System.Windows.Forms.TabPage();
			this.tabPage_wdisp = new System.Windows.Forms.TabPage();
			this.tabPage_wgcpj = new System.Windows.Forms.TabPage();
			this.tabPage_wdcnl = new System.Windows.Forms.TabPage();
			this.tabPage_wv02q = new System.Windows.Forms.TabPage();
			this.tabPage_stak = new System.Windows.Forms.TabPage();
			this.tabPage_satbmb = new System.Windows.Forms.TabPage();
			this.cb_path = new System.Windows.Forms.ComboBox();
			this.cms_deletPath = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmi_delete = new System.Windows.Forms.ToolStripMenuItem();
			this.bt_selectPath = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.tsb_searchText = new System.Windows.Forms.ToolStripComboBox();
			this.tsb_searchDown = new System.Windows.Forms.ToolStripButton();
			this.tsb_searchUp = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsb_fullSimpleShow = new System.Windows.Forms.ToolStripButton();
			this.tsb_explain = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsb_showHideNumber = new System.Windows.Forms.ToolStripButton();
			this.tabControl.SuspendLayout();
			this.cms_deletPath.SuspendLayout();
			this.panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPage_summary);
			this.tabControl.Controls.Add(this.tabPage_wmass);
			this.tabControl.Controls.Add(this.tabPage_wzq);
			this.tabControl.Controls.Add(this.tabPage_wdisp);
			this.tabControl.Controls.Add(this.tabPage_wgcpj);
			this.tabControl.Controls.Add(this.tabPage_wdcnl);
			this.tabControl.Controls.Add(this.tabPage_wv02q);
			this.tabControl.Controls.Add(this.tabPage_stak);
			this.tabControl.Controls.Add(this.tabPage_satbmb);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.Location = new System.Drawing.Point(0, 25);
			this.tabControl.Multiline = true;
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(502, 371);
			this.tabControl.TabIndex = 0;
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
			// 
			// tabPage_summary
			// 
			this.tabPage_summary.Location = new System.Drawing.Point(4, 22);
			this.tabPage_summary.Name = "tabPage_summary";
			this.tabPage_summary.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage_summary.Size = new System.Drawing.Size(494, 345);
			this.tabPage_summary.TabIndex = 0;
			this.tabPage_summary.Text = "指标信息";
			this.tabPage_summary.ToolTipText = "结构、抗震、6个比等关键信息";
			this.tabPage_summary.UseVisualStyleBackColor = true;
			// 
			// tabPage_wmass
			// 
			this.tabPage_wmass.Location = new System.Drawing.Point(4, 22);
			this.tabPage_wmass.Name = "tabPage_wmass";
			this.tabPage_wmass.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage_wmass.Size = new System.Drawing.Size(494, 349);
			this.tabPage_wmass.TabIndex = 1;
			this.tabPage_wmass.Text = "WMASS";
			this.tabPage_wmass.UseVisualStyleBackColor = true;
			// 
			// tabPage_wzq
			// 
			this.tabPage_wzq.Location = new System.Drawing.Point(4, 22);
			this.tabPage_wzq.Name = "tabPage_wzq";
			this.tabPage_wzq.Size = new System.Drawing.Size(494, 349);
			this.tabPage_wzq.TabIndex = 2;
			this.tabPage_wzq.Text = "WZQ";
			this.tabPage_wzq.UseVisualStyleBackColor = true;
			// 
			// tabPage_wdisp
			// 
			this.tabPage_wdisp.Location = new System.Drawing.Point(4, 22);
			this.tabPage_wdisp.Name = "tabPage_wdisp";
			this.tabPage_wdisp.Size = new System.Drawing.Size(494, 349);
			this.tabPage_wdisp.TabIndex = 3;
			this.tabPage_wdisp.Text = "WDISP";
			this.tabPage_wdisp.UseVisualStyleBackColor = true;
			// 
			// tabPage_wgcpj
			// 
			this.tabPage_wgcpj.Location = new System.Drawing.Point(4, 22);
			this.tabPage_wgcpj.Name = "tabPage_wgcpj";
			this.tabPage_wgcpj.Size = new System.Drawing.Size(494, 349);
			this.tabPage_wgcpj.TabIndex = 4;
			this.tabPage_wgcpj.Text = "WGCPJ";
			this.tabPage_wgcpj.UseVisualStyleBackColor = true;
			// 
			// tabPage_wdcnl
			// 
			this.tabPage_wdcnl.Location = new System.Drawing.Point(4, 22);
			this.tabPage_wdcnl.Name = "tabPage_wdcnl";
			this.tabPage_wdcnl.Size = new System.Drawing.Size(494, 349);
			this.tabPage_wdcnl.TabIndex = 5;
			this.tabPage_wdcnl.Text = "WDCNL";
			this.tabPage_wdcnl.UseVisualStyleBackColor = true;
			// 
			// tabPage_wv02q
			// 
			this.tabPage_wv02q.Location = new System.Drawing.Point(4, 22);
			this.tabPage_wv02q.Name = "tabPage_wv02q";
			this.tabPage_wv02q.Size = new System.Drawing.Size(494, 349);
			this.tabPage_wv02q.TabIndex = 7;
			this.tabPage_wv02q.Text = "WV02Q";
			this.tabPage_wv02q.UseVisualStyleBackColor = true;
			// 
			// tabPage_stak
			// 
			this.tabPage_stak.Location = new System.Drawing.Point(4, 22);
			this.tabPage_stak.Name = "tabPage_stak";
			this.tabPage_stak.Size = new System.Drawing.Size(494, 349);
			this.tabPage_stak.TabIndex = 6;
			this.tabPage_stak.Text = "SAT-K";
			this.tabPage_stak.UseVisualStyleBackColor = true;
			// 
			// tabPage_satbmb
			// 
			this.tabPage_satbmb.Location = new System.Drawing.Point(4, 22);
			this.tabPage_satbmb.Name = "tabPage_satbmb";
			this.tabPage_satbmb.Size = new System.Drawing.Size(494, 349);
			this.tabPage_satbmb.TabIndex = 8;
			this.tabPage_satbmb.Text = "SATBMB";
			this.tabPage_satbmb.UseVisualStyleBackColor = true;
			// 
			// cb_path
			// 
			this.cb_path.ContextMenuStrip = this.cms_deletPath;
			this.cb_path.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cb_path.FormattingEnabled = true;
			this.cb_path.Location = new System.Drawing.Point(0, 0);
			this.cb_path.MaxDropDownItems = 10;
			this.cb_path.Name = "cb_path";
			this.cb_path.Size = new System.Drawing.Size(435, 20);
			this.cb_path.TabIndex = 1;
			this.cb_path.SelectedIndexChanged += new System.EventHandler(this.cb_path_SelectedIndexChanged);
			this.cb_path.Leave += new System.EventHandler(this.cb_path_Leave);
			// 
			// cms_deletPath
			// 
			this.cms_deletPath.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_delete});
			this.cms_deletPath.Name = "cms_deletPath";
			this.cms_deletPath.Size = new System.Drawing.Size(101, 26);
			this.cms_deletPath.Text = "删除";
			// 
			// tsmi_delete
			// 
			this.tsmi_delete.Name = "tsmi_delete";
			this.tsmi_delete.Size = new System.Drawing.Size(100, 22);
			this.tsmi_delete.Text = "删除";
			this.tsmi_delete.Click += new System.EventHandler(this.tsmi_delete_Click);
			// 
			// bt_selectPath
			// 
			this.bt_selectPath.Dock = System.Windows.Forms.DockStyle.Right;
			this.bt_selectPath.Location = new System.Drawing.Point(435, 0);
			this.bt_selectPath.Name = "bt_selectPath";
			this.bt_selectPath.Size = new System.Drawing.Size(67, 20);
			this.bt_selectPath.TabIndex = 2;
			this.bt_selectPath.Text = "选择项目";
			this.bt_selectPath.UseVisualStyleBackColor = true;
			this.bt_selectPath.Click += new System.EventHandler(this.bt_selectPath_Click);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.cb_path);
			this.panel1.Controls.Add(this.bt_selectPath);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(502, 20);
			this.panel1.TabIndex = 3;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.tabControl);
			this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
			this.splitContainer1.Size = new System.Drawing.Size(502, 425);
			this.splitContainer1.SplitterDistance = 25;
			this.splitContainer1.TabIndex = 4;
			// 
			// toolStrip1
			// 
			this.toolStrip1.CanOverflow = false;
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.tsb_searchText,
            this.tsb_searchDown,
            this.tsb_searchUp,
            this.toolStripSeparator2,
            this.tsb_fullSimpleShow,
            this.tsb_explain,
            this.toolStripSeparator3,
            this.tsb_showHideNumber});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(502, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(35, 22);
			this.toolStripLabel1.Text = "搜索:";
			// 
			// tsb_searchText
			// 
			this.tsb_searchText.AutoSize = false;
			this.tsb_searchText.AutoToolTip = true;
			this.tsb_searchText.Name = "tsb_searchText";
			this.tsb_searchText.Size = new System.Drawing.Size(150, 25);
			this.tsb_searchText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tsb_searchText_KeyPress);
			this.tsb_searchText.MouseEnter += new System.EventHandler(this.tsb_searchText_MouseEnter);
			// 
			// tsb_searchDown
			// 
			this.tsb_searchDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsb_searchDown.Image = ((System.Drawing.Image)(resources.GetObject("tsb_searchDown.Image")));
			this.tsb_searchDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsb_searchDown.Name = "tsb_searchDown";
			this.tsb_searchDown.Size = new System.Drawing.Size(36, 22);
			this.tsb_searchDown.Text = "向下";
			this.tsb_searchDown.Click += new System.EventHandler(this.tsb_searchDown_Click);
			// 
			// tsb_searchUp
			// 
			this.tsb_searchUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsb_searchUp.Image = ((System.Drawing.Image)(resources.GetObject("tsb_searchUp.Image")));
			this.tsb_searchUp.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsb_searchUp.Name = "tsb_searchUp";
			this.tsb_searchUp.Size = new System.Drawing.Size(36, 22);
			this.tsb_searchUp.Text = "向上";
			this.tsb_searchUp.Click += new System.EventHandler(this.tsb_searchUp_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tsb_fullSimpleShow
			// 
			this.tsb_fullSimpleShow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsb_fullSimpleShow.Image = ((System.Drawing.Image)(resources.GetObject("tsb_fullSimpleShow.Image")));
			this.tsb_fullSimpleShow.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsb_fullSimpleShow.Name = "tsb_fullSimpleShow";
			this.tsb_fullSimpleShow.Size = new System.Drawing.Size(60, 22);
			this.tsb_fullSimpleShow.Text = "详细显示";
			this.tsb_fullSimpleShow.Click += new System.EventHandler(this.tsb_fullSimpleShow_Click);
			// 
			// tsb_explain
			// 
			this.tsb_explain.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsb_explain.Image = ((System.Drawing.Image)(resources.GetObject("tsb_explain.Image")));
			this.tsb_explain.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsb_explain.Name = "tsb_explain";
			this.tsb_explain.Size = new System.Drawing.Size(96, 22);
			this.tsb_explain.Text = "指标说明及调整";
			this.tsb_explain.Click += new System.EventHandler(this.tsb_explain_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// tsb_showHideNumber
			// 
			this.tsb_showHideNumber.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsb_showHideNumber.Enabled = false;
			this.tsb_showHideNumber.Image = ((System.Drawing.Image)(resources.GetObject("tsb_showHideNumber.Image")));
			this.tsb_showHideNumber.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsb_showHideNumber.Name = "tsb_showHideNumber";
			this.tsb_showHideNumber.Size = new System.Drawing.Size(60, 22);
			this.tsb_showHideNumber.Text = "隐藏行号";
			this.tsb_showHideNumber.Click += new System.EventHandler(this.tsb_showHideNumber_Click);
			// 
			// InfoControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.splitContainer1);
			this.Name = "InfoControl";
			this.Size = new System.Drawing.Size(502, 425);
			this.tabControl.ResumeLayout(false);
			this.cms_deletPath.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			this.splitContainer1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPage_summary;
		private System.Windows.Forms.TabPage tabPage_wmass;
		private System.Windows.Forms.ComboBox cb_path;
		private System.Windows.Forms.Button bt_selectPath;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TabPage tabPage_wzq;
		private System.Windows.Forms.TabPage tabPage_wdisp;
		private System.Windows.Forms.TabPage tabPage_wgcpj;
		private System.Windows.Forms.TabPage tabPage_wdcnl;
		private System.Windows.Forms.TabPage tabPage_stak;
		private System.Windows.Forms.TabPage tabPage_wv02q;
		private System.Windows.Forms.TabPage tabPage_satbmb;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_showHideNumber;
        private System.Windows.Forms.ToolStripButton tsb_fullSimpleShow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton tsb_searchUp;
        private System.Windows.Forms.ToolStripButton tsb_searchDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripComboBox tsb_searchText;
		private System.Windows.Forms.ContextMenuStrip cms_deletPath;
		private System.Windows.Forms.ToolStripMenuItem tsmi_delete;
        private System.Windows.Forms.ToolStripButton tsb_explain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
	}
}
