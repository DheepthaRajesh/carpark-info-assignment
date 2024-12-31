using Microsoft.EntityFrameworkCore;
using CarPark_Info.Models;

namespace CarPark_Info.Repositories.Core
{
    /* This file contains the implementation of the CarPark repository where we define our implementation of the filter 
    operations. 

    This allows for scalability as more filter functions can be added here according to user and system preferences. 

    Currently, the filtering operation is only for the CarPark entity but this can be expanded to include filtering for the 
    other 2 entities as well (User and Favourite) in addition to the CRUD operations
    */
    public class CarParkRepository : GenericRepository<CarPark>, ICarParkRepository
    {
        private readonly CarParkContext _context;

        public CarParkRepository(CarParkContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<CarPark>> OfferFreeParking()
        {
            // To filter the list of carparks that offer free parking:
            // If the value is not 'NO', then the carpark offers free parking (eg. 'SUN & PH FR 7AM-10.30PM')

            return await FilterAsync(c => c.free_parking != "NO"); 
        }

        public async Task<List<CarPark>> OfferNightParking()
        {
            /* To filter the list of carparks that offer night parking:
            If the value is 'YES', then the carpark offers night parking */

            return await FilterAsync(c => c.night_parking == "YES");
        }

        public async Task<List<CarPark>> MeetHeightRequirement(double vehicleHeight)
        {
            /* To filter the list of carparks that meet the vehicle height requirements:
            If the vehicle height is lesser than or equal to the carpark's gantry height, then it meets the vehicle height 
            requirements. 
            
            Here we assume that carparks with gantry_height = 0.0 mean open car parks or barrier free car parks. Hence 
            we include these carparks as those that meet vehicle height requirements as well (this can be changed here) */

            // Ensure valid vehicleHeight input
            if (vehicleHeight <= 0)
            {
                throw new ArgumentException("Vehicle height must be greater than 0.");
            }

            return await FilterAsync(c => c.gantry_height >= vehicleHeight || c.gantry_height == 0.00);
        }

    }
}