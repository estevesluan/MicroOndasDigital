using MO.Models;
using MO.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MO.Controllers
{
    public class MicroOndasController : Controller
    {
        public ActionResult Index()
        {
            MicroOndas mo = new MicroOndas(1, 60 * 2, 1, 10);
            Temporario._microOndas.Add(mo);
            ViewBag.programas = MicroOndas._Programas;
            ViewBag.codigo = mo._Codigo;
            return View();
        }

        public ActionResult IniciarAquecimento(string conteudo, string codigo, int? potencia, int? tempoSegundos)
        {
            try
            {
                //Verifica campos fornecidos como parâmetros
                if (tempoSegundos == null)
                {
                    return Json(new { erro = "Informe o tempo" }, JsonRequestBehavior.AllowGet);
                }

                MicroOndas mo = Temporario._microOndas.Where(x => x._Codigo == codigo).FirstOrDefault();
                if (mo == null || string.IsNullOrEmpty(mo._Codigo))
                {
                    return Json(new { erro = "Micro-ondas inválido" }, JsonRequestBehavior.AllowGet);
                }

                //Potência não informada deve receber o padrão definido Util/Constantes.cs
                if (potencia == null)
                {
                    potencia = Constantes.MICRO_ONDAS_POTENCIA_PADRAO;
                }

                //Iniciar procedimento
                string stringAquecida;
                if (mo._Cancelado || !mo._Pausado)
                {
                    stringAquecida = mo.IniciarAquecimento(conteudo, (int)potencia, (int)tempoSegundos);
                }
                else
                {
                    stringAquecida = mo.IniciarAquecimento(conteudo, mo._Potencia, (int)mo._Tempo.TotalSeconds);
                }
                bool terminou = mo._Tempo.TotalSeconds == 0 ? true : false;

                //Valida parâmetros informados e iniciar aquecimento/ Retorna o resultado para tela
                return Json(new { conteudo = stringAquecida, terminou}, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { erro = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult IniciarAquecimentoPrograma(string conteudo, string codigo, string programaNome)
        {
            try
            {
                MicroOndas mo = Temporario._microOndas.Where(x => x._Codigo == codigo).FirstOrDefault();
                if (mo == null || string.IsNullOrEmpty(mo._Codigo))
                {
                    return Json(new { erro = "Micro-ondas inválido" }, JsonRequestBehavior.AllowGet);
                }

                ProgramaAquecimento pa = MicroOndas._Programas.Where(x => x._Nome == programaNome).FirstOrDefault();
                if (pa == null || pa._Nome == "")
                {
                    return Json(new { erro = "Programa inválido" }, JsonRequestBehavior.AllowGet);
                }

                //Validar Programa
                pa.ValidarConteudo(conteudo);

                //Iniciar procedimento
                string stringAquecida;
                if (mo._Cancelado || !mo._Pausado)
                {
                    stringAquecida = mo.IniciarAquecimento(conteudo, pa._Potencia, (int)pa._Tempo.TotalSeconds, pa._Aquecimento);
                }
                else
                {
                    stringAquecida = mo.IniciarAquecimento(conteudo, mo._Potencia, (int)mo._Tempo.TotalSeconds, mo._Aquecimento);
                }

                bool terminou = mo._Tempo.TotalSeconds == 0 ? true : false;

                //Valida parâmetros informados e iniciar aquecimento/ Retorna o resultado para tela
                return Json(new { conteudo = stringAquecida, terminou }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { erro = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult IniciarAquecimentoRapido(string conteudo, string codigo)
        {
            try
            {
                //Valida micro-ondas
                MicroOndas mo = Temporario._microOndas.Where(x => x._Codigo == codigo).FirstOrDefault();
                if (mo == null || string.IsNullOrEmpty(mo._Codigo))
                {
                    return Json(new { erro = "Micro-ondas inválido" }, JsonRequestBehavior.AllowGet);
                }
                //Iniciar procedimento
                string result = mo.IniciarRapido(conteudo);

                bool terminou = mo._Tempo.TotalSeconds == 0 ? true : false;

                return Json(new { conteudo = result, terminou }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { erro = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ListaProgramaAquecimento(string pesquisaAlimento)
        {
            //Iniciar procedimento
            try
            {
                //Realizar filtro
                pesquisaAlimento = pesquisaAlimento.ToUpper();
                List<ProgramaAquecimento> listaProgramaAquecimeto = new List<ProgramaAquecimento>();

                foreach (var item in MicroOndas._Programas)
                {
                    if(item._Alimentos.Where(x => x._Nome.ToUpper().Contains(pesquisaAlimento)).Count() > 0)
                    {
                        listaProgramaAquecimeto.Add(item);
                    }
                }
                //Retorna alimentos que possuem no nome a pesquisa informada
                return PartialView("Tabela",listaProgramaAquecimeto);
            }
            catch (Exception)
            {
                return Json(new { erro = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Pausar(string codigo)
        {
            try
            {
                //Valida micro-ondas
                MicroOndas mo = Temporario._microOndas.Where(x => x._Codigo == codigo).FirstOrDefault();
                if (mo == null || string.IsNullOrEmpty(mo._Codigo))
                {
                    return Json(new { erro = "Micro-ondas inválido" }, JsonRequestBehavior.AllowGet);
                }

                //Pausar
                mo.Pausar();
                return Json(new { conteudo = mo._Conteudo }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { erro = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Cancelar(string codigo)
        {
            try
            {
                //Valida micro-ondas
                MicroOndas mo = Temporario._microOndas.Where(x => x._Codigo == codigo).FirstOrDefault();
                if (mo == null || string.IsNullOrEmpty(mo._Codigo))
                {
                    return Json(new { erro = "Micro-ondas inválido" }, JsonRequestBehavior.AllowGet);
                }

                //Cancelar
                mo.Cancelar();
                return Json(new { conteudo = mo._Conteudo }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new { erro = "" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}