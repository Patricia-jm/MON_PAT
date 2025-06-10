using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace Fabrica
{
    public partial class fabricante : System.Web.UI.Page
    {
        string CadenaConexion;
        SqlConnection conexion;

        protected void Page_Load(object sender, EventArgs e)
        {
            CadenaConexion = ConfigurationManager.ConnectionStrings["ConexionFb"].ConnectionString;
            conexion = new SqlConnection(CadenaConexion);

            if (!IsPostBack)
            {
                consultarFabricante();
            }
        }

        public void insertarFabricante(string Nombre, string PaisOrigen)
        {
            try
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_insertar_fabricante", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@Nombre", Nombre);
                    comando.Parameters.AddWithValue("@PaisOrigen", PaisOrigen);
                    SqlParameter returnValue = new SqlParameter();
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    comando.Parameters.Add(returnValue);
                    comando.ExecuteNonQuery();
                    int result = (int)returnValue.Value;

                    if (result == 0)
                    {
                        lbl_mensajes.Text = "Fabricante registrado correctamente.";
                        lbl_mensajes.CssClass = "alert alert-success";
                    }
                    else if (result == -1)
                    {
                        lbl_mensajes.Text = "Advertencia: El fabricante con este nombre ya existe.";
                        lbl_mensajes.CssClass = "alert alert-warning";
                    }
                    else
                    {
                        lbl_mensajes.Text = "Error desconocido al registrar el fabricante.";
                        lbl_mensajes.CssClass = "alert alert-danger";
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_mensajes.Text = "Error inesperado al registrar fabricante: " + ex.Message;
                lbl_mensajes.CssClass = "alert alert-danger";
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            insertarFabricante(txt_Nombre.Text, txt_PaisOrigen.Text);
            limpiarCampos();
            consultarFabricante();
        }

        protected void btn_eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_eliminar.Text))
                {
                    lbl_mensajes.Text = "Por favor, ingrese el ID del fabricante a eliminar.";
                    lbl_mensajes.CssClass = "alert alert-warning";
                    return;
                }
                if (!int.TryParse(txt_eliminar.Text, out int idFabricanteAEliminar))
                {
                    lbl_mensajes.Text = "El ID ingresado no es un número válido.";
                    lbl_mensajes.CssClass = "alert alert-warning";
                    return;
                }

                using (SqlCommand cmd = new SqlCommand("sp_eliminar_fabricante", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdFabricante", idFabricanteAEliminar);
                    conexion.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        lbl_mensajes.Text = "Fabricante eliminado correctamente.";
                        lbl_mensajes.CssClass = "alert alert-success";
                    }
                    else
                    {
                        lbl_mensajes.Text = "No se encontró el fabricante con el ID proporcionado.";
                        lbl_mensajes.CssClass = "alert alert-warning";
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_mensajes.Text = "Error al eliminar el fabricante: " + ex.Message;
                lbl_mensajes.CssClass = "alert alert-danger";
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
                limpiarCampos();
                consultarFabricante();
            }
        }

        protected void btn_buscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_buscar.Text))
                {
                    lbl_mensajes.Text = "Por favor, ingrese el ID del fabricante a buscar.";
                    lbl_mensajes.CssClass = "alert alert-warning";
                    consultarFabricante();
                    return;
                }
                if (!int.TryParse(txt_buscar.Text, out int idFabricanteABuscar))
                {
                    lbl_mensajes.Text = "El ID ingresado no es un número válido.";
                    lbl_mensajes.CssClass = "alert alert-warning";
                    consultarFabricante();
                    return;
                }

                using (SqlCommand cmd = new SqlCommand("sp_buscar_fabricante", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdFabricante", idFabricanteABuscar);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    grid_fabricantes.DataSource = dt;
                    grid_fabricantes.DataBind();

                    if (dt.Rows.Count > 0)
                    {
                        lbl_mensajes.Text = "Fabricante encontrado correctamente.";
                        lbl_mensajes.CssClass = "alert alert-success";
                    }
                    else
                    {
                        lbl_mensajes.Text = "No se encontró ningún fabricante con el ID proporcionado.";
                        lbl_mensajes.CssClass = "alert alert-warning";
                        consultarFabricante();
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_mensajes.Text = "Error al buscar fabricante: " + ex.Message;
                lbl_mensajes.CssClass = "alert alert-danger";
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
                txt_buscar.Text = "";
            }
        }

        public void modificarFabricante(int IdFabricante, string Nombre, string PaisOrigen)
        {
            try
            {
                conexion.Open();
                using (SqlCommand comando = new SqlCommand("sp_modificar_fabricante", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@IdFabricante", IdFabricante);
                    comando.Parameters.AddWithValue("@Nombre", Nombre);
                    comando.Parameters.AddWithValue("@PaisOrigen", PaisOrigen);
                    SqlParameter returnValue = new SqlParameter();
                    returnValue.Direction = ParameterDirection.ReturnValue;
                    comando.Parameters.Add(returnValue);
                    comando.ExecuteNonQuery();
                    int result = (int)returnValue.Value;

                    if (result == 0)
                    {
                        lbl_mensajes.Text = "Fabricante modificado correctamente.";
                        lbl_mensajes.CssClass = "alert alert-success";
                    }
                    else if (result == -1)
                    {
                        lbl_mensajes.Text = "Error: El nuevo nombre de fabricante ya existe.";
                        lbl_mensajes.CssClass = "alert alert-warning";
                    }
                    else if (result == -2)
                    {
                        lbl_mensajes.Text = "Error: No se encontró el fabricante con el ID proporcionado.";
                        lbl_mensajes.CssClass = "alert alert-warning";
                    }
                    else
                    {
                        lbl_mensajes.Text = "Error desconocido al modificar el fabricante.";
                        lbl_mensajes.CssClass = "alert alert-danger";
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_mensajes.Text = "Error inesperado al modificar fabricante: " + ex.Message;
                lbl_mensajes.CssClass = "alert alert-danger";
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
        }

        protected void btn_modificar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_IdFabricante.Text) || string.IsNullOrWhiteSpace(txt_ModificarNombre.Text) || string.IsNullOrWhiteSpace(txt_ModificarPaisOrigen.Text))
            {
                lbl_mensajes.Text = "Por favor, complete todos los campos (ID, Nuevo Nombre, Nuevo País de Origen) para modificar.";
                lbl_mensajes.CssClass = "alert alert-warning";
                return;
            }
            if (!int.TryParse(txt_IdFabricante.Text, out int idFabricante))
            {
                lbl_mensajes.Text = "El ID del fabricante no es un número válido.";
                lbl_mensajes.CssClass = "alert alert-warning";
                return;
            }

            modificarFabricante(idFabricante, txt_ModificarNombre.Text, txt_ModificarPaisOrigen.Text);
            limpiarCampos();
            consultarFabricante();
        }

        public void consultarFabricante()
        {
            using (SqlCommand comando = new SqlCommand("sp_consultar_fabricantes", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                try
                {
                    conexion.Open();
                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable tabla = new DataTable();
                    adaptador.Fill(tabla);
                    grid_fabricantes.DataSource = tabla;
                    grid_fabricantes.DataBind();
                }
                catch (Exception ex)
                {
                    lbl_mensajes.Text = "Error al consultar fabricantes: " + ex.Message;
                    lbl_mensajes.CssClass = "alert alert-danger";
                }
                finally
                {
                    if (conexion.State == ConnectionState.Open)
                    {
                        conexion.Close();
                    }
                }
            }
        }

        public void limpiarCampos()
        {
            txt_Nombre.Text = "";
            txt_PaisOrigen.Text = "";

            txt_IdFabricante.Text = "";
            txt_ModificarNombre.Text = "";
            txt_ModificarPaisOrigen.Text = "";

            txt_eliminar.Text = "";
            txt_buscar.Text = "";

            txt_IdFabricante.ReadOnly = false;
            lbl_mensajes.Text = "";
            lbl_mensajes.CssClass = "";
        }

        protected void grid_fabricantes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grid_fabricantes.SelectedRow != null)
            {
                GridViewRow row = grid_fabricantes.SelectedRow;

                txt_IdFabricante.Text = grid_fabricantes.DataKeys[row.RowIndex].Value.ToString();
                txt_ModificarNombre.Text = row.Cells[2].Text;
                txt_ModificarPaisOrigen.Text = row.Cells[3].Text;

                txt_IdFabricante.ReadOnly = true;
                lbl_mensajes.Text = "Datos cargados para modificación. Edite los campos y presione 'Modificar'.";
                lbl_mensajes.CssClass = "alert alert-info";

                txt_Nombre.Text = "";
                txt_PaisOrigen.Text = "";
            }
        }
    }
}