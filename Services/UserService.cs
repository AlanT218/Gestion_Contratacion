using Proyecto_Gestion.Dtos;
using Proyecto_Gestion.Repositories;
using Proyecto_Gestion.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proyecto_Gestion.Services
{
    public class UserService
    {       public UserDto CreateUser(UserDto userModel)
            {
                UserDto responseUserDto = new UserDto();
                UserRepository userRepository = new UserRepository();

                try
                {
                    // Encriptar la contraseña
                    userModel.Contrasenia = EncryptUtility.GetSHA256(userModel.Contrasenia);

                    // Buscar si el usuario ya existe por el nombre
                    if (userRepository.BuscarUsuario(userModel.Nombres))
                    {
                        responseUserDto.Response = 0;
                        responseUserDto.Mensaje = "Usuario ya existe";
                    }
                    else
                    {
                        // Crear el usuario
                        if (userRepository.CreateUser(userModel) != 0)
                        {
                            responseUserDto.Response = 1;
                            responseUserDto.Mensaje = "Creación exitosa";
                        }
                        else
                        {
                            responseUserDto.Response = 0;
                            responseUserDto.Mensaje = "Algo pasó";
                        }
                    }

                    return responseUserDto;
                }
                catch (Exception e)
                {
                    responseUserDto.Response = 0;
                    responseUserDto.Mensaje = e.InnerException != null ? e.InnerException.ToString() : e.Message;
                    return responseUserDto;
                }
            }

            public UserDto LoginUser(UserDto userModel)
            {
                UserRepository userRepository = new UserRepository();

                // Encriptar la contraseña antes de enviar para el login
                userModel.Contrasenia = EncryptUtility.GetSHA256(userModel.Contrasenia);

                // Llamar al método Login
                UserDto userResponse = userRepository.Login(userModel);

                // Verificar si se encontró el usuario (Id_usuario no será 0 si es correcto)
                if (userResponse.Id_usuario != 0)
                {
                    userResponse.Mensaje = "Inicio de sesión exitoso";
                }
                else
                {
                    userResponse.Mensaje = "Error en el inicio de sesión, nombre de usuario o contraseña incorrectos";
                }

                return userResponse;
            }
        }

        //    public UserDto CreateUser(UserDto userModel)
        //    {
        //        UserDto responseUserDto = new UserDto();
        //        UserRepository userRepository = new UserRepository();
        //        try
        //        {
        //            userModel.Password = EncryptUtility.GetSHA256(userModel.Password);

        //            if (userReposiyoty.BuscarUsuario(userModel.Username))
        //            {
        //                responseUserDto.Response = 0;
        //                responseUserDto.Message = "Usuario ya existe";
        //            }
        //            else
        //            {
        //                userModel.IdRole = 2;
        //                userModel.IdState = 1;
        //                if (userRepository.CreateUser(userModel) != 0)
        //                {
        //                    responseUserDto.Response = 1;
        //                    responseUserDto.Message = "Creacion exitosa";
        //                }
        //                else
        //                {
        //                    responseUserDto.Response = 0;
        //                    responseUserDto.Message = "Algo paso";
        //                }
        //            }

        //            return responseUserDto;
        //        }
        //        catch (Exception e)
        //        {
        //            responseUserDto.Response = 0;
        //            responseUserDto.Message = e.InnerException.ToString();
        //            return responseUserDto;
        //        }
        //    }

        //    public UserDto LoginUser(UserDto userModel)
        //    {
        //        UserReposiyoty userReposiyoty = new UserReposiyoty();
        //        userModel.Password = EncryptUtility.GetSHA256(userModel.Password);
        //        UserDto userResponse = userReposiyoty.Login(userModel);
        //        if (userResponse.IdUser != 0)
        //        {
        //            userResponse.Message = "Successful Login";
        //        }
        //        else
        //        {
        //            userResponse.Message = "Error Login, Username or Password are Wrong";
        //        }

        //        return userResponse;
        //    }
        //}
    }
