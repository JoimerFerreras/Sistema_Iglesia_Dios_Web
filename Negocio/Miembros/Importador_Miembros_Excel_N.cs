using ClosedXML.Excel;
using Entidades.Miembros;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;

namespace Negocio.Miembros
{
    /// <summary>
    /// Lee la primera hoja del archivo Excel y crea los seis DataTable
    /// requeridos por el procedimiento almacenado de importación masiva.
    /// </summary>
    public class Importador_Miembros_Excel_N
    {
        private static readonly CultureInfo CulturaDO =
            CultureInfo.GetCultureInfo("es-DO");

        /*
            El importador se crea por cada solicitud, por lo que estos campos
            solo existen mientras se procesa un archivo.
        */
        private Importacion_Miembros_E _resultadoActual;
        private Dictionary<int, string> _columnasGridPorNumero;

        public Importacion_Miembros_E Leer(Stream archivo)
        {
            if (archivo == null)
                throw new ArgumentNullException("archivo");

            Importacion_Miembros_E resultado = CrearEstructuras();
            _resultadoActual = resultado;
            _columnasGridPorNumero =
                new Dictionary<int, string>();

            using (XLWorkbook libro = new XLWorkbook(archivo))
            {
                IXLWorksheet hoja = libro.Worksheets.FirstOrDefault();

                if (hoja == null || hoja.FirstRowUsed() == null)
                {
                    resultado.AgregarError(
                        new Error_Importacion_Excel_E
                        {
                            Mensaje = "El archivo Excel está vacío."
                        });

                    return resultado;
                }

                IXLRow filaEncabezados = hoja.FirstRowUsed();
                int primeraFilaDatos =
                    filaEncabezados.RowNumber() + 1;
                int ultimaFila =
                    hoja.LastRowUsed().RowNumber();

                CrearVistaPrevia(
                    resultado,
                    hoja,
                    filaEncabezados,
                    primeraFilaDatos,
                    ultimaFila);

                Dictionary<string, int> columnas =
                    ObtenerColumnas(
                        filaEncabezados,
                        resultado);

                ValidarColumnasObligatorias(
                    columnas,
                    resultado,
                    filaEncabezados.RowNumber());

                /*
                    Si faltan encabezados obligatorios no es posible validar
                    correctamente las filas.
                */
                if (resultado.Errores_Detallados.Any(
                        error =>
                            error.Es_Encabezado &&
                            error.Columna_Excel == 0))
                {
                    return resultado;
                }

                for (int numeroFila = primeraFilaDatos;
                     numeroFila <= ultimaFila;
                     numeroFila++)
                {
                    IXLRow fila = hoja.Row(numeroFila);

                    if (FilaEstaVacia(fila))
                        continue;

                    resultado.Total_Filas_Leidas++;

                    try
                    {
                        if (AgregarFila(
                                resultado,
                                fila,
                                columnas))
                        {
                            resultado.Total_Filas_Validas++;
                        }
                    }
                    catch (Exception ex)
                    {
                        resultado.AgregarError(
                            new Error_Importacion_Excel_E
                            {
                                Fila_Excel = numeroFila,
                                Mensaje =
                                    "No se pudo procesar la fila: " +
                                    ex.Message
                            });
                    }
                }

                /*
                    Esta validación debe ejecutarse antes de llamar a SQL.
                    Así, cuando un número alternativo positivo aparece más
                    de una vez en el mismo Excel, se marcan en rojo todas
                    las celdas involucradas en lugar de recibir solamente
                    el THROW general del procedimiento almacenado.
                */
                ValidarNumerosAlternativosDuplicados(
                    hoja,
                    columnas,
                    primeraFilaDatos,
                    ultimaFila,
                    resultado);
            }

            if (resultado.Total_Filas_Leidas == 0 &&
                resultado.Errores.Count == 0)
            {
                resultado.AgregarError(
                    new Error_Importacion_Excel_E
                    {
                        Mensaje =
                            "El archivo no contiene filas de miembros para importar."
                    });
            }

            return resultado;
        }

        private void CrearVistaPrevia(
            Importacion_Miembros_E resultado,
            IXLWorksheet hoja,
            IXLRow filaEncabezados,
            int primeraFilaDatos,
            int ultimaFila)
        {
            DataTable datosExcel =
                new DataTable("Datos_Excel");

            datosExcel.Columns.Add(
                "Fila Excel",
                typeof(int));

            int primeraColumna =
                filaEncabezados.FirstCellUsed()
                    .Address.ColumnNumber;

            int ultimaColumna =
                hoja.LastCellUsed()
                    .Address.ColumnNumber;

            for (int numeroColumna = primeraColumna;
                 numeroColumna <= ultimaColumna;
                 numeroColumna++)
            {
                string nombreBase =
                    filaEncabezados
                        .Cell(numeroColumna)
                        .GetFormattedString()
                        .Trim();

                if (string.IsNullOrWhiteSpace(nombreBase))
                {
                    nombreBase =
                        "Columna " + numeroColumna;
                }

                string nombreGrid = nombreBase;
                int consecutivo = 2;

                while (datosExcel.Columns.Contains(nombreGrid))
                {
                    nombreGrid =
                        nombreBase + " (" + consecutivo + ")";
                    consecutivo++;
                }

                datosExcel.Columns.Add(
                    nombreGrid,
                    typeof(string));

                _columnasGridPorNumero[numeroColumna] =
                    nombreGrid;
            }

            for (int numeroFila = primeraFilaDatos;
                 numeroFila <= ultimaFila;
                 numeroFila++)
            {
                IXLRow fila = hoja.Row(numeroFila);

                if (FilaEstaVacia(fila))
                    continue;

                DataRow filaVista = datosExcel.NewRow();
                filaVista["Fila Excel"] = numeroFila;

                for (int numeroColumna = primeraColumna;
                     numeroColumna <= ultimaColumna;
                     numeroColumna++)
                {
                    string nombreGrid =
                        _columnasGridPorNumero[numeroColumna];

                    filaVista[nombreGrid] =
                        fila.Cell(numeroColumna)
                            .GetFormattedString();
                }

                datosExcel.Rows.Add(filaVista);
            }

            resultado.Datos_Excel = datosExcel;
        }

        #region Creación de los DataTable

        private Importacion_Miembros_E CrearEstructuras()
        {
            Importacion_Miembros_E resultado =
                new Importacion_Miembros_E();

            resultado.Miembros = CrearTablaMiembros();
            resultado.Informacion_Familiar_1 =
                CrearTablaInformacionFamiliar1();
            resultado.Informacion_Familiar_2 =
                CrearTablaInformacionFamiliar2();
            resultado.Informacion_Laboral =
                CrearTablaInformacionLaboral();
            resultado.Nivel_Academico =
                CrearTablaNivelAcademico();
            resultado.Pasatiempos =
                CrearTablaPasatiempos();

            return resultado;
        }

        private DataTable CrearTablaMiembros()
        {
            DataTable dt = new DataTable("Miembros");

            AgregarColumna(dt, "ImportKey", typeof(Guid), false);
            AgregarColumna(dt, "Nombres", typeof(string), false);
            AgregarColumna(dt, "Apellidos", typeof(string), false);
            AgregarColumna(dt, "Nombre_Pila", typeof(string), true);
            AgregarColumna(dt, "Sexo", typeof(int), true);
            AgregarColumna(dt, "Fecha_Nacimiento", typeof(DateTime), true);
            AgregarColumna(dt, "Estado_Civil", typeof(int), true);
            AgregarColumna(dt, "Tiene_Hijos", typeof(bool), true);
            AgregarColumna(dt, "Email", typeof(string), true);
            AgregarColumna(dt, "Celular", typeof(string), true);
            AgregarColumna(dt, "Sector", typeof(string), true);
            AgregarColumna(dt, "Barrio_Residencial", typeof(string), true);
            AgregarColumna(dt, "Calle", typeof(string), true);
            AgregarColumna(dt, "Numero_Casa", typeof(string), true);
            AgregarColumna(dt, "Es_Miembro", typeof(bool), true);
            AgregarColumna(dt, "Desde_Cuando_Miembro", typeof(DateTime), true);
            AgregarColumna(
                dt,
                "Le_Gustaria_Pertenecer_Ministerio",
                typeof(bool),
                true);
            AgregarColumna(
                dt,
                "Numero_Alternativo_Miembro",
                typeof(int),
                true);
            AgregarColumna(
                dt,
                "Comentarios_Diacono_Lider_Ministerio",
                typeof(string),
                true);
            AgregarColumna(dt, "Revisado_Por", typeof(string), true);
            AgregarColumna(dt, "Autorizado_Por", typeof(string), true);
            AgregarColumna(dt, "Id_Rol_Ministerio", typeof(int), true);
            AgregarColumna(dt, "Id_Ministerio", typeof(int), true);
            AgregarColumna(dt, "Id_Departamento", typeof(int), true);

            return dt;
        }

        private DataTable CrearTablaInformacionFamiliar1()
        {
            DataTable dt =
                new DataTable("Miembros_Informacion_Familiar_1");

            AgregarColumna(dt, "ImportKey", typeof(Guid), false);

            AgregarColumna(dt, "Conyuge_Nombre", typeof(string), true);
            AgregarColumna(dt, "Conyuge_Cristiano", typeof(bool), true);
            AgregarColumna(
                dt,
                "Conyuge_FechaNacimiento",
                typeof(DateTime),
                true);

            for (int numero = 1; numero <= 6; numero++)
            {
                AgregarColumna(
                    dt,
                    "Hijo" + numero + "_Nombre",
                    typeof(string),
                    false);

                AgregarColumna(
                    dt,
                    "Hijo" + numero + "_FechaNacimiento",
                    typeof(DateTime),
                    true);

                AgregarColumna(
                    dt,
                    "Hijo" + numero + "_Cristiano",
                    typeof(bool),
                    true);
            }

            return dt;
        }

        private DataTable CrearTablaInformacionFamiliar2()
        {
            DataTable dt =
                new DataTable("Miembros_Informacion_Familiar_2");

            AgregarColumna(dt, "ImportKey", typeof(Guid), false);

            AgregarColumna(
                dt,
                "Padre_Nombre_Completo",
                typeof(string),
                true);
            AgregarColumna(dt, "Padre_Edad", typeof(int), true);
            AgregarColumna(dt, "Padre_Empleado", typeof(bool), true);
            AgregarColumna(
                dt,
                "Padre_Negocio_Propio",
                typeof(bool),
                true);
            AgregarColumna(dt, "Padre_Celular", typeof(string), true);
            AgregarColumna(
                dt,
                "Padre_Miembro_Iglesia",
                typeof(bool),
                true);

            AgregarColumna(
                dt,
                "Madre_Nombre_Completo",
                typeof(string),
                true);
            AgregarColumna(dt, "Madre_Edad", typeof(int), true);
            AgregarColumna(dt, "Madre_Empleada", typeof(bool), true);
            AgregarColumna(
                dt,
                "Madre_Negocio_Propio",
                typeof(bool),
                true);
            AgregarColumna(dt, "Madre_Celular", typeof(string), true);
            AgregarColumna(
                dt,
                "Madre_Miembro_Iglesia",
                typeof(bool),
                true);

            for (int numero = 1; numero <= 5; numero++)
            {
                AgregarColumna(
                    dt,
                    "Hermano" + numero + "_Nombre_Completo",
                    typeof(string),
                    true);

                AgregarColumna(
                    dt,
                    "Hermano" + numero + "_Escolaridad",
                    typeof(string),
                    true);

                AgregarColumna(
                    dt,
                    "Hermano" + numero + "_Correo_Electronico",
                    typeof(string),
                    true);

                AgregarColumna(
                    dt,
                    "Hermano" + numero + "_Celular",
                    typeof(string),
                    true);
            }

            return dt;
        }

        private DataTable CrearTablaInformacionLaboral()
        {
            DataTable dt =
                new DataTable("Miembros_Informacion_Laboral");

            AgregarColumna(dt, "ImportKey", typeof(Guid), false);
            AgregarColumna(dt, "Empleado_Privado", typeof(bool), true);
            AgregarColumna(dt, "Empleado_Publico", typeof(bool), true);
            AgregarColumna(dt, "Dueno_Negocio", typeof(bool), true);
            AgregarColumna(dt, "Independiente", typeof(bool), true);
            AgregarColumna(dt, "Otros", typeof(bool), true);
            AgregarColumna(
                dt,
                "Nombre_Empresa_Negocio",
                typeof(string),
                true);

            return dt;
        }

        private DataTable CrearTablaNivelAcademico()
        {
            DataTable dt =
                new DataTable("Miembros_Nivel_Academico");

            AgregarColumna(dt, "ImportKey", typeof(Guid), false);
            AgregarColumna(dt, "Primario", typeof(bool), true);
            AgregarColumna(dt, "Secundario", typeof(bool), true);
            AgregarColumna(
                dt,
                "Grado_Universitario",
                typeof(bool),
                true);
            AgregarColumna(
                dt,
                "Post_Grado_Maestria",
                typeof(bool),
                true);

            return dt;
        }

        private DataTable CrearTablaPasatiempos()
        {
            DataTable dt =
                new DataTable("Miembros_Pasatiempos");

            AgregarColumna(dt, "ImportKey", typeof(Guid), false);
            AgregarColumna(dt, "Cine", typeof(bool), true);
            AgregarColumna(dt, "Leer", typeof(bool), true);
            AgregarColumna(dt, "Ver_TV", typeof(bool), true);
            AgregarColumna(dt, "Socializar", typeof(bool), true);
            AgregarColumna(dt, "Viajar", typeof(bool), true);
            AgregarColumna(dt, "Otros", typeof(string), true);

            return dt;
        }

        private void AgregarColumna(
            DataTable dt,
            string nombre,
            Type tipo,
            bool permiteNull)
        {
            DataColumn columna = dt.Columns.Add(nombre, tipo);
            columna.AllowDBNull = permiteNull;
        }

        #endregion

        #region Conversión de una fila del Excel

        private bool AgregarFila(
            Importacion_Miembros_E resultado,
            IXLRow fila,
            Dictionary<string, int> columnas)
        {
            int cantidadErroresAntes =
                resultado.Errores_Detallados.Count;

            Guid importKey = Guid.NewGuid();

            string nombres = ObtenerTexto(
                fila, columnas, "Nombres", 50, true);

            string apellidos = ObtenerTexto(
                fila, columnas, "Apellidos", 50, true);

            int sexo = ObtenerSexo(fila, columnas);

            DataRow miembro = resultado.Miembros.NewRow();

            miembro["ImportKey"] = importKey;
            miembro["Nombres"] = nombres;
            miembro["Apellidos"] = apellidos;
            miembro["Nombre_Pila"] =
                ObtenerTexto(fila, columnas, "Nombre_Pila", 30, false);
            miembro["Sexo"] = sexo;
            miembro["Fecha_Nacimiento"] =
                ADbNull(ObtenerFecha(
                    fila, columnas, "Fecha_Nacimiento"));
            miembro["Estado_Civil"] =
                ObtenerEstadoCivil(fila, columnas);
            miembro["Tiene_Hijos"] =
                ObtenerBooleano(fila, columnas, "Tiene_Hijos");
            miembro["Email"] =
                ObtenerTexto(fila, columnas, "Email", 50, false);
            miembro["Celular"] =
                ObtenerTexto(fila, columnas, "Celular", 15, false);
            miembro["Sector"] =
                ObtenerTexto(fila, columnas, "Sector", 50, false);
            miembro["Barrio_Residencial"] =
                ObtenerTexto(
                    fila,
                    columnas,
                    "Barrio_Residencial",
                    50,
                    false);
            miembro["Calle"] =
                ObtenerTexto(fila, columnas, "Calle", 80, false);
            miembro["Numero_Casa"] =
                ObtenerTexto(
                    fila,
                    columnas,
                    "Numero_Casa",
                    10,
                    false);
            miembro["Es_Miembro"] =
                ObtenerBooleano(fila, columnas, "Es_Miembro");
            miembro["Desde_Cuando_Miembro"] =
                ADbNull(ObtenerFecha(
                    fila,
                    columnas,
                    "Desde_Cuando_Miembro"));
            miembro["Le_Gustaria_Pertenecer_Ministerio"] =
                ObtenerBooleano(
                    fila,
                    columnas,
                    "Le_Gustaria_Pertenecer_Ministerio");
            miembro["Numero_Alternativo_Miembro"] =
                ObtenerEntero(
                    fila,
                    columnas,
                    "Numero_Alternativo_Miembro",
                    0);
            miembro["Comentarios_Diacono_Lider_Ministerio"] =
                ObtenerTexto(
                    fila,
                    columnas,
                    "Comentarios_Diacono_Lider_Ministerio",
                    300,
                    false);
            miembro["Revisado_Por"] =
                ObtenerTexto(
                    fila,
                    columnas,
                    "Revisado_Por",
                    30,
                    false);
            miembro["Autorizado_Por"] =
                ObtenerTexto(
                    fila,
                    columnas,
                    "Autorizado_Por",
                    30,
                    false);
            miembro["Id_Rol_Ministerio"] =
                ObtenerEntero(
                    fila,
                    columnas,
                    "Id_Rol_Ministerio",
                    0);
            miembro["Id_Ministerio"] =
                ObtenerEntero(
                    fila,
                    columnas,
                    "Id_Ministerio",
                    0);
            miembro["Id_Departamento"] =
                ObtenerEntero(
                    fila,
                    columnas,
                    "Id_Departamento",
                    0);

            DataRow familiar1 =
                resultado.Informacion_Familiar_1.NewRow();

            familiar1["ImportKey"] = importKey;
            familiar1["Conyuge_Nombre"] =
                ObtenerTexto(
                    fila,
                    columnas,
                    "Conyuge_Nombre",
                    80,
                    false);
            familiar1["Conyuge_Cristiano"] =
                ObtenerBooleano(
                    fila,
                    columnas,
                    "Conyuge_Cristiano");
            familiar1["Conyuge_FechaNacimiento"] =
                ADbNull(ObtenerFecha(
                    fila,
                    columnas,
                    "Conyuge_FechaNacimiento"));

            for (int numero = 1; numero <= 6; numero++)
            {
                familiar1["Hijo" + numero + "_Nombre"] =
                    ObtenerTexto(
                        fila,
                        columnas,
                        "Hijo" + numero + "_Nombre",
                        80,
                        false);

                familiar1[
                    "Hijo" + numero + "_FechaNacimiento"] =
                    ADbNull(ObtenerFecha(
                        fila,
                        columnas,
                        "Hijo" + numero + "_FechaNacimiento"));

                familiar1["Hijo" + numero + "_Cristiano"] =
                    ObtenerBooleano(
                        fila,
                        columnas,
                        "Hijo" + numero + "_Cristiano");
            }

            DataRow familiar2 =
                resultado.Informacion_Familiar_2.NewRow();

            familiar2["ImportKey"] = importKey;
            familiar2["Padre_Nombre_Completo"] =
                ObtenerTexto(
                    fila,
                    columnas,
                    "Padre_Nombre_Completo",
                    80,
                    false);
            familiar2["Padre_Edad"] =
                ADbNull(ObtenerEnteroNullable(
                    fila,
                    columnas,
                    "Padre_Edad"));
            familiar2["Padre_Empleado"] =
                ObtenerBooleano(
                    fila,
                    columnas,
                    "Padre_Empleado");
            familiar2["Padre_Negocio_Propio"] =
                ObtenerBooleano(
                    fila,
                    columnas,
                    "Padre_Negocio_Propio");
            familiar2["Padre_Celular"] =
                ObtenerTexto(
                    fila,
                    columnas,
                    "Padre_Celular",
                    15,
                    false);
            familiar2["Padre_Miembro_Iglesia"] =
                ObtenerBooleano(
                    fila,
                    columnas,
                    "Padre_Miembro_Iglesia");

            familiar2["Madre_Nombre_Completo"] =
                ObtenerTexto(
                    fila,
                    columnas,
                    "Madre_Nombre_Completo",
                    80,
                    false);
            familiar2["Madre_Edad"] =
                ADbNull(ObtenerEnteroNullable(
                    fila,
                    columnas,
                    "Madre_Edad"));
            familiar2["Madre_Empleada"] =
                ObtenerBooleano(
                    fila,
                    columnas,
                    "Madre_Empleada");
            familiar2["Madre_Negocio_Propio"] =
                ObtenerBooleano(
                    fila,
                    columnas,
                    "Madre_Negocio_Propio");
            familiar2["Madre_Celular"] =
                ObtenerTexto(
                    fila,
                    columnas,
                    "Madre_Celular",
                    15,
                    false);
            familiar2["Madre_Miembro_Iglesia"] =
                ObtenerBooleano(
                    fila,
                    columnas,
                    "Madre_Miembro_Iglesia");

            for (int numero = 1; numero <= 5; numero++)
            {
                familiar2[
                    "Hermano" + numero + "_Nombre_Completo"] =
                    ObtenerTexto(
                        fila,
                        columnas,
                        "Hermano" + numero + "_Nombre_Completo",
                        80,
                        false);

                familiar2[
                    "Hermano" + numero + "_Escolaridad"] =
                    ObtenerTexto(
                        fila,
                        columnas,
                        "Hermano" + numero + "_Escolaridad",
                        30,
                        false);

                familiar2[
                    "Hermano" + numero + "_Correo_Electronico"] =
                    ObtenerTexto(
                        fila,
                        columnas,
                        "Hermano" + numero + "_Correo_Electronico",
                        50,
                        false);

                familiar2[
                    "Hermano" + numero + "_Celular"] =
                    ObtenerTexto(
                        fila,
                        columnas,
                        "Hermano" + numero + "_Celular",
                        15,
                        false);
            }

            DataRow laboral =
                resultado.Informacion_Laboral.NewRow();

            laboral["ImportKey"] = importKey;
            laboral["Empleado_Privado"] =
                ObtenerBooleano(
                    fila,
                    columnas,
                    "Empleado_Privado");
            laboral["Empleado_Publico"] =
                ObtenerBooleano(
                    fila,
                    columnas,
                    "Empleado_Publico");
            laboral["Dueno_Negocio"] =
                ObtenerBooleano(
                    fila,
                    columnas,
                    "Dueno_Negocio");
            laboral["Independiente"] =
                ObtenerBooleano(
                    fila,
                    columnas,
                    "Independiente");
            laboral["Otros"] =
                ObtenerBooleano(
                    fila,
                    columnas,
                    "Otros_Trabajo");
            laboral["Nombre_Empresa_Negocio"] =
                ObtenerTexto(
                    fila,
                    columnas,
                    "Nombre_Empresa_Negocio",
                    80,
                    false);

            DataRow nivel =
                resultado.Nivel_Academico.NewRow();

            nivel["ImportKey"] = importKey;
            nivel["Primario"] =
                ObtenerBooleano(
                    fila,
                    columnas,
                    "Primario");
            nivel["Secundario"] =
                ObtenerBooleano(
                    fila,
                    columnas,
                    "Secundario");
            nivel["Grado_Universitario"] =
                ObtenerBooleano(
                    fila,
                    columnas,
                    "Grado_Universitario");
            nivel["Post_Grado_Maestria"] =
                ObtenerBooleano(
                    fila,
                    columnas,
                    "Post_Grado_Maestria");

            DataRow pasatiempos =
                resultado.Pasatiempos.NewRow();

            pasatiempos["ImportKey"] = importKey;
            pasatiempos["Cine"] =
                ObtenerBooleano(fila, columnas, "Cine");
            pasatiempos["Leer"] =
                ObtenerBooleano(fila, columnas, "Leer");
            pasatiempos["Ver_TV"] =
                ObtenerBooleano(fila, columnas, "Ver_TV");
            pasatiempos["Socializar"] =
                ObtenerBooleano(fila, columnas, "Socializar");
            pasatiempos["Viajar"] =
                ObtenerBooleano(fila, columnas, "Viajar");
            pasatiempos["Otros"] =
                ObtenerTexto(
                    fila,
                    columnas,
                    "Otros_Pasatiempos",
                    100,
                    false);

            /*
                Se agregan las seis filas únicamente después de que todos
                los campos hayan sido validados. Así no quedan DataTable
                desalineados cuando una fila del Excel tiene errores.
            */
            if (resultado.Errores_Detallados.Count >
                cantidadErroresAntes)
            {
                return false;
            }

            resultado.Miembros.Rows.Add(miembro);
            resultado.Informacion_Familiar_1.Rows.Add(familiar1);
            resultado.Informacion_Familiar_2.Rows.Add(familiar2);
            resultado.Informacion_Laboral.Rows.Add(laboral);
            resultado.Nivel_Academico.Rows.Add(nivel);
            resultado.Pasatiempos.Rows.Add(pasatiempos);

            return true;
        }

        #endregion

        #region Lectura y validación de celdas

        private Dictionary<string, int> ObtenerColumnas(
            IXLRow filaEncabezados,
            Importacion_Miembros_E resultado)
        {
            Dictionary<string, int> columnas =
                new Dictionary<string, int>(
                    StringComparer.OrdinalIgnoreCase);

            foreach (IXLCell celda in filaEncabezados.CellsUsed())
            {
                string nombre =
                    celda.GetFormattedString().Trim();

                if (nombre.Length == 0)
                    continue;

                if (columnas.ContainsKey(nombre))
                {
                    resultado.AgregarError(
                        CrearErrorCelda(
                            filaEncabezados,
                            celda.Address.ColumnNumber,
                            nombre,
                            "El encabezado '" + nombre +
                            "' aparece más de una vez.",
                            true));
                }
                else
                {
                    columnas.Add(
                        nombre,
                        celda.Address.ColumnNumber);
                }
            }

            return columnas;
        }

        private void ValidarColumnasObligatorias(
            Dictionary<string, int> columnas,
            Importacion_Miembros_E resultado,
            int filaEncabezados)
        {
            string[] obligatorias =
            {
                "Nombres",
                "Apellidos",
                "Sexo"
            };

            foreach (string columna in obligatorias)
            {
                if (!columnas.ContainsKey(columna))
                {
                    resultado.AgregarError(
                        new Error_Importacion_Excel_E
                        {
                            Fila_Excel = filaEncabezados,
                            Nombre_Columna = columna,
                            Es_Encabezado = true,
                            Mensaje =
                                "No se encontró la columna obligatoria '" +
                                columna + "'."
                        });
                }
            }
        }

        private Error_Importacion_Excel_E CrearErrorCelda(
            IXLRow fila,
            int numeroColumna,
            string nombreColumna,
            string mensaje,
            bool esEncabezado)
        {
            string nombreColumnaGrid = string.Empty;

            if (_columnasGridPorNumero != null)
            {
                _columnasGridPorNumero.TryGetValue(
                    numeroColumna,
                    out nombreColumnaGrid);
            }

            return new Error_Importacion_Excel_E
            {
                Fila_Excel = fila.RowNumber(),
                Columna_Excel = numeroColumna,
                Nombre_Columna = nombreColumna,
                Nombre_Columna_Grid = nombreColumnaGrid,
                Valor = fila.Cell(numeroColumna)
                    .GetFormattedString(),
                Mensaje = mensaje,
                Es_Encabezado = esEncabezado
            };
        }

        private void RegistrarErrorCelda(
            IXLRow fila,
            Dictionary<string, int> columnas,
            string nombreColumna,
            string mensaje)
        {
            int numeroColumna;

            if (!columnas.TryGetValue(
                    nombreColumna,
                    out numeroColumna))
            {
                _resultadoActual.AgregarError(
                    new Error_Importacion_Excel_E
                    {
                        Fila_Excel = fila.RowNumber(),
                        Nombre_Columna = nombreColumna,
                        Mensaje = mensaje
                    });

                return;
            }

            _resultadoActual.AgregarError(
                CrearErrorCelda(
                    fila,
                    numeroColumna,
                    nombreColumna,
                    mensaje,
                    false));
        }

        private void ValidarNumerosAlternativosDuplicados(
            IXLWorksheet hoja,
            Dictionary<string, int> columnas,
            int primeraFilaDatos,
            int ultimaFila,
            Importacion_Miembros_E resultado)
        {
            int numeroColumna;

            if (!columnas.TryGetValue(
                    "Numero_Alternativo_Miembro",
                    out numeroColumna))
            {
                return;
            }

            Dictionary<int, List<int>> filasPorNumero =
                new Dictionary<int, List<int>>();

            for (int numeroFila = primeraFilaDatos;
                 numeroFila <= ultimaFila;
                 numeroFila++)
            {
                IXLRow fila = hoja.Row(numeroFila);

                if (FilaEstaVacia(fila))
                    continue;

                IXLCell celda = fila.Cell(numeroColumna);
                int numeroAlternativo;

                if (!TryObtenerEnteroSinRegistrarError(
                        celda,
                        out numeroAlternativo))
                {
                    /*
                        Si el contenido no es un entero, el error de tipo ya
                        fue registrado por ObtenerEnteroNullable durante la
                        validación normal de la fila.
                    */
                    continue;
                }

                /*
                    Cero representa "sin número alternativo" en el sistema.
                    Por eso puede repetirse y no se considera duplicado.
                */
                if (numeroAlternativo <= 0)
                    continue;

                List<int> filas;

                if (!filasPorNumero.TryGetValue(
                        numeroAlternativo,
                        out filas))
                {
                    filas = new List<int>();
                    filasPorNumero.Add(
                        numeroAlternativo,
                        filas);
                }

                filas.Add(numeroFila);
            }

            foreach (KeyValuePair<int, List<int>> grupo in
                filasPorNumero.Where(item => item.Value.Count > 1))
            {
                string filasTexto =
                    string.Join(
                        ", ",
                        grupo.Value
                            .Select(fila => fila.ToString())
                            .ToArray());

                foreach (int numeroFila in grupo.Value)
                {
                    resultado.AgregarError(
                        CrearErrorCelda(
                            hoja.Row(numeroFila),
                            numeroColumna,
                            "Numero_Alternativo_Miembro",
                            "El número alternativo " +
                            grupo.Key +
                            " está repetido en las filas " +
                            filasTexto +
                            ". Cada número mayor que cero debe ser único.",
                            false));
                }
            }
        }

        private bool TryObtenerEnteroSinRegistrarError(
            IXLCell celda,
            out int valor)
        {
            valor = 0;

            if (celda == null || celda.IsEmpty())
                return false;

            if (celda.TryGetValue<int>(out valor))
                return true;

            double numero;

            if (celda.TryGetValue<double>(out numero) &&
                Math.Abs(numero - Math.Round(numero)) < 0.0000001)
            {
                valor = Convert.ToInt32(Math.Round(numero));
                return true;
            }

            string texto =
                celda.GetFormattedString().Trim();

            return int.TryParse(
                texto,
                NumberStyles.Integer,
                CulturaDO,
                out valor);
        }

        private bool FilaEstaVacia(IXLRow fila)
        {
            return !fila.CellsUsed().Any(
                celda =>
                    !string.IsNullOrWhiteSpace(
                        celda.GetFormattedString()));
        }

        private string ObtenerTexto(
            IXLRow fila,
            Dictionary<string, int> columnas,
            string nombreColumna,
            int longitudMaxima,
            bool obligatorio)
        {
            int numeroColumna;

            if (!columnas.TryGetValue(
                    nombreColumna,
                    out numeroColumna))
            {
                if (obligatorio)
                {
                    RegistrarErrorCelda(
                        fila,
                        columnas,
                        nombreColumna,
                        "Falta la columna obligatoria '" +
                        nombreColumna + "'.");
                }

                return string.Empty;
            }

            string valor = fila
                .Cell(numeroColumna)
                .GetFormattedString()
                .Trim();

            if (obligatorio &&
                string.IsNullOrWhiteSpace(valor))
            {
                RegistrarErrorCelda(
                    fila,
                    columnas,
                    nombreColumna,
                    "El campo '" + nombreColumna +
                    "' no puede estar vacío.");
            }

            if (longitudMaxima > 0 &&
                valor.Length > longitudMaxima)
            {
                RegistrarErrorCelda(
                    fila,
                    columnas,
                    nombreColumna,
                    "El campo '" + nombreColumna +
                    "' supera los " + longitudMaxima +
                    " caracteres.");
            }

            return valor;
        }

        private int ObtenerEntero(
            IXLRow fila,
            Dictionary<string, int> columnas,
            string nombreColumna,
            int valorPredeterminado)
        {
            int? valor = ObtenerEnteroNullable(
                fila,
                columnas,
                nombreColumna);

            return valor.HasValue
                ? valor.Value
                : valorPredeterminado;
        }

        private int? ObtenerEnteroNullable(
            IXLRow fila,
            Dictionary<string, int> columnas,
            string nombreColumna)
        {
            int numeroColumna;

            if (!columnas.TryGetValue(
                    nombreColumna,
                    out numeroColumna))
            {
                return null;
            }

            IXLCell celda = fila.Cell(numeroColumna);

            if (celda.IsEmpty())
                return null;

            int entero;

            if (celda.TryGetValue<int>(out entero))
                return entero;

            double numero;

            if (celda.TryGetValue<double>(out numero))
            {
                if (Math.Abs(numero - Math.Round(numero)) < 0.0000001)
                    return Convert.ToInt32(Math.Round(numero));
            }

            string texto = celda.GetFormattedString().Trim();

            if (texto.Length == 0)
                return null;

            if (int.TryParse(
                texto,
                NumberStyles.Integer,
                CulturaDO,
                out entero))
            {
                return entero;
            }

            RegistrarErrorCelda(
                fila,
                columnas,
                nombreColumna,
                "El campo '" + nombreColumna +
                "' debe contener un número entero.");

            return null;
        }

        private DateTime? ObtenerFecha(
            IXLRow fila,
            Dictionary<string, int> columnas,
            string nombreColumna)
        {
            int numeroColumna;

            if (!columnas.TryGetValue(
                    nombreColumna,
                    out numeroColumna))
            {
                return null;
            }

            IXLCell celda = fila.Cell(numeroColumna);

            if (celda.IsEmpty())
                return null;

            DateTime fecha;

            if (celda.TryGetValue<DateTime>(out fecha))
                return fecha.Date;

            double numeroFecha;

            if (celda.TryGetValue<double>(out numeroFecha))
            {
                try
                {
                    return DateTime.FromOADate(numeroFecha).Date;
                }
                catch
                {
                    // Continúa intentando como texto.
                }
            }

            string texto = celda.GetFormattedString().Trim();

            if (texto.Length == 0)
                return null;

            string[] formatos =
            {
                "dd/MM/yyyy",
                "d/M/yyyy",
                "dd-MM-yyyy",
                "d-M-yyyy",
                "yyyy-MM-dd"
            };

            if (DateTime.TryParseExact(
                texto,
                formatos,
                CulturaDO,
                DateTimeStyles.None,
                out fecha))
            {
                return fecha.Date;
            }

            if (DateTime.TryParse(
                texto,
                CulturaDO,
                DateTimeStyles.None,
                out fecha))
            {
                return fecha.Date;
            }

            RegistrarErrorCelda(
                fila,
                columnas,
                nombreColumna,
                "La fecha del campo '" + nombreColumna +
                "' no es válida. Use dd/MM/yyyy.");

            return null;
        }

        private bool ObtenerBooleano(
            IXLRow fila,
            Dictionary<string, int> columnas,
            string nombreColumna)
        {
            int numeroColumna;

            if (!columnas.TryGetValue(
                    nombreColumna,
                    out numeroColumna))
            {
                return false;
            }

            IXLCell celda = fila.Cell(numeroColumna);

            if (celda.IsEmpty())
                return false;

            bool booleano;

            if (celda.TryGetValue<bool>(out booleano))
                return booleano;

            string valor = celda
                .GetFormattedString()
                .Trim()
                .ToLowerInvariant();

            switch (valor)
            {
                case "":
                case "0":
                case "no":
                case "n":
                case "false":
                    return false;

                case "1":
                case "si":
                case "sí":
                case "s":
                case "true":
                case "x":
                case "yes":
                case "y":
                    return true;

                default:
                    RegistrarErrorCelda(
                        fila,
                        columnas,
                        nombreColumna,
                        "El campo '" + nombreColumna +
                        "' debe contener Sí/No, 1/0 o True/False.");

                    return false;
            }
        }

        private int ObtenerSexo(
            IXLRow fila,
            Dictionary<string, int> columnas)
        {
            string valor = ObtenerTexto(
                fila,
                columnas,
                "Sexo",
                20,
                true).ToLowerInvariant();

            if (string.IsNullOrWhiteSpace(valor))
                return 0;

            switch (valor)
            {
                case "1":
                case "m":
                case "masculino":
                case "hombre":
                    return 1;

                case "2":
                case "f":
                case "femenino":
                case "mujer":
                    return 2;

                default:
                    RegistrarErrorCelda(
                        fila,
                        columnas,
                        "Sexo",
                        "El sexo debe ser Masculino, Femenino, 1 o 2.");

                    return 0;
            }
        }

        private int ObtenerEstadoCivil(
            IXLRow fila,
            Dictionary<string, int> columnas)
        {
            string valor = ObtenerTexto(
                fila,
                columnas,
                "Estado_Civil",
                30,
                false).ToLowerInvariant();

            switch (valor)
            {
                case "":
                case "0":
                case "sin especificar":
                    return 0;

                case "1":
                case "soltero":
                case "soltera":
                case "soltero/a":
                    return 1;

                case "2":
                case "casado":
                case "casada":
                case "casado/a":
                    return 2;

                case "3":
                case "union libre":
                case "unión libre":
                    return 3;

                case "4":
                case "otro":
                case "otra":
                    return 4;

                default:
                    RegistrarErrorCelda(
                        fila,
                        columnas,
                        "Estado_Civil",
                        "El estado civil debe ser 0, 1, 2, 3, 4, " +
                        "Soltero/a, Casado/a, Unión libre u Otro.");

                    return 0;
            }
        }

        private object ADbNull(DateTime? valor)
        {
            return valor.HasValue
                ? (object)valor.Value
                : DBNull.Value;
        }

        private object ADbNull(int? valor)
        {
            return valor.HasValue
                ? (object)valor.Value
                : DBNull.Value;
        }

        #endregion
    }
}
