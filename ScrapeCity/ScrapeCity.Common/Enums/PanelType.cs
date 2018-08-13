using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ScrapeCity.Common.Enums
{
    public enum PanelType
    {
        [Display(Name = "Not Available")]
        [EnumMember(Value = "Not Available")]
        NotAvailable = 0,

        #region TN (Twisted Nematic)
        [Display(Name = "TN")]
        [EnumMember(Value = "TN")]
        TN = 100,
        #endregion

        #region IPS ( In-Plane Switching )
        [Display(Name = "IPS")]
        [EnumMember(Value = "IPS")]
        IPS = 200,
        [Display(Name = "S IPS")]
        [EnumMember(Value = "S IPS")]
        S_IPS = 210,
        [Display(Name = "H IPS")]
        [EnumMember(Value = "H IPS")]
        H_IPS = 220,
        [Display(Name = "E IPS")]
        [EnumMember(Value = "E IPS")]
        E_IPS = 230,
        [Display(Name = "P IPS")]
        [EnumMember(Value = "P IPS")]
        P_IPS = 240,
        [Display(Name = "AH IPS")]
        [EnumMember(Value = "AH IPS")]
        AH_IPS = 250,
        [Display(Name = "AH3 IPS")]
        [EnumMember(Value = "AH3 IPS")]
        AH3_IPS = 250,
        #endregion

        #region VA ( Vertical Alignment)
        [Display(Name = "VA")]
        [EnumMember(Value = "VA")]
        VA = 300,
        [Display(Name = "SVA")]
        [EnumMember(Value = "SVA")]
        SVA = 310,
        [Display(Name = "S-PVA")]
        [EnumMember(Value = "S-PVA")]
        S_PVA = 320,
        [Display(Name = "MVA")]
        [EnumMember(Value = "MVA")]
        MVA = 330,
        [Display(Name = "Super MVA")]
        [EnumMember(Value = "Super MVA")]
        Super_MVA = 340,
        [Display(Name = "AMVA")]
        [EnumMember(Value = "AMVA")]
        AMVA = 350,
        [Display(Name = "AMVA+")]
        [EnumMember(Value = "AMVA+")]
        AMVA_Plus = 360,
        [Display(Name = "AMVA+ (SNB)")]
        [EnumMember(Value = "AMVA+ (SNB)")]
        AMVA_Plus_SNB = 370,
        [Display(Name = "AMVA+ (D2D)")]
        [EnumMember(Value = "AMVA+ (D2D)")]
        AMVA_Plus_D2D = 380,
        [Display(Name = "AMVA3")]
        [EnumMember(Value = "AMVA3")]
        AMVA_3 = 390,
        #endregion

        #region AHVA ( Advanced Hyper-Viewing Angle)
        [Display(Name = "AHVA")]
        [EnumMember(Value = "AHVA")]
        AHVA = 400,
        [Display(Name = "AHVA IPS")]
        [EnumMember(Value = "AHVA IPS")]
        AHVA_IPS = 410,
        #endregion

        #region PLS ( Plane to Line Switching) 
        [Display(Name = "PLS")]
        [EnumMember(Value = "PLS")]
        PLS = 500,
        [Display(Name = "AD-PLS")]
        [EnumMember(Value = "AD-PLS")]
        AD_PLS = 510,
        #endregion

        #region ADS ( Advanced Super Dimension Switch) 
        [Display(Name = "ADS")]
        [EnumMember(Value = "ADS")]
        ADS = 600,
        #endregion

        #region IGZO ( Indium Gallium Zinc Oxide) 
        [Display(Name = "IGZO")]
        [EnumMember(Value = "IGZO")]
        IGZO = 700,
        #endregion

        #region OLED (Organic Light-Emitting Diode)
        [Display(Name = "OLED")]
        [EnumMember(Value = "OLED")]
        OLED = 800,
        #endregion
    }
}
