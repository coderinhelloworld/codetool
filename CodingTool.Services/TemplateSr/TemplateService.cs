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
                var res= await context.Template.ToListAsync();
                return res;
            }
        }

        public async void DeleteById(int id)
        {
            using (var context = new SqlLiteDbContext())
            {
                context.Remove(await GetTemplateById(id));
                context.SaveChanges();
            }
        }

         public async Task<Template> GetTemplateById(int id)
        {
            using (var context = new SqlLiteDbContext())
            {
              var res=  await context.FindAsync<Template>(id);
                return res;
            }
        }


    }
}
