using System.Data;
using Dapper;

namespace BuildingBlocks.Dapper
{

    public class MemberIdConverter : SqlMapper.TypeHandler<MemberId>
    {
        public override void SetValue(IDbDataParameter parameter, MemberId value)
        {
            parameter.Value = (int) value;
        }

        public override MemberId Parse(object value)
        {
            return MemberId.From((int) value);
        }
    }
}