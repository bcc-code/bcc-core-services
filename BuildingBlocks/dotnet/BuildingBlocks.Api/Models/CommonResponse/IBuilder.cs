namespace BuildingBlocks.Api.Models.CommonResponse
{
    public interface IBuilder<TBuildingType>
    {
        TBuildingType Build();
    }
}