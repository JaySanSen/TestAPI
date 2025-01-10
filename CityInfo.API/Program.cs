
//A web application needs a host to run. This host is created by WebApplication.CreateBuilder. WebApplication implements IApplication builder.
using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



//Services collection on builder is what is used to configure services.
// This is the built in dependency injection.


//builder.Services.AddControllers();
// This calls registers services that are required to use controllers.

builder.Services.AddControllers(options =>{
  options.ReturnHttpNotAcceptable = true;// This will return 406 Not Acceptable if the format requested is not supported by the application. For example client request xml but only json is supported then the 406 error will be display if Accept Header is application/xml
}).AddXmlDataContractSerializerFormatters();

//AddXmlDataContractSerializerFormatters will add xml format as additional supported format other than json format (json is usually default).

//Adding problem details. This is useful when multiple servers are used and can be used to check which server is returning error.
builder.Services.AddProblemDetails(options =>
{
  options.CustomizeProblemDetails = ctx => //just shortform for context
  {
    ctx.ProblemDetails.Extensions.Add("Server", Environment.MachineName);
    ctx.ProblemDetails.Extensions.Add("test","test");
  };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();// Used for Swagger services
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

//Once all services are configured the application can be built
// This will create an object of web application which implements IApplicationBuilder.
var app = builder.Build();

/* Configure the HTTP request pipeline. This will inform .NET how to handle http requests also known as middleware.
Middle ware is components which handle these requests.
Middle ware is software component which are assemblies in an application pipeline to handle requests and response. Order in which we add middle ware is important.
*/

/*checks whether we are running in Development Environment. Only if we are running in Developement environment Swagger middleware is triggered.*/
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();// Swagger related middleware
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
  endpoints.MapControllers();
});


// app.MapControllers();
/* This is shortcut and used in current modern version of .NET.
Older versions user app.UseRouting() and app.UseEndpoints().
*/


app.Run();
