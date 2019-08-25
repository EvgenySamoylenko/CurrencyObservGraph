using System;
using System.Windows;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Threading;

using static WpfSampleBasicChart.ChartControl.BasicChart;
using static WpfSampleBasicChart.MainWindow;

namespace WpfSampleBasicChart
{
    class ObservationManagerFromFile : ObservationManager
    {

        public ObservationManagerFromFile()
        {
            timeTickLenInMilliSec = 1000;
        }

        public override void Initialize()
        {
            currentFileName = "Data\\Data1.csv";

            if (!string.IsNullOrEmpty(lastLoadedFile) && currentFileName == lastLoadedFile)
                return; // нет данных

            observs.Clear();
            string[] data1 = File.ReadAllLines(currentFileName);
            lastLoadedFile = currentFileName;
            //--------------------------------------заполнение списка данными---------------------------------
            foreach (var line in data1)
            {
                var strValues = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                string strDate = strValues[0];
                string strPrice = strValues[1];
                var obs = new Observation();
                obs.dt = StrToDt(strDate);
                obs.price = StrToDouble(strPrice);
                observs.Add(obs);
            }
            //--------------------------------------заполнение списка данными---------------------------------

            //------------------вычетание промежутка времени между 1 и второй датой---------------------
            for (int step = 0; step < observs.Count - 1; step++)
            {
                var obs = observs[step];
                obs.TimeDiff = obs.dt - observs[0].dt;
            }
            //------------------вычетание промежутка времени между 1 и второй датой---------------------
        }

        public override void WebData() { }
    }
}
