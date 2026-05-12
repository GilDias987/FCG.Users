using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FCG.Users.Domain.Documents
{
    public class AuditDocument
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Table { get; set; }
        public string RecordId { get; set; }
        public string Action { get; set; }
        public DateTime TimestampUtc { get; set; }
        public AuditUser User { get; set; }
        public Dictionary<string, AuditChange> Changes { get; set; }
    }
}
