using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace Infrastructure.Data
{
    public class DataContext : DbContext
    {
        private const string TypeCreated = "Created";
        private const string TypeModified = "Modified";
        private const string DateFormat = "dd/MM/yyyy HH:mm:ss";

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Tracker> Logs { get; set; }

        public override int SaveChanges()
        {
            var logEntries = new List<Tracker>();

            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        logEntries.Add(new Tracker
                        {
                            Type = TypeCreated,
                            CreatedDate = DateTime.Now.ToString(DateFormat),
                            NewValue = GetAddedPropertiesValues(entry.Properties)

                        });
                        break;
                    case EntityState.Modified:
                        logEntries.Add(new Tracker
                        {
                            Type = TypeModified,
                            ModifiedDate = DateTime.Now.ToString(DateFormat),
                            OldValue = GetModifiedPropertiesValues(entry.Properties, false),
                            NewValue = GetModifiedPropertiesValues(entry.Properties, true)
                        });
                        break;
                }
            }

            foreach (var logEntry in logEntries)
            {
                Logs.Add(logEntry);
            }

            return base.SaveChanges();
        }

        private static string GetModifiedPropertiesValues(IEnumerable<PropertyEntry> properties, bool isNewValue)
        {
            var oldValues = new List<Dictionary<string, string>>();

            oldValues.AddRange(
                properties
                .Where(p => p.IsModified && !p.Metadata.IsPrimaryKey())
                .Select(p => new Dictionary<string, string>
                {
                    {
                        p.Metadata.Name,
                        (isNewValue ? p.CurrentValue?.ToString() : p.OriginalValue?.ToString()) ?? string.Empty
                    }
                }));

            return JsonConvert.SerializeObject(oldValues);
        }

        private static string GetAddedPropertiesValues(IEnumerable<PropertyEntry> properties)
        {
            var newValues = new List<Dictionary<string, string>>();

            newValues.AddRange(
                properties
                .Where(p => !p.Metadata.IsPrimaryKey())
                .Select(p => new Dictionary<string, string>
                {
                    {
                        p.Metadata.Name,
                        p.CurrentValue?.ToString() ?? string.Empty
                    }
                }));

            return JsonConvert.SerializeObject(newValues);
        }
    }
}
