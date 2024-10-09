using Oracle.ManagedDataAccess.Client;
using personCatalogAPI.DTO;
using personCatalogAPI.Model;
using System.Data;

namespace personCatalogAPI.Service
{
    public class PersonService
    {
        private readonly string _connectionString;
        public PersonService(IConfiguration configuration) 
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection");
        }

        public async Task<List<Person>> GetAll()
        {
            var persons = new List<Person>();
            OracleConnection connection = null;
            OracleCommand command = null;
            OracleDataReader reader = null;

            try
            {
                connection = new OracleConnection(_connectionString);
                await connection.OpenAsync();
                command = new OracleCommand("Select * from VIEW_PERSON", connection);
                reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync()) 
                {
                    persons.Add(new Person
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                        Apellido = reader.GetString(reader.GetOrdinal("apellido")),
                        Email = reader.GetString(reader.GetOrdinal("email")),
                        Direccion = reader.GetString(reader.GetOrdinal("direccion"))
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    await reader.CloseAsync();
                    await reader.DisposeAsync();
                }

                if (!String.IsNullOrEmpty(command.ToString()))
                    await command.DisposeAsync();
                
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    await connection.CloseAsync();
                    await connection.DisposeAsync();
                }

            }
            
            return persons;
            
        }

        public async Task<Person> GetPersonByID(int Id)
        {
            OracleConnection connection = null;
            OracleCommand command = null;
            OracleDataReader reader = null;
            Person person = null;

            try
            {
                connection = new OracleConnection(_connectionString);
                await connection.OpenAsync();
                command = new OracleCommand("GetPersonByID", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("id", OracleDbType.Int32).Value = Id;
                command.Parameters.Add("cur", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    person = new Person
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        Nombre = reader.GetString(reader.GetOrdinal("nombre")),
                        Apellido = reader.GetString(reader.GetOrdinal("apellido")),
                        Email = reader.GetString(reader.GetOrdinal("email")),
                        Direccion = reader.GetString(reader.GetOrdinal("direccion"))
                    };
                }

            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    await reader.CloseAsync();
                    await reader.DisposeAsync();
                }

                if (command != null)
                    await command.DisposeAsync();

                if (connection != null && connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                    await connection.DisposeAsync();
                }
            }

            return person;
        }

        public async Task AddPerson(PersonCreateDTO personDTO)
        {
            OracleConnection connection = null;
            OracleCommand command = null;

            try
            {
                connection = new OracleConnection(_connectionString);
                await connection.OpenAsync();
                command = new OracleCommand("addPerson", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.Add("nombre", OracleDbType.Varchar2).Value = personDTO.Nombre;
                command.Parameters.Add("apellido", OracleDbType.Varchar2).Value = personDTO.Apellido;
                command.Parameters.Add("email", OracleDbType.Varchar2).Value = personDTO.Email;
                command.Parameters.Add("direccion", OracleDbType.Varchar2).Value = personDTO.Direccion;

                await command.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (command != null)
                    await command.DisposeAsync();

                if (connection != null && connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                    await connection.DisposeAsync();
                }
            }
        }

        public async Task UpdatePerson(Person person)
        {
            OracleConnection connection = null;
            OracleCommand command = null;

            try
            {
                connection = new OracleConnection(_connectionString);
                await connection.OpenAsync();
                command = new OracleCommand("updatePerson", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("id", OracleDbType.Int32).Value = person.Id;
                command.Parameters.Add("nombre", OracleDbType.Varchar2).Value = person.Nombre;
                command.Parameters.Add("apellido", OracleDbType.NVarchar2).Value = person.Apellido;
                command.Parameters.Add("email", OracleDbType.NVarchar2).Value = person.Email;
                command.Parameters.Add("direccion", OracleDbType.NVarchar2).Value = person.Direccion;

                await command.ExecuteNonQueryAsync();

            }
            finally
            {
                if (command != null)
                    await command.DisposeAsync();
                
                if (connection != null && connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                    await connection.DisposeAsync();
                }
            }

        }

        public async Task DeletePerson (int Id)
        {
            OracleConnection connection = null;
            OracleCommand command = null;

            try
            {
                connection = new OracleConnection(_connectionString);
                await connection.OpenAsync();
                command = new OracleCommand("deletePerson", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.Add("id", OracleDbType.Int32).Value = Id;

                await command.ExecuteNonQueryAsync();
            }
            finally
            {
                if (command != null)
                    await command.DisposeAsync();

                if (connection != null && connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                    await connection.DisposeAsync();
                }
            }
        }
    }
}
