using Microsoft.VisualStudio.TestTools.UnitTesting;
using MO.Models;
using MO.Util;

namespace TesteMO
{
    [TestClass]
    public class UnitTestMicroOndas
    {
        //configuração micro-ondas
        private int tempoMinimo = 1;
        private int tempoMaximo = 60 * 2;
        private int potenciaMinima = 1;
        private int potenciaMaxima = 10;

        [TestMethod]
        public void TestStringInicio()
        {
            //Resultado
            var result = "";
            //Saida esperada
            string esperado = "t......";
            //Chamar procedimento
            try
            {
                MicroOndas mo = new MicroOndas(tempoMinimo, tempoMaximo, potenciaMinima, potenciaMaxima);
                result = mo.IniciarAquecimento("t", potenciaMinima, tempoMinimo + 5);
            }
            catch (System.Exception ex)
            {
                result = ex.Message;
            }

            Assert.AreEqual(esperado, result.ToString());
        }

        [TestMethod]
        public void TestStringInicioRapido()
        {
            //Resultado
            var result = "";
            //Saida esperada
            string esperado = "t";
            //Calcular saida esperada conforme as configuracoes de constantes
            string aquecimentoSegundo = "";

            for (int p = 0; p < Constantes.INICIO_RAPIDO_POTENCIA; p++)
            {
                aquecimentoSegundo += ".";
            }

            for (int i = 0; i < Constantes.INICIO_RAPIDO_TEMPO_SEGUNDOS; i++)
            {
                esperado += aquecimentoSegundo;
            }

            //Chamar procedimento
            try
            {
                MicroOndas mo = new MicroOndas(tempoMinimo, tempoMaximo, potenciaMinima, potenciaMaxima);
                result = mo.IniciarRapido("t");
            }
            catch (System.Exception ex)
            {
                result = ex.Message;
            }

            Assert.AreEqual(esperado, result.ToString());
        }

        [TestMethod]
        public void TestStringPotenciaIncorreta()
        {
            //Resultado
            var result = "";
            //Saida esperada
            string esperado = "A potência deve ser maior que " + this.potenciaMinima + " e menor que " + potenciaMaxima;
            //Chamar procedimento
            try
            {
                MicroOndas mo = new MicroOndas(tempoMinimo, tempoMaximo, potenciaMinima, potenciaMaxima);
                result = mo.IniciarAquecimento("t", potenciaMinima - 1, tempoMinimo);
            }
            catch (System.Exception ex)
            {
                result = ex.Message;
            }
            
            Assert.AreEqual(esperado.ToLower(), result.ToString().ToLower());
        }
    }
}