using System;

namespace Scheduler.Server.Services
{
    public interface IScheduleCommandsService
    {

        void ExecuteCommand(MachineCommand command);

    }
}