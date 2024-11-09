using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Proyecto_Gestion.Utilities
{
    public class EmailConfigUtility
    {
        private SmtpClient cliente;
        private MailMessage email;
        private string Host = "smtp.gmail.com";
        private int Port = 587;
        private string User = "Pruebitaspss@gmail.com";
        private string Password = "ocehnkfhugyipyur"; // Contraseña de Aplicación 
        private bool EnabledSSL = true;

        public EmailConfigUtility()
        {
            cliente = new SmtpClient(Host, Port)
            {
                EnableSsl = EnabledSSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(User, Password)
            };
        }

        // Método principal para enviar correos
        public void EnviarCorreo(string destinatario, string asunto, int templateType, string name)
        {
            try
            {
                string mensaje = ObtenerPlantilla(templateType, name);
                email = new MailMessage(User, destinatario, asunto, mensaje)
                {
                    IsBodyHtml = true
                };
                cliente.Send(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar correo: " + ex.Message);
                throw; // Asegúrate de manejar esta excepción donde se llama a este método
            }
        }

        // Método para obtener la plantilla según el tipo
        private string ObtenerPlantilla(int templateType, string name)
        {
            switch (templateType)
            {
                case 1:
                    return ObtenerPlantillaBienvenida(name);
                case 2:
                    return ObtenerPlantillaRechazo(name);
                case 3:
                    return ObtenerPlantillaAceptacion(name);
                default:
                    throw new ArgumentException("Tipo de plantilla no válido");
            }
        }

        // Plantilla de bienvenida
        private string ObtenerPlantillaBienvenida(string name)
        {
            return $@"
        <!DOCTYPE html>
        <html lang='es'>
        <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <title>Bienvenido</title>
            <style>
                body {{ font-family: Arial, sans-serif; color: #333; }}
                .header {{ background-color: #4CAF50; color: white; padding: 10px; text-align: center; }}
                .content {{ margin: 20px; }}
                .footer {{ background-color: #f1f1f1; padding: 10px; text-align: center; }}
            </style>
        </head>
        <body>
            <div class='header'>
                <h1>Bienvenido al sistema, {name}!</h1>
            </div>
            <div class='content'>
                <p>Nos alegra que hayas ingresado correctamente. ¡Bienvenido de nuevo!</p>
            </div>
            <div class='footer'>
                <p>&copy; 2024 Tu Empresa</p>
            </div>
        </body>
        </html>";
        }

        // Plantilla de rechazo
        private string ObtenerPlantillaRechazo(string name)
        {
            return $@"
        <!DOCTYPE html>
        <html lang='es'>
        <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <title>Rechazado</title>
            <style>
                body {{ font-family: Arial, sans-serif; color: #333; }}
                .header {{ background-color: #FF5733; color: white; padding: 10px; text-align: center; }}
                .content {{ margin: 20px; }}
                .footer {{ background-color: #f1f1f1; padding: 10px; text-align: center; }}
            </style>
        </head>
        <body>
            <div class='header'>
                <h1>Hola {name},</h1>
            </div>
            <div class='content'>
                <p>Lamentablemente, tu candidatura para la oferta ha sido rechazada.</p>
            </div>
            <div class='footer'>
                <p>&copy; 2024 Tu Empresa</p>
            </div>
        </body>
        </html>";
        }

        // Plantilla de aceptación
        private string ObtenerPlantillaAceptacion(string name)
        {
            return $@"
        <!DOCTYPE html>
        <html lang='es'>
        <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <title>Aceptado</title>
            <style>
                body {{ font-family: Arial, sans-serif; color: #333; }}
                .header {{ background-color: #28A745; color: white; padding: 10px; text-align: center; }}
                .content {{ margin: 20px; }}
                .footer {{ background-color: #f1f1f1; padding: 10px; text-align: center; }}
            </style>
        </head>
        <body>
            <div class='header'>
                <h1>¡Felicidades {name}!</h1>
            </div>
            <div class='content'>
                <p>Nos complace informarte que has sido aceptado para continuar con el proceso de selección.</p>
            </div>
            <div class='footer'>
                <p>&copy; 2024 Tu Empresa</p>
            </div>
        </body>
        </html>";
        }
    }
}
//    public class EmailConfigUtility
//    {
//        private SmtpClient cliente;
//        private MailMessage email;
//        private string Host = "smtp.gmail.com";
//        private int Port = 587;
//        private string User = "Pruebitaspss@gmail.com";
//        private string Password = "ocehnkfhugyipyur"; //Contraseña de Aplicación 
//        private bool EnabledSSL = true;
//        public EmailConfigUtility()
//        {
//            cliente = new SmtpClient(Host, Port)
//            {
//                EnableSsl = EnabledSSL,
//                DeliveryMethod = SmtpDeliveryMethod.Network,
//                UseDefaultCredentials = false,
//                Credentials = new NetworkCredential(User, Password)
//            };
//        }
//        // Método principal para enviar correos
//        //public void EnviarCorreo(string destinatario, string asunto, string templateType, string name)
//        //{
//        //    string mensaje = ObtenerPlantilla(templateType, name);
//        //    email = new MailMessage(User, destinatario, asunto, mensaje)
//        //    {
//        //        IsBodyHtml = true
//        //    };
//        //    cliente.Send(email);
//        //}
//        public void EnviarCorreo(string destinatario, string asunto, int templateType, string name)
//        {
//            try
//            {
//                string mensaje = ObtenerPlantilla(templateType, name);
//                email = new MailMessage(User, destinatario, asunto, mensaje)
//                {
//                    IsBodyHtml = true
//                };
//                cliente.Send(email);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Error al enviar correo: " + ex.Message);
//                throw;
//            }
//        }


//            // Método para obtener la plantilla según el tipo
//            private string ObtenerPlantilla(int templateType, string name)
//            {
//                switch (templateType)
//                {
//                    case 1:
//                        return ObtenerPlantillaBienvenida(name);
//                    case 2:
//                        return ObtenerPlantillaRechazo(name);
//                    case 3:
//                        return ObtenerPlantillaAceptacion(name);
//                    default:
//                        throw new ArgumentException("Tipo de plantilla no válido");
//                }
//            }

//        // Plantilla de bienvenida
//        private string ObtenerPlantillaBienvenida(string name)
//        {
//            return $@"
//        <!DOCTYPE html>
//        <html lang='es'>
//        <head>
//            <meta charset='UTF-8'>
//            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
//            <title>Bienvenido</title>
//            <style>
//                body {{ font-family: Arial, sans-serif; color: #333; }}
//                .header {{ background-color: #4CAF50; color: white; padding: 10px; text-align: center; }}
//                .content {{ margin: 20px; }}
//                .footer {{ background-color: #f1f1f1; padding: 10px; text-align: center; }}
//            </style>
//        </head>
//        <body>
//            <div class='header'>
//                <h1>Bienvenido al sistema, {name}!</h1>
//            </div>
//            <div class='content'>
//                <p>Nos alegra que hayas ingresado correctamente. ¡Bienvenido de nuevo!</p>
//            </div>
//            <div class='footer'>
//                <p>&copy; 2024 Tu Empresa</p>
//            </div>
//        </body>
//        </html>";
//        }

//        // Plantilla de rechazo
//        private string ObtenerPlantillaRechazo(string name)
//        {
//            return $@"
//        <!DOCTYPE html>
//        <html lang='es'>
//        <head>
//            <meta charset='UTF-8'>
//            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
//            <title>Rechazado</title>
//            <style>
//                body {{ font-family: Arial, sans-serif; color: #333; }}
//                .header {{ background-color: #FF5733; color: white; padding: 10px; text-align: center; }}
//                .content {{ margin: 20px; }}
//                .footer {{ background-color: #f1f1f1; padding: 10px; text-align: center; }}
//            </style>
//        </head>
//        <body>
//            <div class='header'>
//                <h1>Hola {name},</h1>
//            </div>
//            <div class='content'>
//                <p>Lamentablemente, tu candidatura para la oferta ha sido rechazada.</p>
//            </div>
//            <div class='footer'>
//                <p>&copy; 2024 Tu Empresa</p>
//            </div>
//        </body>
//        </html>";
//        }

//        // Plantilla de aceptación
//        private string ObtenerPlantillaAceptacion(string name)
//        {
//            return $@"
//        <!DOCTYPE html>
//        <html lang='es'>
//        <head>
//            <meta charset='UTF-8'>
//            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
//            <title>Aceptado</title>
//            <style>
//                body {{ font-family: Arial, sans-serif; color: #333; }}
//                .header {{ background-color: #28A745; color: white; padding: 10px; text-align: center; }}
//                .content {{ margin: 20px; }}
//                .footer {{ background-color: #f1f1f1; padding: 10px; text-align: center; }}
//            </style>
//        </head>
//        <body>
//            <div class='header'>
//                <h1>¡Felicidades {name}!</h1>
//            </div>
//            <div class='content'>
//                <p>Nos complace informarte que has sido aceptado para continuar con el proceso de selección.</p>
//            </div>
//            <div class='footer'>
//                <p>&copy; 2024 Tu Empresa</p>
//            </div>
//        </body>
//        </html>";
//        }
//    }
//    //public void EnviarCorreo(String destinatario, string asunto, string mensaje, bool esHtlm = false)
//    //{
//    //    email = new MailMessage(User, destinatario, asunto, mensaje);
//    //    email.IsBodyHtml = esHtlm;
//    //    cliente.Send(email);
//    //}

//}