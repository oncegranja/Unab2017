using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.IO;
using System.Text;
using System.Globalization;
using System.Web.Mvc;
using Garantia_4.Models;
using Garantia_4.Funciones;


namespace Garantia_4.Controllers
{
    public class GeneraArchivosController : Controller
    {
        [HandleError()]

        [HttpGet]
        public ActionResult Generacion()
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


        [HttpGet]
        public ActionResult CargaArchivos()
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

            Sva_TGSC_OPE_SIS_PRD_Carga_V2_Result carga_archivos = new Sva_TGSC_OPE_SIS_PRD_Carga_V2_Result();
            carga_archivos = new CargaArchivo().carga_archivos();

            ViewBag.estadocarga = carga_archivos.isExists;

            return View();
        }


        [HttpGet]
        public ActionResult ReporteCorfo()
        {

            List<Sva_TGSC_SOL_GeneraCargaCorfo_Result> genera_corfo = new List<Sva_TGSC_SOL_GeneraCargaCorfo_Result>();
            //var etapa = 3;
            genera_corfo = new GeneraCorfo().Genera_Corfo(Constantes.digitoTres);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (var listado in genera_corfo) {
                sb.Append(listado.n_operacion_ifi);
                sb.Append("\t");
                sb.Append(listado.rut_empresa);
                sb.Append("\t");
                sb.Append(listado.sector_economico_sbif);
                sb.Append("\t");
                sb.Append(listado.inicio_actividades);
                sb.Append("\t");
                sb.Append(listado.ventas_empresa);
                sb.Append("\t");
                sb.Append(listado.localizacion);
                sb.Append("\t");
                sb.Append(listado.clasificacion_riesgo_empresa);
                sb.Append("\t");
                sb.Append(listado.genero);
                sb.Append("\t");
                sb.Append(listado.exportador);
                sb.Append("\t");
                sb.Append(listado.fecha_aprobacion_operacion);
                sb.Append("\t");
                sb.Append(listado.tipo_operacion);
                sb.Append("\t");
                sb.Append(listado.moneda_operacion);
                sb.Append("\t");
                sb.AppendFormat("{0:C}", listado.monto_operacion_moneda_origen);
                sb.Append("\t");
                sb.Append(listado.monto_operacion_sin_comision);
                sb.Append("\t");
                sb.Append(listado.monto_capital_trabajo);
                sb.Append("\t");
                sb.Append(listado.monto_inversion);
                sb.Append("\t");
                sb.Append(listado.monto_refinanciamiento);
                sb.Append("\t");
                sb.Append(listado.tipo_plan_de_pago);
                sb.Append("\t");
                sb.Append(listado.meses_periodo_gracia);
                sb.Append("\t");
                sb.Append(listado.tipo_gracia_);
                sb.Append("\t");
                sb.Append(listado.frecuencia_pagos);
                sb.Append("\t");
                sb.Append(listado.numero_cuotas);
                sb.Append("\t");
                sb.Append(listado.fecha_primer_vencimiento);
                sb.Append("\t");
                sb.Append(listado.meses_no_pago);
                sb.Append("\t");
                sb.Append(listado.tasa_interes_anual_operacion);
                sb.Append("\t");
                sb.Append(listado.tipo_tasa_interes);
                sb.Append("\t");
                sb.Append(listado.garantias_adicionales);
                sb.Append("\t");
                sb.Append(listado.descripcion_seguro);
                sb.Append("\t");
                sb.Append(listado.zona_catastrofe);
                sb.Append("\t");
                sb.Append(listado.calle);
                sb.Append("\t");
                sb.Append(listado.numero);
                sb.Append("\t");
                sb.Append(listado.complemento_direccion);
                sb.Append("\t");
                sb.Append(listado.telefono);
                sb.Append("\t");
                sb.Append(listado.correo_electronico);
                sb.Append("\t");
                sb.Append(listado.num_cec);
                sb.Append("\t");
                sb.Append(listado.descripcion_del_proyecto);
                sb.Append("\t");
                sb.AppendLine();
                }
            //System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
            Encoding encoding = Encoding.UTF8;
            Response.ClearContent();
            Response.Buffer = true;
            //Response.AddHeader("content-disposition", string.Format("attachment; filename=Hoja1.xls", "Hoja1"));
            Response.AddHeader("content-disposition", string.Format("attachment; filename=Hoja1.xls"));
            Response.ContentType = "application/ms-excel";
            Response.Charset = encoding.EncodingName;
            Response.ContentEncoding = Encoding.Unicode;
            Response.Write(sb.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("Verlistado");
        }

        [HttpGet]
        public ActionResult CargaInterfaces()
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


            List<Svt_Verifica_Archivos_V2_Result> obtienearchivos = new List<Svt_Verifica_Archivos_V2_Result>();
            obtienearchivos = new ObtieneArchivo().obtiene_archivos();

            string[] archivos = new string[4];
            archivos[0] = Constantes.sisProdComex;
            archivos[1] = Constantes.sisProdCredito;
            archivos[2] = Constantes.sisProdFactoring;
            archivos[3] = Constantes.sisProdLeasing;

            string NomSistemaProd = "  ";

            for (int i = 0; i < obtienearchivos.Count(); i++)
            {
                ViewData["listaarchivos"] = archivos;

                //var resp_sistema = obtienearchivos[i].estado;
                var nombrecampo = "existe" + obtienearchivos[i].sistema;
                var nombrecampo2 = "existe2" + obtienearchivos[i].sistema;
                var nombrecampo3 = "existe3" + obtienearchivos[i].sistema;


                if (obtienearchivos[i].sistema == Constantes.sisProdComex)
                {
                    NomSistemaProd = Constantes.nomComex;
                }

                if (obtienearchivos[i].sistema == Constantes.sisProdCredito)
                {
                    NomSistemaProd = Constantes.nomCredito;
                }

                if (obtienearchivos[i].sistema == Constantes.sisProdFactoring)
                {
                    NomSistemaProd =Constantes.nomFactoring;
                }

                if (obtienearchivos[i].sistema == Constantes.sisProdLeasing)
                {
                    NomSistemaProd = Constantes.NomLeasing;
                }



                if (obtienearchivos[i].estado == Constantes.numUno)
                {

                    ViewData.Add(nombrecampo, "alert alert-success alert-white rounded");
                    ViewData.Add(nombrecampo2, "<strong>Correcto!</strong> Archivo de " + NomSistemaProd + " Existe");
                    ViewData.Add(nombrecampo3, "fa fa-check");
                }
                else
                {
                    ViewData.Add(nombrecampo, "alert alert-danger alert-white rounded");
                    ViewData.Add(nombrecampo2, "<strong>ERROR!</strong> Archivo de " + NomSistemaProd + " No Existe");
                    ViewData.Add(nombrecampo3, "fa fa-times-circle");
                    ViewBag.valida = Constantes.numUno;
                }
            }

            return View();
        }


        [HttpGet]
        public ActionResult ver_documentos(Int64 id_ifi)
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


            return PartialView();
        }


    }
}

