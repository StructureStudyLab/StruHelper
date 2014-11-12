using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace Satwe_tool
{
	class BaseData
	{
		public int m_lineIndex = 0;
		public string Title;		
		protected string Separator = "--------------------------------------------------------------\n";
		public string GetData(int showType)
		{
            if (showType == 0)
            {
                return GetSimpleData();
            }
            else if (showType == 1)
                return GetFullData();
            else
                return GetExplainData();
		}
		protected virtual string GetSimpleData() { return string.Empty; }
		protected virtual string GetFullData() { return string.Empty; }
        protected virtual string GetExplainData() { return string.Empty; }
	}
}
