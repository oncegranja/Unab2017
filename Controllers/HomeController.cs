using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;
using System.Web.Mvc;
using Garantia_4.Models;
using Garantia_4.Funciones;

namespace Garantia_4.Controllers
{
    public class HomeController : Controller
    {

        [HandleError()]


        [HttpGet]
        public ActionResult Index()
           {
            utlidades.NomUser(@User.Identity.Name);

            //Svc_TGSC_USR_SIS_VerUsuarioSistema_Result usuario = new Svc_TGSC_USR_SIS_VerUsuarioSistema_Result();
            //usuario = new ObtieneUsuario().LeeUsuario(usr_lgn);
            //ViewBag.nombre = usuario.nombre;
            //ViewBag.unidad = usuario.nombreUnidad;
            //ViewBag.login = usuario.login;
            //ViewBag.perfil = usuario.Pfl_Id;
            //Session["username"] = usuario.nombre;
            //Session["nombreUnidad"] = usuario.nombreUnidad;
            //Session["perfil"] = usuario.Pfl_Id;
            //Session["login"] = usuario.login;
                        
            return View();
        }

        [HttpGet]
        public JsonResult Dashboard()
        {

            List<Svc_TGSC_Dashboard_Result> GeneraDashboard = new List<Svc_TGSC_Dashboard_Result>();
            GeneraDashboard = new generaDashboard().Genera_Dashboard();
            return Json(GeneraDashboard, JsonRequestBehavior.AllowGet);
        }




    }
}
