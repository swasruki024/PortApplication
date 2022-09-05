using PortApplication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortApplication.Services.Interfaces
{
    public interface IPortService
    {
        /// <summary>
        /// Retrieve list of all ports from json file
        /// </summary>
        /// <returns></returns>
        List<Port> GetAllPorts();
    }
}
