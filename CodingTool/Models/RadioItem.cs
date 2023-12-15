using CodingTool.Models.Enums.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTool.Models
{
    public class RadioItem
    {
        public string Content { get; set; }
        public bool  IsCheck { get; set; }

        public NamingCase NamingCase { get; set; }
    }
}
