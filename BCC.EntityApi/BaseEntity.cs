using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using Microsoft.EntityFrameworkCore;

namespace BCC.EntityApi
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset CreatedAt { get; private set; } = DateTimeOffset.Now;
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTimeOffset UpdatedAt { get; private set; } = DateTimeOffset.Now;

        public void Touch(EntityState State)
        {
            var now = DateTime.Now;

            UpdatedAt = now;
            if (State == EntityState.Added)
            {
                CreatedAt = now;
            }
        }

        public void Save(EntityState State)
        {
            Touch(State);
        }
    }
}