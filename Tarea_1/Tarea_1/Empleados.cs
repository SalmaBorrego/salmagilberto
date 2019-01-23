using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Tarea_1
{
    [Serializable]
    public class Empleados
    {
        public int ID_E { get; set; }
        public string Nombres_E { get; set; }
        public string Apellidos_E { get; set; }
        public string Edad_E { get; set; }
        public string FechaIngreso_E { get; set; }
        public string Horas_E { get; set; }
        public string Sueldo_E { get; set; }
        public string Pago_E { get; set; }
        public string Observaciones_E { get; set; }




        SqlConnection con = new SqlConnection(miconexion.conexion);

        public void listarClientes(DataGridView data)
        {
            try
            {
                con.Open();
                SqlCommand comando = new SqlCommand("select *from Empleado", con);
                comando.Connection = con;
                comando.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(comando);
                da.Fill(dt);
                data.DataSource = dt;
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Llame al Administrador");
            }
        }
    }
}
