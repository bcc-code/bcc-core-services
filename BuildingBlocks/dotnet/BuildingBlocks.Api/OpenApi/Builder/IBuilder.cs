namespace BuildingBlocks.Api.OpenApi.Builder
{
    public interface IBuilder<TBuildingType>
    {
        TBuildingType Build();
    }
}