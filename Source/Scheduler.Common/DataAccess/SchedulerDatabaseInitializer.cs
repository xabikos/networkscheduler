using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Scheduler.Common.DataAccess
{
    internal class SchedulerDatabaseInitializer : DropCreateDatabaseIfModelChanges<SchedulerContext>
    {
        protected override void Seed(SchedulerContext context)
        {
            //Insert some initial test data

            context.Commands.AddRange(new List<MachineCommand>
            {
                new MachineCommand
                {
                    Name = "Update to .NET Framework 3.5",
                    Category = MachineCommandCategory.UpdateDotNetFramework
                },
                new MachineCommand
                {
                    Name = "Update to Latest .NET Framework",
                    Category = MachineCommandCategory.UpdateDotNetFramework
                },
                new MachineCommand
                {
                    Name = "Perform Windows update",
                    Category = MachineCommandCategory.UpdateWindows
                },
                new MachineCommand
                {
                    Name = "Clean files from primary disk",
                    Category = MachineCommandCategory.CleanFileDisk
                }
            });

            context.SaveChanges();

            base.Seed(context);
        }
    }
}