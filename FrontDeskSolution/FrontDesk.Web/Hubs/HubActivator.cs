using Microsoft.AspNet.SignalR.Hubs;
using StructureMap;

namespace FrontDesk.Web.Hubs
{
    public class HubActivator : IHubActivator
    {
        private readonly IContainer container;

        public HubActivator(IContainer container)
        {
            this.container = container;
        }

        public IHub Create(HubDescriptor descriptor)
        {
            return (IHub)container.GetInstance(descriptor.HubType);
        }
    }
}