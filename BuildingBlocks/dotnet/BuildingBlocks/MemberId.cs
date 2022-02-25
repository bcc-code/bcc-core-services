namespace BuildingBlocks
{
    public class MemberId : ValueObject<MemberId>
    {
        private MemberId(int value)
        {
            if (value == default)
                throw new ArgumentNullException(
                    nameof(value),
                    "The Id cannot be empty"
                );

            Value = value;
        }

        private int Value { get; }

        public static MemberId From(int value)
        {
            return new MemberId(value);
        }
        
        public static implicit operator int(MemberId self)
        {
            return self.Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}