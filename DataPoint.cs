using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSampleBasicChart
{
    public class DataPoint:NotifierBase
    {
        private TimeSpan m_Frequency = new TimeSpan();
        public TimeSpan Frequency
        {
            get { return m_Frequency; }
            set
            {
                SetProperty(ref m_Frequency, value);
            }
        }

        private double m_Value = new double();
        public double Value
        {
            get { return m_Value; }
            set
            {
                SetProperty(ref m_Value, value);
            }
        }
    }
}
