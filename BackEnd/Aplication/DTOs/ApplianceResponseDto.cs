namespace BackEnd.Aplication.DTOs;

public class ApplianceResponseDto
{
    public int UserId { get; set; }
    public string CityName { get; set; }
    public string CountryName { get; set; }
    public byte[] IdentityCard { get; set; }
    public bool IsApproved { get; set; }
}