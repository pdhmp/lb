using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NestDLL;

namespace OppOp
{
    class Utils
    {
        public static List<DateTime> getBovOpenDates(DateTime IniDate)
        {
            List<DateTime> Dates = new List<DateTime>();
            DataTable Tb;
            string SqlQuery =
                "SELECT DISTINCT(SrDate) " +
                "FROM NESTDB.dbo.Tb053_Precos_Indices (NOLOCK) " +
                "WHERE IdSecurity = 1073 AND " +
                "    SrType = 1 AND " +
                "    SOURCE = 1";

            using (newNestConn conn = new newNestConn())
            {
                Tb = conn.Return_DataTable(SqlQuery);
            }

            for (int i = 0; i < Tb.Rows.Count; i++)
            {
                if (DateTime.Parse(Tb.Rows[i][0].ToString()) >= IniDate)
                    Dates.Add(DateTime.Parse(Tb.Rows[i][0].ToString()));
            }

            return Dates;
        }
    }
}
