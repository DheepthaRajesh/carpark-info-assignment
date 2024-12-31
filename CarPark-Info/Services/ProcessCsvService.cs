using CarPark_Info.Repositories;
using CarPark_Info.Models;
using Microsoft.EntityFrameworkCore;
using EFCore.BulkExtensions; 
using CsvHelper;
using System.Globalization;
using System.Configuration;
using System.IO;

namespace CarPark_Info.Services
{
    /* This file is for processing the csv file into the database.
    It starts a transaction for the entire file processing and the entire file rolls back in case of an error. 

    Since the assignment mentions the csv as a daily delta file, we aim to insert the record if doesnt exist and if the record
    exists, we will update it in the database. And to account for the large size of the dataset, we use EFCore.BulkExtensions's 
    BulkInsertOrUpdate method. 
    */

    public class ProcessCsvService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProcessCsvService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task ProcessCsvFile()
        {
            // Start a transaction for the entire file processing
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                // The csv file is stored under the DataSource/ folder in the root directory
                var currentDir = Directory.GetCurrentDirectory();
                var dataSourcePath = Path.Combine(currentDir, "DataSource", "hdb-carpark-information-20220824010400.csv");
                Console.WriteLine($"Computed file path: {dataSourcePath}");

                // var projectDirectory = Directory.GetParent(AppContext.BaseDirectory)?.Parent?.Parent?.FullName;
                // var dataSourcePath = Path.Combine(projectDirectory, "DataSource", "hdb-carpark-information-20220824010400.csv");
                using (var reader = new StreamReader(dataSourcePath))
                // We use CsvHelper to read and process the csv file into the CarPark table
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var csvRecords = csv.GetRecords<CarPark>().ToList();

        
                    // Convert CSV records into a list
                    var carParkList = csvRecords.Select(record => new CarPark
                    {
                        car_park_no = record.car_park_no,
                        address = record.address,
                        x_coord = record.x_coord,
                        y_coord = record.y_coord,
                        car_park_type = record.car_park_type,
                        type_of_parking_system = record.type_of_parking_system,
                        short_term_parking = record.short_term_parking,
                        free_parking = record.free_parking,
                        night_parking = record.night_parking,
                        car_park_decks = record.car_park_decks,
                        gantry_height = record.gantry_height,
                        car_park_basement = record.car_park_basement
                    }).ToList();

                    Console.WriteLine($"Processing {csvRecords.Count} records from CSV.");

                    // Bulk Insert or Update
                    await _unitOfWork.BulkUpsertAsync(carParkList);

                    await _unitOfWork.SaveAsync(); // Save changes to the database
                    await _unitOfWork.CommitTransactionAsync(); // Commit the transaction
            }
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollBackTransactionAsync(); // Rollback the entire file processing
                Console.WriteLine($"Error processing file: {ex.Message}");
                throw; // Rethrow the exception to propagate it further if needed
            }
        }
        
        
    }
}
