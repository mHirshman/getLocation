using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        public int getDistance(string soruce, string destination)
        {
            SqlConnection con = null;
            int distanceId, distanceKM = 0;

            try
            {
                con = new SqlConnection("Server=(LocalDb)\\v11.0;Initial Catalog=DestentionDB.mdf;Integrated Security=True;");
                //    SqlConnection con = new SqlConnection("Server=localhost\SQLExpress;"
                // + "Database=Database1;");

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                DataTable dt = getDistanceByDB(con, soruce, destination);

                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    distanceId = (int)dr[0];
                    distanceKM = (int)dr[1];
                }
                else
                {
                    distanceKM = getDistanceByGoogleMapAPI(soruce, destination);
                    updateDestinationInDB();
                }

            }
            catch
            {

            }
            finally
            {
                if (con != null && con.State != ConnectionState.Closed)
                {
                    con.Close();
                }

            }

            updateNumberOfSearchesInDB();
            return distanceKM;

        }

        private DataTable getDistanceByDB(SqlConnection con, string soruce, string destination)
        {

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();

            SqlCommand sqlCommand = con.CreateCommand();
            sqlCommand.CommandText = "select d.id, d.distanceKM from distance_of_destination dod " +
                "JOIN destination d1 ON d1.id = dod.destination1 " +
                "JOIN destination d2 ON d2.id = dod.destination2 " +
                "WHERE (d1.name = @sourceName && d2.name = @destinatioName) " +
                "OR (d2.name = @sourceName && d1.name = @destinatioName) ";
            sqlCommand.Parameters.Add("@sourceName", SqlDbType.NVarChar, 40, soruce);
            sqlCommand.Parameters.Add("@destinatioName", SqlDbType.NVarChar, 40, destination);

            sqlDataAdapter.SelectCommand = sqlCommand;

            DataTable dt = new DataTable();

            sqlDataAdapter.Fill(dt);

            return dt;
        }

        protected int getDistanceByGoogleMapAPI(string soruce, string destination)
        {
            ///get Data from googleMaps

            //string url = "http://maps.googleapis.com/maps/api/directions/json?origin=" + origin + "&destination=" + destination + "&sensor=false";
            //string requesturl = url;
            //string requesturl = @"http://maps.googleapis.com/maps/api/directions/json?origin=" + from + "&alternatives=false&units=imperial&destination=" + to + "&sensor=false";
            //string content = fileGetContents(requesturl);
            // JObject o = JObject.Parse(content);
            return 0;
        }

        protected void updateDestinationInDB()
        {

        }

        protected void updateNumberOfSearchesInDB()
        {

        }

        //protected string fileGetContents(string fileName)
        //{
        //    string sContents = string.Empty;
        //    string me = string.Empty;
        //    try
        //    {
        //        if (fileName.ToLower().IndexOf("http:") > -1)
        //        {
        //            System.Net.WebClient wc = new System.Net.WebClient();
        //            byte[] response = wc.DownloadData(fileName);
        //            sContents = System.Text.Encoding.ASCII.GetString(response);

        //        }
        //        else
        //        {
        //            System.IO.StreamReader sr = new System.IO.StreamReader(fileName);
        //            sContents = sr.ReadToEnd();
        //            sr.Close();
        //        }
        //    }
        //    catch { sContents = "unable to connect to server "; }
        //    return sContents;
        //}
    }
}
