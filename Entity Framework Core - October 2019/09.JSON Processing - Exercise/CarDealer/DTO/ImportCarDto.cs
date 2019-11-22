using System;
using System.Collections.Generic;
using System.Text;

namespace CarDealer.DTO
{
    public class ImportCarDto
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public long TravelDistance { get; set; }

        public int[] PartsId { get; set; }

    }
}
