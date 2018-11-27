namespace Domain.Abstractions
{
    public abstract class ExternalEntity : Entity
    {
        public string ExternalId { get; protected set; }
    }
}
