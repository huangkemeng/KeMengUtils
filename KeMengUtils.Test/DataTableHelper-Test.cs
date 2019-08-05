using KeMengUtils.DataHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xunit;

namespace KeMengUtils.Test
{
    public class DataTableHelper_Test
    {
        public readonly DataTable dataTable;
        public DataTableHelper_Test()
        {
            DataTable table = new DataTable();
            table.Columns.Add("column");
            DataRow dr = table.NewRow();
            dr["column"] = "row";
            table.Rows.Add(dr);
            dataTable = table;
        }

        [Fact]
        public void TryGetValueTest()
        {
            if (dataTable.Rows[0].TryGetValue("column", out object value))
            {
                var v = value;
            }
        }

    }
}
