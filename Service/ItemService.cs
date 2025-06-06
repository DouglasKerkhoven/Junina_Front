namespace curitibano.blazor.junino.Service
{
    public class ItemService
    {
        private readonly HttpClient _http;
        private string url { get; set; } = Environment.GetEnvironmentVariable("URL_JUNINA_API/");
        public ItemService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Item>> ObterTodos()
        {
            return await _http.GetFromJsonAsync<List<Item>>($"{url}api/item");
        }

        public async Task<Item?> ObterPorId(int id)
        {
            return await _http.GetFromJsonAsync<Item>($"{url}api/item/id/{id}");
        }

        public async Task Adicionar(Item item)
        {
            await _http.PostAsJsonAsync($"{url}api/item", item);
        }

        public async Task Excluir(int id)
        {
            await _http.DeleteAsync($"{url}api/item/id/{id}");
        }

        public async Task Atualizar(Item item)
        {
            // PATCH requer o uso de HttpRequestMessage
            await _http.PatchAsJsonAsync($"{url}api/item/id/{item.Id}", item);
        }

        public async Task DarBaixa(int id, int qtd,int pagamentoId)
        {
            /*var request = new HttpRequestMessage(HttpMethod.Patch, $"api/item/baixa/")
            {
                Content = JsonContent.Create(command)
            };*/
            await _http.PatchAsJsonAsync($"{url}api/item/baixa", new { ItemId = id , QtdVendida = qtd, FormaId = pagamentoId});



        }
    }


    public class Item
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }

    }

    public class BaixarItemCommand
    {
        public int Id { get; set; }
    }
}
