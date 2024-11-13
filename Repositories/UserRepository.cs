using Proyecto_Gestion.Dtos;
using Proyecto_Gestion.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Proyecto_Gestion.Repositories
{
    public class UserRepository
    {
        public int CreateUser(UserDto user)
        {
            int comando = 0;
            DbContextUtility Connection = new DbContextUtility();
            Connection.Connect();

            string SQL = "INSERT INTO [bdProyecto].[dbo].[usuario] (nit, tipo_docu, nombres, apellidos, correo, contrasenia, rol, estado) " +
                         "VALUES (@Nit, @Tipo_docu, @Nombres, @Apellidos, @Correo, @Contrasenia, @Rol,'C');";

            using (SqlCommand command = new SqlCommand(SQL, Connection.CONN()))
            {
                command.Parameters.AddWithValue("@Nit", user.Nit);
                command.Parameters.AddWithValue("@Tipo_docu", user.Tipo_docu);
                command.Parameters.AddWithValue("@Nombres", user.Nombres);
                command.Parameters.AddWithValue("@Apellidos", user.Apellidos);
                command.Parameters.AddWithValue("@Correo", user.Correo);
                command.Parameters.AddWithValue("@Contrasenia", user.Contrasenia);
                command.Parameters.AddWithValue("@Rol", 1); // Rol de Candidato por defecto

                comando = command.ExecuteNonQuery();
            }
            Connection.Disconnect();

            return comando;
        }

        //public int CreateUser(UserDto user)
        //{
        //    int comando = 0;
        //    DbContextUtility Connection = new DbContextUtility();
        //    Connection.Connect();

        //    string SQL = "INSERT INTO [bdProyecto].[dbo].[usuario] (nit, tipo_docu, nombres, apellidos, correo, contrasenia) " +
        //                 "VALUES (@Nit, @Tipo_docu, @Nombres, @Apellidos, @Correo, @Contrasenia);";

        //    using (SqlCommand command = new SqlCommand(SQL, Connection.CONN()))
        //    {
        //        command.Parameters.AddWithValue("@Nit", user.Nit);
        //        command.Parameters.AddWithValue("@Tipo_docu", user.Tipo_docu);
        //        command.Parameters.AddWithValue("@Nombres", user.Nombres);
        //        command.Parameters.AddWithValue("@Apellidos", user.Apellidos);
        //        command.Parameters.AddWithValue("@Correo", user.Correo);  // Correo añadido
        //        command.Parameters.AddWithValue("@Contrasenia", user.Contrasenia);

        //        comando = command.ExecuteNonQuery();
        //    }
        //    Connection.Disconnect();

        //    return comando;
        //}

        public bool BuscarUsuario(string nombres)
        {
            bool result = false;
            string SQL = "SELECT nombres, apellidos, correo, contrasenia, id_usuario, nit, tipo_docu " +
                         "FROM [bdProyecto].[dbo].[usuario] " +
                         "WHERE nombres = @Nombres;";

            DbContextUtility Connection = new DbContextUtility();
            Connection.Connect();

            using (SqlCommand command = new SqlCommand(SQL, Connection.CONN()))
            {
                command.Parameters.AddWithValue("@Nombres", nombres);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = true;
                    }
                }
            }
            Connection.Disconnect();

            return result;
        }

        public UserDto Login(UserDto user)
        {
            UserDto userResult = new UserDto();

            string SQL = "SELECT nombres, apellidos, correo, contrasenia, id_usuario, nit, tipo_docu, rol " +
                         "FROM [bdProyecto].[dbo].[usuario] " +
                         "WHERE nit = @Nit AND contrasenia = @Contrasenia;";

            DbContextUtility Connection = new DbContextUtility();
            Connection.Connect();

            using (SqlCommand command = new SqlCommand(SQL, Connection.CONN()))
            {
                command.Parameters.AddWithValue("@Nit", user.Nit);
                command.Parameters.AddWithValue("@Contrasenia", user.Contrasenia);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        userResult.Id_usuario = reader.GetInt32(reader.GetOrdinal("id_usuario"));
                        userResult.Nit = reader.GetInt32(reader.GetOrdinal("nit"));
                        userResult.Tipo_docu = reader.GetString(reader.GetOrdinal("tipo_docu"));
                        userResult.Nombres = reader.GetString(reader.GetOrdinal("nombres"));
                        userResult.Apellidos = reader.GetString(reader.GetOrdinal("apellidos"));
                        userResult.Correo = reader.GetString(reader.GetOrdinal("correo"));
                        userResult.Contrasenia = reader.GetString(reader.GetOrdinal("contrasenia"));
                        userResult.Rol = reader.GetInt32(reader.GetOrdinal("rol"));
                        userResult.Response = 1;  // Login exitoso
                    }
                    else
                    {
                        userResult.Response = 0;  // Credenciales incorrectas
                        userResult.Mensaje = "NIT o contraseña incorrectos.";
                    }
                }
            }
            Connection.Disconnect();

            return userResult;
        }

        //public UserDto Login(UserDto user)
        //{
        //    UserDto userResult = new UserDto();

        //    string SQL = "SELECT nombres, apellidos, correo, contrasenia, id_usuario, nit, tipo_docu " +
        //                 "FROM [bdProyecto].[dbo].[usuario] " +
        //                 "WHERE nit = @Nit AND contrasenia = @Contrasenia;";

        //    DbContextUtility Connection = new DbContextUtility();
        //    Connection.Connect();

        //    using (SqlCommand command = new SqlCommand(SQL, Connection.CONN()))
        //    {
        //        command.Parameters.AddWithValue("@Nit", user.Nit);
        //        command.Parameters.AddWithValue("@Contrasenia", user.Contrasenia);

        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            if (reader.Read())
        //            {
        //                userResult.Id_usuario = reader.GetInt32(reader.GetOrdinal("id_usuario"));
        //                userResult.Nit = reader.GetInt32(reader.GetOrdinal("nit"));
        //                userResult.Tipo_docu = reader.GetString(reader.GetOrdinal("tipo_docu"));
        //                userResult.Nombres = reader.GetString(reader.GetOrdinal("nombres"));
        //                userResult.Apellidos = reader.GetString(reader.GetOrdinal("apellidos"));
        //                userResult.Correo = reader.GetString(reader.GetOrdinal("correo")); // Correo añadido
        //                userResult.Contrasenia = reader.GetString(reader.GetOrdinal("contrasenia"));
        //                userResult.Response = 1;  // Login exitoso
        //            }
        //            else
        //            {
        //                userResult.Response = 0;  // Credenciales incorrectas
        //                userResult.Mensaje = "NIT o contraseña incorrectos.";
        //            }
        //        }
        //    }
        //    Connection.Disconnect();

        //    return userResult;
        //}

        public UserDto ObtenerUsuarioPorId(int id)
        {
            UserDto user = null;

            string sql = "SELECT id_usuario, nombres, apellidos, nit, tipo_docu, correo " +
                         "FROM [bdProyecto].[dbo].[usuario] " +
                         "WHERE id_usuario = @Id";

            DbContextUtility Connection = new DbContextUtility();
            Connection.Connect();

            using (SqlCommand command = new SqlCommand(sql, Connection.CONN()))
            {
                command.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new UserDto
                        {
                            Id_usuario = reader.GetInt32(0),
                            Nombres = reader.GetString(1),
                            Apellidos = reader.GetString(2),
                            Nit = reader.GetInt32(3),
                            Tipo_docu = reader.GetString(4),
                            Correo = reader.GetString(5)
                        };
                    }
                }
            }

            Connection.Disconnect();
            return user;
        }


        public (List<UserDto> Aceptados, List<UserDto> Rechazados, List<UserDto> Candidatos, List<UserDto> Empleados) GetCandidatos(string cargo = null)
        {
            List<UserDto> aceptados = new List<UserDto>();
            List<UserDto> rechazados = new List<UserDto>();
            List<UserDto> candidatos = new List<UserDto>();
            List<UserDto> empleados = new List<UserDto>();

            string sql = "SELECT id_usuario, nombres, apellidos, nit, tipo_docu, correo, estado " +
                             "FROM [bdProyecto].[dbo].[usuario] " +
                             "WHERE rol != 3"; // Solo usuarios con rol 1

                if (!string.IsNullOrEmpty(cargo))
                {
                    sql += " AND cargo = @Cargo";
                }

                DbContextUtility Connection = new DbContextUtility();
                Connection.Connect();

                using (SqlCommand command = new SqlCommand(sql, Connection.CONN()))
                {
                    if (!string.IsNullOrEmpty(cargo))
                    {
                        command.Parameters.AddWithValue("@Cargo", cargo);
                    }

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserDto candidato = new UserDto
                            {
                                Id_usuario = reader.GetInt32(0),
                                Nombres = reader.GetString(1),
                                Apellidos = reader.GetString(2),
                                Nit = reader.GetInt32(3),
                                Tipo_docu = reader.GetString(4),
                                Correo = reader.IsDBNull(5) ? null : reader.GetString(5),
                                Estado = reader.GetString(6)
                            };

                            if (candidato.Estado == "A")
                            {
                                aceptados.Add(candidato);
                            }
                            else if (candidato.Estado == "R")
                            {
                                rechazados.Add(candidato);
                            }
                            else if (candidato.Estado == "E")
                            {
                                empleados.Add(candidato);
                            }
                            else
                            {
                                candidatos.Add(candidato);
                            }
                        }
                    }
                }

                Connection.Disconnect();
                return (aceptados, rechazados, candidatos, empleados);
            }

            public void ActualizarEstadoUsuario(int idUsuario, char nuevoEstado)
            {
                string sql = "UPDATE [bdProyecto].[dbo].[usuario] SET estado = @Estado WHERE id_usuario = @IdUsuario";

                DbContextUtility Connection = new DbContextUtility();
                Connection.Connect();

                using (SqlCommand command = new SqlCommand(sql, Connection.CONN()))
                {
                    command.Parameters.AddWithValue("@Estado", nuevoEstado);
                    command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                    command.ExecuteNonQuery();
                }

                Connection.Disconnect();
            }
        }

    }
        //public List<UserDto> GetCandidatos(string cargo = null)
        //{
        //    List<UserDto> candidatos = new List<UserDto>();

//    // Consulta SQL para obtener usuarios con rol 1 (Candidato) y, opcionalmente, filtrar por cargo
//    string sql = "SELECT id_usuario, nombres, apellidos, nit, tipo_docu, correo " +
//                 "FROM [bdProyecto].[dbo].[usuario] " +
//                 "WHERE rol = 1"; // Filtra por rol 1 (Candidato)

//    // Añadir condición de filtrado por cargo si se proporciona un cargo
//    if (!string.IsNullOrEmpty(cargo))
//    {
//        sql += " AND cargo = @Cargo";
//    }

//    // Conexión a la base de datos
//    DbContextUtility Connection = new DbContextUtility();
//    Connection.Connect();

//    // Crear y ejecutar el comando SQL
//    using (SqlCommand command = new SqlCommand(sql, Connection.CONN()))
//    {
//        // Añadir parámetro de cargo si se proporciona
//        if (!string.IsNullOrEmpty(cargo))
//        {
//            command.Parameters.AddWithValue("@Cargo", cargo);
//        }

//        // Ejecutar el comando y leer los resultados
//        using (SqlDataReader reader = command.ExecuteReader())
//        {
//            while (reader.Read())
//            {
//                // Crear objeto UserDto con los datos del candidato
//                UserDto candidato = new UserDto
//                {
//                    Id_usuario = reader.GetInt32(0),
//                    Nombres = reader.GetString(1),
//                    Apellidos = reader.GetString(2),
//                    Nit = reader.GetInt32(3),
//                    Tipo_docu = reader.GetString(4),
//                    Correo = reader.IsDBNull(5) ? null : reader.GetString(5) // Verifica si el correo es nulo
//                };
//                // Añadir candidato a la lista
//                candidatos.Add(candidato);
//            }
//        }
//    }

// Desconectar de la base de datos
//    Connection.Disconnect();
//    return candidatos;
//}
