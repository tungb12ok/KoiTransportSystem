using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto
{
    public class UpdateHealthStatusRequest
    {
        public string HealthStatus { get; set; }
        public List<HealthCertificateDto> Certificates { get; set; }
    }

}
