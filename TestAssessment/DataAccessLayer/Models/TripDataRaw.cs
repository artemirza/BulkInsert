using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class TripDataRaw
    {
        public string tpep_pickup_datetime { get; set; }
        public string tpep_dropoff_datetime { get; set; }
        public string passenger_count { get; set; }
        public string trip_distance { get; set; }
        public string store_and_fwd_flag { get; set; }
        public string PULocationID { get; set; }
        public string DOLocationID { get; set; }
        public string fare_amount { get; set; }
        public string tip_amount { get; set; }
    }
}
