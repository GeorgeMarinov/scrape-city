using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ScrapeCity.Common.Enums
{
    public enum VideoPortEnum
    {
        not_available = 0,
        [Display(Name = "AV component Input")]
        [EnumMember(Value = "AV component Input")]
        AV_component_input = 10,

        [Display(Name = "D-sub")]
        [EnumMember(Value = "D-sub")]
        D_sub = 20,

        [Display(Name = "RS232 (C)")]
        [EnumMember(Value = "RS232 (C)")]
        RS232_C = 30,

        VGA = 40,
        DVI = 50,
        [Display(Name = "DVI-D Dual Link")]
        [EnumMember(Value = "DVI-D Dual Link")]
        DVI_D_Dual_Link = 51,

        #region HDMI
        [Display(Name = "HDMI")]
        [EnumMember(Value = "HDMI")]
        HDMI = 1000,
        //[Display(Name = "HDMI 1.0")]
        //[EnumMember(Value = "HDMI 1.0")]
        //HDMI_1 = 1100,
        //[Display(Name = "HDMI 1.1")]
        //[EnumMember(Value = "HDMI 1.1")]
        //HDMI_1_1 = 1110,
        [Display(Name = "HDMI 1.2")]
        [EnumMember(Value = "HDMI 1.2")]
        HDMI_1_2 = 1120,
        //[Display(Name = "HDMI 1.2a")]
        //[EnumMember(Value = "HDMI 1.2a")]
        //HDMI_1_2a = 1121,
        [Display(Name = "HDMI 1.3")]
        [EnumMember(Value = "HDMI 1.3")]
        HDMI_1_3 = 1130,
        //[Display(Name = "HDMI 1.3a")]
        //[EnumMember(Value = "HDMI 1.3a")]
        //HDMI_1_3a = 1131,
        //[Display(Name = "HDMI 1.3b")]
        //[EnumMember(Value = "HDMI 1.3b")]
        //HDMI_1_3b = 1132,
        //[Display(Name = "HDMI 1.3b1")]
        //[EnumMember(Value = "HDMI 1.3b1")]
        //HDMI_1_3b1 = 1133,
        //[Display(Name = "HDMI 1.3c")]
        //[EnumMember(Value = "HDMI 1.3c")]
        //HDMI_1_3c = 1134,

        [Display(Name = "HDMI 1.4")]
        [EnumMember(Value = "HDMI 1.4")]
        HDMI_1_4 = 1140,
        [Display(Name = "HDMI 1.4 (MHL)")]
        [EnumMember(Value = "HDMI 1.4 (MHL)")]
        HDMI_1_4_MHL = 1141,
        //[Display(Name = "HDMI 1.4a")]
        //[EnumMember(Value = "HDMI 1.4a")]
        //HDMI_1_4a = 1141,
        //[Display(Name = "HDMI 1.4b")]
        //[EnumMember(Value = "HDMI 1.4b")]
        //HDMI_1_4b = 1142,
        [Display(Name = "HDMI MHL")]
        [EnumMember(Value = "HDMI MHL")]
        HDMI_MHL = 1200,
        [Display(Name = "HDMI 2")]
        [EnumMember(Value = "HDMI 2")]
        HDMI_2 = 2000,
        [Display(Name = "HDMI 2 (MHL)")]
        [EnumMember(Value = "HDMI 2 (MHL)")]
        HDMI_2_0_MHL = 2001,
        //[Display(Name = "HDMI 2.1")]
        //[EnumMember(Value = "HDMI 2.1")]
        //HDMI_2_1 = 2100,
        //[Display(Name = "HDMI 2.2")]
        //[EnumMember(Value = "HDMI 2.2")]
        //HDCP_2_2 = 2200,
        #endregion

        #region DP
        [Display(Name = "Display Port")]
        [EnumMember(Value = "Display Port")]
        DisplayPort = 3000,
        [Display(Name = "DisplayPort (input)")]
        [EnumMember(Value = "DisplayPort (input)")]
        DisplayPort_input = 3100,
        [Display(Name = "DisplayPort (input mini)")]
        [EnumMember(Value = "DisplayPort (input mini)")]
        DisplayPort_Input_mini = 3101,
        [Display(Name = "DisplayPort (output)")]
        [EnumMember(Value = "DisplayPort (output)")]
        DisplayPort_Output = 3110,
        [Display(Name = "DisplayPort (output MST)")]
        [EnumMember(Value = "DisplayPort (output MST)")]
        DisplayPort_Output_MST = 3111,
        //[Display(Name = "DisplayPort 1.0")]
        //[EnumMember(Value = "DisplayPort 1.0")]
        //DisplayPort_1 = 3100,
        //[Display(Name = "DisplayPort 1.1")]
        //[EnumMember(Value = "DisplayPort 1.1")]
        //DisplayPort_1_1 = 3110,
        [Display(Name = "DisplayPort 1.2")]
        [EnumMember(Value = "DisplayPort 1.2")]
        DisplayPort_1_2 = 3120,
        [Display(Name = "DisplayPort 1.2 mini")]
        [EnumMember(Value = "DisplayPort 1.2 mini")]
        DisplayPort_1_2_mini = 3121,
        [Display(Name = "DisplayPort 1.2 (MST)")]
        [EnumMember(Value = "DisplayPort 1.2 (MST)")]
        DisplayPort_1_2_MST = 3122,
        [Display(Name = "DisplayPort 1.2 (output)")]
        [EnumMember(Value = "DisplayPort 1.2 (output)")]
        DisplayPort_1_2_Output = 3123,
        [Display(Name = "DisplayPort 1.2 (output MST)")]
        [EnumMember(Value = "DisplayPort 1.2 (output MST)")]
        DisplayPort_1_2_Output_MST = 3124,
        //[Display(Name = "DisplayPort 1.2a")]
        //[EnumMember(Value = "DisplayPort 1.2a")]
        //DisplayPort_1_2a = 3122,
        //[Display(Name = "DisplayPort 1.3")]
        //[EnumMember(Value = "DisplayPort 1.3")]
        //DisplayPort_1_3 = 3130,
        [Display(Name = "DisplayPort 1.4")]
        [EnumMember(Value = "DisplayPort 1.4")]
        DisplayPort_1_4 = 3140,
        [Display(Name = "DisplayPort 1.4 (mini)")]
        [EnumMember(Value = "DisplayPort 1.4 (mini)")]
        DisplayPort_1_4_mini = 3141,
        #endregion

        #region Thunderbolt
        [Display(Name = "Thunderbolt 1")]
        [EnumMember(Value = "Thunderbolt 1")]
        Thunderbolt_1 = 4100,
        [Display(Name = "Thunderbolt 2")]
        [EnumMember(Value = "Thunderbolt 2")]
        Thunderbolt_2 = 4200,
        [Display(Name = "Thunderbolt 3")]
        [EnumMember(Value = "Thunderbolt 3")]
        Thunderbolt_3 = 4300,
        [Display(Name = "Thunderbolt 3 (input)")]
        [EnumMember(Value = "Thunderbolt 3 (input)")]
        Thunderbolt_3_input = 4400,
        [Display(Name = "Thunderbolt 3 (output)")]
        [EnumMember(Value = "Thunderbolt 3 (output)")]
        Thunderbolt_3_output = 4500,
        #endregion
    }
}
