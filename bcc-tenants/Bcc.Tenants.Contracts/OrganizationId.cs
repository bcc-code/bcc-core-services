using BuildingBlocks;

namespace Bcc.Tenants.Contracts;

public class OrganizationId : ValueObject<OrganizationId>
{
    private OrganizationId(Guid value)
    {
        if (value == default)
            throw new ArgumentNullException(
                nameof(value),
                "The Id cannot be empty"
            );

        Value = value;
    }

    private Guid Value { get; }

    public static OrganizationId From(Guid value)
    {
        return new OrganizationId(value);
    }
        
    public static implicit operator Guid(OrganizationId self)
    {
        return self.Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}