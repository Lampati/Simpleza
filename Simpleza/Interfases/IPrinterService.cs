using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Simpleza.Interfases
{
    public interface IPrinterService
    {       
        bool? ElegirImpresora();

        void Imprimir();

        void AsignarDocumento(string nombre, string textoImprimir);
    }
}
