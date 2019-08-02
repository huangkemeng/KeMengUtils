using KeMengUtils.RegexHelper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Xunit;

namespace KeMengUtils.Test
{
    public class CommonRegexs_Test
    {
        [Theory]
        [InlineData("123123", true)]
        [InlineData("123213232321321324808923918720839712364710", true)]
        [InlineData(".123213232321321324808923918720839712364710", false)]
        [InlineData("!123213232321321324808923918720839712364710", false)]
        [InlineData("x123213232321321324808923918720839712364710", false)]
        [InlineData("123213232321321324808923918720839712364710 ", false)]
        public void OnlyNumberTest(string nums, bool exp)
        {
            Regex regex = CommonRegexs.GetInstance().OnlyNumber();
            bool result = regex.IsMatch(nums);
            Assert.StrictEqual(exp, result);
        }

        [Theory]
        [InlineData("12345", 5, true)]
        [InlineData("123451", 6, true)]
        [InlineData("x123451", 6, false)]
        [InlineData("12!", 2, false)]
        [InlineData("", 0, true)]
        [InlineData("9999", 5, false)]
        [InlineData("987654", 5, false)]
        public void OnlyMNumberTest(string str, int m, bool exp)
        {
            Regex regex = CommonRegexs.GetInstance().OnlyMNumber(m);
            bool result = regex.IsMatch(str);
            Assert.StrictEqual(exp, result);
        }

        [Theory]
        [InlineData("123123", 5, true)]
        [InlineData("123456", 6, true)]
        [InlineData("123456", 7, false)]
        [InlineData("x123123", 6, false)]
        public void AtLeastMNumberTest(string str, int m, bool exp)
        {
            Regex regex = CommonRegexs.GetInstance().AtLeastMNumber(m);
            bool result = regex.IsMatch(str);
            Assert.StrictEqual(exp, result);
        }

        [Theory]
        [InlineData("123123", 1, 6, true)]
        [InlineData("123123", 1, 7, true)]
        [InlineData("123123", 6, 6, true)]
        [InlineData("1231234", 6, 6, false)]
        [InlineData("123123", 7, 6, false)]
        public void OnlyNumberBetweenMNTest(string str, int m, int n, bool exp)
        {
            if (m > n)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    CommonRegexs.GetInstance().OnlyNumberBetweenMN(m, n);
                });
            }
            else
            {
                Regex regex = CommonRegexs.GetInstance().OnlyNumberBetweenMN(m, n);
                bool result = regex.IsMatch(str);
                Assert.StrictEqual(exp, result);
            }
        }


    }
}
