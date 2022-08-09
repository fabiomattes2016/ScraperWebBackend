using HtmlAgilityPack;
using ScraperWeb.API.Models;

namespace ScraperWeb.API.Services
{
    public class Scraper
    {
        /*
         * Classe responsável por fazer o scraping dos resultados
         * da busca no google, sem o uso de API's externas
         */
        public List<Resultado> Buscar(string termo, int? paginas = 100)
        {
            // Cria-se uma lista de resultados
            List<Resultado> resultado = new();

            // Laço para contagem de páginas
            for (int i = 1; i <= paginas; i++)
            {

                // URL base do google search onde: passa-se a busca, o número de resultados, e o total de páginas a serem carregadas
                string url = $"https://www.google.com/search?q={termo}&oq={termo}&num=100000&aqs=chrome..69i57j46i175i199i512j0i512l3j69i60l3.2413j0j7&sourceid=chrome&ie=UTF-8&start={(i - 1) * 10}";
                //string url = $"http://webcache.googleusercontent.com/search?q=cache:{termo}&num=100000&start={(i - 1) * 10}";

                // Carrega-se o componente HtmlAgilityPack -> https://html-agility-pack.net
                HtmlWeb web = new HtmlWeb();

                // Aqui faz-se uso de qual tipo de navegador está fazendo a requisição (Chrome)
                web.UserAgent = "User-Agent:Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/103.0.0.0 Safari/537.36";
                web.UsingCacheIfExists = true;

                // Delay para evitar bloqueio temporario de IP
                Thread.Sleep(15000);

                // Cria-se o objeto com o html completo
                var htmlDoc = web.Load(url);

                try
                {
                    // Carrega o nó pai onde terá todos os nós filhos
                    HtmlNodeCollection nos = htmlDoc.DocumentNode.SelectNodes("//div[@class='yuRUbf']");

                    // Laço para criar a lista com os objetos de resultados
                    foreach (var tag in nos)
                    {
                        // Dentro da tag <a> encontra-se o atributo href="http://url"
                        // Dentro da tag <h3> encontra-se o texto do título
                        var dados = new Resultado(
                            termo, 
                            tag.Descendants("h3").FirstOrDefault().InnerText,
                            tag.Descendants("a").FirstOrDefault().Attributes["href"].Value
                        );

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
