using System;
using System.Collections.Generic;
using System.Text;

namespace Satwe_tool
{
	public class AnalyzerWmass
	{
		private DataSummary _data;
		private RichTextBoxEx _richTextBox;
		public void AnalyseIndex()
		{
			_data = new DataSummary();
			_richTextBox = new RichTextBoxEx();
		}
	}
}
