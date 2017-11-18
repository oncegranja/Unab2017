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
    public class ConsultasController : Controller
    {
        // GET: Consultas
        [HandleError()]
        public ActionResult Index()
        {

            return View();
        }

        public JsonResult ListSolicitudesRutCli(int rut)
        {
            var usr_lgn_pfl = "";

           
            if ((int)Session["perfil"] == Constantes.digitoUno || (int)Session["perfil"] == Constantes.digitoTres)
            {
                usr_lgn_pfl = "";
            }
            else
            {
                usr_lgn_pfl = (string)Session["login"];
            }


            List<Svc_TGSC_SOL_ListaSolicitudRut_Result> ListaSolicitudes = new List<Svc_TGSC_SOL_ListaSolicitudRut_Result>();
            ListaSolicitudes = new ObtieneListaSolicitudes().Obtiene_List_Rut_Clie(rut, usr_lgn_pfl);
            return Json(ListaSolicitudes, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ListSolicitudesIfi(int ifi)
        {
            var usr_lgn_pfl = "";

            if ((int)Session["perfil"] == Constantes.digitoUno || (int)Session["perfil"] == Constantes.digitoTres)
            {
                usr_lgn_pfl = "";
            }
            else
            {
                usr_lgn_pfl = (string)Session["login"];
            }


            List<Svc_TGSC_SOL_ListaSolicitudIFI_Result> ListaSolicitudes = new List<Svc_TGSC_SOL_ListaSolicitudIFI_Result>();
            ListaSolicitudes = new ObtieneListaSolicitudes().Obtiene_List_Ifi(ifi, usr_lgn_pfl);
            return Json(ListaSolicitudes, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ListSolicitudesFecha(DateTime fecha1, DateTime fecha2)
        {
            var usr_lgn_pfl = "";

            if ((int)Session["perfil"] == Constantes.digitoUno || (int)Session["perfil"] == Constantes.digitoTres)
            {
                usr_lgn_pfl = "";
            }
            else
            {
                usr_lgn_pfl = (string)Session["login"];
            }

            List<Svc_TGSC_SOL_ListaSolicitudFecha_Result> ListaSolicitudesFecha = new List<Svc_TGSC_SOL_ListaSolicitudFecha_Result>();
            ListaSolicitudesFecha = new ObtieneListaSolicitudes().Obtiene_List_Fecha(fecha1, fecha2, usr_lgn_pfl);
            return Json(ListaSolicitudesFecha, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ListSolicitudesEstado(int Estado)
        {
            var usr_lgn_pfl = "";

            if ((int)Session["perfil"] == Constantes.digitoUno || (int)Session["perfil"] == Constantes.digitoTres)
            {
                usr_lgn_pfl = "";
            }
            else
            {
                usr_lgn_pfl = (string)Session["login"];
            }


            List<Svc_TGSC_SOL_ListaSolicitudEstado_Result> ListaSolicitudesEstado = new List<Svc_TGSC_SOL_ListaSolicitudEstado_Result>();
            ListaSolicitudesEstado = new ObtieneListaSolicitudes().Obtiene_List_Estado(Estado, usr_lgn_pfl);
            return Json(ListaSolicitudesEstado, JsonRequestBehavior.AllowGet);
        }


    }
}
