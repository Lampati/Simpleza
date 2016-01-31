using Microsoft.Win32;
using Simpleza.Interfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpleza.Helpers
{
    public class FileManagerDialogService : IFileManagerDialogService
    {
        public virtual string OpenFileDialog()
        {
            string archElegido = null;
            OpenFileDialog d = new OpenFileDialog();
            d.AddExtension = true;
            d.DefaultExt = "gar";
            d.Multiselect = false;
            d.Filter = "Archivo GarGar (*.gar)|*.gar|Todos los Archivos (*.*)|*.*";

            if (d.ShowDialog() == true)
            {
                archElegido = d.FileName;
            }

            return archElegido;

        }

        public virtual string SaveFileDialog()
        {
            string archElegido = null;
            SaveFileDialog d = new SaveFileDialog();
            d.AddExtension = true;
            d.DefaultExt = "gar";
            d.Filter = "Archivo GarGar (*.gar)|*.gar|Todos los Archivos (*.*)|*.*";

            if (d.ShowDialog() == true)
            {
                archElegido = d.FileName;
            }

            return archElegido;
        }
    }
}
