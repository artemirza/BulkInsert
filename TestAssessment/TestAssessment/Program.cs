using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using DataAccessLayer.Data;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

namespace TestAssessment
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var dbContext = new AppDbContext();
            ITripRepository tripRepository = new TripRepository(dbContext);
            ICsvProcessService csvService = new CsvProcessService();
            ITripService tripService = new TripService(tripRepository, csvService);

            var csvFilePath = "F:/study/textfile/sample-cab-data.csv";
            var csvDublicateFilePath = "F:/study/textfile/duplicates.csv";

            await tripService.BulkInsertCsv(csvFilePath, csvDublicateFilePath);
        }
    }
}
