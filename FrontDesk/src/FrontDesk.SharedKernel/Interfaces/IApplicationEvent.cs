using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontDesk.SharedKernel.Interfaces
{
    public interface IApplicationEvent
    {
        string EventType { get; }
    }
}
