using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FunnyTranslator.Models;
using FunnyTranslator.Services;

namespace FunnyTranslator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITranslatorService _translatorService;

        public HomeController(ILogger<HomeController> logger, ITranslatorService translatorService)
        {
            _logger = logger;
            _translatorService = translatorService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _logger.LogInformation("View created");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(MessageViewModel messageViewModel)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation($"text given: {messageViewModel.Text}");

                var message = await _translatorService.YodaTranslate(messageViewModel);

                _logger.LogInformation($"text translated: {message}");

                ViewData["Message"] = message;
            }
            return View();
        }
    }
}
