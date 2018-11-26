using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MO.Models
{
    public class Alimento
    {
        //Atributos
        private string _nome;
        private string _descricao;

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
        public string _Descricao
        {
            get
            {
                return _descricao;
            }

            set
            {
                _descricao = value;
            }
        }

        public Alimento(string nome, string descricao)
        {
            this._Nome = nome;
            this._Descricao = descricao;
        }
    }
}