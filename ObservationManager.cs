using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WpfSampleBasicChart
{
    class ObservationManager
    {
        protected List<Observation> observs = new List<Observation>();
        public int ObservationCount => observs.Count;
        public int timeTickLenInMilliSec = 9000;

        public string currentFileName;
        public string lastLoadedFile = null;

        static public DateTime StrToDt(string dtVal)
        {
            DateTime value;
            dtVal = dtVal.Trim();
            bool bParsedOk = DateTime.TryParse(dtVal, CultureInfo.InvariantCulture, DateTimeStyles.None, out value);
            if (!bParsedOk)
            {
                return DateTime.MinValue;
            }
            return value;
        }

        static public double StrToDouble(string dblValStr)
        {
            double value;
            dblValStr = dblValStr.Trim();
            bool bParsedOk = double.TryParse(dblValStr, NumberStyles.AllowDecimalPoint, CultureInfo.CreateSpecificCulture("en-US"), out value); //CultureInfo.InvariantCulture
            if (!bParsedOk)
            {
                return double.MinValue;
            }
            return value;
        }

        public virtual Observation GetObservation(int indexOfCurrentObservation)
        {
            return observs[indexOfCurrentObservation];
        }

        public int GetObservationCount => observs.Count;

        public virtual void AddObservation(Observation obs)
        {
            observs.Add(obs);
        }

        public virtual void Initialize() { }
        public virtual void WebData() { }

    }
 }
