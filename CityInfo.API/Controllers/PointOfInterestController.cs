using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
  [ApiController]
  //When we call the api we will need to provide the city ID . It will be a mandatory field since it is needed for the route.
  [Route("api/cities/{cityId}/[controller]")]
  public class PointOfInterestController : ControllerBase
  {
    [HttpGet]
    public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
    {
      var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
      if (city == null)
      {
        return NotFound();
      }
      else
      {
        return Ok(city.PointOfInterest);
      }
    }

    [HttpGet("{pointofinterestid}")]
    public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
    {
      var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
      if (city == null)
      {
        return NotFound();
      }
      var pointOfInterest = city.PointOfInterest.FirstOrDefault(p => p.Id == pointOfInterestId);
      if (pointOfInterest == null)
      {
        return NotFound();
      }

      return Ok(pointOfInterest);
    }
  }
}