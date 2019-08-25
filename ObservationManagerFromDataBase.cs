using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Data.SqlClient;

namespace WpfSampleBasicChart
{
    class ObservationManagerFromDataBase : ObservationManager
    {
        public ObservationManagerFromDataBase()
        {
            timeTickLenInMilliSec = 1000;
        }
        public override void WebData() { Initialize(); }

        public override void Initialize()
        {
            try
            {
                SqlConnection Connect = new SqlConnection(Properties.Settings.Default.SQLConnectionString);
                Connect.Open();

                SqlCommand command = new SqlCommand(Connect.ConnectionString, Connect);
                command.CommandText = "SELECT * FROM Observations";
                SqlDataReader DataRead = command.ExecuteReader();
                DataRead.Read();
                foreach (var data in DataRead)
                {
                    var obs = new Observation();
                    obs.dt = Convert.ToDateTime(DataRead.GetValue(1));
                    obs.price = Convert.ToDouble( DataRead.GetValue(3) );
                    observs.Add(obs);
                }
            } catch (Exception except) { MessageBox.Show(except.ToString()); }

            //------------------вычетание промежутка времени между 1 и второй датой---------------------
            for (int step = 0; step < observs.Count - 1; step++)
            {
                var obs = observs[step];
                obs.TimeDiff = obs.dt - observs[0].dt;
            }
            //------------------вычетание промежутка времени между 1 и второй датой---------------------
        }
    }
}
