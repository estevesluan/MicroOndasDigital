using MO.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MO.Models
{
    public class ProgramaAquecimento : IProgramaAquecimento
    {
        //Atributos
        private string _nome;
        private string _instrucoes;
        private int _potencia;
        private TimeSpan _tempo;
        private char _aquecimento;
        private List<Alimento> _alimentos;

        //Get e Set
        public string _Nome
        {
            get
            {
                return _nome;
            }

            set
            {
                _nome = value;
            }
        }
        public string _Instrucoes
        {
            get
            {
                return _instrucoes;
            }

            set
            {
                _instrucoes = value;
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
        public List<Alimento> _Alimentos
        {
            get
            {
                return _alimentos;
            }

            set
            {
                _alimentos = value;
            }
        }

        public ProgramaAquecimento(string nome, string instrucoes, int potenciaSegundos, int tempoSegundos, char aquecimento, List<Alimento> alimentos)
        {
            this._Nome = nome;
            this._Instrucoes = instrucoes;
            this._Potencia = potenciaSegundos;
            this._Tempo = TimeSpan.FromSeconds(tempoSegundos);
            this._Aquecimento = aquecimento;
            this._Alimentos = alimentos;
        }

        public bool ValidarConteudo(string conteudo)
        {
            //Conteudo pode ser o caminho para um arquivo
            if (File.Exists(conteudo))
            {
                //Ler conteudo no arquivo
                StreamReader sr = new StreamReader(conteudo);
                //Atualizar o conteúdo para aquecimento
                conteudo = sr.ReadToEnd();
                sr.Close();
            }

            //Verdadeiro quando o conteúdo possui o nome de algum alimento do programa
            if (this._Alimentos.Where(x => conteudo.Contains(x._Nome)).Count() == 0)
            {
                throw new Exception("Alimento incompatível");
            }

            return true;
        }
    }
}