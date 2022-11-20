using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public struct ErrorsList
    {
        public string property { get; set; }
        public string error { get; set; }
    }
    public class Errors
    {
        public string message { get; set; }
        public List<ErrorsList> errors { get; set; }
    }
}
