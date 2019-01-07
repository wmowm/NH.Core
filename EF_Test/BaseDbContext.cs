using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF_Test
{
    public class BaseDbContext:DbContext
    {

        protected string connectionName;
        public BaseDbContext(string connName)
        {
            this.connectionName = connName;
            //使用NoTracking提升查询速度
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<Dict> Dict { get; set; }
        public DbSet<DictType> DictType { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dict>().ToTable("Dict");
            modelBuilder.Entity<DictType>().ToTable("DictType");


            var properties = modelBuilder.Model.GetEntityTypes()
                   .SelectMany(t => t.GetProperties())
                   .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?));
            foreach (var property in properties)
            {
                if (property.ClrType == typeof(decimal))
                {
                    property.Relational().ColumnType = "decimal(28, 16)";
                }
                else
                {
                    property.Relational().ColumnType = "decimal?(28, 16)";
                }

            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.ConfigureWarnings(opt => opt.Ignore(RelationalEventId.AmbientTransactionWarning));
            switch ("mysql")
            {
                case "mysql":
                    optionsBuilder.UseMySql("server=193.112.104.103;database=tibos;uid=root;pwd=123456;port=3308;Charset=utf8;");
                    break;
                case "sqlserver":
                    break;
                case "sqlite":
                    break;
            }
        }
    }
}
