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
    public class MantenedoresController : Controller
    {
        private DB_DESARROLLOEntities1 db = new DB_DESARROLLOEntities1();


        // GET: Catastrofes

        public ActionResult MantenedorCTF()
        {
          
            try
            {
                return View();
            }
            catch
            {                                                    //'OK - Fix by re-throwing the generic
                return RedirectToAction("error", "home");       //      exception at the end of the catch block
                throw;
            }
        }

        public ActionResult ListaCatastrofeLocalidad()
        {
            if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        public JsonResult ListaTipoCTFId(int Id_Ctf)
        {

            List<Svc_TGSC_REL_CTF_COM_VerComunaMantenedor_V2_Result> Lista_CTF = new List<Svc_TGSC_REL_CTF_COM_VerComunaMantenedor_V2_Result>();
            Lista_CTF = new ObtieneParametros().Obtiene_Catastrofe_Id(Id_Ctf);

            return Json(Lista_CTF, JsonRequestBehavior.AllowGet);
        }


        public ActionResult InsertaTipoCatastrofeLoc()
        {
            if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.estadoctf = Constantes.numUno;
            ViewBag.fechaingreso = DateTime.Now.ToString("yyyy/MM/dd");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //
        public ActionResult InsertaTipoCatastrofeLoc([Bind(Include = "Ctf_Cod, Ctf_Des, Ctf_Est, Ctf_Rsl")] C_Ctf GuardaCtf)
        {
            try
            {
                if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
                {
                    return RedirectToAction("Index", "Home");
                }

                var _Ctf_Cod = GuardaCtf.Ctf_Cod;
                var _Ctf_Des = GuardaCtf.Ctf_Des;
                var _Ctf_Est = GuardaCtf.Ctf_Est;
                var _Ctf_Rsl = GuardaCtf.Ctf_Rsl;


                if (ModelState.IsValid)
                {
                    Sva_TGSC_CTF_InsertaCatastrofes_Result Guarda_Ctf = new Sva_TGSC_CTF_InsertaCatastrofes_Result();
                    Guarda_Ctf = new GuardaMantenedores().GuardarCtf(_Ctf_Cod, _Ctf_Des, _Ctf_Est, _Ctf_Rsl);
                    return RedirectToAction("AgregaCatastrofe");
                }

                return View(GuardaCtf);
            }
                catch
            {
                    return RedirectToAction("error", "home");
            }
        }


        public ActionResult ListaTipoCatastrofe()
        {
            if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        public JsonResult ListaTipoCTF()
        {

            List<Svc_TGSC_CTF_VerCatastrofe_02_Result> Lista_CTF = new List<Svc_TGSC_CTF_VerCatastrofe_02_Result>();
            Lista_CTF = new ObtieneParametros().obtiene_catastrofe_02();

            return Json(Lista_CTF, JsonRequestBehavior.AllowGet);
        }


        public ActionResult InsertaTipoCatastrofe()
        {
            if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }


            ViewBag.estadoctf = Constantes.numUno;
            ViewBag.fechaingreso = DateTime.Now.ToString("yyyy/MM/dd");
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //
        public ActionResult InsertaTipoCatastrofe([Bind(Include = "Ctf_Cod, Ctf_Des, Ctf_Est, Ctf_Rsl")] C_Ctf GuardaCtf)
        {
            if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }

            var _Ctf_Cod = GuardaCtf.Ctf_Cod;
            var _Ctf_Des = GuardaCtf.Ctf_Des;
            var _Ctf_Est = GuardaCtf.Ctf_Est;
            var _Ctf_Rsl = GuardaCtf.Ctf_Rsl;


            if (ModelState.IsValid)
            {
                Sva_TGSC_CTF_InsertaCatastrofes_Result Guarda_Ctf = new Sva_TGSC_CTF_InsertaCatastrofes_Result();
                Guarda_Ctf = new GuardaMantenedores().GuardarCtf(_Ctf_Cod, _Ctf_Des, _Ctf_Est, _Ctf_Rsl);
                return View("ListaTipoCatastrofe");
            }

            return View(GuardaCtf);
        }


        public JsonResult ListaCatastrofes()
        {

            List<Svc_TGSC_REL_CTF_COM_CatastrofesTodas_Result> Ver_Catastrofe = new List<Svc_TGSC_REL_CTF_COM_CatastrofesTodas_Result>();
            Ver_Catastrofe = new ObtieneParametros().obtiene_catastrofe_Mant();

            return Json(Ver_Catastrofe, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AgregaCatastrofe()
        {

            if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }


            //string Ctf_Est = "1";
            List<Svc_TGSC_CTF_VerCatastrofe_Result> ListaCatastrofe = new List<Svc_TGSC_CTF_VerCatastrofe_Result>();
            ListaCatastrofe = new ObtieneCatastrofe().obtiene_catastrofe(Constantes.numUno);
            ViewBag.M_Catastrofe = new SelectList(ListaCatastrofe, "Ctf_Cod", "Ctf_Des");

            ViewBag.M_Regiones = new SelectList(db.TGSC_RGN, "Rgn_Cod", "Rgn_Nom");

            ViewBag.M_Comuna = new SelectList(db.TGSC_COM, "Com_Cod", "Com_Nom");

            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AgregaCatastrofe_2([Bind(Include = "Com_Cod, Ctf_Cod")] TGSC_REL_CTF_COM tGSC_REL_CTF_COM)
        {

            if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }

            var Com_Cod = tGSC_REL_CTF_COM.Com_Cod;
            var Ctf_Cod = tGSC_REL_CTF_COM.Ctf_Cod;
            //var Estado_CTF = "1";

            if (ModelState.IsValid)
            {
                

                Sva_TGSC_REL_CTF_COM_InsertaCatastrofes_Result Guarda_Catastrofe = new Sva_TGSC_REL_CTF_COM_InsertaCatastrofes_Result();
                Guarda_Catastrofe = new GuardaCatastrofe().Guarda_Catastrofe(Com_Cod, Ctf_Cod, Constantes.numUno);
                //return RedirectToAction("Index");
                //var valor_moneda = 1;
                return Json(Constantes.digitoUno, JsonRequestBehavior.AllowGet);
            }

            ViewBag.Com_Id = new SelectList(db.TGSC_COM, "Com_Cod", "Com_Cod", tGSC_REL_CTF_COM.Com_Cod);
            ViewBag.Ctf_Id = new SelectList(db.TGSC_CTF, "Ctf_Cod", "Ctf_Cod", tGSC_REL_CTF_COM.Ctf_Cod);
            
            return View(tGSC_REL_CTF_COM);
        }



        public ActionResult EditaCatastrofe([Bind(Include = "Cod_Ctf, Cantidad_Ctf")] C_EditaCtf eDITACTF)
        {
            var CodCtf = eDITACTF.Cod_Ctf;
            var Total_Reg = eDITACTF.Cantidad_Ctf;

            Svc_TGSC_REL_CTF_COM_VerLocalizacion_Result Detalle_Catastrofe_Loc = new Svc_TGSC_REL_CTF_COM_VerLocalizacion_Result();
            Detalle_Catastrofe_Loc = new ObtieneParametros().Detalle_Catastrofe_Loc(CodCtf);


            ViewBag.Cod_Ctf = eDITACTF.Cod_Ctf;
            ViewBag.Region = Detalle_Catastrofe_Loc.Rgn_Nom;
            ViewBag.Comuna = Detalle_Catastrofe_Loc.Com_Nom;
            ViewBag.Catastrofe = Detalle_Catastrofe_Loc.Ctf_Des;
            ViewBag.TotalReg = eDITACTF.Cantidad_Ctf;

            if (eDITACTF.Cantidad_Ctf == Constantes.digitoCero)
            {
                ViewBag.Observaciones = Constantes.mensajeCatastrofeElimina;
            }
            else
            {
                ViewBag.Observaciones = Constantes.mensajeCatastrofeEstado;
            }
       
            return PartialView();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]

        public ActionResult EditaCatastrofe2([Bind(Include = "Cod_Ctf")] C_EditaCtf eDITACTF)
        {
            var CodCtf = eDITACTF.Cod_Ctf;
            var Total_Reg = eDITACTF.Cantidad_Ctf;

            if (ModelState.IsValid)
            {
                Sva_TGSC_REL_CTF_COM_EliminaCatastrofes_Result Elimina_CTF = new Sva_TGSC_REL_CTF_COM_EliminaCatastrofes_Result();
                Elimina_CTF = new GuardaMantenedores().EliminaCtf(CodCtf);
                return RedirectToAction("AgregaCatastrofe", "Mantenedores");
            }

            return PartialView();
        }
            


        public JsonResult ListaComunas(int Reg_id)
        {
            List<Svc_TGSC_RGN_VerComunas_Result> ListaComuna = new List<Svc_TGSC_RGN_VerComunas_Result>();
            ListaComuna = new ObtieneParametros().Obtiene_Comuna(Reg_id);
            return Json(ListaComuna, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ListaClasificacionRiesgo()
        {

            if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        public JsonResult ListaClaRie()
        {
            List<Svc_TGSC_CLA_RIE_VerClasificaRiesgo_02_Result> Lista_Cla_Rie = new List<Svc_TGSC_CLA_RIE_VerClasificaRiesgo_02_Result>();
            Lista_Cla_Rie = new ObtieneParametros().Obtiene_Cla_Rie_02();

            return Json(Lista_Cla_Rie, JsonRequestBehavior.AllowGet);
        }


        public ActionResult InsertaClasificacionRiesgo()
        {
            if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.estadoctf = Constantes.numUno;
            ViewBag.fechaingreso = DateTime.Now.ToString("yyyy/MM/dd");
            return PartialView();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //
        public ActionResult InsertaClasificacionRiesgo([Bind(Include = "Cla_Rie_Id, Cla_Rie_Tip, Cla_Rie_Por_Pvs, Cla_Rie_Est")] C_ClaRie _GuardaClaRie)
        {
            if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }

            var _Cla_Rie_Id = _GuardaClaRie.Cla_Rie_Id;
            var _Cla_Rie_Tip = _GuardaClaRie.Cla_Rie_Tip;
            var _Cla_Rie_Por_Pvs = _GuardaClaRie.Cla_Rie_Por_Pvs;
            var _Cla_Rie_Est = _GuardaClaRie.Cla_Rie_Est;


            if (ModelState.IsValid)
            {
                Sva_TGSC_CLA_RIE_InsertaClasificacionRiesgo_Result Guarda_SecEco = new Sva_TGSC_CLA_RIE_InsertaClasificacionRiesgo_Result();
                Guarda_SecEco = new GuardaMantenedores().GuardarClaRie(_Cla_Rie_Id, _Cla_Rie_Tip, _Cla_Rie_Por_Pvs, _Cla_Rie_Est);
                return View("ListaClasificacionRiesgo");
            }

            return View(_GuardaClaRie);
        }



        // INICIO SECTOR ECONOMICO

        public ActionResult ListaSectorEconomico()
        {
            if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        public JsonResult ListaSecEco()
        {

            List<Svc_TGSC_SCE_VerSectorEconomico_02_Result> Lista_Sec_Eco = new List<Svc_TGSC_SCE_VerSectorEconomico_02_Result>();
            Lista_Sec_Eco = new ObtieneParametros().obtiene_Sec_Eco_02();

            return Json(Lista_Sec_Eco, JsonRequestBehavior.AllowGet);
        }


        public ActionResult InsertaSectorEconomico()
        {
            if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.estadoctf = Constantes.numUno;
            ViewBag.fechaingreso = DateTime.Now.ToString("yyyy/MM/dd");
            return PartialView();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //
        public ActionResult InsertaSectorEconomico([Bind(Include = "Sce_Id, Sce_Cod, Sce_Des, Sce_Est")] C_SecEco _GuardaSecEco)
        {

            if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }

            var _Sce_Id = _GuardaSecEco.Sce_Id;
            var _Sce_Cod = _GuardaSecEco.Sce_Cod;
            var _Sce_Des = _GuardaSecEco.Sce_Des;
            var _Sce_Est = _GuardaSecEco.Sce_Est;


            if (ModelState.IsValid)
            {
                Sva_TGSC_SCE_InsertaSectorEconomico_Result Guarda_SecEco = new Sva_TGSC_SCE_InsertaSectorEconomico_Result();
                Guarda_SecEco = new GuardaMantenedores().GuardarSecEco(_Sce_Id, _Sce_Cod, _Sce_Des, _Sce_Est);
                return View("ListaSectorEconomico");
            }

            return View(_GuardaSecEco);
        }

        // FIN SECTOR ECONOMICO


        // INICIO GARANTIA

        public ActionResult ListaGarantias()
        {
            if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        public JsonResult ListaGar()
        {
            List<Svc_TGSC_GAR_ADC_VerGarantia_02_Result> Lista_Gar = new List<Svc_TGSC_GAR_ADC_VerGarantia_02_Result>();
            Lista_Gar = new ObtieneParametros().Obtiene_Gar_Adc_02();

            return Json(Lista_Gar, JsonRequestBehavior.AllowGet);
        }



        public ActionResult InsertaGarantias()
        {
            if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }



            ViewBag.estadoctf = Constantes.numUno;
            ViewBag.fechaingreso = DateTime.Now.ToString("yyyy/MM/dd");
            return PartialView();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        //
        public ActionResult InsertaGarantias([Bind(Include = "Gar_Adc_Id, Gar_Adc_Cod, Gar_Adc_Des, Gar_Adc_Est")] C_Garantia _GuardaGarantia)
        {
            if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }

            var _Gar_Adc_Id = _GuardaGarantia.Gar_Adc_Id;
            var _Gar_Adc_Cod = _GuardaGarantia.Gar_Adc_Cod;
            var _Gar_Adc_Des = _GuardaGarantia.Gar_Adc_Des;
            var _Gar_Adc_Est = _GuardaGarantia.Gar_Adc_Est;


            if (ModelState.IsValid)
            {
                Sva_TGSC_GAR_ADC_InsertaGarantias_Result Guarda_Garantia = new Sva_TGSC_GAR_ADC_InsertaGarantias_Result();
                Guarda_Garantia = new GuardaMantenedores().GuardarGarantia(_Gar_Adc_Id, _Gar_Adc_Cod, _Gar_Adc_Des, _Gar_Adc_Est);
                return View("ListaGarantias");
            }

            return View(_GuardaGarantia);
        }

        // FIN GARANTIA


        // INICIO SEGUROS

        public ActionResult ListaSeguros()
        {
            if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        public JsonResult ListaSeg()
        {

            List<Svc_TGSC_SEG_REF_VerSeguro_02_Result> Lista_Seg = new List<Svc_TGSC_SEG_REF_VerSeguro_02_Result>();
            Lista_Seg = new ObtieneParametros().Obtiene_Des_Seg_02();

            return Json(Lista_Seg, JsonRequestBehavior.AllowGet);
        }


        public ActionResult InsertaSeguro()
        {
            if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.estadoctf = Constantes.numUno;
            ViewBag.fechaingreso = DateTime.Now.ToString("yyyy/MM/dd");
            return PartialView();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //
        public ActionResult InsertaSeguro([Bind(Include = "Seg_Ref_Id, Seg_Ref_Cod, Seg_Ref_Des, Seg_Ref_Est")] C_Seguro _GuardaGarantia)
        {

            if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }

            var _Seg_Ref_Id = _GuardaGarantia.Seg_Ref_Id;
            var _Seg_Ref_Cod = _GuardaGarantia.Seg_Ref_Cod;
            var _Seg_Ref_Des = _GuardaGarantia.Seg_Ref_Des;
            var _Seg_Ref_Est = _GuardaGarantia.Seg_Ref_Est;


            if (ModelState.IsValid)
            {
                Sva_TGSC_SEG_REF_InsertaSeguros_Result Guarda_Garantia = new Sva_TGSC_SEG_REF_InsertaSeguros_Result();
                Guarda_Garantia = new GuardaMantenedores().GuardarSeguros(_Seg_Ref_Id, _Seg_Ref_Cod, _Seg_Ref_Des, _Seg_Ref_Est);

                return View("ListaSeguros");
            }

            return View(_GuardaGarantia);
        }


        // FIN SEGUROS


        // INICIO TIPO DE GRACIA

        public ActionResult ListaTipoGracia()
        {
            if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        
        public JsonResult ListaTipGra()
        {

            List<Svc_TGSC_TIP_GRC_VerGracia_02_Result> Lista_Tip_Grc = new List<Svc_TGSC_TIP_GRC_VerGracia_02_Result>();
            Lista_Tip_Grc = new ObtieneParametros().Obtiene_Tip_Grc_02();

            return Json(Lista_Tip_Grc, JsonRequestBehavior.AllowGet);
        }


        public ActionResult InsertaTipoGracia()
        {
            if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }
            

            ViewBag.estadoctf = Constantes.numUno;
            ViewBag.fechaingreso = DateTime.Now.ToString("yyyy/MM/dd");
            return PartialView();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        //
        public ActionResult InsertaTipoGracia([Bind(Include = "Tip_Grc_Id, Tip_Grc_Des, Tip_Grc_Est")] C_TipGra _GuardaTipGra)
        {
            if (Session["perfil"] == null || (int)Session["perfil"] == Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }

            var _Tip_Grc_Id = _GuardaTipGra.Tip_Grc_Id;
            var _Tip_Grc_Des = _GuardaTipGra.Tip_Grc_Des;
            var _Tip_Grc_Est = _GuardaTipGra.Tip_Grc_Est;


            if (ModelState.IsValid)
            {
                Sva_TGSC_TIP_GRC_InsertaTipoGracia_Result Guarda_TipGra = new Sva_TGSC_TIP_GRC_InsertaTipoGracia_Result();
                Guarda_TipGra = new GuardaMantenedores().GuardarTipGra(_Tip_Grc_Id, _Tip_Grc_Des, _Tip_Grc_Est);
                return View("ListaTipoGracia");
            }

            return View(_GuardaTipGra);
        }


        // FIN TIPO DE GRACIA

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

