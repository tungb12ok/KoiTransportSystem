using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Enums
{
    public enum OrderStatus
    {
        WaitingForPayment,
        Pending,        
        HealthCheck,    
        HealthChecked,  
        Packing,       
        Packed,         
        InTransit,     
        Delivered,   
        Success,
        Canceled     
    }   
}
