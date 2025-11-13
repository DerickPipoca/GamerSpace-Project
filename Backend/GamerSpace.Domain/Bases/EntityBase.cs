namespace GamerSpace.Domain.Bases
{
    public abstract class EntityBase
    {
        public long Id { get; protected set; }
        public DateTime CreateTime { get; protected set; }
        public bool Disabled { get; protected set; }

        public EntityBase()
        {
            CreateTime = DateTime.UtcNow;
            Disabled = false;
        }

        public virtual void Disable()
        {
            Disabled = true;
        }

        public virtual void Enable()
        {
            Disabled = false;
        }
    }
}