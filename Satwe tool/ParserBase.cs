using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace Satwe_tool
{	
	internal class ParserBase
	{
		public string m_content;
		protected string[] m_contentArray;
		protected ConstSearchKey m_key;
		protected static ParserBase _instanceLeft;
        protected static ParserBase _instanceRight;
		protected ParserBase()
		{

		}
		public virtual bool ReadFile(string filePath)
		{
			if (!File.Exists(filePath))
			{
				 return false;
			}
            Encoding coder = Encoding.Default;
			m_content = File.ReadAllText(filePath, coder);
            m_content = m_content.Replace('\0', ' ');
			m_contentArray = File.ReadAllLines(filePath, coder);
            for (int i = 0; i < m_contentArray.Length; i++)
            {
                m_contentArray[i] = m_contentArray[i].Replace('\0', ' ');
            }
			return true;
		}	

		public string FullContent(bool withNumber=true)
		{
			if (withNumber) {
                StringBuilder sb=new StringBuilder();
				for (int i = 0; i < m_contentArray.Length; i++) {
					int number = i + 1;
					sb.AppendFormat("{0,-6}{1}\n", number, m_contentArray[i]);
				}
				return sb.ToString();
			} else
				return m_content;			
		}
	}
}
