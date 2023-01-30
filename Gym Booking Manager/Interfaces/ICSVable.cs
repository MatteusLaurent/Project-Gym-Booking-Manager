using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// CSV = Comma-Separated Values.
// The current database implementation uses the .csv format for local files (just text with "fields" separated by commas, and "rows" with a linebreak).
// No need to get fancy for now.

namespace Gym_Booking_Manager
{
    internal interface ICSVable
    {
        public string CSVify();
    }
}

// However, for the curious, a library to look into for more robust .NET CSV (comma-separated values) file interaction: https://joshclose.github.io/CsvHelper/