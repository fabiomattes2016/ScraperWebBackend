using HtmlAgilityPack;
using ScraperWeb.API.Models;
using ScraperWeb.API.Services.Interfaces;

namespace ScraperWeb.API.Services
{
    public class Scraper : IScraper
    {
        /*
         * Classe responsável por fazer o scraping dos resultados
         * da busca no google, sem o uso de API's externas
         */
        public List<Resultado> Buscar(string assunto, int? paginas = 10)
        {
            // Cria-se uma lista de resultados
            List<Resultado> resultado = new();

            // Laço para contagem de páginas
            for (int i = 1; i <= paginas; i++)
            {
                
                // URL base do google search onde: passa-se a busca, o número de resultados, e o total de páginas a serem carregadas
                string url = $"https://www.google.com/search?q={assunto}&num=15000&start={(i - 1) * 10}";

                // Carrega-se o componente HtmlAgilityPack -> https://html-agility-pack.net
                HtmlWeb web = new HtmlWeb();

                // Aqui faz-se uso de qual tipo de navegador está fazendo a requisição (Chrome)
                web.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.36";

                // Delay para evitar bloqueio temporario de IP
                Thread.Sleep(15000);

                // Cria-se o objeto com o html completo
                var htmlDoc = web.Load(url);

                // Carrega o nó pai onde terá todos os nós filhos
                HtmlNodeCollection nos = htmlDoc.DocumentNode.SelectNodes("//div[@class='yuRUbf']");

                try
                {
                    // Laço para criar a lista com os objetos de resultados
                    foreach (var tag in nos)
                    {
                        var dados = new Resultado();

                        // Dentro da tag <a> encontra-se o atributo href="http://url"
                        dados.Url = tag.Descendants("a").FirstOrDefault().Attributes["href"].Value;

                        // Dentro da tag <h3> encontra-se o texto do título
                        dados.Titulo = tag.Descendants("h3").FirstOrDefault().InnerText;

                        // Adiciona o objeto na lista 
                        resultado.Add(dados);
                    }

                    // Retorno dos resultados
                    return resultado;
                }
                catch (Exception ex)
                {
                    resultado = null;
                }
            }

            return resultado;
        }
    }
}
