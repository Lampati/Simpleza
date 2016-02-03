using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simpleza.Model;
using System.IO;
using CompiladorGargar.Resultado.Auxiliares;
using CompiladorGargar.Sintactico.ErroresManager.Errores;

namespace SimplezaTestProject
{
    [TestClass]
    public class MensajeCompilacionTest
    {
        [TestMethod]
        public void MensajeCompilacionConPropiedades()
        {

            MensajeError mens = new CompiladorGargar.Sintactico.ErroresManager.Errores.ErrorVariableRepetida("hola");

            ErrorCompilacion err = new ErrorCompilacion(
               mens,
                CompiladorGargar.GlobalesCompilador.TipoError.Semantico,
                1,
                2);

            MensajeCompilacion mensComp = new MensajeCompilacion(err);

            Assert.IsTrue(mensComp.Mensaje == mens.Descripcion);
            Assert.IsTrue(mensComp.Codigo == mens.CodigoGlobal);
            Assert.IsTrue(mensComp.Linea == err.Fila);
            Assert.IsTrue(mensComp.Columna == err.Columna);

        }

    }
}
