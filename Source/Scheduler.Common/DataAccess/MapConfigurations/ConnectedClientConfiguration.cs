using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Scheduler.Common.DataAccess.MapConfigurations
{
    /// <summary>
    /// Map configuration class for <see cref="ConnectedClient"/>
    /// </summary>
    public class ConnectedClientConfiguration : EntityTypeConfiguration<ConnectedClient>
    {
        public ConnectedClientConfiguration()
        {
            //ToTable("ConnectedClients");

            Property(ccc => ccc.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(ccc=>ccc.Client).WithMany().HasForeignKey(ccc=>ccc.ClientId).WillCascadeOnDelete(true);
        }
    }
}
