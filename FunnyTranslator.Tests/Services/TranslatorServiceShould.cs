using FunnyTranslator.Models;
using FunnyTranslator.Services;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace FunnyTranslator.Tests
{
    public class TranslatorServiceShould
    {
        [Fact]
        public async Task Translate_Yoda_Correctly()
        {
            //given
            var model = new MessageViewModel()
            {
                Text = "this is war"
            };

            //when
            var mock = new Mock<ILogger<TranslatorService>>();

            ILogger<TranslatorService> logger = mock.Object;

            logger = Mock.Of<ILogger<TranslatorService>>();

            var sut = new TranslatorService(logger);

            var result = await sut.YodaTranslate(model);

            //then
            Assert.Equal("War,  this is", result);
        }
    }
}
