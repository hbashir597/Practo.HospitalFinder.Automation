namespace Practo.HospitalFinder.Playwright.Models;

public class HospitalResult
{
    public string Name { get; set; } = string.Empty;

    public double? Rating { get; set; }

    public bool IsOpen24x7 { get; set; }

    public string DetailsUrl { get; set; } = string.Empty;
}