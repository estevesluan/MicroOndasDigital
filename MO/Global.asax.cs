using MO.Models;
using MO.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MO
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Carregar padrão de programa de aquecimento
            this.AdicionarProgramaPadrao();
            //Inicar dados temporários
            this.IniciarDadosTemporarios();
        }

        public void AdicionarProgramaPadrao()
        {
            //Programas de aquecimento padrão do micro-ondas
            MicroOndas._Programas = new List<ProgramaAquecimento>
            {
                new ProgramaAquecimento("P1", "I1", 5, 30, '!', new List<Alimento> { new Alimento("a1", "D1") }),
                new ProgramaAquecimento("P2", "I2", 1, 60, '@', new List<Alimento> { new Alimento("a2", "D2") }),
                new ProgramaAquecimento("P3", "I3", 2, 80, '#', new List<Alimento> { new Alimento("a3", "D3") }),
                new ProgramaAquecimento("P4", "I4", 6, 25, '$', new List<Alimento> { new Alimento("a4", "D4") }),
                new ProgramaAquecimento("P5", "I5", 9, 61, '%', new List<Alimento> { new Alimento("a5", "D5") })
            };
        }

        public void IniciarDadosTemporarios()
        {
            //Micro-ondas temporário
            Temporario._microOndas = new List<MicroOndas>();
        }
    }
}
