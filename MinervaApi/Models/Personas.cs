namespace Minerva.Models
{
    public class Personas
    {
        public int? personaId { get; set; }
        public string? personaName { get; set; }
    }

    public class Persona
    {
        public int? personaAutoId { get; set; }
        public int? personaId { get; set; }
        public int? tenantId { get; set; }
        public string? personaName { get; set; }
        public int? projectPersona { get; set; }
    }
}
