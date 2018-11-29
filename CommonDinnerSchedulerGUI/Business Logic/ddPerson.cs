using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDinnerSchedulerGUI.Business_Logic
{
    public class ddPerson
    {
        public string Name;
        public int weekdaysParticipatingIn;
        public int nTimesCook;
        public float cookWeekdaysRatio;

        public string weekdaysParticipatingInString { get { return weekdaysParticipatingIn.ToString(); } }
        public string nTimesCookString { get { return nTimesCook.ToString(); } }
        public string cookWeekdaysRatioString { get { return string.Format("{0:F1}", cookWeekdaysRatio); } }

        public ddPerson(string name, int wdp, int ntc)
        {
            Name = name;
            weekdaysParticipatingIn = wdp;
            nTimesCook = ntc;
            cookWeekdaysRatio = nTimesCook / weekdaysParticipatingIn;
        }
    }
}
