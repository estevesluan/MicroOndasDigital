using MO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MO.Controllers
{
    public class ProgramaAquecimentoController : Controller
    {
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(string nome, string instrucoes, int tempoSegundos, int potencia, char aquecimento, string alimentos)
        {
            try
            {
                //Divide os alimentos informados com o separador ','
                List<Alimento> listaAlimentos = new List<Alimento>();
                foreach(string a in alimentos.Split(','))
                {
                    listaAlimentos.Add(new Alimento(a,""));
                }

                MicroOndas._Programas.Add(new ProgramaAquecimento(nome, instrucoes, tempoSegundos, potencia, aquecimento, listaAlimentos));
            }
            catch (Exception)
            {
                return Json(new { erro = "Erro ao gravar programa" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { sucesso = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListaProgramaAquecimento(string pesquisaAlimento)
        {
            try
            {
                pesquisaAlimento = pesquisaAlimento.ToUpper();
                List<ProgramaAquecimento> listaProgramaAquecimeto = new List<ProgramaAquecimento>();
                MicroOndas mo = new MicroOndas(1, 60 * 2, 1, 10);

                foreach (var item in MicroOndas._Programas)
                {
                    if(item._Alimentos.Where(x => x._Nome.ToUpper().Contains(pesquisaAlimento)).Count() > 0)
                    {
                        listaProgramaAquecimeto.Add(item);
                    }
                }

                return PartialView("Tabela",listaProgramaAquecimeto);
            }
            catch (Exception)
            {
                return Json(new { erro = "" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}