using FunnyTranslator.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FunnyTranslator.Services
{
    public interface ITranslatorService
    {
        Task<string> YodaTranslate(MessageViewModel messageViewModel);
    }
    public class TranslatorService : ITranslatorService
    {
        public async Task<string> YodaTranslate(MessageViewModel messageViewModel)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri("https://api.funtranslations.com/translate/");

            var uri = string.Format("https://api.funtranslations.com/translate/yoda.json?text={0}",
                                                      Uri.EscapeDataString(messageViewModel.Text));

            var response = await client.GetAsync(uri);

            var json = await response.Content.ReadAsStringAsync();

            var message = JsonConvert.DeserializeObject<Translation>(json);

            return message.Contents.Translated;
        }
    }
}
