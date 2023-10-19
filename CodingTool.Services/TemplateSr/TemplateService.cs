using CoodingTool.Data;
using CoodingTool.Data.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CodingTool.Services.TemplateSr
{
    public class TemplateService
    {

        public async Task<List<Template>> GetListAsync()
        {
            using (var context = new SqlLiteDbContext())
            {
                return await context.Template.ToListAsync();
            }
        }


    }
}
