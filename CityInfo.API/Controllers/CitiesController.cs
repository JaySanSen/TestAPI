using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  //the [controller] will be the controller name here in this case cities. Cities comes from CitiesController but Controller word is ignored.
  //you can also hardcode api/cities. This will maintain consistency in case class/controller name changes.
  public class CitiesController : ControllerBase
  {
    // [HttpGet("api/[controller]")] 
    //you can mention like this for each HTTP verb. but to maintain consistency across all the HTTP verbs use Route attribute at class/controller level
    [HttpGet]
    public ActionResult<IEnumerable<CityDto>> GetCities()
    {
      // return new JsonResult(CitiesDataStore.Current.Cities);
      return Ok(CitiesDataStore.Current.Cities);
    }
    [HttpGet("{id}")]
    public ActionResult<CityDto> GetCity(int id)
    {
      // return new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(cityObject => cityObject.Id == id) != null ? CitiesDataStore.Current.Cities.FirstOrDefault(cityObject => cityObject.Id == id) : "No city found");
      // return new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(cityObject => cityObject.Id == id));
      var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == id);
      if (cityToReturn == null)
      {
        var problemDetails = new ProblemDetails()
        {
          Status = 404,
          Title = "Not Found",
          Detail = $"No city with ID {id} was found",
          //This will return /api/Cities/10
          Instance = HttpContext.Request.Path
        };
        return NotFound(problemDetails);
      }
      return Ok(cityToReturn);
    }

  }
}