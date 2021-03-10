using WTPCore.Data.Interfaces.Base;
using CollectionsPattern;

namespace WTPCore.WorkTeacherPlan
{
    public class WTPParamInfo: BaseRow<WTPParamInfos, IWTPPARAMINFO, WTPParamInfo>
    {
        public WTPParamInfo(WTPParamInfos ParamInfos, IWTPPARAMINFO DataRow)
            : base(DataRow, ParamInfos)
        {

        }
    }
}
