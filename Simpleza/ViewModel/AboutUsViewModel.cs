using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Simpleza.ViewModel
{
    public class AboutUsViewModel : ViewModelBase
    {
        public ICommand CommandClose { get; set; }

        private string autor;
        public string Autor
        {
            get
            {
                return autor;
            }
            set
            {
                autor = value;
                RaisePropertyChanged("Autor");
            }
        }

        private string versionApp;
        public string VersionApp
        {
            get
            {
                return versionApp;
            }
            set
            {
                versionApp = value;
                RaisePropertyChanged("VersionApp");
            }
        }

        private string versionCompilador;
        public string VersionCompilador
        {
            get
            {
                return versionCompilador;
            }
            set
            {
                versionCompilador = value;
                RaisePropertyChanged("VersionCompilador");
            }
        }

        public AboutUsViewModel()
        {
            this.CommandClose = new RelayCommand<Window>(CloseWindow);
            Autor = new StringBuilder().AppendLine("Federico Lanzani").AppendLine("Uciel Cohen").AppendLine("Ezequiel Tolstanov").ToString();

            VersionApp = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            foreach (var assembly in Assembly.GetExecutingAssembly().GetReferencedAssemblies())
            {
                if (assembly.Name == "Compilador")
                {
                    VersionCompilador = assembly.Version.ToString();
                }
            }
        }

        [ExcludeFromCodeCoverage]
        private void CloseWindow(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
    }
}
