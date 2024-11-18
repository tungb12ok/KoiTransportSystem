using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public partial class Vehicle
    {
        public Vehicle()
        {
            TransportProcesses = new HashSet<TransportProcess>();
        }

        public int VehicleId { get; set; }
        public string LicensePlate { get; set; } = null!;
        public decimal Capacity { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<TransportProcess> TransportProcesses { get; set; }
    }
}
