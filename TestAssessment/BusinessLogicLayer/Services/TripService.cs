using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepository;
        private readonly ICsvProcessService _csvService;

        public TripService(ITripRepository tripRepository, ICsvProcessService csvService)
        {
            _tripRepository = tripRepository;
            _csvService = csvService;
        }

        public async Task BulkInsertCsv(string filePath, string duplicatesFilePath)
        {
            var trips = _csvService.ReadCsv(filePath);

            var uniqueTrips = RemoveDuplicates(trips, duplicatesFilePath);

            await _tripRepository.BulkInsertTrips(uniqueTrips);
        }

        private List<TripData> RemoveDuplicates(List<TripData> trips, string duplicatesFilePath)
        {
            var groupedTrips = trips.GroupBy(t => new { t.tpep_pickup_datetime, t.tpep_dropoff_datetime, t.passenger_count });

            var uniqueTrips = groupedTrips.Select(g => g.First()).ToList();

            var duplicateTrips = groupedTrips
                .Where(g => g.Count() > 1)
                .SelectMany(g => g.Skip(1))
                .ToList();

            _csvService.WriteCsv(duplicatesFilePath, duplicateTrips); 

            return uniqueTrips;
        }
    }
}
