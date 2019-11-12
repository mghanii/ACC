using System;

namespace ACC.Common.Types
{
    public abstract class EntityBase : IIdentifiable
    {
        public string Id { get; protected set; }
        public DateTimeOffset CreationDate { get; protected set; }
        public DateTimeOffset UpdateDate { get; protected set; }

        public EntityBase(string id)
        {
            Id = id;
            CreationDate = DateTimeOffset.UtcNow;
            SetUpdateDate();
        }

        protected virtual void SetUpdateDate() => UpdateDate = DateTimeOffset.UtcNow;
    }
}