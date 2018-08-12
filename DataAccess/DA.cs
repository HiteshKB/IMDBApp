using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DA
    {
        public static DataTable ExecStoreProc(string storeProc)
        {
            DataTable table = new DataTable();
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStringDb"].ConnectionString))
                using (var cmd = new SqlCommand(storeProc, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(table);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return table;
        }

        public static int InsertActor(string name, string sex, string dOB, string bIO, string actorOrProducer = "actor")
        {
            int res = -1;
            string sp = string.Empty;
            if (actorOrProducer == "producer")
                sp = "spInsertProducer";
            else
                sp = "spInsertActor";
            try
            {
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStringDb"].ConnectionString))
                using (var cmd = new SqlCommand(sp, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name.Trim();
                    cmd.Parameters.Add("@sex", SqlDbType.VarChar).Value = sex.Trim();
                    cmd.Parameters.Add("@dob", SqlDbType.VarChar).Value = dOB.Trim();
                    cmd.Parameters.Add("@bio", SqlDbType.VarChar).Value = bIO.Trim();
                    cmd.Connection = con;
                    con.Open();
                    res = cmd.ExecuteNonQuery();
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int InsertMovie(string name, string year, string plot, string poster, string producer, out int movieId)
        {
            int res = -1;
            try
            {
                movieId = 0;
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStringDb"].ConnectionString))
                using (var cmd = new SqlCommand("spInsertMovie", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = name.Trim();
                    cmd.Parameters.Add("@year", SqlDbType.SmallInt).Value = year.Trim();
                    cmd.Parameters.Add("@plot", SqlDbType.VarChar).Value = plot.Trim();
                    cmd.Parameters.Add("@poster", SqlDbType.VarChar).Value = poster.Trim();
                    cmd.Parameters.Add("@prdcrID", SqlDbType.SmallInt).Value = producer.Trim();

                    SqlParameter outParam = new SqlParameter("@movieID", SqlDbType.SmallInt);
                    outParam.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(outParam);
                    cmd.Connection = con;
                    con.Open();
                    res = cmd.ExecuteNonQuery();
                    movieId =int.Parse(cmd.Parameters["@movieID"].Value.ToString());
                    return res;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int InsertMovieActors(int movieId, string actors)
        {
            int res = 0;
            string[] lstActors = actors.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                foreach (string str in lstActors)
                {
                    using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStringDb"].ConnectionString))
                    using (var cmd = new SqlCommand("spMapMovieActor", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@movieID", SqlDbType.SmallInt).Value = movieId;
                        cmd.Parameters.Add("@actorID", SqlDbType.SmallInt).Value = str.Trim();
                        cmd.Connection = con;
                        con.Open();
                        res += cmd.ExecuteNonQuery();
                    }
                };
                if (res == lstActors.Length)
                    return res;
                else
                    return -1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
