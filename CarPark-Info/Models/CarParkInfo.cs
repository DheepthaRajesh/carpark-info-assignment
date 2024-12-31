namespace CarPark_Info.Models
{

/* We define our 3 tables and their schemas:

1. CarPark - contains information about carparks with car_park_no as primary key. (This table is derived from the csv file 
provided in the assignment)

2. User - contains information about users with userId as primary key. This table needs to be populated (currently populated 
with 3 test users in the CarParkContext.cs file). This table is to facilitate the user stories where a user can add a 
specific carpark as a favourite.

3. Favourite - contains information about users and their favourited carparks with car_park_no and userId as foreign keys. 
    This is a junction table to represent the many to many relationship between User and Carpark where I have made an 
    assumption that a single user can favourite multiple carparks and a single carpark can be favourited by many users.


*/
public class CarPark
{
    public string car_park_no { get; set; } = string.Empty;
    public string address { get; set; }  = string.Empty;
    public double x_coord { get; set; }  
    public double y_coord { get; set; } 
    public string car_park_type { get; set; }  = string.Empty;
    public string type_of_parking_system { get; set; }  = string.Empty;
    public string short_term_parking { get; set; }  = string.Empty;
    public string free_parking { get; set; }  = string.Empty;
    public string night_parking { get; set; }  = string.Empty;
    public int car_park_decks { get; set; } 
    public double gantry_height { get; set; }  
    public string car_park_basement { get; set; }  = string.Empty;

    public List<User> Users { get; } = new();
    public List<Favourite> Favourites { get; } = new();

}


    public class User
{
    public string UserId { get; set; }  = string.Empty;
    public string username { get; set; }  = string.Empty;
    public string email { get; set; }  = string.Empty;

    public List<CarPark> CarParks { get; } = new();
    public List<Favourite> Favourites { get; } = new();

}

    public class Favourite
{
    public string UserId { get; set; }  = string.Empty;
    public string car_park_no { get; set; }  = string.Empty;

    public User User { get; set; } = new();

    public CarPark CarPark { get; set; } = new();

}

}
