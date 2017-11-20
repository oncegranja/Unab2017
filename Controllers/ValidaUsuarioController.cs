using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Garantia_4.Models;
using Garantia_4.Funciones;


namespace Garantia_4.Controllers
{
    public class ValidaUsuarioController : Controller
    {
        // GET: ValidaUsuario
        [HttpGet]
        public ActionResult Index(string login)
        {

           
            Svc_TGSC_USR_SIS_VerUsuarioSistema_Result usuario = new Svc_TGSC_USR_SIS_VerUsuarioSistema_Result();
            usuario = new ObtieneUsuario().LeeUsuario(login);


            ViewBag.nombre = usuario.nombre;
            ViewBag.unidad = usuario.nombreUnidad;
            ViewBag.nom_oficina = usuario.Nombre_oficina;
            ViewBag.LoginEjec = usuario.login;
            ViewBag.perfil = usuario.Pfl_Id;
            ViewBag.RutEjecutivo = usuario.Rut;

            LoginUsuario datausuario = new LoginUsuario();
            datausuario.Nombre = usuario.nombre;
            TempData["data_usuario"] = datausuario.Nombre;
            

            return View();
        }



    }
}
