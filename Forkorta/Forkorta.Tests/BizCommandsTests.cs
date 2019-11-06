using Forkorta.Biz;
using Forkorta.Biz.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Forkorta.Tests
{
    public class BizCommandsTests
    {
        string connectionString = Environment.GetEnvironmentVariable("ForkortaTableStorageConnectionString", EnvironmentVariableTarget.User);

        [Fact]
        public async Task Short_Url_Command_Returns_Not_Null()
        {
            SubjectUrl url = new SubjectUrl("https://www.microsoft.com/en-my/", connectionString);
            ICommand command = new ShortUrlCommand(url, UrlAction.Short);
            var result = await command.ExecuteAction();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Deshort_Url_Command_Gives_Correct_Value()
        {
            var givenUrl = "MccLSngp";
            var expectedUrl = "https://www.microsoft.com/en-my/";
            SubjectUrl url = new SubjectUrl(givenUrl, connectionString);
            ICommand command = new ShortUrlCommand(url, UrlAction.Deshort);
            var result = await command.ExecuteAction();

            Assert.Equal(result, expectedUrl);
        }


    }
}
