using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Satwe_tool
{
	public partial class RichTextBoxControl : UserControl
	{
		public RichTextBoxEx TextPanel
		{
			get { return richTextBoxEx1; }
		}
		public RichTextBoxControl()
		{
			InitializeComponent();
		}		

		private void richTextBoxEx1_VScroll(object sender, EventArgs e)
		{
			int p = richTextBoxEx1.GetPositionFromCharIndex(0).Y % (richTextBoxEx1.Font.Height + 1);
			richTextBoxEx1.Location = new Point(0, p);
			//updateLabelRowIndex();
		}

		private void updateLabelRowIndex()
		{
			//we get index of first visible char and number of first visible line
			Point pos = new Point(0, 0);
			int firstIndex = this.richTextBoxEx1.GetCharIndexFromPosition(pos);
			int firstLine = this.richTextBoxEx1.GetLineFromCharIndex(firstIndex);

			//now we get index of last visible char and number of last visible line
			pos.X += this.richTextBoxEx1.ClientRectangle.Width;
			pos.Y += this.richTextBoxEx1.ClientRectangle.Height;
			int lastIndex = this.richTextBoxEx1.GetCharIndexFromPosition(pos);
			int lastLine = this.richTextBoxEx1.GetLineFromCharIndex(lastIndex);

			//this is point position of last visible char, 
			//we'll use its Y value for calculating numberLabel size
			pos = this.richTextBoxEx1.GetPositionFromCharIndex(lastIndex);

// 			rtb_lineNumber.Text = "";
// 			for (int i = firstLine; i <= lastLine + 1; i++) {
// 				rtb_lineNumber.Text += i + 1 + "\r\n";
// 			}
		}     
	}
}
