using Forkorta.Biz;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Forkorta.Tests
{
    public class BizTests
    {
        string connectionString = "";

        [Fact]
        public void Is_Random_String_Generated()
        {
            var result = SubjectUrlUtil.GetShortUrl(8);
            Assert.NotNull(result);
        }
        [Fact]
        public void Is_Random_String_Generated_In_CorrectLength()
        {
            var length = 6;
            var result = SubjectUrlUtil.GetShortUrl(length);
            Assert.Equal(length, result.Length);
        }

        [Fact]
        public async Task Is_Shorting_Url_And_Write_To_Table()
        {
            var url = "https://www.microsoft.com/en-my/";
            SubjectUrl subjectUrl = new SubjectUrl(url, connectionString);
            var result = await subjectUrl.ShortenUrl();

            Assert.NotNull(result);
        }

        [Fact]
        public async Task Retrieve_Long_Url_For_Given_Short_Url()
        {
            var url = "MccLSngp";
            SubjectUrl subjectUrl = new SubjectUrl(url, connectionString);
            var result = await subjectUrl.DeshortenUrl();

            Assert.Equal("https://www.microsoft.com/en-my/", result);
        }
    }
}
