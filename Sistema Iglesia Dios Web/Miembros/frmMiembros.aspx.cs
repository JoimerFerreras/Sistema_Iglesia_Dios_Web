// Autor: Joimer Ferreras

using Entidades.Miembros;
using Negocio.Miembros;
using Negocio.Util_N;
using Sistema_Iglesia_Dios_Web.Utilidad_Cliente;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio.Ministerios;

namespace Sistema_Iglesia_Dios_Web.Miembros
{
    public partial class frmMiembros : System.Web.UI.Page
    {
        #region Declaraciones
        Miembro_N miembro_N = new Miembro_N();

        public string ID_REGISTRO
        {
            get
            {
                if (Utilidad_N.ValidarNull(ViewState["ID_REGISTRO"]))
                {
                    return "";
                }
                return ViewState["ID_REGISTRO"].ToString();
            }
            set
            {
                ViewState["ID_REGISTRO"] = value;
            }
        }

        public bool EDITAR_REGISTRO
        {
            get
            {
                if (Utilidad_N.ValidarNull(ViewState["EDITAR_REGISTRO"]))
                {
                    ViewState["EDITAR_REGISTRO"] = new bool();
                }
                return (bool)ViewState["EDITAR_REGISTRO"];
            }
            set
            {
                ViewState["EDITAR_REGISTRO"] = value;
            }
        }

        public DataTable DT_DATOS
        {
            get
            {
                if (Utilidad_N.ValidarNull(ViewState["DT_DATOS"]))
                {
                    ViewState["DT_DATOS"] = new DataTable();
                }
                return (DataTable)ViewState["DT_DATOS"];
            }
            set
            {
                ViewState["DT_DATOS"] = value;
            }
        }
        #endregion

        #region Metodos/ Procedimientos

        // CONSULTA
        private void LimpiarFiltros()
        {
            rbtnTipoFecha.SelectedValue = "2";

            //Primero obtenemos el día actual
            DateTime date = DateTime.Now;

            //Asi obtenemos el primer dia del mes actual
            DateTime PrimerDiaMes = new DateTime(date.Year, date.Month, 1);

            //Y de la siguiente forma obtenemos el ultimo dia del mes
            //agregamos 1 mes al objeto anterior y restamos 1 día.
            DateTime UltimoDiaMes = PrimerDiaMes.Date.AddMonths(1).AddDays(-1);

            string PrimerDiaAnio = "1/1/" + DateTime.Now.Year.ToString();
            //string UltimoDiaAnio = "31/12/" + DateTime.Now.Year.ToString();

            dtpFechaDesdeFiltro.SelectedDate = DateTime.Parse(PrimerDiaAnio);
            dtpFechaHastaFiltro.SelectedDate = DateTime.Now;

            txtTextoBusquedaFiltro.Text = "";
            cmbSexoFiltro.SelectedValue = "0";
            cmbEstadoCivilFiltro.SelectedValue = "0";
            cmbMinisterioFiltro.SelectedValue = "0";
        }

        private void Consultar()
        {
            DateTime fecha;

            if (dtpFechaDesdeFiltro.SelectedDate != null && dtpFechaHastaFiltro.SelectedDate != null)
            {
                if (DateTime.TryParse(dtpFechaDesdeFiltro.SelectedDate.Value.ToString(), out fecha) == true)
                {
                    if (DateTime.TryParse(dtpFechaHastaFiltro.SelectedDate.Value.ToString(), out fecha) == true)
                    {
                        DT_DATOS = miembro_N.Consultar(
                        rbtnTipoFecha.SelectedValue,
                        dtpFechaDesdeFiltro.SelectedDate.Value,
                        dtpFechaHastaFiltro.SelectedDate.Value,
                        txtTextoBusquedaFiltro.Text,
                        cmbSexoFiltro.SelectedValue,
                        cmbEstadoCivilFiltro.SelectedValue,
                        cmbMinisterioFiltro.SelectedValue);

                        gvDatos.DataSource = DT_DATOS;
                        gvDatos.DataBind();
                    }
                    else
                    {
                        Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Advertencia", "Fecha Desde y Fechas Hasta deben tener valores válidos", "warning");
                    }
                }
                else
                {
                    Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Advertencia", "Fecha Desde y Fechas Hasta deben tener valores válidos", "warning");
                }
            }
            else
            {
                Utilidad_C.MostrarAlerta_Personalizada(this, this.GetType(), "Advertencia", "Fecha Desde y Fechas Hasta deben tener valores válidos", "warning");
            }
        }

        private void LlenarCombos()
        {
            DataTable dt = new DataTable();

            // Ministerio
            Ministerio_N Ministerio_N = new Ministerio_N();
            dt = Ministerio_N.ListaCombo("0", false);
            cmbMinisterioFiltro.DataSource = dt;
            cmbMinisterioFiltro.DataValueField = "Id_Ministerio";
            cmbMinisterioFiltro.DataTextField = "Nombre_Ministerio";
            cmbMinisterioFiltro.DataBind();
        }

        private void ActualizarGrid()
        {
            gvDatos.DataSource = DT_DATOS;
            gvDatos.DataBind();
        }




        // REGISTRO

        private bool ValidarCampos()
        {
            bool Validacion = false;

            if (txtNombres_Miembro.Text.Length == 0)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "El nombre del miembro no puede estar vacío");
                txtNombres_Miembro.Focus();
            }
            else if (txtApellidos_Miembro.Text.Length == 0)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "El apellido del miembro no puede estar vacío");
                txtApellidos_Miembro.Focus();
            }
            else if (dtpFechaNacimiento.SelectedDate == null)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "La fecha de nacimiento debe ser válida"); 
                dtpFechaNacimiento.Focus();
            }
            else if (dtpDesdeCuandoMiembro.SelectedDate == null)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Personalizado(this, this.GetType(), "La fecha de cuando se unió el miembro debe ser válida");
                dtpDesdeCuandoMiembro.Focus();
            }
            else
            {
                Validacion = true;
            }

            return Validacion;
        }

        private void GuardarRegistro()
        {
            try
            {
                if (ValidarCampos() == true)
                {
                    // Agregacion de la informacion basica del miembro
                    Miembro_E miembro_E = new Miembro_E();
                    miembro_E.Id_Miembro = int.Parse(ID_REGISTRO);
                    miembro_E.Nombres = txtNombres_Miembro.Text;
                    miembro_E.Apellidos = txtApellidos_Miembro.Text;
                    miembro_E.Nombre_Pila = txtNombrePila.Text;
                    miembro_E.Sexo = int.Parse(rbtnSexo.SelectedValue);
                    miembro_E.Fecha_Nacimiento = dtpFechaNacimiento.SelectedDate == null ? null : dtpFechaNacimiento.SelectedDate;
                    miembro_E.Estado_Civil = int.Parse(cmbEstadoCivil.SelectedValue);
                    miembro_E.Tiene_Hijos = chkTieneHijos.Checked;
                    miembro_E.Email = txtEmail.Text;
                    miembro_E.Celular = txtCelular.Text;
                    miembro_E.Sector = txtSector.Text;
                    miembro_E.Barrio_Residencial = txtBarrio_Residencial.Text;
                    miembro_E.Calle = txtCalle.Text;
                    miembro_E.Numero_Casa = txtNumeroCasa.Text;
                    miembro_E.Numero_Alternativo_Miembro = txtNumeroMiembroAlternativo.Text.Length == 0 ? 0 : int.Parse(txtNumeroMiembroAlternativo.Text);
                    miembro_E.Es_Miembro = chkEsMiembro.Checked;
                    miembro_E.Desde_Cuando_Miembro = dtpDesdeCuandoMiembro.SelectedDate == null ? null : dtpDesdeCuandoMiembro.SelectedDate;
                    miembro_E.Pertenece_Ministerio = chkPertenece_Ministerio.Checked;
                    miembro_E.Le_Gustaria_Pertenecer_Ministerio = chkLe_Gustaria_Pertenecer_Ministerio.Checked;
                    miembro_E.Rol_Miembro = int.Parse(cmbRol_Miembro.SelectedValue);
                    miembro_E.Otro_Rol = txtOtroRol.Text;
                    miembro_E.Nombre_Diacono = txtNombre_Diacono.Text;
                    miembro_E.Nombre_Lider_Ministerio = txtNombreLiderMinisterio.Text;
                    miembro_E.Comentarios_Diacono_Lider_Ministerio = txtComentariosDiaconoLiderMinisterio.Text;
                    miembro_E.Revisado_Por = txtRevisadoPor.Text;
                    miembro_E.Autorizado_Por = txtAutorizadoPor.Text;


                    // Agregacion del nivel academico del miembro
                    Miembro_Nivel_Academico_E nivel_academico_E = new Miembro_Nivel_Academico_E();
                    nivel_academico_E.Primario = chkNivelPrimario.Checked;
                    nivel_academico_E.Secundario = chkNivelSecundario.Checked;
                    nivel_academico_E.Grado_Universitario = chkGradoUniversitario.Checked;
                    nivel_academico_E.Post_Grado_Maestria = chkNivePostGrado_Maestria.Checked;


                    // Agregacion de la informacion laboral del miembro
                    Miembro_Informacion_Laboral_E info_laboral_E = new Miembro_Informacion_Laboral_E();
                    info_laboral_E.Empleado_Privado = chkEmpleadoPrivado.Checked;
                    info_laboral_E.Empleado_Publico = chkEmpleadoPublico.Checked;
                    info_laboral_E.Dueno_Negocio = chkDuenoNegocio.Checked;
                    info_laboral_E.Independiente = chkIndependiente.Checked;
                    info_laboral_E.Otros = chkOtros.Checked;
                    info_laboral_E.Nombre_Empresa_Negocio = txtNombreEmpresaNegocio.Text;


                    // Agregacion de la informacion familiar 1 del miembro
                    Miembro_Informacion_Familiar1_E info_familiar1_E = new Miembro_Informacion_Familiar1_E();
                    info_familiar1_E.Conyuge_Nombre = txtNombreConyuge.Text;
                    info_familiar1_E.Conyuge_Cristiano = chkConyugeCristiano.Checked;
                    info_familiar1_E.Conyuge_FechaNacimiento = dtpFechaNacimiento_Conyuge.SelectedDate == null ? null : dtpFechaNacimiento_Conyuge.SelectedDate;

                    info_familiar1_E.Hijo1_Nombre = txtHijo1_Nombre.Text;
                    info_familiar1_E.Hijo1_Cristiano = chkHijo1_Cristiano.Checked;
                    info_familiar1_E.Hijo1_FechaNacimiento = dtpHijo1_FechaNacimiento.SelectedDate == null ? null : dtpHijo1_FechaNacimiento.SelectedDate;

                    info_familiar1_E.Hijo2_Nombre = txtHijo2_Nombre.Text;
                    info_familiar1_E.Hijo2_Cristiano = chkHijo2_Cristiano.Checked;
                    info_familiar1_E.Hijo2_FechaNacimiento = dtpHijo2_FechaNacimiento.SelectedDate == null ? null : dtpHijo2_FechaNacimiento.SelectedDate;

                    info_familiar1_E.Hijo3_Nombre = txtHijo3_Nombre.Text;
                    info_familiar1_E.Hijo3_Cristiano = chkHijo3_Cristiano.Checked;
                    info_familiar1_E.Hijo3_FechaNacimiento = dtpHijo3_FechaNacimiento.SelectedDate == null ? null : dtpHijo3_FechaNacimiento.SelectedDate;


                    info_familiar1_E.Hijo4_Nombre = txtHijo4_Nombre.Text;
                    info_familiar1_E.Hijo4_Cristiano = chkHijo4_Cristiano.Checked;
                    info_familiar1_E.Hijo4_FechaNacimiento = dtpHijo4_FechaNacimiento.SelectedDate == null ? null : dtpHijo4_FechaNacimiento.SelectedDate;

                    info_familiar1_E.Hijo5_Nombre = txtHijo5_Nombre.Text;
                    info_familiar1_E.Hijo5_Cristiano = chkHijo5_Cristiano.Checked;
                    info_familiar1_E.Hijo5_FechaNacimiento = dtpHijo5_FechaNacimiento.SelectedDate == null ? null : dtpHijo5_FechaNacimiento.SelectedDate;

                    info_familiar1_E.Hijo6_Nombre = txtHijo6_Nombre.Text;
                    info_familiar1_E.Hijo6_Cristiano = chkHijo6_Cristiano.Checked;
                    info_familiar1_E.Hijo6_FechaNacimiento = dtpHijo6_FechaNacimiento.SelectedDate == null ? null : dtpHijo6_FechaNacimiento.SelectedDate;


                    // Agregacion de la informacion familiar 2 del miembro
                    Miembro_Informacion_Familiar2_E info_familiar2_E = new Miembro_Informacion_Familiar2_E();
                    info_familiar2_E.Padre_Nombre_Completo = txtPadre_NombreCompleto.Text;
                    info_familiar2_E.Padre_Edad = txtPadre_Edad.Text.Length == 0 ? 0 : int.Parse(txtPadre_Edad.Text);
                    info_familiar2_E.Padre_Empleado = chkPadre_Empleado.Checked;
                    info_familiar2_E.Padre_Negocio_Propio = chkPadre_NegocioPropio.Checked;
                    info_familiar2_E.Padre_Celular = txtPadre_Celular.Text;
                    info_familiar2_E.Padre_Miembro_Iglesia = chkPadre_MiembroIglesia.Checked;

                    info_familiar2_E.Madre_Nombre_Completo = txtMadre_NombreCompleto.Text;
                    info_familiar2_E.Madre_Edad = txtMadre_Edad.Text.Length == 0 ? 0 : int.Parse(txtMadre_Edad.Text);
                    info_familiar2_E.Madre_Empleada = chkMadre_Empleada.Checked;
                    info_familiar2_E.Madre_Negocio_Propio = chkMadre_NegocioPropio.Checked;
                    info_familiar2_E.Madre_Celular = txtMadre_Celular.Text;
                    info_familiar2_E.Madre_Miembro_Iglesia = chkMadre_MiembroIglesia.Checked;

                    info_familiar2_E.Hermano1_Nombre_Completo = txtHermano1_NombreCompleto.Text;
                    info_familiar2_E.Hermano1_Escolaridad = txtHermano1_Escolaridad.Text;
                    info_familiar2_E.Hermano1_Correo_Electronico = txtHermano1_CorreoElectronico.Text;
                    info_familiar2_E.Hermano1_Celular = txtHermano1_Celular.Text;

                    info_familiar2_E.Hermano2_Nombre_Completo = txtHermano2_NombreCompleto.Text;
                    info_familiar2_E.Hermano2_Escolaridad = txtHermano2_Escolaridad.Text;
                    info_familiar2_E.Hermano2_Correo_Electronico = txtHermano2_CorreoElectronico.Text;
                    info_familiar2_E.Hermano2_Celular = txtHermano2_Celular.Text;

                    info_familiar2_E.Hermano3_Nombre_Completo = txtHermano3_NombreCompleto.Text;
                    info_familiar2_E.Hermano3_Escolaridad = txtHermano3_Escolaridad.Text;
                    info_familiar2_E.Hermano3_Correo_Electronico = txtHermano3_CorreoElectronico.Text;
                    info_familiar2_E.Hermano3_Celular = txtHermano3_Celular.Text;

                    info_familiar2_E.Hermano4_Nombre_Completo = txtHermano4_NombreCompleto.Text;
                    info_familiar2_E.Hermano4_Escolaridad = txtHermano4_Escolaridad.Text;
                    info_familiar2_E.Hermano4_Correo_Electronico = txtHermano4_CorreoElectronico.Text;
                    info_familiar2_E.Hermano4_Celular = txtHermano4_Celular.Text;

                    info_familiar2_E.Hermano5_Nombre_Completo = txtHermano5_NombreCompleto.Text;
                    info_familiar2_E.Hermano5_Escolaridad = txtHermano5_Escolaridad.Text;
                    info_familiar2_E.Hermano5_Correo_Electronico = txtHermano5_CorreoElectronico.Text;
                    info_familiar2_E.Hermano5_Celular = txtHermano5_Celular.Text;


                    // Agregacion de los pasatiempos del miembro
                    Miembro_Pasatiempos_E pasatiempos_E = new Miembro_Pasatiempos_E();
                    pasatiempos_E.Cine = chkCine.Checked;
                    pasatiempos_E.Leer = chkLeer.Checked;
                    pasatiempos_E.Ver_TV = chkVerTV.Checked;
                    pasatiempos_E.Socializar = chkSocializar.Checked;
                    pasatiempos_E.Viajar = chkViajar.Checked;
                    pasatiempos_E.Otros = txtOtrosPasatiempos.Text;


                    if (EDITAR_REGISTRO == true)
                    {
                        // Agregar registro
                        string MensajeSalida = miembro_N.Guardar(
                        miembro_E,
                        info_familiar1_E,
                        info_familiar2_E,
                        info_laboral_E,
                        nivel_academico_E,
                        pasatiempos_E);

                        if (MensajeSalida == "1")
                        {
                            Utilidad_C.MostrarAlerta_Guardar_Success(this, this.GetType());
                            LimpiarCampos();
                        }
                        else
                        {
                            Utilidad_C.MostrarAlerta_Guardar_Error(this, this.GetType());
                        }
                    }
                    else
                    {
                        // Guardar registro existente
                        string MensajeSalida = miembro_N.Agregar(
                        miembro_E,
                        info_familiar1_E,
                        info_familiar2_E,
                        info_laboral_E,
                        nivel_academico_E,
                        pasatiempos_E);

                        if (MensajeSalida == "1")
                        {
                            Utilidad_C.MostrarAlerta_Guardar_Success(this, this.GetType());
                            LimpiarCampos();
                        }
                        else
                        {
                            Utilidad_C.MostrarAlerta_Guardar_Error(this, this.GetType());
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                Utilidad_C.MostrarAlerta_Guardar_Error_Fatal(this, this.GetType());
            }
           
        }

        private void VerRegistro()
        {
            // Llenado de datos generales
            Miembro_E Miembro_E = new Miembro_E();
            Miembro_E = miembro_N.ObtenerRegistro(ID_REGISTRO);

            txtIdMiembro.Text = Miembro_E.Id_Miembro.ToString();
            txtNombres_Miembro.Text = Miembro_E.Nombres;
            txtApellidos_Miembro.Text = Miembro_E.Apellidos;
            txtNombrePila.Text = Miembro_E.Nombre_Pila;
            rbtnSexo.SelectedValue = Miembro_E.Sexo.ToString();
            dtpFechaNacimiento.SelectedDate = Miembro_E.Fecha_Nacimiento;
            cmbEstadoCivil.SelectedValue = Miembro_E.Estado_Civil.ToString();
            chkTieneHijos.Checked = Miembro_E.Tiene_Hijos;
            txtEmail.Text = Miembro_E.Email;
            txtCelular.Text = Miembro_E.Celular;
            txtSector.Text = Miembro_E.Sector;
            txtBarrio_Residencial.Text = Miembro_E.Barrio_Residencial;
            txtCalle.Text = Miembro_E.Calle;
            txtNumeroCasa.Text = Miembro_E.Numero_Casa;
            chkEsMiembro.Checked = Miembro_E.Es_Miembro;
            dtpDesdeCuandoMiembro.SelectedDate = Miembro_E.Desde_Cuando_Miembro;
            chkPertenece_Ministerio.Checked = Miembro_E.Pertenece_Ministerio;
            chkLe_Gustaria_Pertenecer_Ministerio.Checked = Miembro_E.Le_Gustaria_Pertenecer_Ministerio;
            txtNumeroMiembroAlternativo.Text = Miembro_E.Numero_Alternativo_Miembro.ToString();
            cmbRol_Miembro.SelectedValue = Miembro_E.Rol_Miembro.ToString("");
            txtOtroRol.Text = Miembro_E.Otro_Rol;
            txtNombre_Diacono.Text = Miembro_E.Nombre_Diacono;
            txtNombreLiderMinisterio.Text = Miembro_E.Nombre_Lider_Ministerio;
            txtComentariosDiaconoLiderMinisterio.Text = Miembro_E.Comentarios_Diacono_Lider_Ministerio;
            txtRevisadoPor.Text = Miembro_E.Revisado_Por;
            txtAutorizadoPor.Text = Miembro_E.Autorizado_Por;


            // Llenado de nivel academico
            Miembro_Nivel_Academico_E Miembro_Nivel_Academico_E = new Miembro_Nivel_Academico_E();
            Miembro_Nivel_Academico_N Miembro_Nivel_Academico_N = new Miembro_Nivel_Academico_N();
            Miembro_Nivel_Academico_E = Miembro_Nivel_Academico_N.ObtenerRegistro(ID_REGISTRO);

            chkNivelPrimario.Checked = Miembro_Nivel_Academico_E.Primario;
            chkNivelSecundario.Checked = Miembro_Nivel_Academico_E.Secundario;
            chkGradoUniversitario.Checked = Miembro_Nivel_Academico_E.Grado_Universitario;
            chkNivePostGrado_Maestria.Checked = Miembro_Nivel_Academico_E.Post_Grado_Maestria;


            // Llenado de informacion laboral
            Miembro_Informacion_Laboral_E Miembro_Informacion_Laboral_E = new Miembro_Informacion_Laboral_E();
            Miembro_Informacion_Laboral_N Miembro_Informacion_Laboral_N = new Miembro_Informacion_Laboral_N();
            Miembro_Informacion_Laboral_E = Miembro_Informacion_Laboral_N.ObtenerRegistro(ID_REGISTRO);

            chkEmpleadoPrivado.Checked = Miembro_Informacion_Laboral_E.Empleado_Privado;
            chkEmpleadoPublico.Checked = Miembro_Informacion_Laboral_E.Empleado_Publico;
            chkDuenoNegocio.Checked = Miembro_Informacion_Laboral_E.Dueno_Negocio;
            chkIndependiente.Checked = Miembro_Informacion_Laboral_E.Independiente;
            chkOtros.Checked = Miembro_Informacion_Laboral_E.Otros;
            txtNombreEmpresaNegocio.Text = Miembro_Informacion_Laboral_E.Nombre_Empresa_Negocio;


            // Llenado de informacion familiar 1
            Miembro_Informacion_Familiar1_E Miembro_Informacion_Familiar1_E = new Miembro_Informacion_Familiar1_E();
            Miembro_Informacion_Familiar1_N Miembro_Informacion_Familiar1_N = new Miembro_Informacion_Familiar1_N();
            Miembro_Informacion_Familiar1_E = Miembro_Informacion_Familiar1_N.ObtenerRegistro(ID_REGISTRO);

            txtNombreConyuge.Text = Miembro_Informacion_Familiar1_E.Conyuge_Nombre;
            chkConyugeCristiano.Checked = Miembro_Informacion_Familiar1_E.Conyuge_Cristiano;
            dtpFechaNacimiento_Conyuge.SelectedDate = Miembro_Informacion_Familiar1_E.Conyuge_FechaNacimiento;

            txtHijo1_Nombre.Text = Miembro_Informacion_Familiar1_E.Hijo1_Nombre;
            dtpHijo1_FechaNacimiento.SelectedDate = Miembro_Informacion_Familiar1_E.Hijo1_FechaNacimiento;
            chkHijo1_Cristiano.Checked = Miembro_Informacion_Familiar1_E.Hijo1_Cristiano;

            txtHijo2_Nombre.Text = Miembro_Informacion_Familiar1_E.Hijo2_Nombre;
            dtpHijo2_FechaNacimiento.SelectedDate = Miembro_Informacion_Familiar1_E.Hijo2_FechaNacimiento;
            chkHijo2_Cristiano.Checked = Miembro_Informacion_Familiar1_E.Hijo2_Cristiano;

            txtHijo3_Nombre.Text = Miembro_Informacion_Familiar1_E.Hijo3_Nombre;
            dtpHijo3_FechaNacimiento.SelectedDate = Miembro_Informacion_Familiar1_E.Hijo3_FechaNacimiento;
            chkHijo3_Cristiano.Checked = Miembro_Informacion_Familiar1_E.Hijo3_Cristiano;

            txtHijo4_Nombre.Text = Miembro_Informacion_Familiar1_E.Hijo4_Nombre;
            dtpHijo4_FechaNacimiento.SelectedDate = Miembro_Informacion_Familiar1_E.Hijo4_FechaNacimiento;
            chkHijo4_Cristiano.Checked = Miembro_Informacion_Familiar1_E.Hijo4_Cristiano;

            txtHijo5_Nombre.Text = Miembro_Informacion_Familiar1_E.Hijo5_Nombre;
            dtpHijo5_FechaNacimiento.SelectedDate = Miembro_Informacion_Familiar1_E.Hijo5_FechaNacimiento;
            chkHijo5_Cristiano.Checked = Miembro_Informacion_Familiar1_E.Hijo5_Cristiano;

            txtHijo6_Nombre.Text = Miembro_Informacion_Familiar1_E.Hijo6_Nombre;
            dtpHijo6_FechaNacimiento.SelectedDate = Miembro_Informacion_Familiar1_E.Hijo6_FechaNacimiento;
            chkHijo6_Cristiano.Checked = Miembro_Informacion_Familiar1_E.Hijo6_Cristiano;


            // Llenado de informacion familiar 2
            Miembro_Informacion_Familiar2_E Miembro_Informacion_Familiar2_E = new Miembro_Informacion_Familiar2_E();
            Miembro_Informacion_Familiar2_N Miembro_Informacion_Familiar2_N = new Miembro_Informacion_Familiar2_N();
            Miembro_Informacion_Familiar2_E = Miembro_Informacion_Familiar2_N.ObtenerRegistro(ID_REGISTRO);

            txtPadre_NombreCompleto.Text = Miembro_Informacion_Familiar2_E.Padre_Nombre_Completo;
            txtPadre_Edad.Text = Miembro_Informacion_Familiar2_E.Padre_Edad.ToString();
            chkPadre_Empleado.Checked = Miembro_Informacion_Familiar2_E.Padre_Empleado;
            chkPadre_NegocioPropio.Checked = Miembro_Informacion_Familiar2_E.Padre_Negocio_Propio;
            txtPadre_Celular.Text = Miembro_Informacion_Familiar2_E.Padre_Celular;
            chkPadre_MiembroIglesia.Checked = Miembro_Informacion_Familiar2_E.Padre_Miembro_Iglesia;

            txtMadre_NombreCompleto.Text = Miembro_Informacion_Familiar2_E.Madre_Nombre_Completo;
            txtMadre_Edad.Text = Miembro_Informacion_Familiar2_E.Madre_Edad.ToString();
            chkMadre_Empleada.Checked = Miembro_Informacion_Familiar2_E.Madre_Empleada;
            chkMadre_NegocioPropio.Checked = Miembro_Informacion_Familiar2_E.Madre_Negocio_Propio;
            txtMadre_Celular.Text = Miembro_Informacion_Familiar2_E.Madre_Celular;
            chkMadre_MiembroIglesia.Checked = Miembro_Informacion_Familiar2_E.Madre_Miembro_Iglesia;

            txtHermano1_NombreCompleto.Text = Miembro_Informacion_Familiar2_E.Hermano1_Nombre_Completo;
            txtHermano1_Escolaridad.Text = Miembro_Informacion_Familiar2_E.Hermano1_Escolaridad;
            txtHermano1_CorreoElectronico.Text = Miembro_Informacion_Familiar2_E.Hermano1_Correo_Electronico;
            txtHermano1_Celular.Text = Miembro_Informacion_Familiar2_E.Hermano1_Celular;

            txtHermano2_NombreCompleto.Text = Miembro_Informacion_Familiar2_E.Hermano2_Nombre_Completo;
            txtHermano2_Escolaridad.Text = Miembro_Informacion_Familiar2_E.Hermano2_Escolaridad;
            txtHermano2_CorreoElectronico.Text = Miembro_Informacion_Familiar2_E.Hermano2_Correo_Electronico;
            txtHermano2_Celular.Text = Miembro_Informacion_Familiar2_E.Hermano2_Celular;

            txtHermano3_NombreCompleto.Text = Miembro_Informacion_Familiar2_E.Hermano3_Nombre_Completo;
            txtHermano3_Escolaridad.Text = Miembro_Informacion_Familiar2_E.Hermano3_Escolaridad;
            txtHermano3_CorreoElectronico.Text = Miembro_Informacion_Familiar2_E.Hermano3_Correo_Electronico;
            txtHermano3_Celular.Text = Miembro_Informacion_Familiar2_E.Hermano3_Celular;

            txtHermano4_NombreCompleto.Text = Miembro_Informacion_Familiar2_E.Hermano4_Nombre_Completo;
            txtHermano4_Escolaridad.Text = Miembro_Informacion_Familiar2_E.Hermano4_Escolaridad;
            txtHermano4_CorreoElectronico.Text = Miembro_Informacion_Familiar2_E.Hermano4_Correo_Electronico;
            txtHermano4_Celular.Text = Miembro_Informacion_Familiar2_E.Hermano4_Celular;

            txtHermano5_NombreCompleto.Text = Miembro_Informacion_Familiar2_E.Hermano5_Nombre_Completo;
            txtHermano5_Escolaridad.Text = Miembro_Informacion_Familiar2_E.Hermano5_Escolaridad;
            txtHermano5_CorreoElectronico.Text = Miembro_Informacion_Familiar2_E.Hermano5_Correo_Electronico;
            txtHermano5_Celular.Text = Miembro_Informacion_Familiar2_E.Hermano5_Celular;


            // Llenado de informacion de pasatiempos
            Miembro_Pasatiempos_E miembro_Pasatiempos_E = new Miembro_Pasatiempos_E();
            Miembro_Pasatiempos_N miembro_Pasatiempos_N = new Miembro_Pasatiempos_N();
            miembro_Pasatiempos_E = miembro_Pasatiempos_N.ObtenerRegistro(ID_REGISTRO);

            chkCine.Checked = miembro_Pasatiempos_E.Cine;
            chkLeer.Checked = miembro_Pasatiempos_E.Leer;
            chkVerTV.Checked = miembro_Pasatiempos_E.Ver_TV;
            chkSocializar.Checked = miembro_Pasatiempos_E.Socializar;
            chkViajar.Checked = miembro_Pasatiempos_E.Viajar;
            txtOtrosPasatiempos.Text = miembro_Pasatiempos_E.Otros;

            rtsTabulador.Tabs[1].Selected = true;
            rmpTabs.SelectedIndex = 1;
        }

        private void LimpiarCampos()
        {
            ID_REGISTRO = "0";
            EDITAR_REGISTRO = false;

            txtIdMiembro.Text = "(Nuevo)";
            txtNumeroMiembroAlternativo.Text = "0";
            txtNombres_Miembro.Text = "";
            txtApellidos_Miembro.Text = "";
            txtNombrePila.Text = "";
            rbtnSexo.SelectedValue = "1";
            dtpFechaNacimiento.SelectedDate = null;
            cmbEstadoCivil.SelectedValue = "1";
            txtEmail.Text = "";
            chkTieneHijos.Checked = false;
            txtCelular.Text = "";
            txtSector.Text = "";
            txtBarrio_Residencial.Text = "";
            txtCalle.Text = "";
            txtNumeroCasa.Text = "";

            chkNivelPrimario.Checked = false;
            chkNivelSecundario.Checked = false;
            chkGradoUniversitario.Checked = false;
            chkNivePostGrado_Maestria.Checked = false;

            chkEmpleadoPrivado.Checked = false;
            chkEmpleadoPublico.Checked = false;
            chkDuenoNegocio.Checked = false;
            chkIndependiente.Checked = false;
            chkOtros.Checked = false;
            txtNombreEmpresaNegocio.Text = "";

            txtNombreConyuge.Text = "";
            dtpFechaNacimiento_Conyuge.SelectedDate = null;
            chkConyugeCristiano.Checked = false;

            txtHijo1_Nombre.Text = "";
            dtpHijo1_FechaNacimiento.SelectedDate = null;
            chkHijo1_Cristiano.Checked = false;

            txtHijo2_Nombre.Text = "";
            dtpHijo2_FechaNacimiento.SelectedDate = null;
            chkHijo2_Cristiano.Checked = false;

            txtHijo3_Nombre.Text = "";
            dtpHijo3_FechaNacimiento.SelectedDate = null;
            chkHijo3_Cristiano.Checked = false;

            txtHijo4_Nombre.Text = "";
            dtpHijo4_FechaNacimiento.SelectedDate = null;
            chkHijo4_Cristiano.Checked = false;

            txtHijo5_Nombre.Text = "";
            dtpHijo5_FechaNacimiento.SelectedDate = null;
            chkHijo5_Cristiano.Checked = false;

            txtHijo6_Nombre.Text = "";
            dtpHijo6_FechaNacimiento.SelectedDate = null;
            chkHijo6_Cristiano.Checked = false;

            txtPadre_NombreCompleto.Text = "";
            txtPadre_Edad.Text = "";
            chkPadre_Empleado.Checked = false;
            chkPadre_NegocioPropio.Checked = false;
            txtPadre_Celular.Text = "";
            chkPadre_MiembroIglesia.Checked = false;

            txtMadre_NombreCompleto.Text = "";
            txtMadre_Edad.Text = "";
            chkMadre_Empleada.Checked = false;
            chkMadre_NegocioPropio.Checked = false;
            txtMadre_Celular.Text = "";
            chkMadre_MiembroIglesia.Checked = false;

            txtHermano1_NombreCompleto.Text = "";
            txtHermano1_Escolaridad.Text = "";
            txtHermano1_CorreoElectronico.Text = "";
            txtHermano1_Celular.Text = "";

            txtHermano2_NombreCompleto.Text = "";
            txtHermano2_Escolaridad.Text = "";
            txtHermano2_CorreoElectronico.Text = "";
            txtHermano2_Celular.Text = "";

            txtHermano3_NombreCompleto.Text = "";
            txtHermano3_Escolaridad.Text = "";
            txtHermano3_CorreoElectronico.Text = "";
            txtHermano3_Celular.Text = "";

            txtHermano4_NombreCompleto.Text = "";
            txtHermano4_Escolaridad.Text = "";
            txtHermano4_CorreoElectronico.Text = "";
            txtHermano4_Celular.Text = "";

            txtHermano5_NombreCompleto.Text = "";
            txtHermano5_Escolaridad.Text = "";
            txtHermano5_CorreoElectronico.Text = "";
            txtHermano5_Celular.Text = "";


            chkCine.Checked = false;
            chkLeer.Checked = false;
            chkVerTV.Checked = false;
            chkSocializar.Checked = false;
            chkViajar.Checked = false;

            txtOtrosPasatiempos.Text = "";

            chkEsMiembro.Checked = false;
            dtpDesdeCuandoMiembro.SelectedDate = null;
            chkPertenece_Ministerio.Checked = false;
            chkLe_Gustaria_Pertenecer_Ministerio.Checked = false;
            cmbRol_Miembro.SelectedValue = "0";
            txtOtroRol.Text = "";
            txtNombre_Diacono.Text = "";
            txtNombreLiderMinisterio.Text = "";
            txtComentariosDiaconoLiderMinisterio.Text = "";

            txtAutorizadoPor.Text = "";
            txtRevisadoPor.Text = "";

            txtNombres_Miembro.Focus();
        }

        private void GenerarReporteExcel()
        {
            // Se establece una lista con el nombre de las columnas del grid
            List<string> NombresColumnas = new List<string>();
            for (int i = 1; i < gvDatos.MasterTableView.Columns.Count; i++)
            {
                NombresColumnas.Add(gvDatos.MasterTableView.Columns[i].HeaderText);
            }

            // Se establece el nombre del reporte
            string NombreReporte = "Reporte_Miembros";
            DataTable dtReporte = DT_DATOS.Copy();

            // Se crea una tabla con los parametros de los filtros
            DataTable dtParametros = new DataTable();
            dtParametros.Columns.Add("Parametro");
            dtParametros.Columns.Add("Valor");

            dtParametros.Rows.Add("Iglesia de Dios La 33 Casa de Fe", "");
            dtParametros.Rows.Add("Relación de Miembros");
            dtParametros.Rows.Add("Fecha/Hora de reporte: " + string.Format("{0:dd/MM/yyyy hh:mm:ss tt}", DateTime.Now));
            dtParametros.Rows.Add("", "");
            dtParametros.Rows.Add("Filtros");
            dtParametros.Rows.Add("Tipo de fecha: ", rbtnTipoFecha.Items[rbtnTipoFecha.SelectedIndex].Text);

            dtParametros.Rows.Add("Fecha inicial: ", string.Format("{0:dd/MM/yyyy}", dtpFechaDesdeFiltro.SelectedDate));
            dtParametros.Rows.Add("Fecha final: ", string.Format("{0:dd/MM/yyyy}", dtpFechaHastaFiltro.SelectedDate));

            dtParametros.Rows.Add("Sexo: ", cmbSexoFiltro.Text);
            //dtParametros.Rows.Add("Via de registro: ", rbtnViaRegistro.Items[rbtnViaRegistro.SelectedIndex].Text);
            dtParametros.Rows.Add("Estado Civil: ", cmbEstadoCivilFiltro.Text);
            dtParametros.Rows.Add("Nombre, Apellido, Nombre de pila o ID: ", txtTextoBusquedaFiltro.Text);
            dtParametros.Rows.Add("Ministerio perteneciente: ", cmbMinisterioFiltro.Text);
            dtParametros.Rows.Add("", "");
            dtParametros.Rows.Add("Total de registros: ", Utilidad_N.FormatearNumero(dtReporte.Rows.Count.ToString(), 0, 0));

            // Se llama al metodo de generar reporte de Utilidad_C
            Utilidad_C utilidad_C = new Utilidad_C();
            utilidad_C.GenerarReporteExcel(dtParametros, dtReporte, NombresColumnas, NombreReporte, this.Page, null);
        }

        #endregion


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            Utilidad_C.RecargarTooltips(this, this.GetType());

            if (!Page.IsPostBack)
            {
                ((SiteMaster)Master).EstablecerNombrePantalla("Miembros");

                LlenarCombos();
                LimpiarFiltros();
                LimpiarCampos();
            }
        }

        // REGISTRO
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarRegistro();
        }

        protected void AgregarMinisterioMiembro_Click(object sender, EventArgs e)
        {

        }

        protected void gvMinisteriosMiembro_SortCommand(object sender, Telerik.Web.UI.GridSortCommandEventArgs e)
        {

        }

        protected void gvMinisteriosMiembro_PageSizeChanged(object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs e)
        {

        }

        protected void gvMinisteriosMiembro_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {

        }


        //CONSULTA
        protected void gvDatos_SortCommand(object sender, Telerik.Web.UI.GridSortCommandEventArgs e)
        {
            ActualizarGrid();
        }

        protected void gvDatos_PageSizeChanged(object sender, Telerik.Web.UI.GridPageSizeChangedEventArgs e)
        {
            ActualizarGrid();
        }

        protected void gvDatos_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            ActualizarGrid();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            ID_REGISTRO = btn.CommandArgument.ToString();
            VerRegistro();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        protected void btnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            LimpiarFiltros();
        }

        protected void btnGenerarPDF_Click(object sender, EventArgs e)
        {

        }

        protected void btnGenerarExcel_Click(object sender, EventArgs e)
        {
            GenerarReporteExcel();
        }


        #endregion
    }
}