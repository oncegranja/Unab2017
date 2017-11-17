using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Garantia_4.Models;

namespace Garantia_4.Funciones
{
    public static class utlidades
        {

        public static string NomUser(string MachineUserName)
        {
            int inicio = MachineUserName.IndexOf('\\') + 1;
            int largo = MachineUserName.Length - inicio;
            string Login = MachineUserName.Substring(inicio, largo);

            Svc_TGSC_USR_SIS_VerUsuarioSistema_Result usuario = new Svc_TGSC_USR_SIS_VerUsuarioSistema_Result();
            usuario = new ObtieneUsuario().LeeUsuario(Login);

            HttpContext.Current.Session["perfil"] = usuario.Pfl_Id;
            HttpContext.Current.Session["username"] = usuario.nombre;
            HttpContext.Current.Session["nombreUnidad"] = usuario.nombreUnidad;
            HttpContext.Current.Session["Nombre_oficina"] = usuario.Nombre_oficina;
            HttpContext.Current.Session["login"] = usuario.login;
            HttpContext.Current.Session["Rut"] = usuario.Rut;
        

            return (Login);
                
        }

        public static object Acceso()
        {
            var acceso = HttpContext.Current.Session["perfil"];

            if (acceso == null)
            {
            
                HttpContext.Current.Response.Redirect("../Home");

               //return new RedirectResult("/Home/Index");

            }

            return ("0");
        }


        public static void validaControlAcceso()
        {

            var acceso = HttpContext.Current.Session["perfil"];

            if (acceso == null)
            {
                HttpContext.Current.Response.Redirect("../Home");
            }

            HttpContext.Current.Session.Abandon();

        }

    }


    static class Constantes
    {

        public const string numUno = "1";
        public const string numDos = "2";
        public const string numTres = "3";
        public const string numCuatro = "4";
        public const string numCinco = "5";

        public const int digitoCero = 0;
        public const int digitoUno = 1;
        public const int digitoDos = 2;
        public const int digitoTres = 3;


        
        public const string objetivoInversion = "inversion";
        public const string objetivoCapitalTrabajo = "capital_de_trabajo";
        public const string objetivoRefinanciamiento = "Refinanciamiento";

        public const string opcionSi = "SI";
        public const string opcionNo = "NO";


        public const string sisProdComex = "CMX_";
        public const string sisProdCredito = "CRE_";
        public const string sisProdFactoring = "FCT_";
        public const string sisProdLeasing = "LEG_";

        public const string nomComex = "Comex";
        public const string nomCredito = "Credito";
        public const string nomFactoring = "Factoring";
        public const string NomLeasing = "Leasing";

        public const string mensajeCatastrofeElimina = "ESTA CATASTROFE SE ELIMINARA DEL SISTEMA YA QUE NO HA SIDO UTILIZADA EN NINGUNA SOLICITUD";
        public const string mensajeCatastrofeEstado = "ESTA CATASTROFE CAMBIARA DE ESTADO DENTRO DEL SISTEMA PERO NO SE ELIMINARA";

    



    }

}
