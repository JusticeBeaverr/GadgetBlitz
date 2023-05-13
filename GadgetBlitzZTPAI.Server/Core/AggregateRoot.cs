using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GadgetBlitzZTPAI.Server.Core
{
    public abstract class AggregateRoot
    {
        //id mongo domyślnie mapowane nie ruszać
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
        public int Version { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public int SchemaVersion { get; set; }
        public IEnumerable<IDomainEvent> Events => _events;

        private readonly List<IDomainEvent> _events = new();
        private bool _versionIncremented;

        protected void AddEvent(IDomainEvent @event)
        {
            if (!_events.Any() && !_versionIncremented)
            {
                Version++;
                _versionIncremented = true;
            }

            _events.Add(@event);
        }

        public void ClearEvents() => _events.Clear();

        protected void IncrementVersion()
        {
            if (_versionIncremented)
            {
                return;
            }

            Version++;
            _versionIncremented = true;
        }

        protected void SetCreationDate() => CreationDate = DateTime.UtcNow;
        protected void SetModyficationDate() => ModificationDate = DateTime.UtcNow;
    }
}
