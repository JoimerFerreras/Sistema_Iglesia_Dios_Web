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
                return Resumen_D.GraficoIngresosVsEgresos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GraficoCuentasCobrarPorMes()
        {
            try
            {
                return Resumen_D.GraficoCuentasCobrarPorMes(6, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GraficoAntiguedadCxC()
        {
            try
            {
                return Resumen_D.GraficoAntiguedadCxC();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GraficoCuentasPagarPorMes()
        {
            try
            {
                return Resumen_D.GraficoCuentasPagarPorMes(6, true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GraficoAntiguedadCxP()
        {
            try
            {
                return Resumen_D.GraficoAntiguedadCxP();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable TotalesMesActual()
        {
            try
            {
                return Resumen_D.TotalesMesActual();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable TotalMiscelaneos()
        {
            try
            {
                return Resumen_D.TotalMiscelaneos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable TotalDescripciones()
        {
            try
            {
                return Resumen_D.TotalDescripciones();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable TotalFormas_Pago()
        {
            try
            {
                return Resumen_D.TotalFormas_Pago();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable TotalMiembros()
        {
            try
            {
                return Resumen_D.TotalMiembros();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable IngresosPorDia_MesActual()
        {
            try
            {
                return Resumen_D.IngresosPorDia_MesActual();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
