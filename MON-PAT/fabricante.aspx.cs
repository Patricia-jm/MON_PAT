using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MON_PAT
{
    public partial class fabricante : System.Web.UI.Page
    {
        String CadenaConexion;
        SqlConnection conexion;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CadenaConexion = ConfigurationManager.ConnectionStrings["mon_pat"].ConnectionString;
            this.conexion = new SqlConnection(CadenaConexion);
            consultarFabricante();
        }

        public void insertarFabricante(string Nombre, string PaisOrigen)
        {
            try
            {
                this.conexion.Open();

                SqlCommand comando = new SqlCommand("sp_insertar_fabricante", this.conexion);
                comando.CommandType = System.Data.CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@Nombre", Nombre);
                comando.Parameters.AddWithValue("@PaisOrigen", PaisOrigen);

                comando.ExecuteNonQuery();

                lbl_mensajes.Text = "Fabricante registrado correctamente.";
                lbl_mensajes.CssClass = "alert alert-success";
            }
            catch (Exception ex)
            {
                lbl_mensajes.Text = "Error: " + ex.Message;
                lbl_mensajes.CssClass = "alert alert-danger";
            }
            finally
            {
                this.conexion.Close();
            }


        }
        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            insertarFabricante(
                txt_Nombre.Text,
                txt_PaisOrigen.Text
            );

            limpiarCampos();
            consultarFabricante();
        }
        public void consultarFabricante()
        {
            SqlCommand comando = new SqlCommand("SELECT * FROM Fabricante", this.conexion);

            try
            {
                this.conexion.Open();
                SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                grid_fabricantes.DataSource = tabla;
                grid_fabricantes.DataBind();
            }
            catch (Exception ex)
            {
                lbl_mensajes.Text = "Error al consultar fabricante: " + ex.Message;
                lbl_mensajes.CssClass = "alert alert-danger";
            }
            finally
            {
                this.conexion.Close();
            }
        }

        // Limpiar los campos de texto
        public void limpiarCampos()
        {
            txt_Nombre.Text = "";
            txt_PaisOrigen.Text = "";
        }



    }
}