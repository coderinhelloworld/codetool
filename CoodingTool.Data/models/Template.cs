using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoodingTool.Data.models
{
    public class Template
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Group { get; set; }
        public string Content { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
