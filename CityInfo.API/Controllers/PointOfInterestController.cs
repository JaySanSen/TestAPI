using CityInfo.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
  [ApiController]
  /*
  When we call the api we will need to provide the city ID . It will be a mandatory field since it is needed for the route.
  */
  [Route("api/cities/{cityId}/[controller]")]
  public class PointOfInterestController : ControllerBase
  {
    private readonly ILogger<PointOfInterestController> _logger;
    public PointOfInterestController(ILogger<PointOfInterestController> logger)
    {
      _logger = logger?? throw new ArgumentNullException(nameof(logger));
    }

    [HttpGet]
    public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
    {
      var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
      if (city == null)
      {
        _logger.LogInformation("City was not found");
        return NotFound();
      }
      else
      {
        return Ok(city.PointOfInterest);
      }
    }

    [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
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
    [HttpPost]
    public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId, PointOfInterestForCreationDto pointOfInterest)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest();
      }
      var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
      if (city == null)
      {
        return NotFound();
      }
      var maxPointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointOfInterest).Max(p => p.Id);
      var finalPointOfInterest = new PointOfInterestDto()
      {
        Id = ++maxPointOfInterestId,
        Name = pointOfInterest.Name,
        Description = pointOfInterest.Description
      };
      city.PointOfInterest.Add(finalPointOfInterest);
      return CreatedAtRoute("GetPointOfInterest", new
      {
        cityId = cityId,
        pointOfInterestId = finalPointOfInterest.Id
      }, finalPointOfInterest);
    }
    [HttpPut]
    public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestForUpdateDto pointOfInterest)
    {
      var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
      if (city == null)
      {
        return NotFound();
      }
      var pointOfInterestFromStore = city.PointOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);
      if (pointOfInterestFromStore == null)
      {
        return NotFound();
      }
      pointOfInterestFromStore.Name = pointOfInterest.Name;
      pointOfInterestFromStore.Description = pointOfInterest.Description;

      return NoContent();
    }

    [HttpPatch]
    public ActionResult PartiallyUpdatedPointOfInterest(int cityId, int pointOfInterestId, JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
    {
      var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
      if (city == null)
      {
        return NotFound();
      }
      var pointOfInterestFromStore = city.PointOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);
      if (pointOfInterestFromStore == null)
      {
        return NotFound();
      }
      var pointOfInterestToPatch = new PointOfInterestForUpdateDto
      {
        Name = pointOfInterestFromStore.Name,
        Description = pointOfInterestFromStore.Description
      };
      patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
      pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

      return NoContent();
    }

    [HttpDelete]
    public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
    {
      var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
      if (city == null)
      {
        return NotFound();
      }
      var pointOfInterestFromStore = city.PointOfInterest.FirstOrDefault(c => c.Id == pointOfInterestId);
      if (pointOfInterestFromStore == null)
      {
        return NotFound();
      }
      city.PointOfInterest.Remove(pointOfInterestFromStore);
      return NoContent();
    }

  }
}