using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;
using System.Web.Mvc;
using Rotativa;
using Garantia_4.Models;
using Garantia_4.Funciones;



namespace Garantia_4.Controllers
{
    public class ListadosController : Controller
    {
        private DB_DESARROLLOEntities1 db = new DB_DESARROLLOEntities1();
        // GET: Listados
            
        public ActionResult VerListados()
        {
            var acceso = Session["perfil"];

            if (acceso == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.nombre = Session["nombre"];
            ViewBag.unidad = Session["nombreUnidad"];
            ViewBag.nom_oficina = Session["Nombre_oficina"];
            ViewBag.LoginEjec = Session["login"];
            ViewBag.perfil = Session["Pfl_Id"];
            ViewBag.RutEjecutivo = Session["Rut"];


            return View();
        }

        public ActionResult VerListadosEtapa()
        {
            var acceso = Session["perfil"];

            if (acceso == null)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.nombre = Session["nombre"];
            ViewBag.unidad = Session["nombreUnidad"];
            ViewBag.nom_oficina = Session["Nombre_oficina"];
            ViewBag.LoginEjec = Session["login"];
            ViewBag.perfil = Session["Pfl_Id"];
            ViewBag.RutEjecutivo = Session["Rut"];

            return View();
        }


        public JsonResult ListSolicitudes(int estado)
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


            List<Svc_TGSC_SOL_ListaSolicitudEstado_Result> ListaSolicitudes = new List<Svc_TGSC_SOL_ListaSolicitudEstado_Result>();
            ListaSolicitudes = new ObtieneListaSolEstado().Obtiene_Lista_Solicitudes_Estado(estado, usr_lgn_pfl);
            return Json(ListaSolicitudes, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ListSolicitudesEtapa(int etapa)
        {
            var usr_lgn_pfl = "";


            if ((int)Session["perfil"] == Constantes.digitoUno || (int)Session["perfil"] == Constantes.digitoTres)
            {
                usr_lgn_pfl = "";
            } else{
                usr_lgn_pfl = (string)Session["login"];
            }

           
            List<Svc_TGSC_SOL_ListaSolicitudEtapa_Result> ListaSolicitudes = new List<Svc_TGSC_SOL_ListaSolicitudEtapa_Result>();
            ListaSolicitudes = new ObtieneListaSolEtapa().Obtiene_Lista_Solicitudes_Etapa(etapa, usr_lgn_pfl);
            return Json(ListaSolicitudes, JsonRequestBehavior.AllowGet);
        }



        public ActionResult ExportAprobacion(int Sol_Id)
        {
            Svc_TGSC_SOL_VerSolicitudId_Result Solicitud = new Svc_TGSC_SOL_VerSolicitudId_Result();
            Solicitud = new ObtieneSolicitud().Obtiene_Solicitud(Sol_Id);

            var nom_file = "";
            nom_file = "Aprobacion_Nº_IFI_" + Solicitud.Sol_Num_Sol + ".pdf";


            Svc_TGSC_SOL_DOC_01_Aprobacion_Result reporte = new Svc_TGSC_SOL_DOC_01_Aprobacion_Result();
            reporte = new GeneraDocumentos().Genera_Aprobacion(Sol_Id);
            ViewBag.reporte = reporte.Rep1;
            ViewBag.str = reporte.Rep1;

            return new Rotativa.PartialViewAsPdf("PDF", reporte.Rep1)
            {
                FileName = nom_file
            };

        }


        public ActionResult ExportAnexo1(int Sol_Id)
        {


            Svc_TGSC_SOL_VerSolicitudId_Result Solicitud = new Svc_TGSC_SOL_VerSolicitudId_Result();
            Solicitud = new ObtieneSolicitud().Obtiene_Solicitud(Sol_Id);

            var nom_file = "";
            nom_file = "Anexo_1_Nº_IFI_" + Solicitud.Sol_Num_Sol + ".pdf";

            Svc_TGSC_SOL_DOC_02_Anexo1_Result reporte = new Svc_TGSC_SOL_DOC_02_Anexo1_Result();
            reporte = new GeneraDocumentos().Genera_Anexo1(Sol_Id);
            ViewBag.reporte = reporte.Rep1;
            ViewBag.str = reporte.Rep1;

            return new Rotativa.PartialViewAsPdf("PDF", reporte.Rep1)
            {
                FileName = nom_file
            };

        }

        public ActionResult ExportAnexo2(int Sol_Id)
        {

            Svc_TGSC_SOL_VerSolicitudId_Result Solicitud = new Svc_TGSC_SOL_VerSolicitudId_Result();
            Solicitud = new ObtieneSolicitud().Obtiene_Solicitud(Sol_Id);

            var nom_file = "";
            nom_file = "Anexo_2_Nº_IFI_" + Solicitud.Sol_Num_Sol + ".pdf";

            Svc_TGSC_SOL_DOC_03_Anexo2_Result reporte = new Svc_TGSC_SOL_DOC_03_Anexo2_Result();
            reporte = new GeneraDocumentos().Genera_Anexo2(Sol_Id);
            ViewBag.reporte = reporte.Rep1;
            ViewBag.str = reporte.Rep1;

            return new Rotativa.PartialViewAsPdf("PDF", reporte.Rep1)
            {
                FileName = nom_file
            };

        }


        public ActionResult ExportDeclaracion(int Sol_Id)
        {

            Svc_TGSC_SOL_VerSolicitudId_Result Solicitud = new Svc_TGSC_SOL_VerSolicitudId_Result();
            Solicitud = new ObtieneSolicitud().Obtiene_Solicitud(Sol_Id);

            var nom_file = "";
            nom_file = "Declaracion_Nº_IFI_" + Solicitud.Sol_Num_Sol + ".pdf";

            Svc_TGSC_SOL_DOC_04_Declaracion_Result reporte = new Svc_TGSC_SOL_DOC_04_Declaracion_Result();
            reporte = new GeneraDocumentos().Genera_Declaracion(Sol_Id);
            ViewBag.reporte = reporte.Rep1;
            ViewBag.str = reporte.Rep1;

            return new Rotativa.PartialViewAsPdf("PDF", reporte.Rep1)
            {
                FileName = nom_file
            };

        }


        public ActionResult ExportCatastrofe(int Sol_Id)
        {
            Svc_TGSC_SOL_VerSolicitudId_Result Solicitud = new Svc_TGSC_SOL_VerSolicitudId_Result();
            Solicitud = new ObtieneSolicitud().Obtiene_Solicitud(Sol_Id);

            var nom_file = "";
            nom_file = "Catastrofe_Nº_IFI_" + Solicitud.Sol_Num_Sol + ".pdf";

            Svc_TGSC_SOL_DOC_05_Catastrofe_Result reporte = new Svc_TGSC_SOL_DOC_05_Catastrofe_Result();
            reporte = new GeneraDocumentos().Genera_Catastrofe(Sol_Id);
            ViewBag.reporte = reporte.Rep1;
            ViewBag.str = reporte.Rep1;

            return new Rotativa.PartialViewAsPdf("PDF", reporte.Rep1)
            {
                FileName = nom_file
            };

        }

        public ActionResult Busqueda()
        {
            return View();
        }
        
                protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
