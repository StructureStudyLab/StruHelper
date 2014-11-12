using System;
using System.Collections.Generic;
using System.Text;

namespace Satwe_tool
{
	internal class ConstSearchKey
	{
		public string StoreyMatInfo;
		public string StructureType;
		public string SesmicInfo;
		public string WindInfo;
		public string BaseCount;
		public ConstSearchKey(int version)
		{
			StoreyMatInfo = "各层构件数量、构件材料和层高";
			StructureType = "结构类别:";
			SesmicInfo = "地震信息";
			BaseCount = "地下室层数";
			WindInfo = "风荷载信息";
		}
	}
}
