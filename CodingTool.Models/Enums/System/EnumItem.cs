using CodingTool.Models.Enums.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTool.Models
{
    public class EnumItem
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public Enum Enum { get; set; }

        public int Value { get; set; }
    }
}
