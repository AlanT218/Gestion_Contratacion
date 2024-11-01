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

            // Consulta SQL 
            string SQL = "INSERT INTO [bdProyecto].[dbo].[usuario] (nit, tipo_docu, nombres, apellidos, contrasenia) " +
                         "VALUES (" + user.Nit + ", '" + user.Tipo_docu + "', '" +
                         user.Nombres + "', '" + user.Apellidos + "', '" + user.Contrasenia + "');";

            using (SqlCommand command = new SqlCommand(SQL, Connection.CONN()))
            {
                comando = command.ExecuteNonQuery();
            }
            Connection.Disconnect();

            return comando;
        }

        public bool BuscarUsuario(string nombres)
        {
            bool result = false;
            string SQL = "SELECT nombres, apellidos, contrasenia, id_usuario, nit, tipo_docu " +
                         "FROM [bdProyecto].[dbo].[usuario] " +
                         "WHERE nombres = '" + nombres + "';";

            DbContextUtility Connection = new DbContextUtility();
            Connection.Connect();

            using (SqlCommand command = new SqlCommand(SQL, Connection.CONN()))
            {
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

            // Modificar la consulta SQL para usar Nit y Contrasenia
            string SQL = "SELECT nombres, apellidos, contrasenia, id_usuario, nit, tipo_docu " +
                         "FROM [bdProyecto].[dbo].[usuario] " +
                         "WHERE nit = @Nit AND contrasenia = @Contrasenia;";

            DbContextUtility Connection = new DbContextUtility();
            Connection.Connect();

            using (SqlCommand command = new SqlCommand(SQL, Connection.CONN()))
            {
                // Usar parámetros para evitar SQL injection
                command.Parameters.AddWithValue("@Nit", user.Nit);
                command.Parameters.AddWithValue("@Contrasenia", user.Contrasenia);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        userResult.Id_usuario = reader.GetInt32(3);
                        userResult.Nit = reader.GetInt32(4);
                        userResult.Tipo_docu = reader.GetString(5);
                        userResult.Nombres = reader.GetString(0);
                        userResult.Apellidos = reader.GetString(1);
                        userResult.Contrasenia = reader.GetString(2);
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
    }
        //public class UserRepository
        //{
        //    public int CreateUser(UserDto user)
        //    {
        //        int comando = 0;
        //        DbContextUtility Connection = new DbContextUtility();
        //        Connection.Connect();
        //        //consulta SQL
        //        string SQL = "INSERT INTO TEST.dbo.[USER] (id_role,id_state,name,username,password) "
        //                    + "VALUES (" + user.IdRole + "," + user.IdState + ",'" + user.Name +
        //                    "','" + user.Username + "','" + user.Password + "');";
        //        using (SqlCommand command = new SqlCommand(SQL, Connection.CONN()))
        //        {
        //            comando = command.ExecuteNonQuery();
        //        }
        //        Connection.Disconnect();

        //        return comando;
        //    }

        //    public bool BuscarUsuario(string username)
        //    {
        //        bool result = false;
        //        string SQL = "SELECT name,username,password,id_user,id_role,id_state " +
        //            "FROM TEST.dbo.[USER] " +
        //            "WHERE username = '" + username + "';";
        //        DbContextUtility Connection = new DbContextUtility();
        //        Connection.Connect();
        //        using (SqlCommand command = new SqlCommand(SQL, Connection.CONN()))
        //        {
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    result = true;
        //                }
        //            }
        //        }
        //        Connection.Disconnect();

        //        return result;
        //    }

        //    public UserDto Login(UserDto user)
        //    {
        //        UserDto userResult = new UserDto();

        //        //Consulta SQL
        //        string SQL = "SELECT name,username,password,id_user,id_role,id_state " +
        //            "FROM TEST.dbo.[USER] " +
        //            "WHERE username = '" + user.Username + "' AND password = '" + user.Password + "';";
        //        DbContextUtility Connection = new DbContextUtility();
        //        Connection.Connect();
        //        using (SqlCommand command = new SqlCommand(SQL, Connection.CONN()))
        //        {
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    userResult.IdUser = reader.GetInt32(3);
        //                    userResult.IdRole = reader.GetInt32(4);
        //                    userResult.IdState = reader.GetInt32(5);
        //                    userResult.Name = reader.GetString(0);
        //                    userResult.Username = reader.GetString(1);
        //                    userResult.Password = reader.GetString(2);
        //                }
        //            }
        //        }
        //        Connection.Disconnect();

        //        return userResult;
        //    }
        //}
    }