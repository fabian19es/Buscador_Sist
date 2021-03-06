using Microsoft.AspNetCore.Mvc;
using SistemaBuscador.Models;
using SistemaBuscador.Repositories;
using System.Threading.Tasks;

namespace SistemaBuscador.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepository _repository;

        public UsuariosController(IUsuarioRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var listaUsuario = await _repository.ObtenerListaUsuarios();
            return View(listaUsuario);
        }

        public async Task<IActionResult> NuevoUsuario()
        {
            var model = await _repository.NuevoUsuarioCreacion();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> NuevoUsuario(UsuarioCreacionModel model)
        {
            if (ModelState.IsValid)
            {
                //Guardar el usuario en la bd
                await _repository.InsertatUsuario(model);
                //var listaUsuario = await _repository.ObtenerListaUsuarios();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> ActualizarUsuario([FromRoute] int id)
        {
            var usuario = await _repository.ObtenerUsuarioPorId(id);
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarUsuario(UsuarioEdicionModel model)
        {
            await _repository.ActualizarUsuario(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CambiarPassword(int id)
        {
            ViewBag.idUsuario = id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CambiarPassword(UsuarioCambioPasswordModel model)
        {
            await _repository.ActualizarPassword(model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuario = await _repository.ObtenerUsuarioPorId(id);
            return View(usuario);
        }
        [HttpPost]
        public async Task<IActionResult> EliminarUsuario(UsuarioEdicionModel model)
        {
            await _repository.EliminarUsuario(model.Id);
            return RedirectToAction("Index");
        }
    }
}