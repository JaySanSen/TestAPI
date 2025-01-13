using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FilesController : ControllerBase
  {
    [HttpGet("{fileId}")]
    public ActionResult GetFile(string fileId)
    {
      var pathToFile = "";
      if(!System.IO.File.Exists(pathToFile)){
        return NotFound();
      }

      var bytes = System.IO.File.ReadAllBytes(pathToFile);
      return File(bytes,"text/plain",Path.GetFileName(pathToFile));
    }

    [HttpPost]
    public async Task<ActionResult> CreateFile(IFormFile file)
    {
      if(file.Length == 0 || file.ContentType != "application/pdf" || file.Length > 20971520)
      {
        return BadRequest("Invalid file type or file size");
      }

      var path = Path.Combine(Directory.GetCurrentDirectory(),$"uploaded_file_{Guid.NewGuid()}.pdf");
      using(var stream = new FileStream(path,FileMode.Create))
      {
        await file.CopyToAsync(stream);
      }
      return Ok("File uploaded successfully");
    }
  }
}