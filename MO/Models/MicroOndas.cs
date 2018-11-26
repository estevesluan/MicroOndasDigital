using MO.Interfaces;
using MO.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace MO.Models
{
    public class MicroOndas : IMicroOndas
    {
        //Atributos de configuração
        protected string _codigo;
        protected TimeSpan _tempoMinimo;
        protected TimeSpan _tempoMaximo;
        protected int _potenciaMinima;
        protected int _potenciaMaxima;
        protected static List<ProgramaAquecimento> _programas;
        //Atributos de funcionamento/Estado
        private bool _pausado;
        private bool _cancelado;
        private string _conteudo;
        private string _conteudoAquecimento;
        private int _potencia;
        private TimeSpan _tempo;
        private char _aquecimento;
        //Get e Set
        public string _Codigo
        {
            get
            {
                return _codigo;
            }

            set
            {
                //Não possui set
            }
        }
        public TimeSpan _TempoMinimo
        {
            get
            {
                return _tempoMinimo;
            }

            set
            {
                //Não possui set
            }
        }
        public TimeSpan _TempoMaximo
        {
            get
            {
                return _tempoMaximo;
            }

            set
            {
                //Não possui set
            }
        }
        public int _PotenciaMinima
        {
            get
            {
                return _potenciaMinima;
            }

            set
            {
                _potenciaMinima = value;
            }
        }
        public int _PotenciaMaxima
        {
            get
            {
                return _potenciaMaxima;
            }

            set
            {
                _potenciaMaxima = value;
            }
        }
        public static List<ProgramaAquecimento> _Programas
        {
            get
            {
                return _programas;
            }

            set
            {
                _programas = value;
            }
        }
        public bool _Pausado
        {
            get
            {
                return _pausado;
            }

            set
            {
                _pausado = value;
            }
        }
        public bool _Cancelado
        {
            get
            {
                return _cancelado;
            }

            set
            {
                _cancelado = value;
            }
        }
        public string _Conteudo
        { 
            get
            {
                return _conteudo;
            }

            set
            {
                _conteudo = value;
            }
        }
        public string _ConteudoAquecimento
        {
            get
            {
                return _conteudoAquecimento;
            }

            set
            {
                _conteudoAquecimento = value;
            }
        }
        public int _Potencia
        {
            get
            {
                return _potencia;
            }

            set
            {
                _potencia = value;
            }
        }
        public TimeSpan _Tempo
        {
            get
            {
                return _tempo;
            }

            set
            {
                _tempo = value;
            }
        }
        public char _Aquecimento
        {
            get
            {
                return _aquecimento;
            }

            set
            {
                _aquecimento = value;
            }
        }

        public MicroOndas(int tempoMinimoSegundos, int tempoMaximoSegundos, int potenciaMinima, int potenciaMaxima)
        {
            this._codigo = Guid.NewGuid().ToString();
            this._tempoMinimo = TimeSpan.FromSeconds(tempoMinimoSegundos);
            this._tempoMaximo = TimeSpan.FromSeconds(tempoMaximoSegundos);
            this._PotenciaMinima = potenciaMinima;
            this._PotenciaMaxima = potenciaMaxima;
        }

        public string IniciarAquecimento(string conteudo, int potencia, int tempoSegundos, char aquecimento = '.')
        {
            try
            {
                //Atualizar o estado do objeto
                this._Pausado = false;
                this._Cancelado = false;
                this._Aquecimento = aquecimento;
                this._Conteudo = conteudo;
                this._Potencia = (int)potencia;
                this._Tempo = TimeSpan.FromSeconds((int)tempoSegundos);

                //Validar parâmetros
                this.ValidarParametrizacao(this._Potencia, this._Tempo);


                //Prepara a string que será concatenada a cada segundo
                string aquecimentoPorSegundo = "";
                for (int p = 0; p < this._Potencia; p++)
                {
                    aquecimentoPorSegundo += aquecimento;
                }

                int tempo;
                //Verificar se é um arquivo ou string
                if (File.Exists(this._Conteudo))
                {
                    //Ler o arquivo
                    StreamReader sr = new StreamReader(this._Conteudo);
                    //Atualizar o conteúdo para aquecimento
                    this._ConteudoAquecimento = sr.ReadToEnd();
                    //Fechar o arquivo
                    sr.Close();
                    tempo = AquecerArquivo(aquecimentoPorSegundo);
                }
                else
                {
                    //Atualizar o conteúdo para aquecimento
                    this._ConteudoAquecimento = conteudo;
                    //Nãp é necessário atualizar o contúdo para aquecimento pois a string atualizada vem como parametro
                    tempo = AquecerString(aquecimentoPorSegundo);
                }

                //Atualiza para o tempo restante
                this._Tempo = TimeSpan.FromSeconds(tempo);

                //Valida se restou tempo (casos com pausa)
                if (tempo > 0)
                {
                    //Aquecimento não terminou, retorna a string inicial -- Quando é arquivo, não altera em tela o resultado do aquecimento (Pois atualiza o arquivo)
                    return File.Exists(this._Conteudo) ? this._Conteudo : this._ConteudoAquecimento;
                }

                //Finaliza o aquecimento e retorna a string aquecida 
                return this._ConteudoAquecimento;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        private int AquecerString(string aquecimentoPorSegundo)
        {
            int t = (int)this._Tempo.TotalSeconds;
            //Aquecimento início
            while (t > 0 && !this._Pausado && !this._Cancelado)
            {
                //Aquecimento - 1 segundo
                Thread.Sleep(1000);
                //Atualiza o conteúdo conforme a pontência configurada
                this._ConteudoAquecimento += aquecimentoPorSegundo;
                t--;
            }
            return t;
        }

        private int AquecerArquivo(string aquecimentoPorSegundo)
        {
            int t = (int)this._Tempo.TotalSeconds;
            //Aquecimento início
            while(t > 0 && !this._Pausado && !this._Cancelado)
            {
                //Aquecimento - 1 segundo
                Thread.Sleep(1000);
                //Atualiza o conteúdo conforme a pontência configurada
                this._ConteudoAquecimento += aquecimentoPorSegundo;
                //Atualizar o arquivo texto a  cada segundo
                StreamWriter sw = new StreamWriter(this._Conteudo);
                sw.Write(this._ConteudoAquecimento);
                sw.Close();
                t--;
            }
            return t;
        }

        public string IniciarRapido(string conteudo)
        {
            //Inicia o aquecimento conforme a configuração padrão na classe Util/Constates.cs
            return IniciarAquecimento(conteudo, Constantes.INICIO_RAPIDO_POTENCIA, Constantes.INICIO_RAPIDO_TEMPO_SEGUNDOS);
        }

        public void ValidarParametrizacao(int potencia, TimeSpan tempo)
        {
            //Validar tempo
            if (this._Tempo < this._TempoMinimo || this._Tempo > this._TempoMaximo)
            {
                throw new Exception("O tempo deve ser maior que " + this._TempoMinimo.ToString("hh\\:mm\\:ss") + " e menor que " + this._TempoMaximo.ToString("hh\\:mm\\:ss"));
            }
            //Validar potência
            if (this._Potencia < this._PotenciaMinima || this._Potencia > this._PotenciaMaxima)
            {
                throw new Exception("A potência deve ser maior que " + this._PotenciaMinima + " e menor que " + this._PotenciaMaxima);
            }
        }

        public void Pausar()
        {
            //Somente quando está em execução
            if(this._Tempo.TotalSeconds > 0)
                this._Pausado = true;
        }

        public void Cancelar()
        {
            //Somente quando está em execução
            if (this._Tempo.TotalSeconds > 0)
                this._Cancelado = true;
        }
    }
}