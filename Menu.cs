using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormularioWin
{
    public partial class Menu : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=DESKTOP-IQRAVC2\SQLEXPRESS01;Initial Catalog=dbProductos;Integrated Security=True");
        SqlCommand command;
        string imgLoc = "";
        public Menu()
        {
            InitializeComponent();

           /**/
        }

        private void Form1_Load(object sender, EventArgs e)
        {            

            // TODO: esta línea de código carga datos en la tabla 'dataSet1.Categoria' Puede moverla o quitarla según sea necesario.
            this.categoriaTableAdapter.Fill(this.dataSet1.Categoria);
            // TODO: esta línea de código carga datos en la tabla 'dataSet1.Productos' Puede moverla o quitarla según sea necesario.
            this.productosTableAdapter.Fill(this.dataSet1.Productos);

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            productosBindingSource.MovePrevious();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            productosBindingSource.MoveNext();
        }

        private void Añadir_Click(object sender, EventArgs e)
        {

            productosBindingSource.AddNew();
            productosTableAdapter.Update(dataSet1.Productos);
        }

        private void Guardar_Click(object sender, EventArgs e)
        {
            
            productosBindingSource.EndEdit();
            productosTableAdapter.Update(dataSet1.Productos);
            MessageBox.Show("Producto Actualizado", "Info");
        }

        private void Borrar_Click(object sender, EventArgs e)
        {
            productosBindingSource.RemoveCurrent();
            productosTableAdapter.Update(dataSet1.Productos);
        }

        private void Cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Busqueda_Click_1(object sender, EventArgs e)
        {
            this.Hide();

            Busqueda form = new Busqueda();
            form.Show();
        }

        private void BuscarImagen_Click(object sender, EventArgs e)
        {
            try
            {
                
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif|All Files (*.*)|*.*";
                dlg.Title = "Seleciona la imagen del producto";
                if(dlg.ShowDialog() == DialogResult.OK)
                {
                    imgLoc = dlg.FileName.ToString();
                    pictureBox1.ImageLocation = imgLoc;
                }
            }
            catch (Exception ex)
            {            
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            try
            {
                //Las imagenes se guardan en tipo byte
                string categoria = comboBox1.Text;
                string texto = textBox2.Text.Replace(",", ".");
                //decimal precio = Convert.ToDecimal(texto);
                //MessageBox.Show(textBox1.Text + textBox2.Text + categoria, "debugg");
                byte[] img = null;
                //Abrimos el flujo de datos con el stream y le permitimos acceder y almacenar la imagen
                FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length); //Almacenamos la imagen en una array de tipo byte
                string sql = "INSERT INTO Productos(Nombre,Precio,Categoria,Imagen)VALUES('"+textBox1.Text+"',"+texto+ ","+ categoria + " ,@img)";
                


                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                    command = new SqlCommand(sql, connection);
                    //command.Parameters.AddWithValue("@precio", precio);
                    command.Parameters.Add(new SqlParameter("@img", img));
                    int x = command.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show(x.ToString() + " datos guardados.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void Actualizar_Click(object sender, EventArgs e)
        {
             try
             {
                string categoria;
                categoria = comboBox1.Text;
                string texto = textBox2.Text.Replace(",", ".");
                double precio;
                precio = Convert.ToDouble(texto);
                //MessageBox.Show(textBox1.Text + textBox2.Text + categoria, "debugg");
                byte[] img = null;
                 //Abrimos el flujo de datos con el stream y le permitimos acceder y almacenar la imagen
                 FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
                 BinaryReader br = new BinaryReader(fs);
                 img = br.ReadBytes((int)fs.Length); //Almacenamos la imagen en una array de tipo byte
                 string sqlUpd = "Update Productos SET Nombre = '" + textBox1.Text + "',Precio = " + precio + ",Categoria = " + categoria + ",Imagen = @img WHERE Nombre = '" + textBox1.Text + "'";
                 if (connection.State != ConnectionState.Open)
                 {
                     connection.Open();
                     command = new SqlCommand(sqlUpd, connection);
                     command.Parameters.Add(new SqlParameter("@img", img));
                     int y = command.ExecuteNonQuery();
                     connection.Close();
                     MessageBox.Show(y.ToString() + " producto(s) actualizado(s).", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }

        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            this.Hide();

            Menu form = new Menu();
            form.Show();
        }
        
    }
}
