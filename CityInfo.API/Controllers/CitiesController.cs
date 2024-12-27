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
    public JsonResult GetCities()
    {
      return new JsonResult(CitiesDataStore.Current.Cities);
    }

  }
}