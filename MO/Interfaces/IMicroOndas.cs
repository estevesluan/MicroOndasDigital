using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MO.Interfaces
{
    interface IMicroOndas
    {
        void ValidarParametrizacao(int potencia, TimeSpan tempoSegundos);
        string IniciarAquecimento(string conteudo, int potencia, int tempo, char aquecimento);
        string IniciarRapido(string conteudo);
        void Pausar();
        void Cancelar();
    }
}
