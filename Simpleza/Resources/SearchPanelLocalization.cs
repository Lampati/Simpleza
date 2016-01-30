using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpleza.Resources
{
    class SearchPanelLocalization : ICSharpCode.AvalonEdit.Search.Localization
    {
        public override string FindNextText
        {
            get
            {
                return "Buscar Siguiente (F3)";
            }
        }
    }
}
