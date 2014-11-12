using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Resources;

namespace Satwe_tool
{
	class DataSummary : BaseData
	{
		public int FloorCount;//总层数
		public int BaseCount;//地下室层数
		public string StructureType;//结构类型
		public List<InfoMaterial> FloorElemMatInfo = new List<InfoMaterial>();//构件数量，材料，层高
		public InfoSesmic SesmicInfo = new InfoSesmic();//地震参数信息
		public InfoWind WindInfo = new InfoWind();
        public List<InfoEi> FloorEiInfo = new List<InfoEi>();
        public InfoEi minRtx,minRty,minRtx1,minRty1,minRtx2,minRty2;
        public List<InfoMass> FloorMassInfo = new List<InfoMass>();
        public InfoMass maxMassInfo,minMassInfo;
        public List<InfoRv> FloorRvInfo = new List<InfoRv>();
        public InfoRv minRvInfo_X, minRvInfo_Y;
        public List<InfoT> VibrationT;
        public InfoT FirstTq, FirstTt;
        public double EffectiveMassFactor_X;
        public double EffectiveMassFactor_Y;
        public List<InfoVG >VgList;
        public InfoVG minVg_X, minVg_Y;
        public InfoDisp maxDispX, maxDispY, maxDispX_floor, maxDispY_floor;
        public InfoDisp maxX_FloorDispAngle, maxY_FloorDispAngle;//最大层间位移
        public List<InfoDisp> DispInfoList;
        public double EGFactor_X, EGFactor_Y;
        

        public DataSummary()
		{
			Title = "结构信息";
		}
		
		protected override string GetSimpleData()
		{
			StringBuilder sb = new StringBuilder();
            try
            {               
                sb.AppendFormat(Separator);
                sb.AppendFormat("             结构总信息\n");
                sb.AppendFormat(Separator);
                sb.AppendFormat("{0} 总共 {1} 层，地下室 {2} 层\n", StructureType, FloorCount, BaseCount);

                sb.AppendFormat("\n地震烈度 {0} 度，框架抗震 {1} 级，剪力墙抗震 {2} 级\n", SesmicInfo.Strength,
                    SesmicInfo.FrameGrade, SesmicInfo.WallGrade);

                sb.AppendFormat("\n基本风压：{0}KN/m2， 结构基本周期：Tx={1} Ty={2}， 地面粗糙度： {3}\n", WindInfo.W0,
                    WindInfo.Tx, WindInfo.Ty, WindInfo.SiteRawType);

                sb.AppendFormat(Separator);
                sb.AppendFormat("             比值信息\n");
                sb.AppendFormat(Separator);

                sb.AppendFormat("刚度比：          Ratx={0,-7} ({1,-2}层) Ratx1={2,-7} ({3,-2}层) Ratx2={4,-7} ({5,-2}层)\n",
                     minRtx.Ratx, minRtx.FoorNo, minRtx1.Ratx1, minRtx1.FoorNo, minRtx2.Ratx2, minRtx2.FoorNo);
                sb.AppendFormat("                  Raty={0,-7} ({1,-2}层) Raty1={2,-7} ({3,-2}层) Raty2={4,-7} ({5,-2}层)\n",
                  minRty.Raty, minRty.FoorNo, minRty1.Raty1, minRty1.FoorNo, minRty2.Raty2, minRty2.FoorNo);


                sb.AppendFormat("\n楼层抗剪承载力比: Min(X向) = {0,-4} ({1,-2}层)    Min(Y向） = {2,-4} ({3,-2}层)\n",
                     minRvInfo_X.Ratio_X, minRvInfo_X.FoorNo, minRvInfo_Y.Ratio_Y, minRvInfo_Y.FoorNo);

               
                sb.AppendFormat("\n剪重比:           Min(X向) = {0:0.0%} ({1}层）  Min(Y向) = {2:0.0%} ({3}层）\n",
					minVg_X.Vx_Ratio / 100, minVg_X.FoorNo, minVg_Y.Vy_Ratio / 100, minVg_Y.FoorNo);
                
                sb.AppendFormat("\n周期比:           ={0:0.0000}  第一扭转振型号:{1,-2} / 第一平动振型号:{2,-2}\n",
                   this.FirstTt.T / this.FirstTq.T, this.FirstTt.ShakeNo, this.FirstTq.ShakeNo);
                sb.AppendFormat("\n位移比:           Max(X向)= {0,-4}   Max(Y向)= {1,-4}\n",
                   maxDispX.XFactor,maxDispY.YFactor);
                sb.AppendFormat("层间位移比:       Max(X向)= {0,-4}   Max(Y向)= {1,-4}\n",
                  maxDispX_floor.XFactor_floor, maxDispY_floor.YFactor_floor);                
                sb.AppendFormat("层间位移角:       Max(X向)= 1/{0,-4} (==工况{1}==）  Max(Y向)= 1/{2,-4}(==工况{3}==）\n",
                maxX_FloorDispAngle.XMaxDisp_floorAngle, maxX_FloorDispAngle.WorkNo, maxY_FloorDispAngle.YMaxDisp_floorAngle, maxY_FloorDispAngle.WorkNo);
                
                sb.AppendFormat("\n刚重比:           X向 = {0,-4}       Y向 = {1,-4}\n", EGFactor_X, EGFactor_Y);

                sb.AppendFormat("\n质量比:           Max = {0,-4}({1,-2}层)    Min = {2,-4}({3,-2}层)\n",
                  maxMassInfo.MassRate, maxMassInfo.FoorNo, minMassInfo.MassRate, minMassInfo.FoorNo);

                sb.AppendFormat("\n有效质量系数:     X向 = {0:0.0%}  Y向 = {1:0.0%}  参与计算振型数：{2}\n",
                   EffectiveMassFactor_X / 100, EffectiveMassFactor_Y / 100, VibrationT.Count);

                return sb.ToString();
            }
            catch (Exception e)
            {
				MessageBox.Show("无法获取关键指标信息，请检查计算结果",e.Message);       
                return sb.ToString();
            }
		}        

        protected override string GetExplainData()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                sb.AppendFormat(Separator);
                sb.AppendFormat("             结构总信息\n");
                sb.AppendFormat(Separator);
                sb.AppendFormat("{0} 总共 {1} 层，地下室 {2} 层\n", StructureType, FloorCount, BaseCount);

                sb.AppendFormat("\n地震烈度 {0} 度，框架抗震 {1} 级，剪力墙抗震 {2} 级\n", SesmicInfo.Strength,
                    SesmicInfo.FrameGrade, SesmicInfo.WallGrade);

                sb.AppendFormat("\n基本风压：{0}KN/m2， 结构基本周期：Tx={1} Ty={2}， 地面粗糙度： {3}\n", WindInfo.W0,
                    WindInfo.Tx, WindInfo.Ty, WindInfo.SiteRawType);
                sb.Append(Properties.Resources.IndexReinDesign);

                sb.AppendFormat(Separator);
                sb.AppendFormat("             比值信息\n");
                sb.AppendFormat(Separator);

                sb.AppendFormat("刚度比：          Ratx={0,-7} ({1,-2}层) Ratx1={2,-7} ({3,-2}层) Ratx2={4,-7} ({5,-2}层)\n",
                     minRtx.Ratx, minRtx.FoorNo, minRtx1.Ratx1, minRtx1.FoorNo, minRtx2.Ratx2, minRtx2.FoorNo);
                sb.AppendFormat("                  Raty={0,-7} ({1,-2}层) Raty1={2,-7} ({3,-2}层) Raty2={4,-7} ({5,-2}层)\n",
                  minRty.Raty, minRty.FoorNo, minRty1.Raty1, minRty1.FoorNo, minRty2.Raty2, minRty2.FoorNo);
                sb.Append(Properties.Resources.IndexEi);
              
                sb.AppendFormat("\n楼层抗剪承载力比: Min(X向) = {0,-4} ({1,-2}层)    Min(Y向） = {2,-4} ({3,-2}层)\n",
                     minRvInfo_X.Ratio_X, minRvInfo_X.FoorNo, minRvInfo_Y.Ratio_Y, minRvInfo_Y.FoorNo);
                sb.Append(Properties.Resources.IndexRv);

               
                sb.AppendFormat("\n剪重比:           Min(X向) = {0:0.0%} ({1}层）  Min(Y向) = {2:0.0%} ({3}层）\n",
                    minVg_X.Vx_Ratio / 100, minVg_X.FoorNo, minVg_Y.Vy_Ratio / 100, minVg_Y.FoorNo);
                sb.Append(Properties.Resources.IndexVg);

                sb.AppendFormat("\n周期比:           ={0:0.0000}  第一扭转振型号:{1,-2} / 第一平动振型号:{2,-2}\n",
                   this.FirstTt.T / this.FirstTq.T, this.FirstTt.ShakeNo, this.FirstTq.ShakeNo);
                sb.Append(Properties.Resources.IndexTg);
                
                sb.AppendFormat("\n位移比:           Max(X向)= {0,-4}   Max(Y向)= {1,-4}\n",
                   maxDispX.XFactor, maxDispY.YFactor);                
                sb.AppendFormat("层间位移比:       Max(X向)= {0,-4}   Max(Y向)= {1,-4}\n",
                  maxDispX_floor.XFactor_floor, maxDispY_floor.YFactor_floor);
                sb.Append(Properties.Resources.IndexDisp);

                sb.AppendFormat("层间位移角:       Max(X向)= 1/{0,-4} (==工况{1}==）  Max(Y向)= 1/{2,-4}(==工况{3}==）\n",
                maxX_FloorDispAngle.XMaxDisp_floorAngle, maxX_FloorDispAngle.WorkNo, maxY_FloorDispAngle.YMaxDisp_floorAngle, maxY_FloorDispAngle.WorkNo);
                sb.Append(Properties.Resources.IndexDispAngle);

                sb.AppendFormat("\n刚重比:           X向 = {0,-4}       Y向 = {1,-4}\n", EGFactor_X, EGFactor_Y);
                sb.Append(Properties.Resources.IndexEg);

                sb.AppendFormat("\n质量比:           Max = {0,-4}({1,-2}层)    Min = {2,-4}({3,-2}层)\n",
                   maxMassInfo.MassRate, maxMassInfo.FoorNo, minMassInfo.MassRate, minMassInfo.FoorNo);

                sb.AppendFormat("\n有效质量系数:     X向 = {0:0.0%}  Y向 = {1:0.0%}  参与计算振型数：{2}\n",
                   EffectiveMassFactor_X / 100, EffectiveMassFactor_Y / 100, VibrationT.Count);

                return sb.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show("无法获取关键指标信息，请检查计算结果", e.Message);
                return sb.ToString();
            }
        }

		protected override string GetFullData()
		{
			StringBuilder sb = new StringBuilder();
            try
            {               
                sb.AppendFormat(Separator);
                sb.AppendFormat("             结构总信息\n");
                sb.AppendFormat(Separator);
                sb.AppendFormat("{0} 总共 {1} 层，地下室 {2} 层\n", StructureType, FloorCount, BaseCount);

//                 sb.AppendFormat("\n楼层(标准层)  塔号   梁数/混凝土/钢筋  墙数/混凝土/钢筋 柱数/混凝土/钢筋 层高  总高\n");
//                 for (int i = 0; i < FloorElemMatInfo.Count; i++)
//                 {
//                     InfoMaterial info = FloorElemMatInfo[i];
//                     sb.AppendFormat("{0,-4} ( {1,-3}) {2,6}{3,7}/C{4}/{5} {6,9}/C{7}/{8} {9,7}/C{10}/{11}      {12}   {13}\n",
//                             info.StoryNo, info.StdStoryNo, info.TowerNo, info.BeamCount, info.BeamRc, info.BeamRf,
//                             info.WallCount, info.WallRc, info.WallRf, info.ColumnCount, info.ColumnRc, info.ColumnRf,
//                             info.StoryHeight, info.TotalHeight);
//                 }

                sb.AppendFormat("\n地震烈度 {0} 度，框架抗震 {1} 级，剪力墙抗震 {2} 级\n", SesmicInfo.Strength,
                    SesmicInfo.FrameGrade, SesmicInfo.WallGrade);
                sb.AppendFormat("  场地类别：                   {0}\n", SesmicInfo.SiteType);
                sb.AppendFormat("  设计地震分组：               {0}\n", SesmicInfo.SiteGroup);
                sb.AppendFormat("  特征周期：                   {0}\n", SesmicInfo.Tg);
                sb.AppendFormat("  考虑偶然偏心：               {0}\n", SesmicInfo.Consider_e == true ? "是" : "否");
                sb.AppendFormat("  考虑双向地震扭转效应：       {0}\n", SesmicInfo.Consider_doubleT == true ? "是" : "否");

                sb.AppendFormat("\n基本风压：{0}KN/m2， 结构基本周期：Tx={1} Ty={2}， 地面粗糙度： {3}\n\n", WindInfo.W0,
                    WindInfo.Tx, WindInfo.Ty, WindInfo.SiteRawType);

                sb.AppendFormat(Separator);
                sb.AppendFormat("             比值信息\n");
                sb.AppendFormat(Separator);

                sb.AppendFormat("刚度比：        Ratx={0,-7} ({1,-2}层) Ratx1={2,-7} ({3,-2}层) Ratx2={4,-7} ({5,-2}层)\n",
                     minRtx.Ratx, minRtx.FoorNo, minRtx1.Ratx1, minRtx1.FoorNo, minRtx2.Ratx2, minRtx2.FoorNo);
                sb.AppendFormat("                Raty={0,-7} ({1,-2}层) Raty1={2,-7} ({3,-2}层) Raty2={4,-7} ({5,-2}层)\n",
                  minRty.Raty, minRty.FoorNo, minRty1.Raty1, minRty1.FoorNo, minRty2.Raty2, minRty2.FoorNo);
                sb.AppendFormat("\n  楼层  塔号  Ratx      Raty      Ratx1     Raty1     Ratx2     Raty2    薄弱层放大系数\n");
                for (int i = 0; i < FloorEiInfo.Count; i++)
                {
                    sb.AppendFormat("  {0,-4}  {1,-4}  {2,-10}{3,-10}{4,-10}{5,-10}{6,-10}{7,-10}{8}\n", FloorEiInfo[i].FoorNo, FloorEiInfo[i].TowerNo,
                        FloorEiInfo[i].Ratx, FloorEiInfo[i].Raty, FloorEiInfo[i].Ratx1, FloorEiInfo[i].Raty1,
                        FloorEiInfo[i].Ratx2, FloorEiInfo[i].Raty2, FloorEiInfo[i].WeekFloorFactor);
                }

                             

                sb.AppendFormat("\n楼层抗剪承载力比: Min(X向) = {0,-4} ({1,-2}层)    Min(Y向） = {2,-4} ({3,-2}层)\n",
                     minRvInfo_X.Ratio_X, minRvInfo_X.FoorNo, minRvInfo_Y.Ratio_Y, minRvInfo_Y.FoorNo);
                sb.AppendFormat("  楼层      比值（X向)     比值（Y向）\n");
                for (int i = 0; i < FloorMassInfo.Count; i++)
                {
                    sb.AppendFormat("  {0,-10}{1,-15}{2}\n", FloorRvInfo[i].FoorNo, FloorRvInfo[i].Ratio_X, FloorRvInfo[i].Ratio_Y);
                }

               
                sb.AppendFormat("\n剪重比:           Min(X向) = {0:0.0%} ({1}层）  Min(Y向) = {2:0.0%} ({3}层）\n",
                   minVg_X.Vx_Ratio / 100, minVg_X.FoorNo, minVg_Y.Vy_Ratio / 100, minVg_Y.FoorNo);
                sb.AppendFormat("  楼层      剪重比(X向)    剪重比(Y向)\n");
                for (int i = 0; i < VgList.Count; i++)
                {
                    sb.AppendFormat("  {0,-10}{1,-15}{2}\n", VgList[i].FoorNo, VgList[i].Vx_Ratio, VgList[i].Vy_Ratio);
                }

                sb.AppendFormat("\n周期比:        ={0:0.0000}  第一扭转振型号:{1,-2} / 第一平动振型号:{2,-2}\n",
                   this.FirstTt.T / this.FirstTq.T, this.FirstTt.ShakeNo, this.FirstTq.ShakeNo);
                sb.AppendFormat("  振型号  周期    转角    平动系数 (X+Y)            扭转系数\n");
                for (int i = 0; i < VibrationT.Count; i++)
                {
                    sb.AppendFormat("  {0,-8}{1,-8}{2,-8}{3,-9}({4,-5}+ {5,-5})   {6}\n",
                        VibrationT[i].ShakeNo, VibrationT[i].T, VibrationT[i].Angle, VibrationT[i].MoveFactor,
                        VibrationT[i].MoveFactor_X, VibrationT[i].MoveFactor_Y, VibrationT[i].TwistFactor);
                }

                sb.AppendFormat("\n位移比:           Max(X向)= {0,-4}   Max(Y向)= {1,-4}\n",
                   maxDispX.XFactor, maxDispY.YFactor);
                sb.AppendFormat("层间位移比:       Max(X向)= {0,-4}   Max(Y向)= {1,-4}\n",
                  maxDispX_floor.XFactor_floor, maxDispY_floor.YFactor_floor);
                sb.AppendFormat("层间位移角:       Max(X向)= 1/{0,-4} (==工况{1}==）  Max(Y向)= 1/{2,-4}(==工况{3}==）\n",
                maxX_FloorDispAngle.XMaxDisp_floorAngle, maxX_FloorDispAngle.WorkNo, maxY_FloorDispAngle.YMaxDisp_floorAngle, maxY_FloorDispAngle.WorkNo);
                for (int i = 0; i < DispInfoList.Count; i++)
                {
                    sb.Append(DispInfoList[i].GetDescription());
                }

                sb.AppendFormat("\n刚重比:           X向 = {0,-4}       Y向 = {1,-4}\n", EGFactor_X, EGFactor_Y);


                sb.AppendFormat("\n质量比:       Max = {0,-4}({1,-2}层)    Min = {2,-4}({3,-2}层)\n",
                    maxMassInfo.MassRate, maxMassInfo.FoorNo, minMassInfo.MassRate, minMassInfo.FoorNo);
                sb.AppendFormat("  楼层      质量比\n");
                for (int i = 0; i < FloorMassInfo.Count; i++)
                {
                    sb.AppendFormat("  {0,-10}{1}\n", FloorMassInfo[i].FoorNo, FloorMassInfo[i].MassRate);
                }


                sb.AppendFormat("\n有效质量系数:     X向 = {0:0.0%}  Y向 = {1:0.0%}  参与计算振型数：{2}\n",
                  EffectiveMassFactor_X / 100, EffectiveMassFactor_Y / 100, VibrationT.Count);

                return sb.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show("无法获取关键指标信息，请检查计算结果",e.Message);
				return sb.ToString();
            }

		}


	}
}
