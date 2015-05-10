using System;
using Scheduler.Common;

namespace Scheduler.Server.Services
{
    public interface IScheduleCommandsService
    {

        void ExecuteCommand(MachineCommand command);

    }
}