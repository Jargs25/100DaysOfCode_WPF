using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace _100DaysOfCode_WPF
{
    public class conexion
    {
        protected SqlConnection conn;

        public void EstablecerConexion()
        {
            string user = "TuUsuario";
            string bd = "TuBD";
            string pass = "TuContraseña*";
            string server = "TuServer";

            string sc = string.Format("User Id ={0}; Password={1}; Initial Catalog={2}; Server={3}",user, pass, bd, server);

            conn = new SqlConnection(sc);
        }
    }
}
