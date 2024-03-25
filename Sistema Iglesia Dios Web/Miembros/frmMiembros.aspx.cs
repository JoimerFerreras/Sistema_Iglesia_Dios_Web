// Autor: Joimer Ferreras

using Entidades.Miembros;
using Negocio.Miembros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sistema_Iglesia_Dios_Web.Miembros
{
    public partial class frmMiembros : System.Web.UI.Page
    {
        #region Declaraciones
        Miembro_N miembro_N = new Miembro_N();
        #endregion

        #region Metodos/ Procedimientos
        private void AgregarMiembro()
        {
            // Agregacion de la informacion basica del miembro

            Miembro_E miembro_E = new Miembro_E();
            miembro_E.Nombres = txtNombres_Miembro.Text;
            miembro_E.Apellidos = txtApellidos_Miembro.Text;
            miembro_E.Nombre_Pila = txtNombrePila.Text;
            miembro_E.Sexo = int.Parse(rbtnSexo.SelectedValue);
            miembro_E.Fecha_Nacimiento = dtpFechaNacimiento.SelectedDate.Value;
            miembro_E.Estado_Civil = int.Parse(cmbEstadoCivil.SelectedValue);
            miembro_E.Tiene_Hijos = chkTieneHijos.Checked;
            miembro_E.Email = txtEmail.Text;
            miembro_E.Celular = txtCelular.Text;
            miembro_E.Sector = txtSector.Text;
            miembro_E.Barrio_Residencial = txtBarrio_Residencial.Text;
            miembro_E.Calle = txtCalle.Text;
            miembro_E.Numero_Casa = txtNumeroCasa.Text;
            miembro_E.Numero_Alternativo_Miembro = int.Parse(txtNumeroMiembroAlternativo.Text);
            miembro_E.Es_Miembro = chkEsMiembro.Checked;
            miembro_E.Desde_Cuando_Miembro = dtpDesdeCuandoMiembro.SelectedDate.Value;
            miembro_E.Pertenece_Ministerio = chkPertenece_Ministerio.Checked;
            miembro_E.Le_Gustaria_Pertenecer_Ministerio = chkLe_Gustaria_Pertenecer_Ministerio.Checked;
            miembro_E.Rol_Miembro = int.Parse(cmbRol_Miembro.SelectedValue);
            miembro_E.Otro_Rol = txtOtroRol.Text;
            miembro_E.Nombre_Diacono = txtNombre_Diacono.Text;
            miembro_E.Nombre_Lider_Ministerio = txtNombreLiderMinisterio.Text;
            miembro_E.Comentarios_Diacono_Lider_Ministerio = txtComentariosDiaconoLiderMinisterio.Text;
            miembro_E.Revisado_Por = txtRevisadoPor.Text;
            miembro_E.Autorizado_Por = txtAutorizadoPor.Text;


            // Agregacion del nivel academica del miembro
            Miembro_Nivel_Academico_E nivel_academico_E = new Miembro_Nivel_Academico_E();
            nivel_academico_E.Primario = chkNivelPrimario.Checked;
            nivel_academico_E.Secundario = chkNivelSecundario.Checked;
            nivel_academico_E.Grado_Universitario = chkGradoUniversitario.Checked;
            nivel_academico_E.Post_Grado_Maestria = chkNivePostGrado_Maestria.Checked;


            // Agregacion de la informacion laboral del miembro
            Miembro_Informacion_Laboral_E info_laboral_E = new Miembro_Informacion_Laboral_E();
            info_laboral_E.Empleado_Privado = chkEmpleadoPrivado.Checked;
            info_laboral_E.Empleado_Publico = chkEmpleadoPublico.Checked;
            info_laboral_E.Independiente = chkIndependiente.Checked;
            info_laboral_E.Otros = chkOtros.Checked;
            info_laboral_E.Nombre_Empresa_Negocio = txtNombreEmpresaNegocio.Text;


            // Agregacion de la informacion familiar 1 del miembro
            Miembro_Informacion_Familiar1_E info_familiar1_E = new Miembro_Informacion_Familiar1_E();
            info_familiar1_E.Conyuge_Nombre = txtNombreConyuge.Text;
            info_familiar1_E.Conyuge_Cristiano = chkConyugeCristiano.Checked;
            info_familiar1_E.Conyuge_FechaNacimiento = dtpFechaNacimiento_Conyuge.SelectedDate.Value;

            info_familiar1_E.Hijo1_Nombre = txtHijo1_Nombre.Text;
            info_familiar1_E.Hijo1_Cristiano = chkHijo1_Cristiano.Checked;
            info_familiar1_E.Hijo1_FechaNacimiento = dtpHijo1_FechaNacimiento.SelectedDate.Value;

            info_familiar1_E.Hijo2_Nombre = txtHijo2_Nombre.Text;
            info_familiar1_E.Hijo2_Cristiano = chkHijo2_Cristiano.Checked;
            info_familiar1_E.Hijo2_FechaNacimiento = dtpHijo2_FechaNacimiento.SelectedDate.Value;

            info_familiar1_E.Hijo3_Nombre = txtHijo3_Nombre.Text;
            info_familiar1_E.Hijo3_Cristiano = chkHijo3_Cristiano.Checked;
            info_familiar1_E.Hijo3_FechaNacimiento = dtpHijo3_FechaNacimiento.SelectedDate.Value;


            info_familiar1_E.Hijo4_Nombre = txtHijo4_Nombre.Text;
            info_familiar1_E.Hijo4_Cristiano = chkHijo4_Cristiano.Checked;
            info_familiar1_E.Hijo4_FechaNacimiento = dtpHijo4_FechaNacimiento.SelectedDate.Value;

            info_familiar1_E.Hijo5_Nombre = txtHijo5_Nombre.Text;
            info_familiar1_E.Hijo5_Cristiano = chkHijo5_Cristiano.Checked;
            info_familiar1_E.Hijo5_FechaNacimiento = dtpHijo5_FechaNacimiento.SelectedDate.Value;

            info_familiar1_E.Hijo6_Nombre = txtHijo6_Nombre.Text;
            info_familiar1_E.Hijo6_Cristiano = chkHijo6_Cristiano.Checked;
            info_familiar1_E.Hijo6_FechaNacimiento = dtpHijo6_FechaNacimiento.SelectedDate.Value;


            // Agregacion de la informacion familiar 2 del miembro
            Miembro_Informacion_Familiar2_E info_familiar2_E = new Miembro_Informacion_Familiar2_E();
            info_familiar2_E.Padre_Nombre_Completo = txtPadre_NombreCompleto.Text;
            info_familiar2_E.Padre_Edad = int.Parse(txtPadre_Edad.Text);
            info_familiar2_E.Padre_Empleado = chkPadre_Empleado.Checked;
            info_familiar2_E.Padre_Negocio_Propio = chkPadre_NegocioPropio.Checked;
            info_familiar2_E.Padre_Celular = txtPadre_Celular.Text;
            info_familiar2_E.Padre_Miembro_Iglesia = chkPadre_MiembroIglesia.Checked;

            info_familiar2_E.Madre_Nombre_Completo = txMadre_NombreCompleto.Text;
            info_familiar2_E.Madre_Edad = int.Parse(txtMadre_Edad.Text);
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


            string MensajeSalida = miembro_N.Agregar(
                miembro_E,
                info_familiar1_E,
                info_familiar2_E,
                info_laboral_E,
                nivel_academico_E,
                pasatiempos_E);
        }

        #endregion



        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            AgregarMiembro();
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
        #endregion
    }
}