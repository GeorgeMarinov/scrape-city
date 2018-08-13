using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ScrapeCity.Common.Enums
{
    public enum Backlight
    {
        [Display(Name = "Not Available" )]
        [EnumMember(Value = "Not Available")]
        NotAvailable = 0,
        [Display(Name = "W-LED")]
        [EnumMember(Value = "W-LED")]
        W_LED = 1,
        [Display(Name = "Direct LED")]
        [EnumMember(Value = "Direct LED")]
        Direct_LED = 2,
        [Display(Name = "Gb-r LED")]
        [EnumMember(Value = "Gb-r LED")]
        Gb_r_LED = 3,
        [Display(Name = "RB-LED")]
        [EnumMember(Value = "RB-LED")]
        RB_LED = 4,
        [Display(Name = "17S4P W-LED")]
        [EnumMember(Value = "17S4P W-LED")]
        W_LED_17S4P = 5,
        [Display(Name = "CCFL Direct backlight")]
        [EnumMember(Value = "CCFL Direct backlight")]
        CCFL_Direct_backlight = 6,
        [Display(Name = "OLED")]
        [EnumMember(Value = "OLED")]
        OLED = 7,
    }
}
