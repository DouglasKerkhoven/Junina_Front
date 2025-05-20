using System;

namespace curitibano.blazor.junino.Service
{
    public class VendasService
    {
        private readonly HttpClient _http;
        private string url { get; set; } = Environment.GetEnvironmentVariable("URL_JUNINA_API");
        public VendasService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Venda>> ObterTodos()
        {
            return await _http.GetFromJsonAsync<List<Venda>>($"{url}api/vendas");
        }
    }

    public class Venda
    {
        public int Id { get; set; }
        public int ItemId { get; set; }   
        public virtual Item Item { get; set; } = null!;
        public DateTime DataVenda { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }
    }
}
