using CarPark_Info.Models;
using CarPark_Info.Repositories;
using Microsoft.AspNetCore.Mvc;

// This file contains the API implementations that can be tested using Swagger and can later be used for the frontend to 
// develop the user stories

namespace CarPark_Info.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarParkController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarParkController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/CarPark
        [HttpGet("carparks")]
        public async Task<IActionResult> GetAllCarParks()
        {
            // To get all carparks to view the entire populated table for testing and validation purposes
            var carParks = await _unitOfWork.CarParkRepository.GetAllAsync();
            return Ok(carParks);
        }

        // GET: api/User
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            // To get all users to view the entire populated table for testing and validation purposes
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            return Ok(users);
        }

        // GET: api/Favourite
        [HttpGet("favourites")]
        public async Task<IActionResult> GetAllFavourites()
        {
            // To get all favourites to view the entire populated table for testing and validation purposes
            var favourites = await _unitOfWork.FavouriteRepository.GetAllAsync();
            return Ok(favourites);
        }


        // GET: api/CarPark/FreeParking
        [HttpGet("FreeParking")]
        public async Task<IActionResult> GetFreeParkingCarParks()
        {
            // To filter carparks that provide free parking:
            var freeParkingCarParks = await _unitOfWork.CarParkRepository.OfferFreeParking();
            return Ok(freeParkingCarParks);
        }

        // GET: api/CarPark/NightParking
        [HttpGet("NightParking")]
        public async Task<IActionResult> GetNightParkingCarParks()
        {
            // To filter carparks that provide night parking:
            var nightParkingCarParks = await _unitOfWork.CarParkRepository.OfferNightParking();
            return Ok(nightParkingCarParks);
        }

        // GET: api/CarPark/Height/{vehicleHeight}
        [HttpGet("Height/{vehicleHeight}")]
        public async Task<IActionResult> GetVehicleHeightCarParks(double vehicleHeight)
        {
            // To filter carparks that meet the vehicle height requirement
            try
            {
                var carParks = await _unitOfWork.CarParkRepository.MeetHeightRequirement(vehicleHeight);
                return Ok(carParks);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/AddFavourite/{UserId}/{car_park_no}
        [HttpPost("AddFavourite/{UserId}/{car_park_no}")]
        public async Task<IActionResult> AddToFavourites(string UserId, string car_park_no)
        {
            /* Allows users to favourite a specific carpark. 
            Currently, for API testing purposes, we have to input the user_id and car_park_no as route parameters. 
            But this endpoint can be used for frontend to allow the users to favourite a carpark in a more intuitive way. 
            */
            var favourite = new Favourite
            {
                UserId = UserId,
                car_park_no = car_park_no
            };

            await _unitOfWork.FavouriteRepository.AddAsync(favourite);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(AddToFavourites), new { UserId = favourite.UserId, car_park_no = favourite.car_park_no }, favourite);
        }
    }
}
