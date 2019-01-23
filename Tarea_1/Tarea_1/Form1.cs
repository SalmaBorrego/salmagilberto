using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml;
using System.Data.SqlClient;


namespace Tarea_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(miconexion.conexion);

        string ruta;
        Empleados E = new Empleados();
        public static List<Empleados> ListaEmpleado;

        public void EmpleadoLista()
        {
            ListaEmpleado = (from c in XDocument.Load(ruta).Root.Elements("Empleado")
                             select new Empleados
                             {

                                 ID_E = (int)c.Element("ID_E"),
                                 Nombres_E = (string)c.Element("Nombres"),
                                 Apellidos_E = (string)c.Element("Apellidos"),
                                 Edad_E = (string)c.Element("Edad_E"),
                                 FechaIngreso_E = (string)c.Element("FechaIngreso_E"),
                                 Horas_E = (string)c.Element("Horas_E"),
                                 Sueldo_E = (string)c.Element("Sueldo_E"),
                                 Pago_E = (string)c.Element("Pago_E"),
                                 Observaciones_E = (string)c.Element("Observaciones_E"),

                             }).ToList();
            dataGridView1.DataSource = ListaEmpleado;

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Registro(); 
        }

        private void Registro()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("Registro_Empleado", con);
            try
            {
                //foreach (DataGridViewRow row in dataGridView1.Rows)
                //{


                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_empleado", Convert.ToInt32(txtID.Text));
                    cmd.Parameters.AddWithValue("@nombre", Convert.ToString(txtNombres.Text));
                    cmd.Parameters.AddWithValue("@apellidos", Convert.ToString(txtApellidos.Text));
                    cmd.Parameters.AddWithValue("@edad", Convert.ToString(txtEdad.Text));
                    cmd.Parameters.AddWithValue("@fecha_ingreso", Convert.ToString(dateTimePicker1.Text));
                    cmd.Parameters.AddWithValue("@horas", Convert.ToString(txtHoras.Text));
                    cmd.Parameters.AddWithValue("@sueldo", Convert.ToString(txtSueldo.Text));
                    cmd.Parameters.AddWithValue("@pago", Convert.ToString(txtPago.Text));
                    cmd.Parameters.AddWithValue("@observaciones", Convert.ToString(txtObservaciones.Text));
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Empelado Registrado con Exito", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    }
                    else
                        MessageBox.Show("No fue Registrado, Intente Nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            //}
            catch (Exception)
            {
                DialogResult res = MessageBox.Show("Deseas soobrescribir", "Empleado existente", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.Yes)
                {
                    SqlCommand cm = new SqlCommand("actualizar_Empleado", con);

                    //foreach (DataGridViewRow row in dataGridView1.Rows)
                    //{


                        cm.CommandType = CommandType.StoredProcedure;
                       
                        cm.Parameters.AddWithValue("@id_empleado", Convert.ToInt32(txtID.Text));
                        cm.Parameters.AddWithValue("@nombre", Convert.ToString(txtNombres.Text));
                        cm.Parameters.AddWithValue("@apellidos", Convert.ToString(txtApellidos.Text));
                        cm.Parameters.AddWithValue("@edad", Convert.ToString(txtEdad.Text));
                        cm.Parameters.AddWithValue("@fecha_ingreso", Convert.ToString(dateTimePicker1.Text));
                        cm.Parameters.AddWithValue("@horas", Convert.ToString(txtHoras.Text));
                        cm.Parameters.AddWithValue("@sueldo", Convert.ToString(txtSueldo.Text));
                        cm.Parameters.AddWithValue("@pago", Convert.ToString(txtPago.Text));
                        cm.Parameters.AddWithValue("@observaciones", Convert.ToString(txtObservaciones.Text));
                        int result = cm.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Empelado Registrado con Exito", "Registro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        }
                        else
                            MessageBox.Show("No fue Registrado, Intente Nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                //}


            }

           con.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = "";
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        void Limpiar()
        {
            groupBox1.Controls.OfType<TextBox>().ToList().ForEach(tb => tb.Text = string.Empty);
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            OpenFileDialog abrir = new OpenFileDialog();
            abrir.Filter = "Tipo XML|*.xml";
            if (abrir.ShowDialog() == DialogResult.OK)
            {
                ruta = abrir.FileName;
                EmpleadoLista();
                if (ListaEmpleado.Count > 1)
                {
                    Limpiar();
                    EmpleadoLista();
                }
                else
                {
                    try
                    {
                        dataGridView1.CurrentCell = null;
                        dataGridView1.Rows[0].Visible = false;
                        txtID.Text = dataGridView1.Rows[0].Cells[0].Value.ToString();
                        txtNombres.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                        txtApellidos.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
                        txtEdad.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
                        dateTimePicker1.Text = dataGridView1.Rows[0].Cells[4].Value.ToString();
                        txtHoras.Text = dataGridView1.Rows[0].Cells[5].Value.ToString();
                        txtSueldo.Text = dataGridView1.Rows[0].Cells[6].Value.ToString();
                        txtPago.Text = dataGridView1.Rows[0].Cells[7].Value.ToString();
                        txtObservaciones.Text = dataGridView1.Rows[0].Cells[8].Value.ToString();
                        dataGridView1.DataSource = null;
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtNombres.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtApellidos.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtEdad.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtHoras.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtSueldo.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtObservaciones.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();

            }
            catch { }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
