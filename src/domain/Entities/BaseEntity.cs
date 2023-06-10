namespace Domain.Entities
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; private set; }

        public DateTime CreateDate { get; private set; }

        protected BaseEntity()
        {
            CreateDate = DateTime.UtcNow;
        }

        protected BaseEntity(TId id)
        {
            Id = id;
            CreateDate = DateTime.UtcNow;
        }
    }
}