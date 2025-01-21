namespace DemoProject.Models.Common;

public class GroupBrandItem
{
    public string MakeName { get; set; } = string.Empty;
    public string MakeNameGroup { get; set; } = string.Empty;
    public string MakeLogo { get; set; } = string.Empty;
    public string DetailNum { get; set; } = string.Empty;
    public string NameRus { get; set; } = string.Empty;
    public decimal OriginalMinPrice { get; set; }
    public string FormattedOriginalMinPrice { get; set; } = string.Empty;
    public decimal AnalogMinPrice { get; set; }
    public string FormattedAnalogMinPrice { get; set; } = string.Empty;
    public int? AddDays { get; set; }
    public string DestLogo { get; set; } = string.Empty;
    public string DelieveryRegionType { get; set; } = string.Empty;
}