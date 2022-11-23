using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkAlert
{
    public class Calendar
    {
        public int Id { get; set; }
        public ICollection<Work> Works { get; set; } = new List<Work>();
        public int UserId { get; set; }
    }
}
