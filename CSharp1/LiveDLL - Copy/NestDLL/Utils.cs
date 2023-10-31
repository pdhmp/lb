using System;
using System.Collections.Generic;
using System.Text;

namespace LiveDLL
{
    public class Utils
    {
        public static DateTime NullDate
        {
            get
            {
                return new DateTime(1850, 01, 01);
            }
        }


        public static TimeSpan Return_TimeZone()
        {
            TimeZone localZone = TimeZone.CurrentTimeZone;
            DateTime currentDate = DateTime.Now;
            DateTime currentUTC = localZone.ToUniversalTime(currentDate);
            TimeSpan currentOffset = localZone.GetUtcOffset(currentDate);

            return currentOffset;
        }

        public static DateTime ParseToDateTime(object strValue)
        {
            if (DBNull.Value.Equals(strValue))
            {
                return new DateTime(1900, 01, 01);
            }
            else if (String.IsNullOrEmpty(Convert.ToString(strValue)))
            {
                return new DateTime(1900, 01, 01);
            }
            else
            {
                return DateTime.Parse(strValue.ToString());
            }
        }

        public static double ParseToDouble(object strValue)
        {
            if (DBNull.Value.Equals(strValue))
            {
                return 0;
            }
            else if (String.IsNullOrEmpty(Convert.ToString(strValue)))
            {
                return 0;
            }
            else
            {
                return double.Parse(strValue.ToString());
            }
        }

        public static double ParseToDouble(object strValue, int retValue)
        {
            if (DBNull.Value.Equals(strValue))
            {
                return retValue;
            }
            else if (String.IsNullOrEmpty(Convert.ToString(strValue)))
            {
                return retValue;
            }
            else
            {
                return double.Parse(strValue.ToString());
            }
        }

        public static string GetTableName(double Id_Ticker)
        {

            string SQLString = "SELECT IdPriceTable FROM NESTSRV06.NESTDB.dbo.Tb001_Securities WHERE IdSecurity=" + Id_Ticker;

            using (newNestConn thisConn = new newNestConn())
            {
                string curType = thisConn.Execute_Query_String(SQLString);

                switch (curType)
                {
                    case "1": return "Tb050_Preco_Acoes_Onshore"; break;
                    case "2": return "Tb051_Preco_Acoes_Offshore"; break;
                    case "3": return "Tb058_Precos_Moedas"; break;
                    case "4": return "Tb053_Precos_Indices"; break;
                    case "5": return "Tb059_Precos_Futuros"; break;
                    case "6": return "Tb057_Precos_Commodities"; break;
                    case "7": return "Tb054_Precos_Opcoes"; break;
                    case "8": return "Tb052_Precos_Titulos"; break;
                    case "9": return "Tb060_Preco_Caixa"; break;
                    case "10": return "Tb056_Precos_Fundos"; break;
                    default: return "";
                }
            }
        }

        public static double GetUSDBRL()
        {
            string SQLString = "SELECT NESTDB.DBO.FCN_GET_PRICE_VALUE_ONLY(900,'" + DateTime.Today.ToString("yyyyMMdd") + "',1,0,0,0,1)";
            double USDBRL = double.NaN;

            using (newNestConn conn = new newNestConn())
            {
                USDBRL = conn.Return_Double(SQLString);
            }

            return USDBRL;
        }

        public static int DateTimeToUnixTimestamp(DateTime DateToConvert)
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0);

            int TimeZone = GetTimeZone(DateToConvert);
            DateTime UTCTime = DateToConvert.AddHours((-1) * TimeZone);

            TimeSpan TimeDiff = UTCTime - UnixEpoch;

            return (int)TimeDiff.TotalSeconds;
        }

        public static int DateTimeToUnixTimestamp2(DateTime DateToConvert)
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0);

            int TimeZone = GetTimeZone(DateToConvert);
            DateTime UTCTime = DateToConvert.AddHours(TimeZone);

            TimeSpan TimeDiff = UTCTime - UnixEpoch;

            return (int)TimeDiff.TotalSeconds;
        }

        public static DateTime UnixTimestampToDateTime(int UnixTimestamp)
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0);
            DateTime UTCTime = UnixEpoch.AddSeconds(UnixTimestamp);

            int TimeZone = GetTimeZone(UTCTime);
            DateTime LocalTime = UTCTime.AddHours(TimeZone);

            int NewTimeZone = GetTimeZone(LocalTime);

            if (TimeZone != NewTimeZone)
            {
                LocalTime = UTCTime.AddHours(NewTimeZone);
            }

            return LocalTime;
        }

        public static int GetTimeZone(DateTime DateRef)
        {
            int TimeZoneDiff = -3;

            if (
                (DateRef >= (new DateTime(2008, 10, 19)) && DateRef < (new DateTime(2010, 02, 15))) ||     //Horario Verao 2008/2009
                (DateRef >= (new DateTime(2009, 10, 18)) && DateRef < (new DateTime(2010, 02, 21))) ||     //Horario Verao 2009/2010
                (DateRef >= (new DateTime(2010, 10, 17)) && DateRef < (new DateTime(2011, 02, 20)))        //Horario Verao 2010/2011
               )
            {
                TimeZoneDiff = -2;
            }

            return TimeZoneDiff;
        }
    }
}
