using CoodingTool.Data.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoodingTool.Data
{
    public class SqlLiteDbContext : DbContext
    {
        public DbSet<Template> Template { get; set; }
        public DbSet<SettingInfo> SettingInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=mydatabase.db");
        }
    }
}
