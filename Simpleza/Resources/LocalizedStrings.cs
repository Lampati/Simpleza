using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpleza.Resources
{
    public class LocalizedStrings
    {
        public LocalizedStrings()
        {
        }

        private static Simpleza.Properties.Resources resource1 = new Simpleza.Properties.Resources();

        public Simpleza.Properties.Resources Resource1 { get { return resource1; } }
    }
}
