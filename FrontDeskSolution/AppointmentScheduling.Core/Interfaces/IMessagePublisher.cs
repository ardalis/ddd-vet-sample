using FrontDesk.SharedKernel.Interfaces;

namespace AppointmentScheduling.Core.Interfaces
{
    public interface IMessagePublisher
    {
        void Publish(IApplicationEvent applicationEvent);
    }
}