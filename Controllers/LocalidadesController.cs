using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Garantia_4.Models;

namespace Garantia_4.Controllers
{
    public class LocalidadesController : Controller
    {
        private DB_DESARROLLOEntities1 db = new DB_DESARROLLOEntities1();

        public JsonResult ListRegiones(int Ctf_Cod)
        {
            List<Svc_TGSC_REL_CTF_COM_VerRegion_Result> ListaRegiones = new List<Svc_TGSC_REL_CTF_COM_VerRegion_Result>();
            ListaRegiones = new ObtieneParametros().obtiene_comuna_region(Ctf_Cod);
            return Json(ListaRegiones, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ListLocalizaciones(int Reg_Id, int Ctf_Cod)
        {
            List<Svc_TGSC_REL_CTF_COM_VerComuna_Result> ListaComuna = new List<Svc_TGSC_REL_CTF_COM_VerComuna_Result>();
            ListaComuna = new ObtieneParametros().Obtiene_Comuna(Reg_Id, Ctf_Cod);
            ViewBag.Rel_Ctf_Com_Id = new SelectList(ListaComuna, "Rel_Ctf_Com_Id", "Com_Nom_02");

            return Json(ListaComuna, JsonRequestBehavior.AllowGet);

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
