using ScraperWeb.API.Models;

namespace ScraperWeb.API.Services.Interfaces
{
    public interface IScraper
    {
        List<Resultado> Buscar(string assunto, int paginas, int resultados);
    }
}
