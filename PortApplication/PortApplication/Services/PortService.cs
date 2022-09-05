using Newtonsoft.Json;
using PortApplication.Models;
using PortApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace PortApplication.Services
{
    public class PortService : IPortService
    {
        /// <see cref="IPortService.GetAllPorts"/>
        public List<Port> GetAllPorts()
        {
            var listOfPorts = new List<Port>();
            try
            {
                // load the json file embedded resource
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Port)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("PortApplication.Ports.json");

                using (var reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    listOfPorts = JsonConvert.DeserializeObject<List<Port>>(json);
                }
            }
            catch (Exception ex)
            {
                App.LogCrash(ex, "IPortService.GetAllPortsAsync");
            }
            
            return listOfPorts;
        }
    }
}
