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
    public class SolicitudesController : Controller 
    {
        private DB_DESARROLLOEntities1 db = new DB_DESARROLLOEntities1();


        public JsonResult DatosClientes(string Sol_Nrt_Emp, string Sol_Drt_Emp)
        {

            string rut_original = Sol_Nrt_Emp.Replace(".", "");
            string rut_largo = new string('0', 9 - rut_original.Length);
            //string rut_cliente = rut_largo + Sol_Nrt_Emp;

            int rut_busqueda = int.Parse(rut_original);


            //Svc_tbcc_cli_VerCliente_Result usuario = new Svc_tbcc_cli_VerCliente_Result();

            var SalDatosCliente = new ObtieneCliente().Obtiene_cliente(rut_busqueda);


            return Json(SalDatosCliente, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Ingreso()
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



            Svc_TGSC_PRA_VerParametros_Result Por_Fjo_Cms = new Svc_TGSC_PRA_VerParametros_Result();
            Por_Fjo_Cms = new ObtieneParametros().Obtiene_Parametros("Por_Fjo_Cms", Constantes.numUno);

            Svc_TGSC_PRA_VerParametros_Result Por_Fjo_Cms_Ctf = new Svc_TGSC_PRA_VerParametros_Result();
            Por_Fjo_Cms_Ctf = new ObtieneParametros().Obtiene_Parametros("Por_Fjo_Cms_Ctf", Constantes.numUno);

            Svc_TGSC_PRA_VerParametros_Result Fec_Cse_Max_Dia = new Svc_TGSC_PRA_VerParametros_Result();
            Fec_Cse_Max_Dia = new ObtieneParametros().Obtiene_Parametros("Fec_Cse_Max_Dia", Constantes.numUno);

            Svc_TGSC_PRA_VerParametros_Result Fec_Cse_Min_Dia = new Svc_TGSC_PRA_VerParametros_Result();
            Fec_Cse_Min_Dia = new ObtieneParametros().Obtiene_Parametros("Fec_Cse_Min_Dia", Constantes.numUno);


            var fecha_minima = Convert.ToInt32(Fec_Cse_Min_Dia.Pra_Val);


            ViewBag.Por_Fjo_Cms = Por_Fjo_Cms.Pra_Val;
            ViewBag.Por_Fjo_Cms_Ctf = Por_Fjo_Cms_Ctf.Pra_Val;

            ViewBag.Fec_Cse_Max_Dia = Fec_Cse_Max_Dia.Pra_Val;
            ViewBag.Fec_Cse_Min_Dia = (fecha_minima * -1);


            //int Ctf_Est = 1;
            //string estados_opc = "1";
            //int Reg_Id = 1;
            //int Ctf_Cod = 1;

            

            List<Svc_TGSC_PRA_VerParametrosList_Result> Obtiene_SINO = new List<Svc_TGSC_PRA_VerParametrosList_Result>();
            Obtiene_SINO = new ObtieneParametros().Obtiene_Parametros_List("Cnd_Si_No", Constantes.numUno);
            ViewBag.Sol_Ina = new SelectList(Obtiene_SINO, "Pra_Val", "Pra_Des");
            ViewBag.Sol_Exp = new SelectList(Obtiene_SINO, "Pra_Val", "Pra_Des", Constantes.numDos);


            List<Svc_TGSC_PRA_VerParametrosList_Result> Obtiene_Tip_Cms = new List<Svc_TGSC_PRA_VerParametrosList_Result>();
            Obtiene_Tip_Cms = new ObtieneParametros().Obtiene_Parametros_List("Tip_Cms", Constantes.numUno);
            ViewBag.Comision = new SelectList(Obtiene_Tip_Cms, "Pra_Val", "Pra_Des");


            List<Svc_TGSC_GNO_VerGenero_Result> Gno_Id = new List<Svc_TGSC_GNO_VerGenero_Result>();
            Gno_Id = new ObtieneParametros().Obtiene_Genero(Constantes.numUno);
            ViewBag.Gno_Id = new SelectList(Gno_Id, "Gno_Id", "Gno_Des");


            List<Svc_TGSC_SCE_VerSectorEconomico_Result> SectorEco = new List<Svc_TGSC_SCE_VerSectorEconomico_Result>();
            SectorEco = new ObtieneParametros().obtiene_Sec_Eco(Constantes.numUno);
            ViewBag.Sce_Id = new SelectList(SectorEco, "Sce_Id", "sec_des");


            List<Svc_TGSC_CLA_RIE_VerClasificaRiesgo_Result> ClaRiesgo = new List<Svc_TGSC_CLA_RIE_VerClasificaRiesgo_Result>();
            ClaRiesgo = new ObtieneParametros().Obtiene_Cla_Rie(Constantes.numUno);
            ViewBag.Cla_Rie_Id = new SelectList(ClaRiesgo, "Cla_Rie_Id", "Cla_Rie_Tip");


            List<Svc_TGSC_REL_CTF_COM_VerCatastrofe_Result> ListaCatastrofe = new List<Svc_TGSC_REL_CTF_COM_VerCatastrofe_Result>();
            ListaCatastrofe = new ObtieneParametros().obtiene_catastrofe();
            ViewBag.Catastrofe = new SelectList(ListaCatastrofe, "Ctf_Cod", "Ctf_Des");


            List<Svc_TGSC_REL_CTF_COM_VerRegion_Result> ListaRegiones = new List<Svc_TGSC_REL_CTF_COM_VerRegion_Result>();
            ListaRegiones = new ObtieneParametros().Obtiene_Region(Constantes.digitoUno);
            ViewBag.Regiones = new SelectList(ListaRegiones, "Rgn_cod", "Rgn_Nom");


            List<Svc_TGSC_REL_CTF_COM_VerComuna_Result> ListaComuna = new List<Svc_TGSC_REL_CTF_COM_VerComuna_Result>();
            ListaComuna = new ObtieneParametros().Obtiene_Comuna(Constantes.digitoUno, Constantes.digitoUno);
            ViewBag.Rel_Ctf_Com_Id = new SelectList(ListaComuna, "Rel_Ctf_Com_Id", "Com_Nom_02");
            

            List<Svc_TGSC_DES_PROY_VerDescripcion_Result> ListaProyecto = new List<Svc_TGSC_DES_PROY_VerDescripcion_Result>();

            //string Proy_Obj_Inv = "inversion";
            //string Proy_Est_Inv = "1";
            ListaProyecto = new ObtieneDesPyt().ObtieneListaProyecto(Constantes.objetivoInversion, Constantes.numUno);
            ViewBag.Des_Proy_Cod_01 = new SelectList(ListaProyecto, "Des_Proy_Cod", "Des_Proy_Des");

            //string Proy_Obj_cp = "capital_de_trabajo";
            //string Proy_Est_cp = "1";
            ListaProyecto = new ObtieneDesPyt().ObtieneListaProyecto(Constantes.objetivoCapitalTrabajo, Constantes.numUno);
            ViewBag.Des_Proy_Cod_02 = new SelectList(ListaProyecto, "Des_Proy_Cod", "Des_Proy_Des");

            //string Proy_Obj_Ref = "Refinanciamiento";
            //string Proy_Est_Ref = "1";
            ListaProyecto = new ObtieneDesPyt().ObtieneListaProyecto(Constantes.objetivoRefinanciamiento, Constantes.numUno);
            ViewBag.Des_Proy_Cod_03 = new SelectList(ListaProyecto, "Des_Proy_Cod", "Des_Proy_Des");
            
            ViewBag.Tip_Ope_Id = new SelectList("");


            List<Svc_TGSC_TIP_MON_VerTipoMoneda_Result> ListMoneda = new List<Svc_TGSC_TIP_MON_VerTipoMoneda_Result>();
            {
                ListMoneda = new ObtieneParametros().Obtiene_Moneda(Constantes.numUno).ToList();
                ViewBag.Tip_Mon_Id = new SelectList(ListMoneda, "Tip_Mon_id", "Tip_Mon_Des");
            }

            List<Svc_TGSC_TIP_GRC_VerGracia_Result> ListGracia = new List<Svc_TGSC_TIP_GRC_VerGracia_Result>();
            {
                ListGracia = new ObtieneParametros().Obtiene_Tip_Grc(Constantes.numUno).ToList();
                ViewBag.Tip_Grc_Id = new SelectList(ListGracia, "Tip_Grc_Id", "tip_Grc_Des");
            }

            List<Svc_TGSC_FRE_PAG_VerFrecuencia_Result> List_Fre_Pag = new List<Svc_TGSC_FRE_PAG_VerFrecuencia_Result>();
            {
                List_Fre_Pag = new ObtieneParametros().Obtiene_Fre_Pag(Constantes.numUno).ToList();
                ViewBag.Fre_Pag_Id = new SelectList(List_Fre_Pag, "Fre_Pag_Id", "Fre_Pag_Des");
            }

            List<Svc_TGSC_TIP_TAS_INT_VerTipoTasa_Result> List_Tip_Tas = new List<Svc_TGSC_TIP_TAS_INT_VerTipoTasa_Result>();
            {
                List_Tip_Tas = new ObtieneParametros().Obtiene_Tip_Tas(Constantes.numUno).ToList();
                ViewBag.Tip_Tas_Int_Cod = new SelectList(List_Tip_Tas, "Tip_Tas_Int_Cod", "Tip_Tas_Int_Des", Constantes.numCinco);
            }

            List<Svc_TGSC_GAR_ADC_VerGarantia_Result> List_Gar_Adc = new List<Svc_TGSC_GAR_ADC_VerGarantia_Result>();
            {
                List_Gar_Adc = new ObtieneParametros().Obtiene_Gar_Adc(Constantes.numUno).ToList();
                ViewBag.Gar_Adc_Id_01 = new SelectList(List_Gar_Adc, "Gar_Adc_Id", "Gar_Adc_Des");
                ViewBag.Gar_Adc_Id_02 = new SelectList(List_Gar_Adc, "Gar_Adc_Id", "Gar_Adc_Des");
            }

            List<Svc_TGSC_SEG_REF_VerSeguro_Result> List_Seg = new List<Svc_TGSC_SEG_REF_VerSeguro_Result>();
            {
                List_Seg = new ObtieneParametros().Obtiene_Seg(Constantes.numUno).ToList();
                ViewBag.Seg_Ref_Id_01 = new SelectList(List_Seg, "Seg_Ref_Id", "Seg_ref_Des");
            }

            
            
            return View();
        }

        // POST: Solicitudes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ingreso([Bind(Include = "Sol_Num_Sol, Sol_Num_Ope, Sol_Fec_Sol, Sol_Num_Fol, Sol_Nrt_Emp, Sol_Drt_Emp, Sol_Rzn_Scl, Sce_Id, Sol_Num_Sec, Sol_Ina, Sol_Tel, Rel_Ctf_Com_Id, Sol_Nom_Loc, Sol_Nom_Call, Sol_Num_Call, Sol_Cmn_Dir, Sol_Cro_Elt, Gno_Id, Sol_Exp, Cla_Rie_Id, Sol_Vnt_Emp, Sol_Mnt_Cbt_Utz, Sol_Mnt_Cms, Sol_Por_Cbt_Dis, Sol_Mnt_Cbt_Dis, Sol_Usu_Ejc, Sol_Rut_Ejc, Sol_Ofi_Cse_Des, Tip_Ope_Id, Tip_Mon_Id, Sol_Mnt_Crd, Sol_Mnt_Cpl_Tbj, Sol_Mnt_Inv, Sol_Mnt_Cp_Rfn, Sol_Fec_Cse, Sol_Fec_Vct, Sol_Val_Dol, Sol_Val_UF, Tip_Grc_Id, Sol_Mes_Grc, Sol_Mes_No_Pag, Fre_Pag_Id, Tip_Tas_Int_Cod, Sol_Int_Anl, Sol_Num_Cuo, Est_Sol_Id, Etp_Sol_Id, Sol_Pzo, Gar_Adc_Id_01, Gar_Adc_Id_02, Seg_Ref_Id_01, Seg_Ref_Id_02, Seg_Ref_Id_03, Des_Proy_Cod_01, Des_Proy_Cod_02, Des_Proy_Cod_03")] GuardarSolicitud _GuardarSolicitud)
            
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



            //Sva_TGSC_NUM_SOL_Numero_IFI_Result num_ifi = new Sva_TGSC_NUM_SOL_Numero_IFI_Result();
            //num_ifi = new ObtieneIFI().Obtiene_IFI();

            //var num_sol = Convert.ToInt32(num_ifi.Num_Ifi);
            //var num_ope = Convert.ToString(num_ifi.Num_Ifi);
            
            //var _Sol_Num_Sol = num_sol; //_GuardarSolicitud.Sol_Num_Sol;
            //var _Sol_Num_Ope = num_ope; //_GuardarSolicitud.Sol_Num_Ope;


            var _Sol_Fec_Sol = DateTime.Now; //_GuardarSolicitud.Sol_Fec_Sol;
            var _Sol_Num_Fol = _GuardarSolicitud.Sol_Num_Fol;
            var _Sol_Nrt_Emp = _GuardarSolicitud.Sol_Nrt_Emp;
            var _Sol_Drt_Emp = _GuardarSolicitud.Sol_Drt_Emp;
            var _Sol_Rzn_Scl = _GuardarSolicitud.Sol_Rzn_Scl;
            var _Sce_Id = _GuardarSolicitud.Sce_Id;
            var _Sol_Num_Sec = _GuardarSolicitud.Sol_Num_Sec;
            var _Sol_Ina = _GuardarSolicitud.Sol_Ina;
            var _Sol_Tel = _GuardarSolicitud.Sol_Tel;
            var _Rel_Ctf_Com_Id = _GuardarSolicitud.Rel_Ctf_Com_Id;
            var _Sol_Nom_Loc = _GuardarSolicitud.Sol_Nom_Loc;
            var _Sol_Nom_Call = _GuardarSolicitud.Sol_Nom_Call;
            var _Sol_Num_Call = _GuardarSolicitud.Sol_Num_Call;
            var _Sol_Cmn_Dir = _GuardarSolicitud.Sol_Cmn_Dir;
            var _Sol_Cro_Elt = _GuardarSolicitud.Sol_Cro_Elt;
            var _Gno_Id = _GuardarSolicitud.Gno_Id;
            var _Sol_Exp = _GuardarSolicitud.Sol_Exp;
            var _Cla_Rie_Id = _GuardarSolicitud.Cla_Rie_Id;
            var _Sol_Vnt_Emp = _GuardarSolicitud.Sol_Vnt_Emp;
            var _Sol_Mnt_Cbt_Utz = _GuardarSolicitud.Sol_Mnt_Cbt_Utz;
            var _Sol_Mnt_Cms = _GuardarSolicitud.Sol_Mnt_Cms;
            var _Sol_Por_Cbt_Dis = _GuardarSolicitud.Sol_Por_Cbt_Dis;
            var _Sol_Mnt_Cbt_Dis = _GuardarSolicitud.Sol_Mnt_Cbt_Dis;
            var _Sol_Usu_Ejc = _GuardarSolicitud.Sol_Usu_Ejc;
            var _Sol_Rut_Ejc = _GuardarSolicitud.Sol_Rut_Ejc;
            var _Sol_Ofi_Cse_Des = _GuardarSolicitud.Sol_Ofi_Cse_Des;
            var _Tip_Ope_Id = _GuardarSolicitud.Tip_Ope_Id;
            var _Tip_Mon_Id = _GuardarSolicitud.Tip_Mon_Id;
            var _Sol_Mnt_Crd = _GuardarSolicitud.Sol_Mnt_Crd;
            var _Sol_Mnt_Cpl_Tbj = _GuardarSolicitud.Sol_Mnt_Cpl_Tbj;
            var _Sol_Mnt_Inv = _GuardarSolicitud.Sol_Mnt_Inv;
            var _Sol_Mnt_Rfn = _GuardarSolicitud.Sol_Mnt_Cp_Rfn;
            var _Sol_Fec_Cse = _GuardarSolicitud.Sol_Fec_Cse;
            var _Sol_Fec_Vct = _GuardarSolicitud.Sol_Fec_Vct;
            var _Sol_Val_Dol = _GuardarSolicitud.Sol_Val_Dol;
            var _Sol_Val_UF = _GuardarSolicitud.Sol_Val_UF;
            var _Tip_Grc_Id = _GuardarSolicitud.Tip_Grc_Id;
            var _Sol_Mes_Grc = _GuardarSolicitud.Sol_Mes_Grc;
            var _Sol_Mes_No_Pag = _GuardarSolicitud.Sol_Mes_No_Pag;
            var _Fre_Pag_Id = _GuardarSolicitud.Fre_Pag_Id;
            var _Tip_Tas_Int_Cod = _GuardarSolicitud.Tip_Tas_Int_Cod;
            var _Sol_Int_Anl = _GuardarSolicitud.Sol_Int_Anl;
            var _Sol_Num_Cuo = _GuardarSolicitud.Sol_Num_Cuo;
            var _Est_Sol_Id = _GuardarSolicitud.Est_Sol_Id;
            var _Etp_Sol_Id = _GuardarSolicitud.Etp_Sol_Id;
            var _Sol_Pzo = _GuardarSolicitud.Sol_Pzo;
            var _Gar_Adc_Id_01 = _GuardarSolicitud.Gar_Adc_Id_01;
            var _Gar_Adc_Id_02 = _GuardarSolicitud.Gar_Adc_Id_02;
            var _Seg_Ref_Id_01 = _GuardarSolicitud.Seg_Ref_Id_01;
            var _Seg_Ref_Id_02 = _GuardarSolicitud.Seg_Ref_Id_02;
            var _Seg_Ref_Id_03 = _GuardarSolicitud.Seg_Ref_Id_03;
            var _Des_Proy_Cod_01 = _GuardarSolicitud.Des_Proy_Cod_01;
            var _Des_Proy_Cod_02 = _GuardarSolicitud.Des_Proy_Cod_02;
            var _Des_Proy_Cod_03 = _GuardarSolicitud.Des_Proy_Cod_03;


       
            if (ModelState.IsValid)
            {

                Sva_TGSC_NUM_SOL_Numero_IFI_Result num_ifi = new Sva_TGSC_NUM_SOL_Numero_IFI_Result();
                num_ifi = new ObtieneIFI().Obtiene_IFI();

                var num_sol = Convert.ToInt32(num_ifi.Num_Ifi);
                var num_ope = Convert.ToString(num_ifi.Num_Ifi);

                var _Sol_Num_Sol = num_sol; 
                var _Sol_Num_Ope = num_ope; 

                Sva_TGSC_SOL_InsertaSolicitud_Result Guarda_Solicitud = new Sva_TGSC_SOL_InsertaSolicitud_Result();
                Guarda_Solicitud = new RegistraSolicitud().Registra_Solicitud(_Sol_Num_Sol, _Sol_Num_Ope, _Sol_Fec_Sol, _Sol_Num_Fol, _Sol_Nrt_Emp, _Sol_Drt_Emp, _Sol_Rzn_Scl, _Sce_Id, _Sol_Num_Sec, _Sol_Ina, _Sol_Tel, _Rel_Ctf_Com_Id, _Sol_Nom_Loc, _Sol_Nom_Call, _Sol_Num_Call, _Sol_Cmn_Dir, _Sol_Cro_Elt, _Gno_Id, _Sol_Exp, _Cla_Rie_Id, _Sol_Vnt_Emp, _Sol_Mnt_Cbt_Utz, _Sol_Mnt_Cms, _Sol_Por_Cbt_Dis, _Sol_Mnt_Cbt_Dis, _Sol_Usu_Ejc, _Sol_Rut_Ejc, _Sol_Ofi_Cse_Des, _Tip_Ope_Id, _Tip_Mon_Id, _Sol_Mnt_Crd, _Sol_Mnt_Cpl_Tbj, _Sol_Mnt_Inv, _Sol_Mnt_Rfn, _Sol_Fec_Cse, _Sol_Fec_Vct, _Sol_Val_Dol, _Sol_Val_UF, _Tip_Grc_Id, _Sol_Mes_Grc, _Sol_Mes_No_Pag, _Fre_Pag_Id, _Tip_Tas_Int_Cod, _Sol_Int_Anl, _Sol_Num_Cuo, _Est_Sol_Id, _Etp_Sol_Id, _Sol_Pzo, _Gar_Adc_Id_01, _Gar_Adc_Id_02, _Seg_Ref_Id_01, _Seg_Ref_Id_02, _Seg_Ref_Id_03, _Des_Proy_Cod_01, _Des_Proy_Cod_02, _Des_Proy_Cod_03);

                Sva_TGSC_SOL_DOC_Documentos_Result Datos_Documentos = new Sva_TGSC_SOL_DOC_Documentos_Result();
                Datos_Documentos = new GeneraDocumentos().Inserta_Reg_Doc(_Sol_Num_Sol);


                return RedirectToAction("Ingresada", "Solicitudes", new { @id_ifi = _Sol_Num_Sol }); //, etapa = gSC_SOL.Etp_Sol_Id
            }


            return View(_GuardarSolicitud);
        }


        public ActionResult Ingresada(int id_ifi)
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


            Svc_TGSC_SOL_VerUsuarioEjecutivo_Result CliEje = new Svc_TGSC_SOL_VerUsuarioEjecutivo_Result();
            CliEje = new ObtieneCliEje().Obtiene_Cli_Eje(id_ifi);


            Svc_TGSC_SOL_VerSolicitudId_Result Solicitud = new Svc_TGSC_SOL_VerSolicitudId_Result();
            Solicitud = new ObtieneSolicitud().Obtiene_Solicitud(CliEje.Sol_Id);

            ViewBag.razon = CliEje.Razon_Social;
            ViewBag.Ejecutivo = CliEje.Ejecutivo;
            ViewBag.Num_Ifi = CliEje.Numero_IFI;
            ViewBag.sol_id = CliEje.Sol_Id;
            ViewBag.etapa = Solicitud.Etp_Sol_Id;
            ViewBag.catastrofe = Solicitud.Ctf_Cod;
            
            return View();
        }


      



        // GET: Solicitudes/Edit/5
        public ActionResult Edit(int id)
        {
            Svc_TGSC_SOL_VerSolicitudId_Result Solicitud = new Svc_TGSC_SOL_VerSolicitudId_Result();
            Solicitud = new ObtieneSolicitud().Obtiene_Solicitud(id);


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

            if (Solicitud.Etp_Sol_Id != 2 || (int)Session["perfil"] == 2 || Solicitud.Est_Sol_Id == 2)
            {
                return RedirectToAction("Index", "Home");
            }



            //string estados_opc = "1";
            //int Reg_Id = 1;

            Svc_TGSC_PRA_VerParametros_Result Por_Fijo_Cms = new Svc_TGSC_PRA_VerParametros_Result();
            Por_Fijo_Cms = new ObtieneParametros().Obtiene_Parametros("Por_Fjo_Cms", Constantes.numUno);

            Svc_TGSC_PRA_VerParametros_Result Por_Fjo_Cms_Ctf = new Svc_TGSC_PRA_VerParametros_Result();
            Por_Fjo_Cms_Ctf = new ObtieneParametros().Obtiene_Parametros("Por_Fjo_Cms_Ctf", Constantes.numUno);


            Svc_TGSC_PRA_VerParametros_Result Fec_Cse_Max_Dia = new Svc_TGSC_PRA_VerParametros_Result();
            Fec_Cse_Max_Dia = new ObtieneParametros().Obtiene_Parametros("Fec_Cse_Max_Dia", Constantes.numUno);

            Svc_TGSC_PRA_VerParametros_Result Fec_Cse_Min_Dia = new Svc_TGSC_PRA_VerParametros_Result();
            Fec_Cse_Min_Dia = new ObtieneParametros().Obtiene_Parametros("Fec_Cse_Min_Dia", Constantes.numUno);


            var fecha_minima = Convert.ToInt32(Fec_Cse_Min_Dia.Pra_Val);


            ViewBag.Por_Fjo_Cms = Por_Fijo_Cms.Pra_Val;
            ViewBag.Por_Fjo_Cms_Ctf = Por_Fjo_Cms_Ctf.Pra_Val;

            ViewBag.Fec_Cse_Max_Dia = Fec_Cse_Max_Dia.Pra_Val;
            ViewBag.Fec_Cse_Min_Dia = (fecha_minima * -1);


            ViewBag.Sol_Num_Sol = Solicitud.Sol_Num_Sol;
            ViewBag.rut = Solicitud.Sol_Nrt_Emp;
            ViewBag.rut_dv = Solicitud.Sol_Drt_Emp;
            ViewBag.Sol_Nrt_Emp = Solicitud.Sol_Nrt_Emp;
            ViewBag.Sol_Drt_Emp = Solicitud.Sol_Drt_Emp;
            ViewBag.Sol_Rzn_Scl = Solicitud.Razon_Social;
            ViewBag.Sol_Nom_Call = Solicitud.Sol_Nom_Call;
            ViewBag.Sol_Num_Call = Solicitud.Sol_Num_Call;
            ViewBag.Sol_Cmn_Dir = Solicitud.Sol_Cmn_Dir;
            ViewBag.Sol_Tel = Solicitud.Telefono;
            ViewBag.Sol_Cro_Elt = Solicitud.Sol_Cro_Elt;
            ViewBag.Gno_id = Solicitud.Genero;
            ViewBag.Sol_Exp = Solicitud.Sol_Ina;
            ViewBag.Sol_Ina = Solicitud.Sol_Exp;
            ViewBag.Sol_Sce = Solicitud.Sector_Economico;
            ViewBag.Cla_Rie_Id = Solicitud.Clasificacion_Riesgo;
            ViewBag.Sol_Vnt_Emp = Solicitud.Sol_Vnt_Emp;
            ViewBag.Sol_Mnt_Cbt_Utz = Solicitud.Sol_Mnt_Cbt_Utz;
            ViewBag.Catastrofe = Solicitud.Tipo_Catastrofe;
            ViewBag.Region = Solicitud.Nombre_Region;
            ViewBag.Localizacion = Solicitud.Localizacion;
            ViewBag.Tip_Ope = Solicitud.Tipo_Operacion;
            ViewBag.Sol_Mnt_Inv = Solicitud.Sol_Mnt_Inv;
            ViewBag.Sol_Mnt_Cpl_Tbj = Solicitud.Sol_Mnt_Cpl_Tbj;
            ViewBag.Sol_Mnt_Cp_Rfn = Solicitud.Sol_Mnt_Rfn;
            ViewBag.Tip_Mon_Id = Solicitud.Tipo_Moneda;
            ViewBag.Sol_Mnt_Crd = Solicitud.Monto_Credito;
            ViewBag.Fecha_Curse = Solicitud.Sol_Fec_Cse;
            ViewBag.Fecha_Venc = Solicitud.Sol_Fec_Vct;
            ViewBag.Sol_Fec_Vct = Solicitud.Sol_Fec_Vct;
            ViewBag.Sol_Fec_Cse = Solicitud.Sol_Fec_Cse;
            ViewBag.Est_Sol_Id = Solicitud.Est_Sol_Id;
            ViewBag.Etp_Sol_Id = Solicitud.Etp_Sol_Id;
            ViewBag.Sol_Val_Dol = Solicitud.Sol_Val_Dol;
            ViewBag.Sol_Val_UF = Solicitud.Sol_Val_UF;
            ViewBag.Tip_Grc_Id = Solicitud.Tipo_Gracia;
            ViewBag.Sol_Mes_Grc = Solicitud.Sol_Mes_Grc;
            ViewBag.Fre_Pag_Id = Solicitud.Frecuencia_Pago;
            ViewBag.Sol_Int_Anl = Solicitud.Sol_Int_Anl;
            ViewBag.Tip_Tas_Int_Cod = Solicitud.Tip_Tas_Int_Des;
            ViewBag.Sol_Num_Cuo = Solicitud.Sol_Num_Cuo;
            ViewBag.Sol_Mnt_Cms = Solicitud.Sol_Mnt_Cms;
            ViewBag.Sol_Por_Cbt_Dis = Solicitud.Sol_Por_Cbt_Dis;
            ViewBag.Sol_Mnt_Cbt_Dis = Solicitud.Sol_Mnt_Cbt_Dis;
            ViewBag.Sol_Usu_Ejc = Solicitud.Sol_Usu_Ejc;
            ViewBag.Sol_Ofi_Cse_Des = Solicitud.Sol_Ofi_Cse_Des;
            ViewBag.Sol_Rut_Ejc = Solicitud.Sol_Rut_Ejc;


            ViewBag.estado = "<span class='badge_gsc_estados badge-success pull-right'>ESTADO :  " + Solicitud.Estado_Solicitud + "</span>";
            ViewBag.etapa = "<span class='badge_gsc_estados badge-alert pull-right'>ETAPA :  "+ Solicitud.Etapa_Solicitud +"</span>";


            List<Svc_TGSC_PRA_VerParametrosList_Result> Obtiene_SINO = new List<Svc_TGSC_PRA_VerParametrosList_Result>();
            Obtiene_SINO = new ObtieneParametros().Obtiene_Parametros_List("Cnd_Si_No", Constantes.numUno);
            ViewBag.Sol_Ina = new SelectList(Obtiene_SINO, "Pra_Val", "Pra_Des", Solicitud.Sol_Ina);
            ViewBag.Sol_Exp = new SelectList(Obtiene_SINO, "Pra_Val", "Pra_Des", Solicitud.Sol_Exp);


            List<Svc_TGSC_PRA_VerParametrosList_Result> Obtiene_Tip_Cms = new List<Svc_TGSC_PRA_VerParametrosList_Result>();
            Obtiene_Tip_Cms = new ObtieneParametros().Obtiene_Parametros_List("Tip_Cms", Constantes.numUno);
            ViewBag.Comision = new SelectList(Obtiene_Tip_Cms, "Pra_Val", "Pra_Des", Solicitud.Tip_Ope_Cms);



            List<Svc_TGSC_GNO_VerGenero_Result> Gno_Id = new List<Svc_TGSC_GNO_VerGenero_Result>();
            Gno_Id = new ObtieneParametros().Obtiene_Genero(Constantes.numUno);
            ViewBag.Gno_id = new SelectList(Gno_Id, "Gno_Id", "Gno_Des", Solicitud.Gno_Id);


            List<Svc_TGSC_SCE_VerSectorEconomico_Result> SectorEco = new List<Svc_TGSC_SCE_VerSectorEconomico_Result>();
            SectorEco = new ObtieneParametros().obtiene_Sec_Eco(Constantes.numUno);
            ViewBag.Sce_Id = new SelectList(SectorEco, "Sce_Id", "sec_des", Solicitud.Sce_Id);


            List<Svc_TGSC_CLA_RIE_VerClasificaRiesgo_Result> ClaRiesgo = new List<Svc_TGSC_CLA_RIE_VerClasificaRiesgo_Result>();
            ClaRiesgo = new ObtieneParametros().Obtiene_Cla_Rie(Constantes.numUno);
            ViewBag.Cla_Rie_Id = new SelectList(ClaRiesgo, "Cla_Rie_Id", "Cla_Rie_Tip", Solicitud.Cla_Rie_Id);


            List<Svc_TGSC_REL_CTF_COM_VerCatastrofe_Result> ListaCatastrofe = new List<Svc_TGSC_REL_CTF_COM_VerCatastrofe_Result>();
            ListaCatastrofe = new ObtieneParametros().obtiene_catastrofe();
            ViewBag.Catastrofe = new SelectList(ListaCatastrofe, "Ctf_Cod", "Ctf_Des", Solicitud.Ctf_Cod);


            List<Svc_TGSC_REL_CTF_COM_VerRegion_Result> ListaRegiones = new List<Svc_TGSC_REL_CTF_COM_VerRegion_Result>();
            ListaRegiones = new ObtieneParametros().Obtiene_Region(Solicitud.Ctf_Cod);
            ViewBag.Regiones = new SelectList(ListaRegiones, "Rgn_cod", "Rgn_Nom", Solicitud.Region_Id);


            var Reg_Id = Solicitud.Region_Id;
            var Catastrofe_Cod = Solicitud.Ctf_Cod;

            List<Svc_TGSC_REL_CTF_COM_VerComuna_Result> ListaComuna = new List<Svc_TGSC_REL_CTF_COM_VerComuna_Result>();
            ListaComuna = new ObtieneParametros().Obtiene_Comuna(Reg_Id, Catastrofe_Cod);
            ViewBag.Rel_Ctf_Com_Id = new SelectList(ListaComuna, "Rel_Ctf_Com_Id", "Com_Nom_02", Solicitud.Rel_Ctf_Com_Id);


            //ViewBag.Comision = Solicitud.Tip_Ope_Cms;


            string tipo = Solicitud.Tip_Ope_Cms;
            List<Svc_TGSC_TIP_OPE_VerOperacion_Result> ListaOperaciones = new List<Svc_TGSC_TIP_OPE_VerOperacion_Result>();
            ListaOperaciones = new ObtieneParametros().Obtiene_List_Ope(tipo, Constantes.numUno, Solicitud.Tip_Ope_Pgm_Cod);
            ViewBag.Tip_Ope_Id = new SelectList(ListaOperaciones, "Tip_Ope_Id", "Tip_Ope_Des", Solicitud.Tip_Ope_Id);


            List<Svc_TGSC_DES_PROY_VerDescripcion_Result> ListaProyecto = new List<Svc_TGSC_DES_PROY_VerDescripcion_Result>();

            List<Svc_TGSC_REL_DES_PROY_VerDescripcion_Result> List_Des_Pro = new List<Svc_TGSC_REL_DES_PROY_VerDescripcion_Result>();
            {
                List_Des_Pro = new ObtieneDesPyt().ObtieneDescProy(Solicitud.Sol_Id).ToList();


                //string Proy_Obj_Inv = "inversion";
                //string Proy_Est_Inv = "1";
                ListaProyecto = new ObtieneDesPyt().ObtieneListaProyecto(Constantes.objetivoInversion, Constantes.numUno);
                ViewBag.Des_Proy_Cod_01 = new SelectList(ListaProyecto, "Des_Proy_Cod", "Des_Proy_Des", 0);


                //string Proy_Obj_cp = "capital_de_trabajo";
                //string Proy_Est_cp = "1";
                ListaProyecto = new ObtieneDesPyt().ObtieneListaProyecto(Constantes.objetivoCapitalTrabajo, Constantes.numUno);
                ViewBag.Des_Proy_Cod_02 = new SelectList(ListaProyecto, "Des_Proy_Cod", "Des_Proy_Des", 0);

                //string Proy_Obj_Ref = "Refinanciamiento";
                //string Proy_Est_Ref = "1";
                ListaProyecto = new ObtieneDesPyt().ObtieneListaProyecto(Constantes.objetivoRefinanciamiento, Constantes.numUno);
                ViewBag.Des_Proy_Cod_03 = new SelectList(ListaProyecto, "Des_Proy_Cod", "Des_Proy_Des", 0);


                ViewBag.ShowInv = "none";
                ViewBag.ShowCp = "none";
                ViewBag.ShowRef = "none";

                foreach (var reg in List_Des_Pro)
                {
                    if (reg.Des_Proy_Obj == "inversion")
                    {
                        //Proy_Obj_Inv = "inversion";
                        //Proy_Est_Inv = "1";
                        ListaProyecto = new ObtieneDesPyt().ObtieneListaProyecto(Constantes.objetivoInversion, Constantes.numUno);
                        ViewBag.Des_Proy_Cod_01 = new SelectList(ListaProyecto, "Des_Proy_Cod", "Des_Proy_Des", reg.Des_Proy_Cod);
                        ViewBag.ShowInv = "show";

                    }

                    if (reg.Des_Proy_Obj == "capital_de_trabajo")
                    {
                        //Proy_Obj_cp = "capital_de_trabajo";
                        //Proy_Est_cp = "1";
                        ListaProyecto = new ObtieneDesPyt().ObtieneListaProyecto(Constantes.objetivoCapitalTrabajo, Constantes.numUno);
                        ViewBag.Des_Proy_Cod_02 = new SelectList(ListaProyecto, "Des_Proy_Cod", "Des_Proy_Des", reg.Des_Proy_Cod);
                        ViewBag.ShowCp = "show";
                    }

                    if (reg.Des_Proy_Obj == "Refinanciamiento")
                    {
                        //Proy_Obj_Ref = "Refinanciamiento";
                        //Proy_Est_Ref = "1";
                        ListaProyecto = new ObtieneDesPyt().ObtieneListaProyecto(Constantes.objetivoRefinanciamiento, Constantes.numUno);
                        ViewBag.Des_Proy_Cod_03 = new SelectList(ListaProyecto, "Des_Proy_Cod", "Des_Proy_Des", reg.Des_Proy_Cod);
                        ViewBag.ShowRef = "show";
                    }
                }
            }

            List<Svc_TGSC_TIP_MON_VerTipoMoneda_Result> ListMoneda = new List<Svc_TGSC_TIP_MON_VerTipoMoneda_Result>();
            {
                ListMoneda = new ObtieneParametros().Obtiene_Moneda(Constantes.numUno).ToList();
                ViewBag.Tip_Mon_Id = new SelectList(ListMoneda, "Tip_Mon_id", "Tip_Mon_Des", Solicitud.Tip_Mon_Id);
            }

            List<Svc_TGSC_TIP_GRC_VerGracia_Result> ListGracia = new List<Svc_TGSC_TIP_GRC_VerGracia_Result>();
            {
                ListGracia = new ObtieneParametros().Obtiene_Tip_Grc(Constantes.numUno).ToList();
                ViewBag.Tip_Grc_Id = new SelectList(ListGracia, "Tip_Grc_Id", "tip_Grc_Des", Solicitud.Tip_Grc_Id);
            }

            List<Svc_TGSC_FRE_PAG_VerFrecuencia_Result> List_Fre_Pag = new List<Svc_TGSC_FRE_PAG_VerFrecuencia_Result>();
            {
                List_Fre_Pag = new ObtieneParametros().Obtiene_Fre_Pag(Constantes.numUno).ToList();
                ViewBag.Fre_Pag_Id = new SelectList(List_Fre_Pag, "Fre_Pag_Id", "Fre_Pag_Des", Solicitud.Fre_Pag_Id);
            }

            List<Svc_TGSC_TIP_TAS_INT_VerTipoTasa_Result> List_Tip_Tas = new List<Svc_TGSC_TIP_TAS_INT_VerTipoTasa_Result>();
            {
                List_Tip_Tas = new ObtieneParametros().Obtiene_Tip_Tas(Constantes.numUno).ToList();
                ViewBag.Tip_Tas_Int_Cod = new SelectList(List_Tip_Tas, "Tip_Tas_Int_Cod", "Tip_Tas_Int_Des", Solicitud.Tip_Tas_Int_Cod);
            }


            List<Svc_TGSC_GAR_ADC_VerGarantia_Result> List_Gar_Adc_1 = new List<Svc_TGSC_GAR_ADC_VerGarantia_Result>();
            {
                List_Gar_Adc_1 = new ObtieneParametros().Obtiene_Gar_Adc(Constantes.numUno).ToList();
                ViewBag.Gar_Adc_Id_01 = new SelectList(List_Gar_Adc_1, "Gar_Adc_Id", "Gar_Adc_Des", 1);

                List_Gar_Adc_1 = new ObtieneParametros().Obtiene_Gar_Adc(Constantes.numUno).ToList();
                ViewBag.Gar_Adc_Id_02 = new SelectList(List_Gar_Adc_1, "Gar_Adc_Id", "Gar_Adc_Des", 1);
            }


            List<Svc_TGSC_REL_GAR_ADC_VerGarantia_Result> Obtiene_Gar = new List<Svc_TGSC_REL_GAR_ADC_VerGarantia_Result>();
            {
                var contador = Constantes.digitoUno;
                Obtiene_Gar = new ObtieneParametros().Obtiene_Des_Gar(Solicitud.Sol_Id).ToList();


                foreach (var Garantia in Obtiene_Gar)
                {
                    if (contador == Constantes.digitoUno)
                    {

                        List<Svc_TGSC_GAR_ADC_VerGarantia_Result> List_Gar_Adc = new List<Svc_TGSC_GAR_ADC_VerGarantia_Result>();
                        {
                            List_Gar_Adc = new ObtieneParametros().Obtiene_Gar_Adc(Constantes.numUno).ToList();
                            ViewBag.Gar_Adc_Id_01 = new SelectList(List_Gar_Adc, "Gar_Adc_Id", "Gar_Adc_Des", Garantia.Gar_Adc_Id);
                        }
                    }
                    else
                    {
                        List<Svc_TGSC_GAR_ADC_VerGarantia_Result> List_Gar_Adc = new List<Svc_TGSC_GAR_ADC_VerGarantia_Result>();
                        List_Gar_Adc = new ObtieneParametros().Obtiene_Gar_Adc(Constantes.numUno).ToList();
                        ViewBag.Gar_Adc_Id_02 = new SelectList(List_Gar_Adc, "Gar_Adc_Id", "Gar_Adc_Des", Garantia.Gar_Adc_Id);
                    }
                    contador = contador + Constantes.digitoUno;
                }
            }



            List<Svc_TGSC_REL_SEG_REF_VerSeguro_Result> Obtiene_Seg = new List<Svc_TGSC_REL_SEG_REF_VerSeguro_Result>();
            {
                Obtiene_Seg = new ObtieneParametros().Obtiene_Des_Seg(Solicitud.Sol_Id).ToList();

                foreach (var seguro in Obtiene_Seg)
                {
                    List<Svc_TGSC_SEG_REF_VerSeguro_Result> List_Seg = new List<Svc_TGSC_SEG_REF_VerSeguro_Result>();
                    {
                        List_Seg = new ObtieneParametros().Obtiene_Seg(Constantes.numUno).ToList();
                        ViewBag.Seg_Ref_Id_01 = new SelectList(List_Seg, "Seg_Ref_Id", "Seg_ref_Des", seguro.Seg_Ref_Id);
                    }
                }
            }


            return View(Solicitud);
        }

        // POST: Solicitudes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sol_Num_Sol, Sol_Num_Ope, Sol_Fec_Sol, Sol_Num_Fol, Sol_Nrt_Emp, Sol_Drt_Emp, Sol_Rzn_Scl, Sce_Id, Sol_Num_Sec, Sol_Ina, Sol_Tel, Rel_Ctf_Com_Id, Sol_Nom_Loc, Sol_Nom_Call, Sol_Num_Call, Sol_Cmn_Dir, Sol_Cro_Elt, Gno_Id, Sol_Exp, Cla_Rie_Id, Sol_Vnt_Emp, Sol_Mnt_Cbt_Utz, Sol_Mnt_Cms, Sol_Por_Cbt_Dis, Sol_Mnt_Cbt_Dis, Sol_Usu_Ejc, Sol_Rut_Ejc, Sol_Ofi_Cse_Des, Tip_Ope_Id, Tip_Mon_Id, Sol_Mnt_Crd, Sol_Mnt_Cpl_Tbj, Sol_Mnt_Inv, Sol_Mnt_Cp_Rfn, Sol_Fec_Cse, Sol_Fec_Vct, Sol_Val_Dol, Sol_Val_UF, Tip_Grc_Id, Sol_Mes_Grc, Sol_Mes_No_Pag, Fre_Pag_Id, Tip_Tas_Int_Cod, Sol_Int_Anl, Sol_Num_Cuo, Est_Sol_Id, Etp_Sol_Id, Sol_Pzo, Gar_Adc_Id_01, Gar_Adc_Id_02, Seg_Ref_Id_01, Seg_Ref_Id_02, Seg_Ref_Id_03, Des_Proy_Cod_01, Des_Proy_Cod_02, Des_Proy_Cod_03")] GuardarSolicitud _GuardarSolicitud)
        {

            var usr_lgn = utlidades.NomUser(@User.Identity.Name);
            Svc_TGSC_USR_SIS_VerUsuarioSistema_Result usuario = new Svc_TGSC_USR_SIS_VerUsuarioSistema_Result();
            usuario = new ObtieneUsuario().LeeUsuario(usr_lgn);

            ViewBag.nombre = usuario.nombre;
            ViewBag.unidad = usuario.nombreUnidad;
            ViewBag.nom_oficina = usuario.Nombre_oficina;
            ViewBag.LoginEjec = usr_lgn;
            ViewBag.perfil = usuario.Pfl_Id;
            ViewBag.RutEjecutivo = usuario.Rut;

            var num_sol = Convert.ToInt32(_GuardarSolicitud.Sol_Num_Sol);
            var num_ope = Convert.ToString(_GuardarSolicitud.Sol_Num_Sol);



            var _Sol_Num_Sol = num_sol; //_GuardarSolicitud.Sol_Num_Sol;
            var _Sol_Num_Ope = num_ope; //_GuardarSolicitud.Sol_Num_Ope;
            var _Sol_Fec_Sol = DateTime.Now; //_GuardarSolicitud.Sol_Fec_Sol;
            var _Sol_Num_Fol = _GuardarSolicitud.Sol_Num_Fol;
            var _Sol_Nrt_Emp = _GuardarSolicitud.Sol_Nrt_Emp;
            var _Sol_Drt_Emp = _GuardarSolicitud.Sol_Drt_Emp;
            var _Sol_Rzn_Scl = _GuardarSolicitud.Sol_Rzn_Scl;
            var _Sce_Id = _GuardarSolicitud.Sce_Id;
            var _Sol_Num_Sec = _GuardarSolicitud.Sol_Num_Sec;
            var _Sol_Ina = _GuardarSolicitud.Sol_Ina;
            var _Sol_Tel = _GuardarSolicitud.Sol_Tel;
            var _Rel_Ctf_Com_Id = _GuardarSolicitud.Rel_Ctf_Com_Id;
            var _Sol_Nom_Loc = _GuardarSolicitud.Sol_Nom_Loc;
            var _Sol_Nom_Call = _GuardarSolicitud.Sol_Nom_Call;
            var _Sol_Num_Call = _GuardarSolicitud.Sol_Num_Call;
            var _Sol_Cmn_Dir = _GuardarSolicitud.Sol_Cmn_Dir;
            var _Sol_Cro_Elt = _GuardarSolicitud.Sol_Cro_Elt;
            var _Gno_Id = _GuardarSolicitud.Gno_Id;
            var _Sol_Exp = _GuardarSolicitud.Sol_Exp;
            var _Cla_Rie_Id = _GuardarSolicitud.Cla_Rie_Id;
            var _Sol_Vnt_Emp = _GuardarSolicitud.Sol_Vnt_Emp;
            var _Sol_Mnt_Cbt_Utz = _GuardarSolicitud.Sol_Mnt_Cbt_Utz;
            var _Sol_Mnt_Cms = _GuardarSolicitud.Sol_Mnt_Cms;
            var _Sol_Por_Cbt_Dis = _GuardarSolicitud.Sol_Por_Cbt_Dis;
            var _Sol_Mnt_Cbt_Dis = _GuardarSolicitud.Sol_Mnt_Cbt_Dis;
            var _Sol_Usu_Ejc = _GuardarSolicitud.Sol_Usu_Ejc;
            var _Sol_Rut_Ejc = _GuardarSolicitud.Sol_Rut_Ejc;
            var _Sol_Ofi_Cse_Des = _GuardarSolicitud.Sol_Ofi_Cse_Des;
            var _Tip_Ope_Id = _GuardarSolicitud.Tip_Ope_Id;
            var _Tip_Mon_Id = _GuardarSolicitud.Tip_Mon_Id;
            var _Sol_Mnt_Crd = _GuardarSolicitud.Sol_Mnt_Crd;
            var _Sol_Mnt_Cpl_Tbj = _GuardarSolicitud.Sol_Mnt_Cpl_Tbj;
            var _Sol_Mnt_Inv = _GuardarSolicitud.Sol_Mnt_Inv;
            var _Sol_Mnt_Rfn = _GuardarSolicitud.Sol_Mnt_Cp_Rfn;
            var _Sol_Fec_Cse = _GuardarSolicitud.Sol_Fec_Cse;
            var _Sol_Fec_Vct = _GuardarSolicitud.Sol_Fec_Vct;
            var _Sol_Val_Dol = _GuardarSolicitud.Sol_Val_Dol;
            var _Sol_Val_UF = _GuardarSolicitud.Sol_Val_UF;
            var _Tip_Grc_Id = _GuardarSolicitud.Tip_Grc_Id;
            var _Sol_Mes_Grc = _GuardarSolicitud.Sol_Mes_Grc;
            var _Sol_Mes_No_Pag = _GuardarSolicitud.Sol_Mes_No_Pag;
            var _Fre_Pag_Id = _GuardarSolicitud.Fre_Pag_Id;
            var _Tip_Tas_Int_Cod = _GuardarSolicitud.Tip_Tas_Int_Cod;
            var _Sol_Int_Anl = _GuardarSolicitud.Sol_Int_Anl;
            var _Sol_Num_Cuo = _GuardarSolicitud.Sol_Num_Cuo;
            var _Est_Sol_Id = _GuardarSolicitud.Est_Sol_Id;
            var _Etp_Sol_Id = _GuardarSolicitud.Etp_Sol_Id;
            var _Sol_Pzo = _GuardarSolicitud.Sol_Pzo;
            var _Gar_Adc_Id_01 = _GuardarSolicitud.Gar_Adc_Id_01;
            var _Gar_Adc_Id_02 = _GuardarSolicitud.Gar_Adc_Id_02;
            var _Seg_Ref_Id_01 = _GuardarSolicitud.Seg_Ref_Id_01;
            var _Seg_Ref_Id_02 = _GuardarSolicitud.Seg_Ref_Id_02;
            var _Seg_Ref_Id_03 = _GuardarSolicitud.Seg_Ref_Id_03;
            var _Des_Proy_Cod_01 = _GuardarSolicitud.Des_Proy_Cod_01;
            var _Des_Proy_Cod_02 = _GuardarSolicitud.Des_Proy_Cod_02;
            var _Des_Proy_Cod_03 = _GuardarSolicitud.Des_Proy_Cod_03;



            if (ModelState.IsValid)
            {

                Sva_TGSC_SOL_InsertaSolicitud_Result Guarda_Solicitud = new Sva_TGSC_SOL_InsertaSolicitud_Result();
                Guarda_Solicitud = new RegistraSolicitud().Registra_Solicitud(_Sol_Num_Sol, _Sol_Num_Ope, _Sol_Fec_Sol, _Sol_Num_Fol, _Sol_Nrt_Emp, _Sol_Drt_Emp, _Sol_Rzn_Scl, _Sce_Id, _Sol_Num_Sec, _Sol_Ina, _Sol_Tel, _Rel_Ctf_Com_Id, _Sol_Nom_Loc, _Sol_Nom_Call, _Sol_Num_Call, _Sol_Cmn_Dir, _Sol_Cro_Elt, _Gno_Id, _Sol_Exp, _Cla_Rie_Id, _Sol_Vnt_Emp, _Sol_Mnt_Cbt_Utz, _Sol_Mnt_Cms, _Sol_Por_Cbt_Dis, _Sol_Mnt_Cbt_Dis, _Sol_Usu_Ejc, _Sol_Rut_Ejc, _Sol_Ofi_Cse_Des, _Tip_Ope_Id, _Tip_Mon_Id, _Sol_Mnt_Crd, _Sol_Mnt_Cpl_Tbj, _Sol_Mnt_Inv, _Sol_Mnt_Rfn, _Sol_Fec_Cse, _Sol_Fec_Vct, _Sol_Val_Dol, _Sol_Val_UF, _Tip_Grc_Id, _Sol_Mes_Grc, _Sol_Mes_No_Pag, _Fre_Pag_Id, _Tip_Tas_Int_Cod, _Sol_Int_Anl, _Sol_Num_Cuo, _Est_Sol_Id, _Etp_Sol_Id, _Sol_Pzo, _Gar_Adc_Id_01, _Gar_Adc_Id_02, _Seg_Ref_Id_01, _Seg_Ref_Id_02, _Seg_Ref_Id_03, _Des_Proy_Cod_01, _Des_Proy_Cod_02, _Des_Proy_Cod_03);

                Sva_TGSC_SOL_DOC_Documentos_Result Datos_Documentos = new Sva_TGSC_SOL_DOC_Documentos_Result();
                Datos_Documentos = new GeneraDocumentos().Inserta_Reg_Doc(_Sol_Num_Sol);

                return View("EditResp");
            }


            return View(_GuardarSolicitud);
        }



        public ActionResult EditResp()
        {


          return PartialView();
        }



        public ActionResult CambiaEstado(int id)
        {
            Svc_TGSC_SOL_VerSolicitudId_Result Solicitud = new Svc_TGSC_SOL_VerSolicitudId_Result();
            Solicitud = new ObtieneSolicitud().Obtiene_Solicitud(id);


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

            if (Solicitud.Etp_Sol_Id >= 3 || (int)Session["perfil"] == 2 || Solicitud.Est_Sol_Id == 2)
            {
                return RedirectToAction("Index", "Home");
            }


            //string estados_opc = "1";

            ViewBag.Sol_Nrt_Emp = Solicitud.Rut_Cliente;
            ViewBag.Sol_Rzn_Scl = Solicitud.Razon_Social;
            ViewBag.Sol_Nom_Call = Solicitud.Sol_Nom_Call;
            ViewBag.Sol_Num_Call = Solicitud.Sol_Num_Call;
            ViewBag.Sol_Cmn_Dir = Solicitud.Sol_Cmn_Dir;
            ViewBag.Sol_Tel = Solicitud.Telefono;
            ViewBag.Sol_Cro_Elt = Solicitud.Sol_Cro_Elt;
            ViewBag.Gno_id = Solicitud.Genero;
            ViewBag.Sol_Exp = Solicitud.Sol_Ina;
            ViewBag.Sol_Ina = Solicitud.Sol_Exp;
            ViewBag.Sol_Sce = Solicitud.Sector_Economico;
            ViewBag.Cla_Rie_Id = Solicitud.Clasificacion_Riesgo;
            ViewBag.Sol_Vnt_Emp = Solicitud.Sol_Vnt_Emp;
            ViewBag.Sol_Mnt_Cbt_Utz = Solicitud.Sol_Mnt_Cbt_Utz;
            ViewBag.Catastrofe = Solicitud.Tipo_Catastrofe;
            ViewBag.Region = Solicitud.Nombre_Region;
            ViewBag.Localizacion = Solicitud.Localizacion;
            ViewBag.Tip_Ope = Solicitud.Tipo_Operacion;
            ViewBag.mnt_inv = Solicitud.Sol_Mnt_Inv;
            ViewBag.mnt_cp_tbj = Solicitud.Sol_Mnt_Cpl_Tbj;
            ViewBag.mnt_cp_rfn = Solicitud.Sol_Mnt_Rfn;
            ViewBag.Tip_Mon_Id = Solicitud.Tipo_Moneda;
            ViewBag.Sol_Mnt_Crd = Solicitud.Monto_Credito;
            ViewBag.Sol_Fec_Cse = Solicitud.Sol_Fec_Cse;
            ViewBag.Sol_Fec_Vct = Solicitud.Sol_Fec_Vct;
            ViewBag.Sol_Val_Dol = Solicitud.Sol_Val_Dol;
            ViewBag.Sol_Val_UF = Solicitud.Sol_Val_UF;
            ViewBag.Tip_Grc_Id = Solicitud.Tipo_Gracia;
            ViewBag.Sol_Mes_Grc = Solicitud.Sol_Mes_Grc;
            ViewBag.Fre_Pag_Id = Solicitud.Frecuencia_Pago;
            ViewBag.Sol_Int_Anl = Solicitud.Sol_Int_Anl;
            ViewBag.Tip_Tas_Int_Cod = Solicitud.Tip_Tas_Int_Des;
            ViewBag.Sol_Num_Cuo = Solicitud.Sol_Num_Cuo;



            List<Svc_TGSC_PRA_VerParametrosList_Result> Obtiene_SINO = new List<Svc_TGSC_PRA_VerParametrosList_Result>();
            Obtiene_SINO = new ObtieneParametros().Obtiene_Parametros_List("Cnd_Si_No", Constantes.numUno);
            ViewBag.Sol_Ina = new SelectList(Obtiene_SINO, "Pra_Val", "Pra_Des", Solicitud.Sol_Ina);
            ViewBag.Sol_Exp = new SelectList(Obtiene_SINO, "Pra_Val", "Pra_Des", Solicitud.Sol_Exp);

            ViewBag.estado = "<span class='badge_gsc_estados badge-success pull-right'>ESTADO :  " + Solicitud.Estado_Solicitud + "</span>";
            ViewBag.etapa = "<span class='badge_gsc_estados badge-alert pull-right'>ETAPA :  "+ Solicitud.Etapa_Solicitud +"</span>";

            List<Svc_TGSC_REL_DES_PROY_VerDescripcion_Result> List_Des_Pro = new List<Svc_TGSC_REL_DES_PROY_VerDescripcion_Result>();
            {
                List_Des_Pro = new ObtieneDesPyt().ObtieneDescProy(Solicitud.Sol_Id).ToList();
                foreach (var reg in List_Des_Pro)
                {
                    if (reg.Des_Proy_Obj == Constantes.objetivoInversion)
                    {
                        ViewBag.Des_Proy_Cod_01_E = reg.Des_Proy_Des;
                    }
                    if (reg.Des_Proy_Obj == Constantes.objetivoCapitalTrabajo)
                    {
                        ViewBag.Des_Proy_Cod_02_E = reg.Des_Proy_Des;
                    }
                    if (reg.Des_Proy_Obj == Constantes.objetivoRefinanciamiento)
                    {
                        ViewBag.Des_Proy_Cod_03_E = reg.Des_Proy_Des;
                    }
                }
            }

            List<Svc_TGSC_REL_GAR_ADC_VerGarantia_Result> List_Des_Gar = new List<Svc_TGSC_REL_GAR_ADC_VerGarantia_Result>();
            {
                List_Des_Gar = new ObtieneParametros().Obtiene_Des_Gar(Solicitud.Sol_Id).ToList();
                var contador = Constantes.digitoUno;
                foreach (var gar in List_Des_Gar)
                {
                    if (contador == 1)
                    {
                        ViewBag.Gar_Adc_Id_01 = gar.Gar_Adc_Des;
                    }
                    else
                    {
                        ViewBag.Gar_Adc_Id_02 = gar.Gar_Adc_Des;
                    }

                    contador = contador + Constantes.digitoUno;
                }

            }

            List<Svc_TGSC_REL_SEG_REF_VerSeguro_Result> List_Des_Seg = new List<Svc_TGSC_REL_SEG_REF_VerSeguro_Result>();
            {
                List_Des_Seg = new ObtieneParametros().Obtiene_Des_Seg(Solicitud.Sol_Id).ToList();
                foreach (var seg in List_Des_Seg)
                {
                    {
                        ViewBag.Seg_Ref_Id_01 = seg.Seg_Ref_Des;
                    }
                }
            }

            List<Svc_TGSC_EST_SOL_VerEstado_Result> List_Estado = new List<Svc_TGSC_EST_SOL_VerEstado_Result>();
            {
                List_Estado = new ObtieneParametros().Obtiene_Estado(Constantes.numUno).ToList();
                ViewBag.Est_Sol_Id = new SelectList(List_Estado, "Est_Sol_Id", "Est_Sol_Des", Solicitud.Est_Sol_Id);
            }


            List<Svc_TGSC_ETP_SOL_VerEtapa_Result> List_Etapa = new List<Svc_TGSC_ETP_SOL_VerEtapa_Result>();
            {
                List_Etapa = new ObtieneParametros().Obtiene_Etapa(Constantes.numUno).ToList();
                ViewBag.Etp_Sol_Id = new SelectList(List_Etapa, "Etp_Sol_Id", "Etp_Sol_Des", Solicitud.Etp_Sol_Id);
            }


            ViewBag.Sol_Mnt_Cms = Solicitud.Sol_Mnt_Cms;
            ViewBag.Sol_Por_Cbt_Dis = Solicitud.Sol_Por_Cbt_Dis;
            ViewBag.Sol_Mnt_Cbt_Dis = Solicitud.Sol_Mnt_Cbt_Dis;
            ViewBag.Sol_Usu_Ejc = Solicitud.Sol_Usu_Ejc;
            ViewBag.Sol_Ofi_Cse_Des = Solicitud.Sol_Ofi_Cse_Des;
            ViewBag.Sol_Rut_Ejc = Solicitud.Sol_Rut_Ejc;


            return View(Solicitud);
        }





        // POST: Solicitudes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //
        public ActionResult CambiaEstado([Bind(Include = "Sol_Num_Sol, Est_Sol_Id, Etp_Sol_Id")] CambiaEstadoSol _CambiaEstadoSolicitud)
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

            var num_sol = Convert.ToInt32(_CambiaEstadoSolicitud.Sol_Num_Sol);
            var num_ope = Convert.ToString(_CambiaEstadoSolicitud.Sol_Num_Sol);

            var _Sol_Num_Sol = num_sol; //_GuardarSolicitud.Sol_Num_Sol;
            //var _Sol_Num_Ope = num_ope; //_GuardarSolicitud.Sol_Num_Ope;
            var _Est_Sol_Id = _CambiaEstadoSolicitud.Est_Sol_Id;
            var _Etp_Sol_Id = _CambiaEstadoSolicitud.Etp_Sol_Id;
                        

            if (ModelState.IsValid)
            {
                Sva_TGSC_SOL_ActualizaEstado_Result Cambia_Estado = new Sva_TGSC_SOL_ActualizaEstado_Result();
                Cambia_Estado = new CambiaEstado().Cambia_Estado(_Est_Sol_Id, _Etp_Sol_Id,  _Sol_Num_Sol);

                return View("EstadoResp");
            }

            return View(_CambiaEstadoSolicitud);
        }



        public ActionResult EstadoResp()
        {


            return PartialView();
        }




        public ActionResult Busqueda(int id)
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


            Svc_TGSC_SOL_VerSolicitudId_Result Solicitud = new Svc_TGSC_SOL_VerSolicitudId_Result();
            Solicitud = new ObtieneSolicitud().Obtiene_Solicitud(id);

            ViewBag.Sol_Num_Sol = Solicitud.Sol_Num_Sol;
            ViewBag.Sol_Nrt_Emp = Solicitud.Rut_Cliente;
            ViewBag.Sol_Rzn_Scl = Solicitud.Razon_Social;
            ViewBag.Sol_Nom_Call = Solicitud.Sol_Nom_Call;
            ViewBag.Sol_Num_Call = Solicitud.Sol_Num_Call;
            ViewBag.Sol_Cmn_Dir = Solicitud.Sol_Cmn_Dir;
            ViewBag.Sol_Tel = Solicitud.Telefono;
            ViewBag.Sol_Cro_Elt = Solicitud.Sol_Cro_Elt;
            ViewBag.Gno_id = Solicitud.Genero;
            ViewBag.Sol_Sce = Solicitud.Sector_Economico;
            ViewBag.Cla_Rie_Id = Solicitud.Clasificacion_Riesgo;
            ViewBag.Sol_Vnt_Emp = Solicitud.Sol_Vnt_Emp;
            ViewBag.Sol_Mnt_Cbt_Utz = Solicitud.Sol_Mnt_Cbt_Utz;
            ViewBag.Catastrofe = Solicitud.Tipo_Catastrofe;
            ViewBag.Region = Solicitud.Nombre_Region;
            ViewBag.Localizacion = Solicitud.Localizacion;
            ViewBag.Tip_Ope = Solicitud.Tipo_Operacion;
            ViewBag.mnt_inv = Solicitud.Sol_Mnt_Inv;
            ViewBag.mnt_cp_tbj = Solicitud.Sol_Mnt_Cpl_Tbj;
            ViewBag.mnt_cp_rfn = Solicitud.Sol_Mnt_Rfn;
            ViewBag.Tip_Mon_Id = Solicitud.Tipo_Moneda;
            ViewBag.Sol_Mnt_Crd = Solicitud.Monto_Credito;
            ViewBag.Sol_Fec_Cse = Solicitud.Sol_Fec_Cse;
            ViewBag.Sol_Fec_Vct = Solicitud.Sol_Fec_Vct;
            ViewBag.Sol_Val_Dol = Solicitud.Sol_Val_Dol;
            ViewBag.Sol_Val_UF = Solicitud.Sol_Val_UF;
            ViewBag.Tip_Grc_Id = Solicitud.Tipo_Gracia;
            ViewBag.Sol_Mes_Grc = Solicitud.Sol_Mes_Grc;
            ViewBag.Fre_Pag_Id = Solicitud.Frecuencia_Pago;
            ViewBag.Sol_Int_Anl = Solicitud.Sol_Int_Anl;
            ViewBag.Tip_Tas_Int_Cod = Solicitud.Tip_Tas_Int_Des;
            ViewBag.Sol_Num_Cuo = Solicitud.Sol_Num_Cuo;
            ViewBag.Sol_Estado = Solicitud.Estado_Solicitud;


            ViewBag.estado = "<span class='badge_gsc_estados badge-success pull-right'>ESTADO :  " + Solicitud.Estado_Solicitud + "</span>";
            ViewBag.etapa = "<span class='badge_gsc_estados badge-alert pull-right'>ETAPA :  " + Solicitud.Etapa_Solicitud + "</span>";


            List<Svc_TGSC_PRA_VerParametrosList_Result> Obtiene_Tip_Cms = new List<Svc_TGSC_PRA_VerParametrosList_Result>();
            Obtiene_Tip_Cms = new ObtieneParametros().Obtiene_Parametros_List("Tip_Cms", "1");
            ViewBag.Comision = new SelectList(Obtiene_Tip_Cms, "Pra_Val", "Pra_Des", Solicitud.Tip_Ope_Cms);



            List<Svc_TGSC_PRA_VerParametrosList_Result> Obtiene_SINO = new List<Svc_TGSC_PRA_VerParametrosList_Result>();
            Obtiene_SINO = new ObtieneParametros().Obtiene_Parametros_List("Cnd_Si_No", "1");
            ViewBag.Sol_Ina = new SelectList(Obtiene_SINO, "Pra_Val", "Pra_Des", Solicitud.Sol_Ina);
            ViewBag.Sol_Exp = new SelectList(Obtiene_SINO, "Pra_Val", "Pra_Des", Solicitud.Sol_Exp);



            List<Svc_TGSC_REL_DES_PROY_VerDescripcion_Result> List_Des_Pro = new List<Svc_TGSC_REL_DES_PROY_VerDescripcion_Result>();
            {
                List_Des_Pro = new ObtieneDesPyt().ObtieneDescProy(Solicitud.Sol_Id).ToList();
                foreach (var reg in List_Des_Pro)
                {
                    if (reg.Des_Proy_Obj == Constantes.objetivoInversion)
                    {
                        ViewBag.Des_Proy_Cod_01 = reg.Des_Proy_Des;
                    }
                    if (reg.Des_Proy_Obj == Constantes.objetivoCapitalTrabajo)
                    {
                        ViewBag.Des_Proy_Cod_02 = reg.Des_Proy_Des;
                    }
                    if (reg.Des_Proy_Obj == Constantes.objetivoRefinanciamiento)
                    {
                        ViewBag.Des_Proy_Cod_03 = reg.Des_Proy_Des;
                    }
                }
            }

            List<Svc_TGSC_REL_GAR_ADC_VerGarantia_Result> List_Des_Gar = new List<Svc_TGSC_REL_GAR_ADC_VerGarantia_Result>();
            {
                List_Des_Gar = new ObtieneParametros().Obtiene_Des_Gar(Solicitud.Sol_Id).ToList();
                var contador = Constantes.digitoUno;

                foreach (var gar in List_Des_Gar)
                {
                    if (contador == 1)
                    {
                        ViewBag.Gar_Adc_Id_01 = gar.Gar_Adc_Des;
                    }
                    else
                    {
                        ViewBag.Gar_Adc_Id_02 = gar.Gar_Adc_Des;
                    }

                    contador = contador + Constantes.digitoUno;
                }

            }

            List<Svc_TGSC_REL_SEG_REF_VerSeguro_Result> List_Des_Seg = new List<Svc_TGSC_REL_SEG_REF_VerSeguro_Result>();
            {
                List_Des_Seg = new ObtieneParametros().Obtiene_Des_Seg(Solicitud.Sol_Id).ToList();
                foreach (var seg in List_Des_Seg)
                {
                    {
                        ViewBag.Seg_Ref_Id_01 = seg.Seg_Ref_Des;
                    }
                }
            }

           
            ViewBag.Sol_Mnt_Cms = Solicitud.Sol_Mnt_Cms;
            ViewBag.Sol_Por_Cbt_Dis = Solicitud.Sol_Por_Cbt_Dis;
            ViewBag.Sol_Mnt_Cbt_Dis = Solicitud.Sol_Mnt_Cbt_Dis;
            ViewBag.Sol_Usu_Ejc = Solicitud.Sol_Usu_Ejc;
            ViewBag.Sol_Ofi_Cse_Des = Solicitud.Sol_Ofi_Cse_Des;
            ViewBag.Sol_Rut_Ejc = Solicitud.Sol_Rut_Ejc;


            return View(Solicitud);
        }


        public ActionResult AbreSolicitud(int id)
        {

            Svc_TGSC_SOL_VerSolicitudId_Result Solicitud = new Svc_TGSC_SOL_VerSolicitudId_Result();
            Solicitud = new ObtieneSolicitud().Obtiene_Solicitud(id);


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

            if (Solicitud.Etp_Sol_Id > Constantes.digitoDos)
            {
                return RedirectToAction("Index", "Home");
            }



            Svc_TGSC_PRA_VerParametros_Result Por_Fjo_Cms = new Svc_TGSC_PRA_VerParametros_Result();
            Por_Fjo_Cms = new ObtieneParametros().Obtiene_Parametros("Por_Fjo_Cms", Constantes.numUno);

            Svc_TGSC_PRA_VerParametros_Result Por_Fjo_Cms_Ctf = new Svc_TGSC_PRA_VerParametros_Result();
            Por_Fjo_Cms_Ctf = new ObtieneParametros().Obtiene_Parametros("Por_Fjo_Cms_Ctf", Constantes.numUno);

            Svc_TGSC_PRA_VerParametros_Result Fec_Cse_Max_Dia = new Svc_TGSC_PRA_VerParametros_Result();
            Fec_Cse_Max_Dia = new ObtieneParametros().Obtiene_Parametros("Fec_Cse_Max_Dia", Constantes.numUno);

            Svc_TGSC_PRA_VerParametros_Result Fec_Cse_Min_Dia = new Svc_TGSC_PRA_VerParametros_Result();
            Fec_Cse_Min_Dia = new ObtieneParametros().Obtiene_Parametros("Fec_Cse_Min_Dia", Constantes.numUno);


            var fecha_minima = Convert.ToInt32(Fec_Cse_Min_Dia.Pra_Val);


            ViewBag.Por_Fjo_Cms = Por_Fjo_Cms.Pra_Val;
            ViewBag.Por_Fjo_Cms_Ctf = Por_Fjo_Cms_Ctf.Pra_Val;

            ViewBag.Fec_Cse_Max_Dia = Fec_Cse_Max_Dia.Pra_Val;
            ViewBag.Fec_Cse_Min_Dia = (fecha_minima * -1);

            //string estados_opc = "1";
                        

            ViewBag.Sol_Num_Sol = Solicitud.Sol_Num_Sol;
            ViewBag.rut = Solicitud.Sol_Nrt_Emp;
            ViewBag.rut_dv = Solicitud.Sol_Drt_Emp;
            ViewBag.Sol_Nrt_Emp = Solicitud.Sol_Nrt_Emp;
            ViewBag.Sol_Drt_Emp = Solicitud.Sol_Drt_Emp;
            ViewBag.Sol_Rzn_Scl = Solicitud.Razon_Social;
            ViewBag.Sol_Nom_Call = Solicitud.Sol_Nom_Call;
            ViewBag.Sol_Num_Call = Solicitud.Sol_Num_Call;
            ViewBag.Sol_Cmn_Dir = Solicitud.Sol_Cmn_Dir;
            ViewBag.Sol_Tel = Solicitud.Telefono;
            ViewBag.Sol_Cro_Elt = Solicitud.Sol_Cro_Elt;
            ViewBag.Gno_id = Solicitud.Genero;
            ViewBag.Sol_Exp = Solicitud.Sol_Ina;
            ViewBag.Sol_Ina = Solicitud.Sol_Exp;
            ViewBag.Sol_Sce = Solicitud.Sector_Economico;
            ViewBag.Cla_Rie_Id = Solicitud.Clasificacion_Riesgo;
            ViewBag.Sol_Vnt_Emp = Solicitud.Sol_Vnt_Emp;
            ViewBag.Sol_Mnt_Cbt_Utz = Solicitud.Sol_Mnt_Cbt_Utz;
            ViewBag.Catastrofe = Solicitud.Tipo_Catastrofe;
            ViewBag.Region = Solicitud.Nombre_Region;
            ViewBag.Localizacion = Solicitud.Localizacion;
            ViewBag.Tip_Ope = Solicitud.Tipo_Operacion;
            ViewBag.Sol_Mnt_Inv = Solicitud.Sol_Mnt_Inv;
            ViewBag.Sol_Mnt_Cpl_Tbj = Solicitud.Sol_Mnt_Cpl_Tbj;
            ViewBag.Sol_Mnt_Cp_Rfn = Solicitud.Sol_Mnt_Rfn;
            ViewBag.Tip_Mon_Id = Solicitud.Tipo_Moneda;
            ViewBag.Sol_Mnt_Crd = Solicitud.Monto_Credito;
            ViewBag.Fecha_Curse = Solicitud.Sol_Fec_Cse;
            ViewBag.Fecha_Venc = Solicitud.Sol_Fec_Vct;
            ViewBag.Sol_Fec_Vct = Solicitud.Sol_Fec_Vct;
            ViewBag.Sol_Fec_Cse = Solicitud.Sol_Fec_Cse;
            ViewBag.Est_Sol_Id = Solicitud.Est_Sol_Id;
            ViewBag.Etp_Sol_Id = Solicitud.Etp_Sol_Id;
            ViewBag.Sol_Val_Dol = Solicitud.Sol_Val_Dol;
            ViewBag.Sol_Val_UF = Solicitud.Sol_Val_UF;
            ViewBag.Tip_Grc_Id = Solicitud.Tipo_Gracia;
            ViewBag.Sol_Mes_Grc = Solicitud.Sol_Mes_Grc;
            ViewBag.Fre_Pag_Id = Solicitud.Frecuencia_Pago;
            ViewBag.Sol_Int_Anl = Solicitud.Sol_Int_Anl;
            ViewBag.Tip_Tas_Int_Cod = Solicitud.Tip_Tas_Int_Des;
            ViewBag.Sol_Num_Cuo = Solicitud.Sol_Num_Cuo;
            ViewBag.Sol_Mnt_Cms = Solicitud.Sol_Mnt_Cms;
            ViewBag.Sol_Por_Cbt_Dis = Solicitud.Sol_Por_Cbt_Dis;
            ViewBag.Sol_Mnt_Cbt_Dis = Solicitud.Sol_Mnt_Cbt_Dis;
            ViewBag.Sol_Usu_Ejc = Solicitud.Sol_Usu_Ejc;
            ViewBag.Sol_Ofi_Cse_Des = Solicitud.Sol_Ofi_Cse_Des;
            ViewBag.Sol_Rut_Ejc = Solicitud.Sol_Rut_Ejc;


            ViewBag.estado = "<span class='badge_gsc_estados badge-success pull-right'>ESTADO :  " + Solicitud.Estado_Solicitud + "</span>";
            ViewBag.etapa = "<span class='badge_gsc_estados badge-alert pull-right'>ETAPA :  " + Solicitud.Etapa_Solicitud + "</span>";


            var Solicitud_Ina = 0;

            if (Solicitud.Sol_Ina == Constantes.opcionSi)
            {
                Solicitud_Ina = Constantes.digitoUno;
            }
            else
            {
                Solicitud_Ina = Constantes.digitoDos;
            }

            var Solicitud_Exp = 0;

            if (Solicitud.Sol_Exp == Constantes.opcionSi)
            {
                Solicitud_Exp = Constantes.digitoUno;
            }
            else
            {
                Solicitud_Exp = Constantes.digitoDos;
            }


            List<Svc_TGSC_PRA_VerParametrosList_Result> Obtiene_SINO = new List<Svc_TGSC_PRA_VerParametrosList_Result>();
            Obtiene_SINO = new ObtieneParametros().Obtiene_Parametros_List("Cnd_Si_No", Constantes.numUno);
            ViewBag.Sol_Ina = new SelectList(Obtiene_SINO, "Pra_Val", "Pra_Des", Solicitud_Ina);
            ViewBag.Sol_Exp = new SelectList(Obtiene_SINO, "Pra_Val", "Pra_Des", Solicitud_Exp);


            List<Svc_TGSC_PRA_VerParametrosList_Result> Obtiene_Tip_Cms = new List<Svc_TGSC_PRA_VerParametrosList_Result>();
            Obtiene_Tip_Cms = new ObtieneParametros().Obtiene_Parametros_List("Tip_Cms", Constantes.numUno);
            ViewBag.Comision = new SelectList(Obtiene_Tip_Cms, "Pra_Val", "Pra_Des", Solicitud.Tip_Ope_Cms);


            List<Svc_TGSC_GNO_VerGenero_Result> Gno_Id = new List<Svc_TGSC_GNO_VerGenero_Result>();
            Gno_Id = new ObtieneParametros().Obtiene_Genero(Constantes.numUno);
            ViewBag.Gno_id = new SelectList(Gno_Id, "Gno_Id", "Gno_Des", Solicitud.Gno_Id);


            List<Svc_TGSC_SCE_VerSectorEconomico_Result> SectorEco = new List<Svc_TGSC_SCE_VerSectorEconomico_Result>();
            SectorEco = new ObtieneParametros().obtiene_Sec_Eco(Constantes.numUno);
            ViewBag.Sce_Id = new SelectList(SectorEco, "Sce_Id", "sec_des", Solicitud.Sce_Id);


            List<Svc_TGSC_CLA_RIE_VerClasificaRiesgo_Result> ClaRiesgo = new List<Svc_TGSC_CLA_RIE_VerClasificaRiesgo_Result>();
            ClaRiesgo = new ObtieneParametros().Obtiene_Cla_Rie(Constantes.numUno);
            ViewBag.Cla_Rie_Id = new SelectList(ClaRiesgo, "Cla_Rie_Id", "Cla_Rie_Tip", Solicitud.Cla_Rie_Id);


            List<Svc_TGSC_REL_CTF_COM_VerCatastrofe_Result> ListaCatastrofe = new List<Svc_TGSC_REL_CTF_COM_VerCatastrofe_Result>();
            ListaCatastrofe = new ObtieneParametros().obtiene_catastrofe();
            ViewBag.Catastrofe = new SelectList(ListaCatastrofe, "Ctf_Cod", "Ctf_Des", Solicitud.Ctf_Cod);


            List<Svc_TGSC_REL_CTF_COM_VerRegion_Result> ListaRegiones = new List<Svc_TGSC_REL_CTF_COM_VerRegion_Result>();
            ListaRegiones = new ObtieneParametros().Obtiene_Region(Solicitud.Ctf_Cod);
            ViewBag.Regiones = new SelectList(ListaRegiones, "Rgn_cod", "Rgn_Nom", Solicitud.Region_Id);


            var Reg_Id = Solicitud.Region_Id;
            var Catastrofe_Cod = Solicitud.Ctf_Cod;

            List<Svc_TGSC_REL_CTF_COM_VerComuna_Result> ListaComuna = new List<Svc_TGSC_REL_CTF_COM_VerComuna_Result>();
            ListaComuna = new ObtieneParametros().Obtiene_Comuna(Reg_Id, Catastrofe_Cod);
            ViewBag.Rel_Ctf_Com_Id = new SelectList(ListaComuna, "Rel_Ctf_Com_Id", "Com_Nom_02", Solicitud.Rel_Ctf_Com_Id);

            
            //ViewBag.Comision = Solicitud.Tip_Ope_Cms;


            string tipo = Solicitud.Tip_Ope_Cms;
            //string estado = "1";

            List<Svc_TGSC_TIP_OPE_VerOperacion_Result> ListaOperaciones = new List<Svc_TGSC_TIP_OPE_VerOperacion_Result>();
            ListaOperaciones = new ObtieneParametros().Obtiene_List_Ope(tipo, Constantes.numUno, "");
            ViewBag.Tip_Ope_Id = new SelectList(ListaOperaciones, "Tip_Ope_Id", "Tip_Ope_Des", Solicitud.Tip_Ope_Id);


            List<Svc_TGSC_DES_PROY_VerDescripcion_Result> ListaProyecto = new List<Svc_TGSC_DES_PROY_VerDescripcion_Result>();

            List<Svc_TGSC_REL_DES_PROY_VerDescripcion_Result> List_Des_Pro = new List<Svc_TGSC_REL_DES_PROY_VerDescripcion_Result>();
            {
                List_Des_Pro = new ObtieneDesPyt().ObtieneDescProy(Solicitud.Sol_Id).ToList();


                //string Proy_Obj_Inv = "inversion";
                //string Proy_Est_Inv = "1";

                ListaProyecto = new ObtieneDesPyt().ObtieneListaProyecto(Constantes.objetivoInversion, Constantes.numUno);
                ViewBag.Des_Proy_Cod_01 = new SelectList(ListaProyecto, "Des_Proy_Cod", "Des_Proy_Des", 0);


                //string Proy_Obj_cp = "capital_de_trabajo";
                //string Proy_Est_cp = "1";
                ListaProyecto = new ObtieneDesPyt().ObtieneListaProyecto(Constantes.objetivoCapitalTrabajo, Constantes.numUno);
                ViewBag.Des_Proy_Cod_02 = new SelectList(ListaProyecto, "Des_Proy_Cod", "Des_Proy_Des", 0);

                //string Proy_Obj_Ref = "Refinanciamiento";
                //string Proy_Est_Ref = "1";

                ListaProyecto = new ObtieneDesPyt().ObtieneListaProyecto(Constantes.objetivoRefinanciamiento, Constantes.numUno);
                ViewBag.Des_Proy_Cod_03 = new SelectList(ListaProyecto, "Des_Proy_Cod", "Des_Proy_Des", 0);


                ViewBag.ShowInv = "none";
                ViewBag.ShowCp = "none";
                ViewBag.ShowRef = "none";

                foreach (var reg in List_Des_Pro)
                {
                    if (reg.Des_Proy_Obj == Constantes.objetivoInversion)
                    {
                        //Proy_Obj_Inv = "inversion";
                        //Proy_Est_Inv = "1";
                        ListaProyecto = new ObtieneDesPyt().ObtieneListaProyecto(Constantes.objetivoInversion, Constantes.numUno);
                        ViewBag.Des_Proy_Cod_01 = new SelectList(ListaProyecto, "Des_Proy_Cod", "Des_Proy_Des", reg.Des_Proy_Cod);
                        ViewBag.ShowInv = "show";

                    }

                    if (reg.Des_Proy_Obj == Constantes.objetivoCapitalTrabajo)
                    {
                        //Proy_Obj_cp = "capital_de_trabajo";
                        //Proy_Est_cp = "1";
                        ListaProyecto = new ObtieneDesPyt().ObtieneListaProyecto(Constantes.objetivoCapitalTrabajo, Constantes.numUno);
                        ViewBag.Des_Proy_Cod_02 = new SelectList(ListaProyecto, "Des_Proy_Cod", "Des_Proy_Des", reg.Des_Proy_Cod);
                        ViewBag.ShowCp = "show";
                    }

                    if (reg.Des_Proy_Obj == Constantes.objetivoRefinanciamiento)
                    {
                        //Proy_Obj_Ref = "Refinanciamiento";
                        //Proy_Est_Ref = "1";
                        ListaProyecto = new ObtieneDesPyt().ObtieneListaProyecto(Constantes.objetivoRefinanciamiento, Constantes.numUno);
                        ViewBag.Des_Proy_Cod_03 = new SelectList(ListaProyecto, "Des_Proy_Cod", "Des_Proy_Des", reg.Des_Proy_Cod);
                        ViewBag.ShowRef = "show";
                    }
                }
            }


            List<Svc_TGSC_TIP_MON_VerTipoMoneda_Result> ListMoneda = new List<Svc_TGSC_TIP_MON_VerTipoMoneda_Result>();
            {
                ListMoneda = new ObtieneParametros().Obtiene_Moneda(Constantes.numUno).ToList();
                ViewBag.Tip_Mon_Id = new SelectList(ListMoneda, "Tip_Mon_id", "Tip_Mon_Des", Solicitud.Tip_Mon_Id);
            }

            List<Svc_TGSC_TIP_GRC_VerGracia_Result> ListGracia = new List<Svc_TGSC_TIP_GRC_VerGracia_Result>();
            {
                ListGracia = new ObtieneParametros().Obtiene_Tip_Grc(Constantes.numUno).ToList();
                ViewBag.Tip_Grc_Id = new SelectList(ListGracia, "Tip_Grc_Id", "tip_Grc_Des", Solicitud.Tip_Grc_Id);
            }

            List<Svc_TGSC_FRE_PAG_VerFrecuencia_Result> List_Fre_Pag = new List<Svc_TGSC_FRE_PAG_VerFrecuencia_Result>();
            {
                List_Fre_Pag = new ObtieneParametros().Obtiene_Fre_Pag(Constantes.numUno).ToList();
                ViewBag.Fre_Pag_Id = new SelectList(List_Fre_Pag, "Fre_Pag_Id", "Fre_Pag_Des", Solicitud.Fre_Pag_Id);
            }

            List<Svc_TGSC_TIP_TAS_INT_VerTipoTasa_Result> List_Tip_Tas = new List<Svc_TGSC_TIP_TAS_INT_VerTipoTasa_Result>();
            {
                List_Tip_Tas = new ObtieneParametros().Obtiene_Tip_Tas(Constantes.numUno).ToList();
                ViewBag.Tip_Tas_Int_Cod = new SelectList(List_Tip_Tas, "Tip_Tas_Int_Cod", "Tip_Tas_Int_Des", Solicitud.Tip_Tas_Int_Cod);
            }


            List<Svc_TGSC_GAR_ADC_VerGarantia_Result> List_Gar_Adc_1 = new List<Svc_TGSC_GAR_ADC_VerGarantia_Result>();
            {
                List_Gar_Adc_1 = new ObtieneParametros().Obtiene_Gar_Adc(Constantes.numUno).ToList();
                ViewBag.Gar_Adc_Id_01 = new SelectList(List_Gar_Adc_1, "Gar_Adc_Id", "Gar_Adc_Des", Constantes.digitoUno);

                List_Gar_Adc_1 = new ObtieneParametros().Obtiene_Gar_Adc(Constantes.numUno).ToList();
                ViewBag.Gar_Adc_Id_02 = new SelectList(List_Gar_Adc_1, "Gar_Adc_Id", "Gar_Adc_Des", Constantes.digitoUno);
            }


            List<Svc_TGSC_REL_GAR_ADC_VerGarantia_Result> Obtiene_Gar = new List<Svc_TGSC_REL_GAR_ADC_VerGarantia_Result>();
            {
                var contador = Constantes.digitoUno;
                Obtiene_Gar = new ObtieneParametros().Obtiene_Des_Gar(Solicitud.Sol_Id).ToList();



                foreach (var Garantia in Obtiene_Gar)
                {
                    if (contador == Constantes.digitoUno)
                    {

                        List<Svc_TGSC_GAR_ADC_VerGarantia_Result> List_Gar_Adc = new List<Svc_TGSC_GAR_ADC_VerGarantia_Result>();
                        {
                            List_Gar_Adc = new ObtieneParametros().Obtiene_Gar_Adc(Constantes.numUno).ToList();
                            ViewBag.Gar_Adc_Id_01 = new SelectList(List_Gar_Adc, "Gar_Adc_Id", "Gar_Adc_Des", Garantia.Gar_Adc_Id);
                        }
                    }
                    else
                    {
                        List<Svc_TGSC_GAR_ADC_VerGarantia_Result> List_Gar_Adc = new List<Svc_TGSC_GAR_ADC_VerGarantia_Result>();
                        List_Gar_Adc = new ObtieneParametros().Obtiene_Gar_Adc(Constantes.numUno).ToList();
                        ViewBag.Gar_Adc_Id_02 = new SelectList(List_Gar_Adc, "Gar_Adc_Id", "Gar_Adc_Des", Garantia.Gar_Adc_Id);
                    }
                    contador = contador + Constantes.digitoUno;
                }
            }



            List<Svc_TGSC_REL_SEG_REF_VerSeguro_Result> Obtiene_Seg = new List<Svc_TGSC_REL_SEG_REF_VerSeguro_Result>();
            {
                Obtiene_Seg = new ObtieneParametros().Obtiene_Des_Seg(Solicitud.Sol_Id).ToList();
                
                foreach (var seguro in Obtiene_Seg){
                    List<Svc_TGSC_SEG_REF_VerSeguro_Result> List_Seg = new List<Svc_TGSC_SEG_REF_VerSeguro_Result>();
                    {
                        List_Seg = new ObtieneParametros().Obtiene_Seg(Constantes.numUno).ToList();
                        ViewBag.Seg_Ref_Id_01 = new SelectList(List_Seg, "Seg_Ref_Id", "Seg_ref_Des", seguro.Seg_Ref_Id);
                        }
                    }
                }


            return View(Solicitud);
        }

        // POST: Solicitudes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AbreSolicitud([Bind(Include = "Sol_Num_Sol, Sol_Num_Ope, Sol_Fec_Sol, Sol_Num_Fol, Sol_Nrt_Emp, Sol_Drt_Emp, Sol_Rzn_Scl, Sce_Id, Sol_Num_Sec, Sol_Ina, Sol_Tel, Rel_Ctf_Com_Id, Sol_Nom_Loc, Sol_Nom_Call, Sol_Num_Call, Sol_Cmn_Dir, Sol_Cro_Elt, Gno_Id, Sol_Exp, Cla_Rie_Id, Sol_Vnt_Emp, Sol_Mnt_Cbt_Utz, Sol_Mnt_Cms, Sol_Por_Cbt_Dis, Sol_Mnt_Cbt_Dis, Sol_Usu_Ejc, Sol_Rut_Ejc, Sol_Ofi_Cse_Des, Tip_Ope_Id, Tip_Mon_Id, Sol_Mnt_Crd, Sol_Mnt_Cpl_Tbj, Sol_Mnt_Inv, Sol_Mnt_Cp_Rfn, Sol_Fec_Cse, Sol_Fec_Vct, Sol_Val_Dol, Sol_Val_UF, Tip_Grc_Id, Sol_Mes_Grc, Sol_Mes_No_Pag, Fre_Pag_Id, Tip_Tas_Int_Cod, Sol_Int_Anl, Sol_Num_Cuo, Est_Sol_Id, Etp_Sol_Id, Sol_Pzo, Gar_Adc_Id_01, Gar_Adc_Id_02, Seg_Ref_Id_01, Seg_Ref_Id_02, Seg_Ref_Id_03, Des_Proy_Cod_01, Des_Proy_Cod_02, Des_Proy_Cod_03")] GuardarSolicitud _GuardarSolicitud)
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


            var num_sol = Convert.ToInt32(_GuardarSolicitud.Sol_Num_Sol);
            var num_ope = Convert.ToString(_GuardarSolicitud.Sol_Num_Sol);



            var _Sol_Num_Sol = num_sol; //_GuardarSolicitud.Sol_Num_Sol;
            var _Sol_Num_Ope = num_ope; //_GuardarSolicitud.Sol_Num_Ope;
            var _Sol_Fec_Sol = DateTime.Now; //_GuardarSolicitud.Sol_Fec_Sol;
            var _Sol_Num_Fol = _GuardarSolicitud.Sol_Num_Fol;
            var _Sol_Nrt_Emp = _GuardarSolicitud.Sol_Nrt_Emp;
            var _Sol_Drt_Emp = _GuardarSolicitud.Sol_Drt_Emp;
            var _Sol_Rzn_Scl = _GuardarSolicitud.Sol_Rzn_Scl;
            var _Sce_Id = _GuardarSolicitud.Sce_Id;
            var _Sol_Num_Sec = _GuardarSolicitud.Sol_Num_Sec;
            var _Sol_Ina = _GuardarSolicitud.Sol_Ina;
            var _Sol_Tel = _GuardarSolicitud.Sol_Tel;
            var _Rel_Ctf_Com_Id = _GuardarSolicitud.Rel_Ctf_Com_Id;
            var _Sol_Nom_Loc = _GuardarSolicitud.Sol_Nom_Loc;
            var _Sol_Nom_Call = _GuardarSolicitud.Sol_Nom_Call;
            var _Sol_Num_Call = _GuardarSolicitud.Sol_Num_Call;
            var _Sol_Cmn_Dir = _GuardarSolicitud.Sol_Cmn_Dir;
            var _Sol_Cro_Elt = _GuardarSolicitud.Sol_Cro_Elt;
            var _Gno_Id = _GuardarSolicitud.Gno_Id;
            var _Sol_Exp = _GuardarSolicitud.Sol_Exp;
            var _Cla_Rie_Id = _GuardarSolicitud.Cla_Rie_Id;
            var _Sol_Vnt_Emp = _GuardarSolicitud.Sol_Vnt_Emp;
            var _Sol_Mnt_Cbt_Utz = _GuardarSolicitud.Sol_Mnt_Cbt_Utz;
            var _Sol_Mnt_Cms = _GuardarSolicitud.Sol_Mnt_Cms;
            var _Sol_Por_Cbt_Dis = _GuardarSolicitud.Sol_Por_Cbt_Dis;
            var _Sol_Mnt_Cbt_Dis = _GuardarSolicitud.Sol_Mnt_Cbt_Dis;
            var _Sol_Usu_Ejc = _GuardarSolicitud.Sol_Usu_Ejc;
            var _Sol_Rut_Ejc = _GuardarSolicitud.Sol_Rut_Ejc;
            var _Sol_Ofi_Cse_Des = _GuardarSolicitud.Sol_Ofi_Cse_Des;
            var _Tip_Ope_Id = _GuardarSolicitud.Tip_Ope_Id;
            var _Tip_Mon_Id = _GuardarSolicitud.Tip_Mon_Id;
            var _Sol_Mnt_Crd = _GuardarSolicitud.Sol_Mnt_Crd;
            var _Sol_Mnt_Cpl_Tbj = _GuardarSolicitud.Sol_Mnt_Cpl_Tbj;
            var _Sol_Mnt_Inv = _GuardarSolicitud.Sol_Mnt_Inv;
            var _Sol_Mnt_Rfn = _GuardarSolicitud.Sol_Mnt_Cp_Rfn;
            var _Sol_Fec_Cse = _GuardarSolicitud.Sol_Fec_Cse;
            var _Sol_Fec_Vct = _GuardarSolicitud.Sol_Fec_Vct;
            var _Sol_Val_Dol = _GuardarSolicitud.Sol_Val_Dol;
            var _Sol_Val_UF = _GuardarSolicitud.Sol_Val_UF;
            var _Tip_Grc_Id = _GuardarSolicitud.Tip_Grc_Id;
            var _Sol_Mes_Grc = _GuardarSolicitud.Sol_Mes_Grc;
            var _Sol_Mes_No_Pag = _GuardarSolicitud.Sol_Mes_No_Pag;
            var _Fre_Pag_Id = _GuardarSolicitud.Fre_Pag_Id;
            var _Tip_Tas_Int_Cod = _GuardarSolicitud.Tip_Tas_Int_Cod;
            var _Sol_Int_Anl = _GuardarSolicitud.Sol_Int_Anl;
            var _Sol_Num_Cuo = _GuardarSolicitud.Sol_Num_Cuo;
            var _Est_Sol_Id = _GuardarSolicitud.Est_Sol_Id;
            var _Etp_Sol_Id = _GuardarSolicitud.Etp_Sol_Id;
            var _Sol_Pzo = _GuardarSolicitud.Sol_Pzo;
            var _Gar_Adc_Id_01 = _GuardarSolicitud.Gar_Adc_Id_01;
            var _Gar_Adc_Id_02 = _GuardarSolicitud.Gar_Adc_Id_02;
            var _Seg_Ref_Id_01 = _GuardarSolicitud.Seg_Ref_Id_01;
            var _Seg_Ref_Id_02 = _GuardarSolicitud.Seg_Ref_Id_02;
            var _Seg_Ref_Id_03 = _GuardarSolicitud.Seg_Ref_Id_03;
            var _Des_Proy_Cod_01 = _GuardarSolicitud.Des_Proy_Cod_01;
            var _Des_Proy_Cod_02 = _GuardarSolicitud.Des_Proy_Cod_02;
            var _Des_Proy_Cod_03 = _GuardarSolicitud.Des_Proy_Cod_03;



            if (ModelState.IsValid)
            {

                Sva_TGSC_SOL_InsertaSolicitud_Result Guarda_Solicitud = new Sva_TGSC_SOL_InsertaSolicitud_Result();
                Guarda_Solicitud = new RegistraSolicitud().Registra_Solicitud(_Sol_Num_Sol, _Sol_Num_Ope, _Sol_Fec_Sol, _Sol_Num_Fol, _Sol_Nrt_Emp, _Sol_Drt_Emp, _Sol_Rzn_Scl, _Sce_Id, _Sol_Num_Sec, _Sol_Ina, _Sol_Tel, _Rel_Ctf_Com_Id, _Sol_Nom_Loc, _Sol_Nom_Call, _Sol_Num_Call, _Sol_Cmn_Dir, _Sol_Cro_Elt, _Gno_Id, _Sol_Exp, _Cla_Rie_Id, _Sol_Vnt_Emp, _Sol_Mnt_Cbt_Utz, _Sol_Mnt_Cms, _Sol_Por_Cbt_Dis, _Sol_Mnt_Cbt_Dis, _Sol_Usu_Ejc, _Sol_Rut_Ejc, _Sol_Ofi_Cse_Des, _Tip_Ope_Id, _Tip_Mon_Id, _Sol_Mnt_Crd, _Sol_Mnt_Cpl_Tbj, _Sol_Mnt_Inv, _Sol_Mnt_Rfn, _Sol_Fec_Cse, _Sol_Fec_Vct, _Sol_Val_Dol, _Sol_Val_UF, _Tip_Grc_Id, _Sol_Mes_Grc, _Sol_Mes_No_Pag, _Fre_Pag_Id, _Tip_Tas_Int_Cod, _Sol_Int_Anl, _Sol_Num_Cuo, _Est_Sol_Id, _Etp_Sol_Id, _Sol_Pzo, _Gar_Adc_Id_01, _Gar_Adc_Id_02, _Seg_Ref_Id_01, _Seg_Ref_Id_02, _Seg_Ref_Id_03, _Des_Proy_Cod_01, _Des_Proy_Cod_02, _Des_Proy_Cod_03);

                Sva_TGSC_SOL_DOC_Documentos_Result Datos_Documentos = new Sva_TGSC_SOL_DOC_Documentos_Result();
                Datos_Documentos = new GeneraDocumentos().Inserta_Reg_Doc(_Sol_Num_Sol);


                return RedirectToAction("Ingresada", "Solicitudes", new { @id_ifi = _Sol_Num_Sol }); //, etapa = gSC_SOL.Etp_Sol_Id
            }

                     
            return View(_GuardarSolicitud);
        }



        public ActionResult Consulta()
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


            //string estados_opc = "1";
         
            List<Svc_TGSC_EST_SOL_VerEstado_Result> List_Estado = new List<Svc_TGSC_EST_SOL_VerEstado_Result>();
            {
                List_Estado = new ObtieneParametros().Obtiene_Estado(Constantes.numUno).ToList();
                ViewBag.Est_Sol_Id = new SelectList(List_Estado, "Est_Sol_Id", "Est_Sol_Des");
            }

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
