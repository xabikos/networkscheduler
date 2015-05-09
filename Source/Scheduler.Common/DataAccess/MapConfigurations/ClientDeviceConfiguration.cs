using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Scheduler.Common.DataAccess.MapConfigurations
{
    /// <summary>
    /// Map configuration class for <see cref="ClientDevice"/>
    /// </summary>
    internal class ClientDeviceConfiguration : EntityTypeConfiguration<ClientDevice>
    {
        public ClientDeviceConfiguration()
        {
            //ToTable("Clients");
            
            Property(cd => cd.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(cd => cd.Name).IsRequired().HasMaxLength(255);
        }
    }
}