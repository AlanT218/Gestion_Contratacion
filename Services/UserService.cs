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
    {
        public UserDto CreateUser(UserDto userModel)
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

                        // Enviar correo de bienvenida al usuario con el correo proporcionado
                        EmailConfigUtility gestorCorreo = new EmailConfigUtility();
                        String destinatario = userModel.Correo; // Usar el correo del usuario
                        String asunto = "Bienvenido al sistema";

                        // Enviar el correo con la plantilla de bienvenida
                        gestorCorreo.EnviarCorreo(destinatario, asunto, 1, userModel.Nombres);
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

        public UserDto ObtenerUsuarioPorId(int id)
        {
            UserRepository userRepository = new UserRepository();
            return userRepository.ObtenerUsuarioPorId(id);
        }

        public List<UserDto> ObtenerCandidatos(string cargo = null)
        {
            UserRepository userRepository = new UserRepository();
            return userRepository.GetCandidatos(cargo);
        }

        public void AceptarUsuario(UserDto userModel)
        {
            EmailConfigUtility gestorCorreo = new EmailConfigUtility();
            String destinatario = userModel.Correo;
            String asunto = "Has sido aceptado para continuar con el proceso";
            gestorCorreo.EnviarCorreo(destinatario, asunto, 3, userModel.Nombres);

        }

        public void RechazarUsuario(UserDto userModel)
        {
            EmailConfigUtility gestorCorreo = new EmailConfigUtility();
            String destinatario = userModel.Correo;
            String asunto = "Lamentablemente, tu candidatura ha sido rechazada";
            gestorCorreo.EnviarCorreo(destinatario, asunto, 2, userModel.Nombres);
        }

    }
}
