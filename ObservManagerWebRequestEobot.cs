using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;
using System.Net;
using System.Data.SqlClient;

namespace WpfSampleBasicChart
{
    class ObservManagerWebRequestEobot : ObservationManager
    {
        //ObservationManagerFromFile ObsManageFromFile = new ObservationManagerFromFile();

        public ObservManagerWebRequestEobot()
        {
        }

        // Create a request for the URL.   
        static string ResponseFromServer;
       public override void WebData()
        {
            // If required by the server, set the credentials.
            WebRequest DataRequest = WebRequest.Create("https://www.eobot.com/api.aspx?supportedcoins=true&currency=USD");
            DataRequest.Credentials = CredentialCache.DefaultCredentials;
            // Get the response.  
            WebResponse DataResponse = DataRequest.GetResponse();
            // Display the status.  
            //Console.WriteLine(((HttpWebResponse)DataResponse).StatusDescription);  
            // Get the stream containing content returned by the server.  
            Stream dataStream = DataResponse.GetResponseStream();
            // Open the stream using a StreamReader for easy access.  
            try
            {
                StreamReader WebAnswerReader = new StreamReader(dataStream);
                // Read the content.  
                ResponseFromServer = WebAnswerReader.ReadToEnd();
                WebAnswerReader.Close();
                DataResponse.Close();
            }
            catch (Exception except)
            {
                MessageBox.Show(except.ToString());
                return;
            }

            string data1 = ResponseFromServer;
            if (string.IsNullOrEmpty(data1))
                return;

             var strValues = ResponseFromServer.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList(); //розделение строки на блоки с инфой валюты
            if (strValues.Count == 0)
                return;

                IdxOfPrice = strValues[0].IndexOf("Price:"); // начало сплита
            string strPrice = strValues[0].Substring(IdxOfPrice + "Price:".Length); //выделение цены сплитом из стороки 

            //--------------------------------------заполнение списка данными---------------------------------
            var obs = new Observation();
            obs.price = StrToDouble(strPrice);
            obs.TimeDiff = DateTime.Now.TimeOfDay;
            //CurrentData = GetDouble(ResponseFromServer, double.MinValue);
            observs.Add(obs);

            //--------------------------------------запись в бд скюл ---------------------------------

            SqlConnection Connect = new SqlConnection(Properties.Settings.Default.SQLConnectionString);
            try
            {
                Connect.Open();

                SqlCommand command = new SqlCommand(Connect.ConnectionString,Connect);

                //command.Parameters["@ObsDate"].Value = DateTime.Now.TimeOfDay;
                //command.Parameters["@Price"].Value = obs.price;
                command.Parameters.AddWithValue("@ObsDate", DateTime.Now);
                command.Parameters.AddWithValue("@Price", obs.price);
                command.CommandText = @"INSERT INTO Observations (ObsDate, Price) VALUES (@ObsDate, @Price)";
                command.ExecuteNonQuery();
            }
            catch (Exception except) { MessageBox.Show(except.ToString()); }
            finally { Connect.Close(); }

            //--------------------------------------запись в бд скюл ---------------------------------
        }

        /*public static double GetDouble(string valuePrm, double defaultValue)
         {
            string lengthToSkip = ResponseFromServer;
            double result;
            string value = valuePrm.Substring(Convert.ToInt16(lengthToSkip));
            //Try parsing in the current culture
            if (!double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out result) &&
                 //Then try in US english
                 !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                 //Then in neutral language
                 !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result))
             {
                 result = defaultValue;
             }
             return result;
         }*/

        public override Observation GetObservation(int indexOfCurrentObservation)
        {
              return observs[indexOfCurrentObservation];
        }

        public int IdxOfPrice;
        public override void Initialize()
        {
            IdxOfPrice = -1;
        }
    }
}
