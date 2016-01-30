using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Simpleza.ViewModel
{
    public class FindReplaceViewModel : ViewModelBase
    {
        /*
        private string textoBuscar;
        public string TextoBuscar
        {
            get
            {
                return textoBuscar;
            }
            set
            {
                textoBuscar = value;
                RaisePropertyChanged("TextoBuscar");
            }
        }

        private string textoReemplazar;
        public string TextoReemplazar
        {
            get
            {
                return textoReemplazar;
            }
            set
            {
                textoReemplazar = value;
                RaisePropertyChanged("TextoReemplazar");
            }
        }

        private bool usarRegex;
        public bool UsarRegex
        {
            get
            {
                return usarRegex;
            }
            set
            {
                usarRegex = value;
                RaisePropertyChanged("UsarRegex");
            }
        }

        private bool usarPalabraCompleta;
        public bool UsarPalabraCompleta
        {
            get
            {
                return usarPalabraCompleta;
            }
            set
            {
                usarPalabraCompleta = value;
                RaisePropertyChanged("UsarPalabraCompleta");
            }
        }

        private bool usarCaseSensitive;
        public bool UsarCaseSensitive
        {
            get
            {
                return usarCaseSensitive;
            }
            set
            {
                usarCaseSensitive = value;
                RaisePropertyChanged("UsarCaseSensitive");
            }
        }

        public ICommand CommandFind { get; set; }
        public ICommand CommandReplace { get; set; }
        public ICommand CommandReplaceAll { get; set; }

        public FindReplaceViewModel()
        {
            CommandFind = new RelayCommand(BuscarSiguiente); 
            

        }

        private void BuscarSiguiente()
        {
            
        }

        public Regex GetRegEx(bool ForceLeftToRight = false)
        {
            Regex r;
            RegexOptions o = RegexOptions.None;

            if (!UsarCaseSensitive)
                o = o | RegexOptions.IgnoreCase;

            if (UsarRegex)
                r = new Regex(TextoBuscar, o);
            else
            {
                string s = Regex.Escape(TextoBuscar);
                if (UsarPalabraCompleta)
                    s = "\\b" + s + "\\b";
                r = new Regex(s, o);
            }

            return r;
        }
         */
    }
}
