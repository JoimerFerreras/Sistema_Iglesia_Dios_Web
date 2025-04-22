using System;
using System.Data;
using Datos.Resumen;

namespace Negocio.Resumen
{
    public class Resumen_N
    {
        Resumen_D Resumen_D = new Resumen_D();

        // Grafico de total de ingresos en el mes
        public DataTable GraficoIngresosMes()
        {
            try
            {
                return Resumen_D.GraficoIngresosMes(DateTime.Now);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
