using Entidades.Miembros;
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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Miembro_E miembro_E = new Miembro_E();
            miembro_E.Nombres = txtNombres_Miembro.Text;
            miembro_E.Apellidos = txtApellidos_Miembro.Text;
            miembro_E.Nombre_Pila = txtNombrePila.Text;
            miembro_E.Sexo = int.Parse(rbtnSexo.SelectedValue);
            miembro_E.Fecha_Nacimiento = dtpFechaNacimiento.SelectedDate.Value;
            miembro_E.Estado_Civil = int.Parse(cmbEstadoCivil.SelectedValue);
            miembro_E.Email = txtEmail.Text;
            miembro_E.Celular = txtCelular.Text;
            miembro_E.Sector = txtSector.Text;
            miembro_E.Barrio_Residencial = txtBarrio_Residencial.Text;
            miembro_E.Calle = txtCalle.Text;
            miembro_E.Numero_Casa = txtNumeroCasa.Text;
            miembro_E.Numero_Alternativo_Miembro = int.Parse(txtNumeroMiembroAlternativo.Text);


        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }
    }
}