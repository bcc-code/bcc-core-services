using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace BCC.EntityApi
{
    public class EntityLog : BaseEntity
    {
        [Required]
        public Guid EntityId { get; set; }

        [Required]
        public string EntityType { get; set; }

        /* JSON map of attributes that changed with their previous values */
        public string Changed { get; set; }

        [StringLength(100)]
        public string Comment { get; set; }

        public EntityState State { get; set; }
    }

    public static class EntityLogExtension
    {
        public static EntityLog Log(this BaseEntity entity, object Changes, string Comment = "", EntityState State = EntityState.Modified)
        {
            var logEntry = new EntityLog()
            {
                EntityId = entity.Id,
                EntityType = entity.GetType().ToString(),
                Changed = JsonSerializer.Serialize(Changes),
                Comment = Comment,
                State = State,
            };

            // TODO: Find out how to save it to the database

            return logEntry;
        }
    }
}
