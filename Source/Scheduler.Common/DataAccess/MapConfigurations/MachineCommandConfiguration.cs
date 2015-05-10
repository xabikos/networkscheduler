using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Scheduler.Common.DataAccess.MapConfigurations
{
    /// <summary>
    /// Map configuration class for <see cref="MachineCommand"/>
    /// </summary>
    internal class MachineCommandConfiguration : EntityTypeConfiguration<MachineCommand>
    {
        public MachineCommandConfiguration()
        {
            ToTable("Commands");
            
            Property(cd => cd.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(cd => cd.Name).IsRequired().HasMaxLength(150);

        }
    }
}