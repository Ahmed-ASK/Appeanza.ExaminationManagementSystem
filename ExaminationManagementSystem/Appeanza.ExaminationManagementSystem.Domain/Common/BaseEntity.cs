namespace Appeanza.ExaminationManagementSystem.Domain.Common
{
    public class BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public TKey? Id { get; set; }
    }
}
