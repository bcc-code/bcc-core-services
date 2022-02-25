namespace BuildingBlocks
{

    public class Personas
    {
        public static SherlockHolmes SherlockHolmes { get; set; } = new() {Id = 1};
        public static WilliamShakespeare WilliamShakespeare { get; set; } = new() {Id = 2};
        public static AnneShakespeare AnneShakespeare { get; set; } = new() {Id = 3};
        public static WinstonChurchill WinstonChurchill { get; set; } = new() {Id = 4};
        public static IsaacNewton IsaacNewton { get; set; } = new() {Id = 5};
        public static SusannaHall SusannaHall { get; set; } = new() {Id = 6};
        public static HamnetShakespeare HamnetShakespeare { get; set; } = new() {Id = 7};
        public static JudithQuiney JudithQuiney { get; set; } = new() {Id = 8};
    }

    public class SherlockHolmes : Persona
    {
    }

    public class WilliamShakespeare : Persona
    {
    }

    public class AnneShakespeare : Persona
    {
    }

    public class WinstonChurchill : Persona
    {
    }

    public class IsaacNewton : Persona
    {
    }

    public class SusannaHall : Persona
    {
    }

    public class HamnetShakespeare : Persona
    {
    }

    public class JudithQuiney : Persona
    {
    }

    public class Persona : IPersonas
    {
        public string DisplayName { get; set; }
        public string Gender { get; set; }
        public int Id { get; set; }
        public int? SpouseId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SourceOrganizationId { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public interface IPersonas
    {
        int Id { get; set; }
        int? SpouseId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        int SourceOrganizationId { get; set; }
        DateTime BirthDate { get; set; }
        string DisplayName { get; set; }
        string Gender { get; set; }
    }


    public static class PersonasExtensions
    {
        public static IPersonas GetPersonaByName(this Personas personas, string name)
        {
            var person = personas.GetType().GetProperty(name)?.GetValue(personas, null)!;
            return (Persona) person;
        }

    }
}