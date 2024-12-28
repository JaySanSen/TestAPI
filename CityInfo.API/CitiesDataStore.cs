using CityInfo.API.Models;

namespace CityInfo.API
{
  public class CitiesDataStore
  {
    public List<CityDto> Cities { get; set; }
    public static CitiesDataStore Current { get; } = new CitiesDataStore();
    public CitiesDataStore()
    {
      this.Cities = new List<CityDto>(){
        new CityDto(){
          Id = 1,
          Name = "New York City",
          Description = "The one with the big park",
          PointOfInterest = new List<PointOfInterestDto>(){
            new PointOfInterestDto(){
              Id = 1,
              Name = "Central Park",
              Description = "The most visited urban park in the US"
            },
            new PointOfInterestDto(){
              Id = 2,
              Name = "Empire State Building",
              Description = "A 102 Story skyscraper located in Manhattan"
            }
          }
        },
        new CityDto(){
          Id = 2,
          Name = "Antwerp",
          Description = "The one with the cathedral that was never finished",
          PointOfInterest = new List<PointOfInterestDto>(){
            new PointOfInterestDto(){
              Id = 3,
              Name = "Cathedral of our Lady",
              Description = "A Gothic style cathedral conceived by architects"
            },
            new PointOfInterestDto(){
              Id = 4,
              Name = "Antwerp Central Station",
              Description = "The finest example of railway architecture"
            }
          }
        },
        new CityDto(){
          Id = 3,
          Name = "Paris",
          Description = "The one with the big tower",
          PointOfInterest = new List<PointOfInterestDto>(){
            new PointOfInterestDto(){
              Id = 5,
              Name = "Eiffel Tower",
              Description = "A wrought iron lattice tower on the Champ de Mars"
            },
            
            new PointOfInterestDto(){
              Id = 6,
              Name = "The Louvre",
              Description = "The world's largest museum"
            }
          }
        }
      };
    }
  }
}
