using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ScrapeCity.Common.Enums
{
    public enum USBPortEnum
    {
        not_available = 0,
        [Display(Name = "USB (mini)"),
        EnumMember(Value = "USB (mini)")]
        usb_mini = 1,

        [Display(Name = "USB (KVM keyboard connection)"),
        EnumMember(Value = "USB (KVM keyboard connection)")]
        usb_KVM = 2,

        [Display(Name = "USB 2.0"),
        EnumMember(Value = "USB 2.0")]
        usb_2_0 = 3,

        [Display(Name = "USB 2.0 (downstream)"),
        EnumMember(Value = "USB 2.0 (downstream)")]
        usb_2_0_downstream = 4,

        [Display(Name = "USB 2.0 (upstream)"),
        EnumMember(Value = "USB 2.0 (upstream)")]
        usb_2_0_upstream = 5,

        [Display(Name = "USB 2.0 (mini)"),
        EnumMember(Value = "USB 2.0 (mini)")]
        usb_2_0_mini = 6,

        [Display(Name = "USB 3.0"),
        EnumMember(Value = "USB 3.0")]
        usb_3_0 = 7,

        [Display(Name = "USB 3.0 (downstream)"),
        EnumMember(Value = "USB 3.0 (downstream)")]
        usb_3_0_downstream = 8,

        [Display(Name = "USB 3.0 (upstream)"),
        EnumMember(Value = "USB 3.0 (upstream)")]
        usb_3_0_upstream = 9,

        [Display(Name = "USB 3.0 Type C"),
        EnumMember(Value = "USB 3.0 Type C")]
        usb_3_0_c = 10,

        [Display(Name = "USB 3.1"),
        EnumMember(Value = "USB 3.1")]
        usb_3_1 = 11,

        [Display(Name = "USB 3.1 Type C"),
        EnumMember(Value = "USB 3.1 Type C")]
        usb_3_1_c = 12,

        [Display(Name = "Type C"),
        EnumMember(Value = "Type C")]
        usb_c = 13,
    }
}
