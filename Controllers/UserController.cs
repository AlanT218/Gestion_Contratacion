using Proyecto_Gestion.Dtos;
using Proyecto_Gestion.Services;
using Proyecto_Gestion.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Gestion.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        // Acción que muestra los detalles de un usuario en base a su id.
        // La lógica aún no está implementada.
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: User/Create
        // Acción que inicializa el formulario de creación de usuario.
        // Se pasa un objeto vacío del tipo UserDto al modelo de la vista.
        public ActionResult Create()
        {
            UserDto user = new UserDto(); // Inicializa un nuevo usuario vacío.
            return View(user);
        }

        // POST: User/Create
        // Acción que recibe un objeto UserDto cuando se envía el formulario de creación.
        // Se llama al servicio UserService para crear el usuario y, según la respuesta,
        // redirige al índice o muestra la vista con los datos del usuario.
        [HttpPost]
        public ActionResult Create(UserDto newUser)
        {
            try
            {
                UserService userService = new UserService(); // Crea una instancia del servicio de usuarios.
                UserDto userResponse = userService.CreateUser(newUser); // Llama al método de creación de usuario.
                if (userResponse.Response == 1)
                {
                    return RedirectToAction("Index"); // Redirige a la vista principal si la creación fue exitosa.
                }
                else
                {
                    return View(userResponse); // Muestra la vista con la respuesta del servicio en caso de error.
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message;
                return View(); // Muestra la vista en caso de que ocurra una excepción.
            }
        }


        // Acción para mostrar la vista de Login (GET)
        // GET: User/Login
        public ActionResult Login()
        {
            UserDto user = new UserDto(); // Inicializa un nuevo usuario vacío.
            return View(user); // Devuelve la vista de login.
        }
         // POST: User/Login
        [HttpPost]

        public ActionResult Login(UserDto user)
        {
            UserService userService = new UserService();
            UserDto userResponse = userService.LoginUser(user);

            if (userResponse.Response == 1)
            {
                // Almacena los datos en variables de sesión
                Session["UserId"] = userResponse.Id_usuario;
                Session["UserNit"] = userResponse.Nit;
                Session["UserName"] = userResponse.Nombres; // Almacena el nombre del usuario
                Session["UserRol"] = userResponse.Rol; // Almacena el rol del usuario

                if (userResponse.Rol == 3)
                {
                    return RedirectToAction("GestionUsuarios", "User"); // Redirige a la vista de candidatos para administradores
                }
                else if (userResponse.Rol == 2)
                {
                    return RedirectToAction("GestionUsuariosAsistente", "User"); // Redirige a la vista 
                }
                else if (userResponse.Rol == 1)
                {
                    return RedirectToAction("VistaForm", "User"); // Redirige a la vista de bienvenida para candidatos
                }
            }
            else
            {
                // Muestra el mensaje de error si las credenciales son incorrectas
                ModelState.AddModelError("", userResponse.Mensaje);
            }

            return View(userResponse); // Devuelve la vista con el modelo del usuario en caso de error
        }

        [ValidacionUtility(1)]
        public ActionResult VistaForm()
        {
            // Verifica si la sesión está activa
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login"); // Redirige a login si la sesión no está activa
            }
        }
        [ValidacionUtility(3)]
        public ActionResult GestionUsuarios()
        {
            return View();
        }
        

        [ValidacionUtility(2)]
        public ActionResult GestionUsuariosAsistente()
        {
            return View();
        }

        [ValidacionUtility(3,2)]
        //public ActionResult Lista(string estado)
        //{
        //    UserService userService = new UserService();
        //    var (aceptados, rechazados, candidatos, empleados) = userService.GetCandidatos();

        //    List<UserDto> usuarios;
        //    switch (estado)
        //    {
        //        case "A":
        //            usuarios = aceptados;
        //            ViewBag.Titulo = "Usuarios Aceptados";


        //            break;
        //        case "R":
        //            usuarios = rechazados;
        //            ViewBag.Titulo = "Usuarios Rechazados";
        //            break;
        //        case "C":
        //            usuarios = candidatos;
        //            ViewBag.Titulo = "Candidatos";
        //            break;
        //        case "E":
        //            usuarios = empleados;
        //            ViewBag.Titulo = "Empleados";
        //            break;
        //        default:
        //            usuarios = new List<UserDto>();
        //            ViewBag.Titulo = "Lista de Usuarios";
        //            break;
        //    }

        //    return View(usuarios);
        //}
        public ActionResult Lista(string estado)
        {
            UserService userService = new UserService();
            var (aceptados, rechazados, candidatos, empleados) = userService.GetCandidatos();

            List<UserDto> usuarios;
            switch (estado)
            {
                case "A":
                    usuarios = aceptados;
                    ViewBag.Titulo = "Usuarios Aceptados";
                    break;
                case "R":
                    usuarios = rechazados;
                    ViewBag.Titulo = "Usuarios Rechazados";
                    break;
                case "C":
                    usuarios = candidatos;
                    ViewBag.Titulo = "Candidatos";
                    break;
                case "E":
                    usuarios = empleados;
                    ViewBag.Titulo = "Empleados";
                    break;
                default:
                    usuarios = new List<UserDto>();
                    ViewBag.Titulo = "Lista de Usuarios";
                    break;
            }

            // Almacenar el estado en la sesión
            Session["EstadoLista"] = estado;

            return View(usuarios);
        }


        //[ValidacionUtility(3,2)]
        //public ActionResult DetallesCandidato(int id)
        //{
        //    UserService userService = new UserService();
        //    UserDto candidato = userService.ObtenerUsuarioPorId(id);

        //    if (candidato == null)
        //    {
        //        return HttpNotFound(); // Devuelve un error 404 si el candidato no existe.
        //    }

        //    return View(candidato);
        //}
        [ValidacionUtility(3, 2)]
        public ActionResult DetallesCandidato(int id)
        {
            UserService userService = new UserService();
            UserDto candidato = userService.ObtenerUsuarioPorId(id);

            if (candidato == null)
            {
                return HttpNotFound(); // Devuelve un error 404 si el candidato no existe.
            }

            // Recupera el estado almacenado en la sesión para usarlo en la vista
            ViewBag.EstadoLista = Session["EstadoLista"];

            return View(candidato);
        }


        //public ActionResult Candidatos(string cargo = null)
        //{
        //    UserService userService = new UserService();
        //    // Obtiene la lista de candidatos
        //    List<UserDto> candidatos = userService.ObtenerCandidatos(cargo); 
        //    return View(candidatos);
        //}

        // Acción para aceptar al candidato
        [HttpPost]
        public ActionResult Aceptar(int id)
        {
            try
            {
                UserService userService = new UserService();
                UserDto user = userService.ObtenerUsuarioPorId(id);
                if (user != null)
                {
                    userService.AceptarUsuario(user);
                    TempData["Mensaje"] = "Candidato aceptado, correo enviado.";
                }
                else
                {
                    TempData["Error"] = "No se pudo encontrar al candidato.";
                }
                return RedirectToAction("DetallesCandidato", new { id = id });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ocurrió un error al aceptar el candidato: " + ex.Message;
                return RedirectToAction("DetallesCandidato", new { id = id });
            }
        }

        // Acción para rechazar al candidato
        [HttpPost]
        public ActionResult Rechazar(int id)
        {
            try
            {
                UserService userService = new UserService();
                UserDto user = userService.ObtenerUsuarioPorId(id);
                if (user != null)
                {
                    userService.RechazarUsuario(user);
                    TempData["Mensaje"] = "Candidato rechazado, correo enviado.";
                }
                else
                {
                    TempData["Error"] = "No se pudo encontrar al candidato.";
                }
                return RedirectToAction("DetallesCandidato", new { id = id });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Ocurrió un error al rechazar el candidato: " + ex.Message;
                return RedirectToAction("DetallesCandidato", new { id = id });
            }
        }
        [ValidacionUtility(1,2,3)]
        public ActionResult PermisoDenegado()
            {
                return View();
            }
        





        // GET: User/Edit/5
        // Acción que muestra el formulario de edición de un usuario en base a su id.
        // La lógica de recuperación del usuario aún< no está implementada.
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
        // Acción que recibe los datos del formulario de edición y actualiza el usuario.
        // La lógica de actualización aún no está implementada.
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Lógica para actualizar el usuario.

                return RedirectToAction("Index"); // Redirige al índice tras la actualización.
            }
            catch
            {
                return View(); // Muestra la vista en caso de que ocurra un error.
            }
        }

        // GET: User/Delete/5
        // Acción que muestra una vista para confirmar la eliminación de un usuario en base a su id.
        // La lógica aún no está implementada.
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
        // Acción que recibe la confirmación de eliminación del usuario.
        // La lógica de eliminación aún no está implementada.
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Lógica para eliminar el usuario.

                return RedirectToAction("Index"); // Redirige al índice tras la eliminación.
            }
            catch
            {
                return View(); // Muestra la vista en caso de que ocurra un error.
            }
        }
    }
}