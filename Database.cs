using MySql.Data.MySqlClient;
using System.Data.Common;


namespace Clase13
{
    public class Database
    {
        private MySqlConnection _connection;
        public Database() {
            _connection = new MySqlConnection("Server=localhost;Database=escuela2;Uid=root;Pwd=password;Port=33061");
        }

        public bool ValidarConexion()
        {
            _connection.Open();
            DbCommand command = _connection.CreateCommand();
            command.CommandText = "SELECT 1";
            DbDataReader reader = command.ExecuteReader();

            bool lectura = reader.Read();
            _connection.Close();
            return lectura;
        }
        public List<Alumno> GetAlumnos()
        {
            _connection.Open();
            DbCommand command = _connection.CreateCommand();
            command.CommandText = "SELECT id, nombre, carnet, direccion from alumno ";
            DbDataReader reader = command.ExecuteReader();
            List<Alumno> alumnos = new List<Alumno>();

            while (reader.Read()) {
                if (reader.HasRows)
                {
                    alumnos.Add(new Alumno
                    {
                        Id = reader.GetInt32(0),
                        Nombre = !reader.IsDBNull(1) ? reader.GetString(1) : "",
                        Carnet = !reader.IsDBNull(2) ? reader.GetString(2) : "",
                        Direccion = !reader.IsDBNull(3) ?  reader.GetString(3) : ""
                    });
                }
               
            }
            _connection.Close();
            return alumnos;
        }

        public int InsertarAlumno(Alumno alumno) {
            _connection.Open();
            MySqlCommand cmd = _connection.CreateCommand();
            cmd.CommandText = "INSERT INTO alumno (nombre, carnet, direccion) VALUES (@nombre, @carnet, @direccion)";

            cmd.Parameters.AddWithValue("@nombre", alumno.Nombre);
            cmd.Parameters.AddWithValue("@carnet", alumno.Carnet);
            cmd.Parameters.AddWithValue("@direccion", alumno.Direccion);

            var result = cmd.ExecuteNonQuery();
            _connection.Close();
            return result;
        }

        public Alumno BuscarPorId(int id) {
            _connection.Open();
            MySqlCommand cmd = _connection.CreateCommand();
            cmd.CommandText = "SELECT id, nombre, carnet, direccion FROM alumno WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", id);

            var reader = cmd.ExecuteReader();

            Alumno alumno = new Alumno();

            while (reader.Read())
            {
                if (reader.HasRows)
                {
                    alumno.Id = reader.GetInt32(0);
                    alumno.Nombre = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                    alumno.Carnet = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    alumno.Direccion = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                }

            }
            _connection.Close();
            return alumno;
        }
    }
}
