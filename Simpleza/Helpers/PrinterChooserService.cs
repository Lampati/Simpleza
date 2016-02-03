using Simpleza.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Simpleza.Helpers
{
    public   class PrinterChooserService : IPrinterService
    {
        PrintDialog printDialog = new PrintDialog();
        FlowDocument documentoAImprimir;


        public virtual void AsignarDocumento(string nombre, string textoImprimir)
        {
            documentoAImprimir = new FlowDocument(new Paragraph(new Run(textoImprimir)));
            documentoAImprimir.Name = nombre;
            documentoAImprimir.PagePadding = new Thickness(25);
            documentoAImprimir.ColumnWidth = printDialog.PrintableAreaWidth;
        }

        public virtual bool? ElegirImpresora()
        {

            return printDialog.ShowDialog();

        }


        public virtual void Imprimir()
        {
            IDocumentPaginatorSource doc = documentoAImprimir;
            printDialog.PrintDocument(doc.DocumentPaginator, "Documento Gargar");
        }
    }
}
