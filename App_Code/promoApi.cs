using System;
using System.Data;
using System.Data.SqlClient;
/// <summary>
/// Summary description for promoApi
/// </summary>
public class promoApi
{
    public static double flatAmt = 0;
    public static double perDis = 0;
    public static double promoAmount(string promocode, double samount, string applicableto, DateTime bdate)
    {
        int promAmt = 0;
        if (checkAvail(promocode, samount, applicableto, bdate))
        {
            var cn = new SqlConnection(Program.Connection);
            try
            {
                if (flatAmt == 0)
                {
                    promAmt = (int)(samount * (perDis / 100));
                    samount = samount - promAmt;
                }
                else
                {
                    samount = samount - flatAmt;
                }
                return samount;
            }
            catch (Exception ex)
            {
                return samount;
            }
        }
        return samount;
    }
    public static bool checkAvail(string promocode, double samount, string applicableto, DateTime bdate)
    {
        var cn = new SqlConnection(Program.Connection);
        try
        {
            cn.Open();
            var cmd =
                new SqlCommand(
                    "select * from promoSetting where promocode='" + promocode + "' and  status='Active' and applicableto='" + applicableto + "' and minamt<=" + samount + " and maxamt>=" + samount + " and bstdt>='" + bdate.ToString("dd-MMM-yyyy") + "' and benddt<='" + bdate.ToString("dd-MMM-yyyy") + "'", cn);
            var dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                SqlCommand cmd1;
                if (dr[7].ToString() == "0")
                {
                    cmd1 =
                   new SqlCommand(
                       "select * from promoSetting where getdate()>=stdt and gatedate()<=enddt", cn);
                    //var dr1 = cmd.ExecuteReader();
                }
                else if (dr[3] == null)
                {
                    cmd1 =
               new SqlCommand(
                   "select * from promoSetting where nooflimit<>anooflimit", cn);
                    //var dr1 = cmd.ExecuteReader();
                }
                else
                {
                    cmd1 =
                   new SqlCommand(
                       "select * from promoSetting where getdate()>=stdt and getdate()<=enddt and nooflimit<>anooflimit and slno='" + dr[0].ToString() + "'", cn);
                }
                dr.Close();
                var dr1 = cmd1.ExecuteReader();
                if (dr1.HasRows)
                {
                    dr1.Read();
                    flatAmt = Convert.ToDouble(dr1[12].ToString());
                    perDis = Convert.ToDouble(dr1[13].ToString());
                    return true;
                }
            }
            cn.Close();
            return false;
        }
        catch (Exception ex)
        {
            if (cn.State == ConnectionState.Open)
                cn.Close();
            return false;
        }
    }
}