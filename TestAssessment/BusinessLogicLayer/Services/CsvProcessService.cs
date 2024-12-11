using BusinessLogicLayer.Interfaces;
using CsvHelper;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class CsvProcessService : ICsvProcessService
    {
        public List<TripData> ReadCsv(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var trips = csv.GetRecords<TripDataRaw>()
                .Select(record => new TripData
                {
                    tpep_pickup_datetime = !string.IsNullOrWhiteSpace(record.tpep_pickup_datetime)
                        ? ConvertToUtc(DateTime.ParseExact(record.tpep_pickup_datetime.Trim(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)) // Изменение тут
                        : DateTime.MinValue,

                    tpep_dropoff_datetime = !string.IsNullOrWhiteSpace(record.tpep_dropoff_datetime)
                        ? ConvertToUtc(DateTime.ParseExact(record.tpep_dropoff_datetime.Trim(), "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture)) // Изменение тут
                        : DateTime.MinValue,

                    passenger_count = int.TryParse(record.passenger_count?.Trim(), out var passengerCount)
                        ? passengerCount
                        : default,

                    trip_distance = double.TryParse(record.trip_distance?.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var tripDistance)
                        ? tripDistance
                        : default,

                    store_and_fwd_flag = !string.IsNullOrWhiteSpace(record.store_and_fwd_flag)
                        ? (record.store_and_fwd_flag.Trim() == "N" ? "No" : "Yes")
                        : "No",

                    PULocationID = int.TryParse(record.PULocationID?.Trim(), out var puLocationId)
                        ? puLocationId
                        : default,

                    DOLocationID = int.TryParse(record.DOLocationID?.Trim(), out var doLocationId)
                        ? doLocationId
                        : default,

                    fare_amount = decimal.TryParse(record.fare_amount?.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var fareAmount)
                        ? fareAmount
                        : default,

                    tip_amount = decimal.TryParse(record.tip_amount?.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var tipAmount)
                        ? tipAmount
                        : default
                })
                .ToList();

            return trips;
        }

        public void WriteCsv(string duplicatesFilePath, List<TripData> duplicateTrips)
        {
            if (!duplicateTrips.Any()) return;

            bool fileExists = File.Exists(duplicatesFilePath);

            using var writer = new StreamWriter(duplicatesFilePath, append: true);
            using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);

            if (!fileExists)
            {
                csv.WriteHeader<TripData>();
                csv.NextRecord();
            }

            csv.WriteRecords(duplicateTrips);
        }

        private static DateTime ConvertToUtc(DateTime estDateTime)
        {
            TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            return TimeZoneInfo.ConvertTimeToUtc(estDateTime, estZone);
        }
    }
}
