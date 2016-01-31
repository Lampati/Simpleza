using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpleza.Model
{
    public class MensajeCompilacion : ObservableObject
    {
        private string mensaje;
        public string Mensaje
        {
            get
            {
                return mensaje;
            }
            set
            {
                Set<string>(() => this.Mensaje, ref mensaje, value);
            }
        }

        private int codigo;
        public int Codigo
        {
            get
            {
                return codigo;
            }
            set
            {
                Set<int>(() => this.Codigo, ref codigo, value);
            }
        }

        private int linea;
        public int Linea
        {
            get
            {
                return linea;
            }
            set
            {
                Set<int>(() => this.Linea, ref linea, value);
            }
        }

        private int columna;

        public int Columna
        {
            get
            {
                return columna;
            }
            set
            {
                Set<int>(() => this.Columna, ref columna, value);
            }
        }

        public MensajeCompilacion(CompiladorGargar.Resultado.Auxiliares.ErrorCompilacion item)
        {
            Codigo = item.Mensaje.CodigoGlobal;
            Mensaje = item.Mensaje.Descripcion;
            Linea = item.Fila;
            Columna = item.Columna;
        }

 

    }
}
