using System.Data.SqlClient;

namespace CrudAdoNet.Datos
{
    public class Conexion
    {
        private string cadenaSql = string.Empty;

        public Conexion()
        {
            //para obtener la cadena de conexion qwue etá en el archivo appsetttings.json
            var Builder = new ConfigurationBuilder().SetBasePath
                (Directory.GetCurrentDirectory()).AddJsonFile
                ("appsettings.json").Build();
            //para obtener el valor de la cadena de conexion
            cadenaSql = Builder.GetSection("ConnectionStrings:cadenaSql").Value;
        }
        //crear metodo para devolver la cadena
        public string getCadenaSql()
        {
            return cadenaSql;
        }
    }
}
