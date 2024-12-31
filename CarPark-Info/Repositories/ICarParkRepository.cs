using Microsoft.EntityFrameworkCore;
using CarPark_Info.Models;

namespace CarPark_Info.Repositories
{
    // This interface is to define the additional methods required for the CarPark entity (ie. the filtering operations)

    // Can be scaled or expanded to include similar interfaces/repositories for other entities to include operations
    // in addition to the CRUD operations defined in the Generic Repository. 
    public interface ICarParkRepository : IGeneric<CarPark>
        {
            Task<List<CarPark>> OfferFreeParking();
            Task<List<CarPark>> OfferNightParking();
            Task<List<CarPark>> MeetHeightRequirement(double vehicleHeight);
        }
}