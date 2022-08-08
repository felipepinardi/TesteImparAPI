using System.Collections.Generic;

namespace TesteImparAPI
{
    public class View
    {
        public View()
        {
            rotas = new List<string>();
            rotas.Add("/cards");
            rotas.Add("/cards/create");
        }
        private List<string> rotas { get; set; }

        public string Mensagem { get { return "API para Teste Ímpar"; } }
        public List<string> Rotas { get { return this.rotas; } }
    }
}
