using SharedKernel.Repository;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain;

public class Entity : IEntity

{
    [Key]
    public Guid Id { get; init; }
    [JsonIgnore]
    public DateTime CreatedAt { get; private set; }
    [JsonIgnore]
    public DateTime? UpdatedAt { get; private set; }
    [JsonIgnore]
    public DateTime? DeletedAt { get; private set; }
    public Entity()
    {
        Id = new Guid();
        CreatedAt = DateTime.UtcNow;
    }

    public Entity(Guid id)
    {
        Id = id;
        CreatedAt = DateTime.UtcNow;
    }

    public void SetUpdateDateTime()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetDeletedDateTime() { DeletedAt = DateTime.UtcNow; }
}
