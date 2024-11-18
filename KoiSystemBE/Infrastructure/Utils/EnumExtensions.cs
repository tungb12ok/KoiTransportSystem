using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utils
{
    public static class EnumExtensions
    {
        public static List<EnumDto> GetEnumList<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T))
                       .Cast<T>()
                       .Select(e => new EnumDto
                       {
                           Name = e.ToString(),
                           Value = Convert.ToInt32(e)
                       }).ToList();
        }
    }

    public class EnumDto
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
