# ScraperWebBackend

Micro serviço de web scraping de resultados do Google


Desenvolvido em .Net Core 6 e Minimal API.
Basta fazer o clone do código, compilar e rodar.

Obs: Há uma chamada de delay no código, pois se executarmos sequancialmente muitas requisições o Google entenderá
como requisições supeitas e irá bloquear por um período de tempo o ip das requisições de origem,
caso aconteça o IP do local onde a API está rodando dever ser atualizado.

Caso queira testar sem baixar: https://apiwebscraper.azurewebsites.net/search?assunto=temaaserpesquisado
