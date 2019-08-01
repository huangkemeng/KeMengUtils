using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using KeMengUtils.DataHelper;
using KeMengUtils.ThrowEx;
using System.Linq.Expressions;

namespace KeMengUtils.Test
{
    public class BasicDataConvert_Test
    {

        [Theory]
        [InlineData("2019年1月1日")]
        [InlineData("2019年1月1日 01:01:01")]
        [InlineData("01:01:01")]
        [InlineData("01:01")]
        [InlineData("3月20日")]
        [InlineData("3月")]
        [InlineData("20日")]
        [InlineData("2019年01月01日")]
        [InlineData("2019-1-1")]
        [InlineData("2019/12/31")]
        [InlineData("2019.12.31")]
        [InlineData("2019.12.31. 1:1:1")]
        [InlineData("2019.12-31")]
        [InlineData("2019-12.31")]
        [InlineData("2019/12-31")]
        [InlineData("2019/12.31")]
        [InlineData("12.31")]
        [InlineData("12/31")]
        [InlineData("12-31")]
        public void ToDateTimeNoThrowTest(string datettime)
        {
            var result = datettime.ToDateTime();
            Assert.IsType<DateTime>(result);
        }

        [Theory]
        [InlineData("2019/11/31")]
        [InlineData("2019/2/29")]
        [InlineData("x")]
        public void ToDateTimeThrowTest(string datettime)
        {
            Assert.Throws<StringIsNotTartgetFormatException>(() => { datettime.ToDateTime(); });
        }

        [Theory]
        [InlineData(true)]
        [InlineData(true, null)]
        [InlineData(true, "", "")]
        [InlineData(true, "   ", "    ")]
        [InlineData(false, "x", "")]
        [InlineData(false, "x", "x")]
        public void StringsIsNullOrWhiteTest(bool exp, params string[] strs)
        {
            bool result = TypeConvert.StringsIsNullOrWhite(strs);
            Assert.Equal(exp, result);
        }


        [Theory]
        [InlineData("1", 1)]
        [InlineData("1.1", 1.1)]
        [InlineData("0", 0)]
        public void ToDecimalTest(string obj, decimal dec)
        {
            try
            {
                decimal result = obj.ToDecimal();
                Assert.StrictEqual<decimal>(dec, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Fact]
        public void FirstDayOfMonthTest()
        {
            DateTime t1 = new DateTime(2019, 8, 20).FirstDayOfMonth();
            DateTime t1exp = new DateTime(2019, 8, 1);
            DateTime t2 = new DateTime(2019, 8, 20).LastDayOfMonth();
            DateTime t2exp = new DateTime(2019, 8, 31);
            Assert.Equal(t1exp, t1);
            Assert.Equal(t2exp, t2);
        }

        [Fact]
        public void ToJArrayTest()
        {
            List<string> a = new List<string> { "1", "2" }; ;
            var b = a.ToJArray();
        }
    }
}
