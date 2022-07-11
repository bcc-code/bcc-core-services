namespace BuildingBlocks.Api.Models.CommonResponse
{
    public abstract class Builder<TBuildingType> : IBuilder<TBuildingType>
    {
        protected Builder(TBuildingType target)
        {
            Target = target;
        }

        protected Builder()
        {
        }

        protected TBuildingType Target { get; set; }

        public virtual TBuildingType Build()
        {
            return Target;
        }
    }
}