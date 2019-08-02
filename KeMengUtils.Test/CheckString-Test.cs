using KeMengUtils.RegexHelper;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace KeMengUtils.Test
{
    public class CheckString_Test
    {
        [Theory]
        [InlineData("你好", true)]
        [InlineData("你好a", false)]
        [InlineData("", false)]
        public void IsChineseChTest(string word, bool exp)
        {
            var result = CheckString.GetInstance().IsChineseCh(word);
            Assert.Equal(exp, result);
        }

        [Theory]
        [InlineData("abababa,", "a", 4)]
        [InlineData("abababa,", "b", 3)]
        [InlineData("abababa,", ",", 1)]
        public void GetStringCountTest(string input, string compare, int times)
        {
            int result = CheckString.GetInstance().GetStringCount(input, compare);
            Assert.Equal(times, result);
        }
        [Theory]
        [InlineData("http://www.jushuitan.com")]
        [InlineData("https://www.jushuitan.com")]
        [InlineData("http://a.jushuitan.com")]
        [InlineData("https://a.jushuitan.net")]
        [InlineData("chrome://a.jushuitan.net")]
        public void IsURLTest(string url)
        {
            var result = CheckString.GetInstance().IsURL(url);
            Assert.True(result);
        }
    }
}
