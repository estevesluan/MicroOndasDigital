using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MO.Models
{
    public class Arquivo
    {
        private string _caminho;

        public string _Caminho
        {
            get
            {
                return _caminho;

            }

            set
            {
                _caminho = value;

            }
        }

        public Arquivo(string caminho)
        {
            this._Caminho = caminho;
        }

        public StreamReader Abrir()
        {
            //Verifica se o arquivo existe
            if(File.Exists(this._caminho))
            {
                //Retorna o leitor do arquivo
                return new StreamReader(this._Caminho);
            }
            //Arquivo não existe
            return null;
        }
    }
}