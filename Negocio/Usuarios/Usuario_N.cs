using Datos.Usuarios;
using Entidades.Usuarios;
using Negocio.Util_N;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Usuarios
{
    public class Usuario_N
    {
        Usuario_D usuario_D = new Usuario_D();

        public Usuario_E Login(string Usuario, string Password)
        {
            try
            {
                return usuario_D.Login(Usuario, Utilidad_N.Encriptar(Password));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
