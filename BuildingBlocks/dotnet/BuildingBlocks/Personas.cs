namespace BuildingBlocks
{

    public class Personas
    {
        public static SherlockHolmes SherlockHolmes { get; set; } = new();
        public static WilliamShakespeare WilliamShakespeare { get; set; } = new();
        public static AnneShakespeare AnneShakespeare { get; set; } = new();
        public static WinstonChurchill WinstonChurchill { get; set; } = new();
        public static IsaacNewton IsaacNewton { get; set; } = new();
        public static SusannaHall SusannaHall { get; set; } = new();
        public static HamnetShakespeare HamnetShakespeare { get; set; } = new();
        public static JudithQuiney JudithQuiney { get; set; } = new();

        //static in case of unit tests running in parallel and multiple instances of CustomWebApplicationFactory
        private static HashSet<int> _personasIdsInDatabase = new HashSet<int>();
        public static void Generate()
        {
            var utcNow = DateTime.UtcNow;
            
            SherlockHolmes = new SherlockHolmes()
            {
                Id = GenerateUniquePersonId(), FirstName = "Sherlock", LastName = "Holmes",
                BirthDate = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day).AddYears(-30),
                Gender = "M", SourceOrganizationId = 69
            };
            AnneShakespeare = new AnneShakespeare()
            {
                Id = GenerateUniquePersonId(), FirstName = "Anne", LastName = "Shakespeare",
                BirthDate = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day).AddYears(-28),
                Gender = "K", SourceOrganizationId = 69
            };
            Personas.WilliamShakespeare = new WilliamShakespeare()
            {
                Id = GenerateUniquePersonId(), FirstName = "William", LastName = "Shakespeare",
                BirthDate = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day).AddYears(-50),
                Gender = "M", SourceOrganizationId = 69
            };
            Personas.WinstonChurchill = new WinstonChurchill()
            {
                Id = GenerateUniquePersonId(), FirstName = "Winston", LastName = "Churchill",
                BirthDate = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day).AddYears(-40),
                Gender = "M", SourceOrganizationId = 69
            };
            Personas.IsaacNewton = new IsaacNewton()
            {
                Id = GenerateUniquePersonId(), FirstName = "Isaac", LastName = "Newton",
                BirthDate = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day).AddYears(-60),
                Gender = "M", SourceOrganizationId = 69
            };
            Personas.JudithQuiney = new JudithQuiney()
            {
                Id = GenerateUniquePersonId(), FirstName = "Judith", LastName = "Quiney",
                BirthDate = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day).AddYears(-17),
                Gender = "K", SourceOrganizationId = 69
            };
            Personas.SusannaHall = new SusannaHall()
            {
                Id = GenerateUniquePersonId(), FirstName = "Susanna", LastName = "Hall",
                BirthDate = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day).AddYears(-24),
                Gender = "M", SourceOrganizationId = 69
            };
            Personas.HamnetShakespeare = new HamnetShakespeare()
            {
                Id = GenerateUniquePersonId(), FirstName = "Hamnet", LastName = "Shakespeare",
                BirthDate = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day).AddYears(-20),
                Gender = "M", SourceOrganizationId = 69
            };

            Personas.WilliamShakespeare.SpouseId = Personas.AnneShakespeare.Id;
            Personas.AnneShakespeare.SpouseId = Personas.WilliamShakespeare.Id;
        }

        private static int GenerateUniquePersonId()
        {
            var exclude = _personasIdsInDatabase;
            var availableIds = Enumerable.Range(1, 10000).Where(i => !exclude.Contains(i));

            var rand = new Random();
            int index = rand.Next(0, 10000 - exclude.Count);
            var personId = availableIds.ElementAt(index);
            
            _personasIdsInDatabase.Add(personId);
            
            return personId;
        }

        public static IPersonas[] GetAllPersonas()
        {
            return new IPersonas[]
            {
                WilliamShakespeare,
                AnneShakespeare,
                SherlockHolmes,
                IsaacNewton,
                WinstonChurchill,
                HamnetShakespeare,
                JudithQuiney,
                SusannaHall
            };
        }
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