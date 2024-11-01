using Proyecto_Gestion.Dtos;
using Proyecto_Gestion.Services;
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

                return RedirectToAction("VistaForm"); // Redirige a la vista de bienvenida
            }
            else
            {
                // Muestra el mensaje de error si las credenciales son incorrectas
                ModelState.AddModelError("", userResponse.Mensaje);
                return View(userResponse);
            }
        }

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

        // GET: User/Edit/5
        // Acción que muestra el formulario de edición de un usuario en base a su id.
        // La lógica de recuperación del usuario aún no está implementada.
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