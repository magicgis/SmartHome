using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Plugin {
    /// <summary>
    /// A network function can get called from the network manager
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class NetworkFunction : System.Attribute{
        /// <summary>
        /// The custom name of the function
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Creates a new instance of the attribute
        /// </summary>
        /// <param name="name">The custom name of the function</param>
        public NetworkFunction(string name) {
            Name = name;
        }
    }
}
