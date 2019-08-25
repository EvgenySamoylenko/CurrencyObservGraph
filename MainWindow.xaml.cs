using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Globalization;
using System.Windows.Threading;

namespace WpfSampleBasicChart
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<LineSeries> mydata = new ObservableCollection<LineSeries>();
        LineSeries MySeries = new LineSeries();
        DispatcherTimer timer = new DispatcherTimer();

        //static readonly string DataSource = File.ReadAllText("Data\\DataSourceConfig.txt");
        string dataSourceType = null;
        ObservationManager ObsManage = null;
        // ObservManagerWebRequestEobot ObsManage = new ObservManagerWebRequestEobot().;
        //ObservationManagerFromDataBase ObsManage = new ObservationManagerFromDataBase();
        // ObservationManagerFromFile ObsManage = new ObservationManagerFromFile();

        int IndexOfCurrentObservation = -1;
        readonly int GraphLeght = 50;
        int timeTickLenInMilliSec = 9000;

        public MainWindow()
        {
            InitializeComponent();
            MyChart.ItemsSource = mydata;
            dataSourceType = File.ReadAllText("Data\\DataSourceConfig.txt");
            ObsManage = GetObservationManager(dataSourceType);
        }

        static readonly string srcName4File = "CSVFile";
        static readonly string srcName4Eobot = "EOBotWebSource";
        static readonly string srcName4SqlDb = "SQLDBSource";

        static private ObservationManager GetObservationManager(String SourceName)
        {
            ObservationManager ObsMan = null;

            if (SourceName == srcName4File)
            {
                ObsMan = new ObservationManagerFromFile(); // // item index load from file csv
            }
            else if (SourceName == srcName4Eobot)
            {
                ObsMan = new ObservManagerWebRequestEobot(); // item index load from websource
            }
            else if (SourceName == srcName4SqlDb)
            {
                ObsMan = new ObservationManagerFromDataBase(); //item index load form sql db
            }
            return ObsMan;
        }

        public void btnNewCurve_Click(object sender, RoutedEventArgs e)
        {
           ObsManage.Initialize();
        }

        void ShowData(int lastToShow,int SizeOfGraph)
        {
            if (lastToShow >= ObsManage.ObservationCount)
                return; //out of range, if statment true, then return false and do nothing
            int countOfObservationToAdd = lastToShow - IndexOfCurrentObservation;
            if (countOfObservationToAdd == 0)
                return; // nothing to do, everything is shown

            if (IndexOfCurrentObservation < lastToShow && MySeries.MyData.Count + countOfObservationToAdd <= SizeOfGraph)
            {
                if (MySeries.MyData.Count + countOfObservationToAdd <= SizeOfGraph)
                {
                    for (int i = 0; i < countOfObservationToAdd; i++)
                    {
                        IndexOfCurrentObservation++;
                        if (IndexOfCurrentObservation == 0)
                        {
                            mydata.Add(MySeries);
                        }
                        Observation obs = ObsManage.GetObservation(IndexOfCurrentObservation);
                        MySeries.MyData.Add(new DataPoint(){ Frequency = obs.TimeDiff,Value = obs.price });
                    }
                }
            }
            else
            {
                int iStartToShow = Math.Max(0, lastToShow - SizeOfGraph + 1);
                int iLimToShow = Math.Min(iStartToShow + SizeOfGraph, ObsManage.GetObservationCount);
                MySeries.MyData.Clear();
                for (int i = iStartToShow; i < iLimToShow; i++)
                {
                    var obs = ObsManage.GetObservation(i); 
                    var dataPnt = new DataPoint() { Frequency = obs.TimeDiff, Value = obs.price };
                    MySeries.MyData.Add(dataPnt);
                }
                IndexOfCurrentObservation = iLimToShow - 1;
            }

            if (mydata.Count == 0)
            {
                mydata.Add(MySeries);
            }
            MyChart.PlotAreaBorder_SizeChanged(this, null);
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            ObsManage.WebData(); // вызов метода WebData из ObservManagerWebRequestEobot
            ShowData(IndexOfCurrentObservation+1, GraphLeght);// вызов метода ShowData 
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
                timer.Stop();
            ShowData(IndexOfCurrentObservation-1, GraphLeght);
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            // System.Diagnostics.Process.Start(Application.ResourceAssembly.Location); // Application.Current.Shutdown();
            //MyChart.Resources.Clear(); // mydata.Clear();
            MySeries.MyData.Clear();
            MyChart.PlotAreaBorder_SizeChanged(this, null);
        }

        private void AutoRun(object sender, RoutedEventArgs e)
        {
            // Timer_Tick(null, null);
            //  return;
            if (timer.IsEnabled) { timer.Stop(); return; }
            //--------------------------ТАЙМЕР---------------------
            timeTickLenInMilliSec = ObsManage.timeTickLenInMilliSec;
            timer.IsEnabled = true;
            timer.Interval = TimeSpan.FromMilliseconds(timeTickLenInMilliSec);
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
            //--------------------------ТАЙМЕР---------------------*/
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void Forward(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
                timer.Stop();
            ShowData(IndexOfCurrentObservation + 1, GraphLeght);
        }

        private void ToStart(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
                timer.Stop();
            ObsManage.Initialize();
            ShowData(GraphLeght - 1, GraphLeght);
        }

        private void ToEnd(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
                timer.Stop();
            ObsManage.Initialize();
            ShowData(ObsManage.ObservationCount - 1, GraphLeght);
        }

        private void ComboDataSource_DropDownClosed(object sender, EventArgs e)
        {
            if (ComboDataSource.Text.ToString() == srcName4File)
            {
                File.WriteAllText("Data\\DataSourceConfig.txt", ComboDataSource.Text.ToString()); // // item index load from file csv
            }
            else if (ComboDataSource.Text.ToString() == srcName4Eobot)
            {
                File.WriteAllText("Data\\DataSourceConfig.txt", ComboDataSource.Text.ToString()); // // item index load from web
            }
            else if (ComboDataSource.Text.ToString() == srcName4SqlDb)
            {
                File.WriteAllText("Data\\DataSourceConfig.txt", ComboDataSource.Text.ToString()); // // item index load from db
            }
        }

    }
}
