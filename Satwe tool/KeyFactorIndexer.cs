using System;
using System.Collections.Generic;
using System.Text;

namespace Satwe_tool
{
	internal class KeyFactorIndexer
	{
        private Dictionary<string, string> _KeyFactors = new Dictionary<string, string>();

        public Dictionary<string, string> KeyFactors
        {
            get { return _KeyFactors; }
        }
        public KeyFactorIndexer()
		{       
                   
            _KeyFactors.Add("位移比", "位移比:");
            _KeyFactors.Add("层间位移比", "层间位移比:");
            _KeyFactors.Add("位移角", "层间位移角:");
            _KeyFactors.Add("周期比", "周期比:");

            _KeyFactors.Add("刚度比", "刚度比:");    
            _KeyFactors.Add("刚重比","刚重比:");
            _KeyFactors.Add("剪重比","剪重比:");            
            _KeyFactors.Add("层间受剪承载力之比", "楼层抗剪承载力比:");
           
            _KeyFactors.Add("质量比", "质量比");
            _KeyFactors.Add("有效质量系数", "有效质量系数:");

		}
	}
}
