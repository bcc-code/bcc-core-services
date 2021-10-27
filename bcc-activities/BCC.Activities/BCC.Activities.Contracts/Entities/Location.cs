using System;
using System.ComponentModel.DataAnnotations;
using BCC.EntityApi;

namespace BCC.Activities.Contracts.Entities
{
    public class Location : BaseEntity
    {
        /* Attributes */
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /* Alpha 2 ISO Country Name */
        [StringLength(2)]
        public string Country { get; set; }

        public Types.GeoPoint GeoPoint { get; set; }
    }
}
