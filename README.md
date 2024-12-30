# Carpark-Info Assignment

This project implements a carpark management system using **.NET 6.0, SQLite with Entity Framework Core**, and follows best coding practices to create a robust, maintainable, and extensible solution. It processes daily delta CSV files, stores the data in a normalized database schema, and provides APIs to fulfill user stories such as filtering carparks and managing user favorites.

## Key Features
* **Batch Job**: Processes a daily delta CSV file using CsvReader. The job performs bulk inserts/updates with rollback in case of errors.
* **Normalized Database Design**: Applies 1NF, 2NF, and 3NF to ensure flexibility, scalability, and referential integrity.
* **User Stories**: Implements APIs to:
        Filter carparks by criteria (free parking, night parking, height requirements).
        Add favorite carparks for users.
* **Extensible Architecture**: Supports changes such as switching the data access technology or interface file format (e.g., CSV to JSON) using the repository pattern and unit of work. 
* **Swagger Documentation**: Detailed API documentation to assist frontend developers.
* **Secure Coding Practices**: Implements validation, error handling, and safeguards against common vulnerabilities.
* **Unit Testing**: Code is designed for testability using the repository pattern and unit of work.
* **Performance and Query Optimization**: Handles large datasets using bulk operations and normalization reduces data dependency and redudancy. 

## Assignment Requirements & Solutions

### 1. Database Schema & Normalization

The schema includes three tables:

* **Carpark Table** : Stores carpark details with **car_park_no** as the primary key. The information processed from the csv file is fed into the CarPark table. 
* **Users Table** : Stores user information with **user_id** as the primary key. Manually populated for testing purposes. 
* **Favourites Table** : A junction table (representing the many to many relationship between Users and CarParks) with composite keys **(user_id, car_park_no)** to link users with multiple favorite carparks. This assumes that multiple users can favourite the same carpark.


Advantages of having a seperate junction table to store the favourites instead of a favourites column in the Users or CarPark table:

* **Flexibility**: Users can select multiple favorite carparks.
* **Scalability**: Avoids adding multiple columns for favorites, which would limit options.
* **Referential Integrity**: Ensures valid relationships between users and carparks.


This data schema adheres to **1NF, 2NF, and 3NF normalization techniques**, ensuring atomic data entries, eliminating partial dependencies, and avoiding transitive dependencies. This design improves performance by reducing redundancy, enhancing scalability, and maintaining data integrity with a well-structured and efficient schema.

#### ER diagram

### 2. Batch job design

* **File Processing**: ProcessCsvService.cs in the Services folder parses the daily delta CSV file using the **CsvHelper** library and processes the records into the CarPark table. 
* **Delta Logic**: Checks for existing records to update; otherwise, inserts new ones using the **BulkInsertOrUpdate function of EFCore.BulkExtensions**. 
* **Error Handling**: Implements rollback for the entire file in case of errors by treating it as a single transaction. 
* **Performance**: Uses bulk insert/update operations for efficiency.



