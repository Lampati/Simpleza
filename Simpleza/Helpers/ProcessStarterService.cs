using Simpleza.Interfases;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Simpleza.Helpers
{
    public  class ProcessStarterService : IProcessStarterService
    {

        public virtual void StartProcess(string fileName)
        {
            Process.Start(fileName);
        }
    }
}
