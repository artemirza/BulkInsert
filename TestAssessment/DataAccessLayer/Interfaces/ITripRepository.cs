﻿using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ITripRepository
    {
        Task BulkInsertTrips(IEnumerable<TripData> trips);
    }
}
