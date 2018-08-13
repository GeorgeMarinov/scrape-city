using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ScrapeCity.Common.Enums
{
    public enum ScreenType
    {
        [Display(Name = "Not Available")]
        [EnumMember(Value = "Not Available")]
        NotAvailable = 0,
        [Display(Name = "Glossy")]
        [EnumMember(Value = "Glossy")]
        Glossy = 1,
        [Display(Name = "Glossy (AG)")]
        [EnumMember(Value = "Glossy (AG)")]
        Glossy_AG = 2,
        [Display(Name = "Anti-Glare/Matte")]
        [EnumMember(Value = "Anti-Glare/Matte")]
        Anti_Glare_Matte = 3,
        [Display(Name = "Anti-Glare/Matte (2H)")]
        [EnumMember(Value = "Anti-Glare/Matte (2H)")]
        Anti_Glare_Matte_2H = 4,
        [Display(Name = "Anti-Glare/Matte (3H)")]
        [EnumMember(Value = "Anti-Glare/Matte (3H)")]
        Anti_Glare_Matte_3H = 5,
    }
}
