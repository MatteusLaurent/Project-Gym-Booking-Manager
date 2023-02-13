using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Booking_Manager.Logger
{
    internal class GBMLogger
    {
        private readonly string _logFilePath;

        public GBMLogger(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void LogActivity(string message)
        {
            using (StreamWriter sw = File.AppendText(_logFilePath))
            {
                sw.WriteLine($"{DateTime.Now} - {message}");
            }
        }
    }
}
