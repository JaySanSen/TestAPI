namespace CityInfo.API.Models
{
  public class CityDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int NumberOfPointsOfInterest
    {
      get
      {
        return this.PointOfInterest.Count;
      }
    }
    public ICollection<PointOfInterestDto> PointOfInterest { get; set; } = new List<PointOfInterestDto>();
  }
}