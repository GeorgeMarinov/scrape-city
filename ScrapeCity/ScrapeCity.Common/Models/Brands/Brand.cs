using Newtonsoft.Json;
using ScrapeCity.Common.Models.Monitors;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrapeCity.Common.Models.Brands
{
    public class Brand
    {
        public Brand()
        {
            Monitors = new List<Monitor>();
        }

        [JsonIgnore]
        public int Id { get; set; }

        [StringLength(450)]
        [Index(IsUnique = true)]
        public string BrandName { get; set; }

        [JsonIgnore]
        public virtual ICollection<Monitor> Monitors { get; set; }
    }
}
