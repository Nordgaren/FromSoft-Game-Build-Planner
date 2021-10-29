using SoulsFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FromSoft_Game_Build_Planner
{
    class DS1CalcCorrect
    {
        public string Name { get; set; }
        public int ID { get; set; }

        public float stgMaxVal0 { get; set; }
        public float stgMaxVal1 { get; set; }
        public float stgMaxVal2 { get; set; }
        public float stgMaxVal3 { get; set; }
        public float stgMaxVal4 { get; set; }

        public float stgMaxValGrow0 { get; set; }
        public float stgMaxValGrow1 { get; set; }
        public float stgMaxValGrow2 { get; set; }
        public float stgMaxValGrow3 { get; set; }
        public float stgMaxValGrow4 { get; set; }

        public float adjPt_MaxValGrow0 { get; set; }
        public float adjPt_MaxValGrow1 { get; set; }
        public float adjPt_MaxValGrow2 { get; set; }
        public float adjPt_MaxValGrow3 { get; set; }
        public float adjPt_MaxValGrow4 { get; set; }

        public DS1CalcCorrect(PARAM.Row calcCorrectParam)
        {
            Name = calcCorrectParam.Name;
            ID = calcCorrectParam.ID;

            stgMaxVal0 = (float)calcCorrectParam.Cells[0].Value;
            stgMaxVal1 = (float)calcCorrectParam.Cells[1].Value;
            stgMaxVal2 = (float)calcCorrectParam.Cells[2].Value;
            stgMaxVal3 = (float)calcCorrectParam.Cells[3].Value;
            stgMaxVal4 = (float)calcCorrectParam.Cells[4].Value;

            stgMaxValGrow0 = (float)calcCorrectParam.Cells[5].Value;
            stgMaxValGrow1 = (float)calcCorrectParam.Cells[6].Value;
            stgMaxValGrow2 = (float)calcCorrectParam.Cells[7].Value;
            stgMaxValGrow3 = (float)calcCorrectParam.Cells[8].Value;
            stgMaxValGrow4 = (float)calcCorrectParam.Cells[9].Value;

            adjPt_MaxValGrow0 = (float)calcCorrectParam.Cells[10].Value;
            adjPt_MaxValGrow1 = (float)calcCorrectParam.Cells[11].Value;
            adjPt_MaxValGrow2 = (float)calcCorrectParam.Cells[12].Value;
            adjPt_MaxValGrow3 = (float)calcCorrectParam.Cells[13].Value;
            adjPt_MaxValGrow4 = (float)calcCorrectParam.Cells[14].Value;
        }

        public static Dictionary<int, DS1CalcCorrect> CalcCorrectGraph;

    }
}
