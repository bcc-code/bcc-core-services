using System.Data;
using System.Data.SqlTypes;
using Dapper;

namespace BuildingBlocks.Dapper
{
    public class DateTimeConverter : SqlMapper.TypeHandler<DateTime>
    {
        public override void SetValue(IDbDataParameter parameter, DateTime value)
        {
            if (DateTime.MinValue == value)
            {
                value = (DateTime) SqlDateTime.MinValue;
            }

            parameter.Value = value;
        }

        public override DateTime Parse(object value)
        {
            return (DateTime) value;
        }
    }
}