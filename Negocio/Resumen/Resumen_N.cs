using System;
using System.Data;
using Datos.Resumen;

namespace Negocio.Resumen
{
    public class Resumen_N
    {
        Resumen_D Resumen_D = new Resumen_D();

        // Grafico de total de ingresos en el mes
        public DataTable GraficoIngresosVsEgresos()
        {
            try
            {
                return Resumen_D.GraficoIngresosVsEgresos(1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GraficoCobrarPorMes()
        {
            try
            {
                return Resumen_D.GraficoCobrarPorMes(6, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
