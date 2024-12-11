using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface ICsvProcessService
    {
        List<TripData> ReadCsv(string filePath);
        void WriteCsv(string duplicatesFilePath, List<TripData> duplicateTrips);
    }
}
