using CrudAdoNet.Models;
using System.Data.SqlClient;
using System.Data;
using System.Net.WebSockets;
using CrudAdoNet.Datos;

namespace crudAdoNet.Datos
{
    public class ContactoDatos
    {
        public List<ContactoModel> Listar()
        {
            var oLista = new List<ContactoModel>();
            var al = new Conexion();
            using (var conexion = new SqlConnection(al.getCadenaSql()))
            {
                //abrir la conexion
                conexion.Open();
                //comando a ejecutar
                SqlCommand cmd = new SqlCommand("sp_Listar", conexion);
                //decir el tipo de comando
                cmd.CommandType = CommandType.StoredProcedure;
                //leer el resultado de la ejecucion del procedimiento almacenado
                using(var dr = cmd.ExecuteReader()) 
                { 
                    while (dr.Read()) 
                    {
                        //una vez que se esten leyendo uno a uno, tambien los guardaremos
                        //en la lista
                        oLista.Add(new ContactoModel() 
                        {// se utilizan las propiedades de la clase
                            IdContacto = Convert.ToInt32(dr["IdContacto"]),
                            Nombre = dr["Nombre"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Clave = dr["Clave"].ToString()
                        });
                    }
                }
            }
            return oLista;
        }

        public ContactoModel ObtenerContacto(int IdContacto)
        {
            //creo un objeto vacio
            var oContacto = new ContactoModel();
            var cn = new Conexion();
            //utilizar using para establecer la cadena de conexion
            using(var conexion = new SqlConnection(cn.getCadenaSql()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener", conexion);
                //enviando un parametro al procedimiento almacenado
                cmd.Parameters.AddWithValue("IdContacto", IdContacto);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader()) 
                { 
                    while (dr.Read())
                    {
                        //asigno los valores al objeto oContacto
                        oContacto.IdContacto = Convert.ToInt32(dr["IdContacto"]);
                        oContacto.Nombre = dr["Nombre"].ToString();
                        oContacto.Telefono = dr["Telefono"].ToString();
                        oContacto.Correo = dr["Correo"].ToString();
                        oContacto.Clave = dr["Clave"].ToString();
                    }
                }
            }
            return oContacto;
        }

        public bool GuardarContacto(ContactoModel model)
        {
            //creo una variable boolean
            bool respuesta;
            try
            {
                var cn = new Conexion();
                //utilizar using para establecer la cadena de conexion
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Guardar", conexion);
                    //enviando un parametro al procedimiento almacenado
                    cmd.Parameters.AddWithValue("Nombre", model.Nombre);    
                    cmd.Parameters.AddWithValue("Telefono", model.Telefono);
                    cmd.Parameters.AddWithValue("Correo", model.Correo);
                    cmd.Parameters.AddWithValue("Clave", model.Clave);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //ejecutar el procedimiento almacenado
                    cmd.ExecuteNonQuery();
                    //si no ocurre un error la variable esta sera true
                    
                }
                respuesta = true;
            }
            catch (Exception e) { 
                string error = e.Message;
                respuesta = false;
            }
            return respuesta;
        }

        public bool EditarContacto(ContactoModel model)
        {
            //creo una variable boolean
            bool respuesta;
            try
            {
                var cn = new Conexion();
                //utilizar using para establecer la cadena de conexion
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Editar", conexion);
                    //enviando un parametro al procedimiento almacenado
                    cmd.Parameters.AddWithValue("IdContacto", model.IdContacto);
                    cmd.Parameters.AddWithValue("Nombre", model.Nombre);
                    cmd.Parameters.AddWithValue("Telefono", model.Telefono);
                    cmd.Parameters.AddWithValue("Correo", model.Correo);
                    cmd.Parameters.AddWithValue("Clave", model.Clave);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //ejecutar el procedimiento almacenado
                    cmd.ExecuteNonQuery();
                    //si no ocurre un error la variable esta sera true
                    
                }
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }
            return respuesta;
        }

        public bool EliminarContacto(int IdContacto)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSql()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_Eliminar", conexion);
                    cmd.Parameters.AddWithValue("IdContacto", IdContacto);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }
            return respuesta;
        }
    }
}
