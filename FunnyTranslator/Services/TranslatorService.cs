using FunnyTranslator.Models;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<TranslatorService> _logger;

        public TranslatorService(ILogger<TranslatorService> logger)
        {
            _logger = logger;
        }
        public async Task<string> YodaTranslate(MessageViewModel messageViewModel)
        {
            var client = new HttpClient();

            client.BaseAddress = new Uri("https://api.funtranslations.com/translate/");

            _logger.LogInformation($"connected to: {client.BaseAddress}");

            var uri = string.Format("https://api.funtranslations.com/translate/yoda.json?text={0}",
                                                      Uri.EscapeDataString(messageViewModel.Text));

            _logger.LogInformation($"Sending request to: {uri}");

            var response = await client.GetAsync(uri);

            var json = await response.Content.ReadAsStringAsync();

            var message = JsonConvert.DeserializeObject<Translation>(json);

            return message.Contents.Translated;
        }
    }
}
