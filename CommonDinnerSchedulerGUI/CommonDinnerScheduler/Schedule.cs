using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CommonDinnerScheduler
{
    public class Schedule
    {
        public List<DinnerDay> dinnerDays;
        public List<String> participants;
        public Dictionary<String, float> nDaysChefPrSignedUpDate;
        public Dictionary<String, int> nDaysSignedUpFor;

        public Schedule()
        {
            dinnerDays=new List<DinnerDay>();
            participants = new List<string>();
            nDaysSignedUpFor = new Dictionary<string, int>();
            nDaysChefPrSignedUpDate = new Dictionary<string, float>();
        }

        public void signPersonUpForDay(String name, DinnerDay day)
        {
            if(!participants.Contains(name))
            {
                participants.Add(name);
            }

            //Sign up person for day
            if(day.AddParticipant(name))
            {
                //Add to dictionary if not there already
                if (nDaysChefPrSignedUpDate.ContainsKey(name) == false)
                {
                    nDaysChefPrSignedUpDate.Add(name, 0);
                }

                //Add to dictionary if not there already
                if (nDaysSignedUpFor.ContainsKey(name) == false)
                {
                    nDaysSignedUpFor.Add(name, 1);
                }
                else
                {
                    //increment number of days signed up for
                    nDaysSignedUpFor[name]++;
                }
            }
        }

        public void update_nTimesChefPrSignedUpDate()
        {
            //Make temporary list of new data
            Dictionary<String, float> newValues = new Dictionary<string, float>();
            foreach(var name in nDaysChefPrSignedUpDate)
            {
                int timesChef = getNumberOfTimesPersonIsChef(name.Key);
                int daysSignedUpFor = nDaysSignedUpFor[name.Key];
                if(daysSignedUpFor==0)
                {
                    //set very high
                    nDaysChefPrSignedUpDate[name.Key] = 1000;
                }
                else
                {
                    newValues[name.Key] = (float)timesChef / (float)daysSignedUpFor;
                }
                
            }

            //Actually update data
            foreach(var name in newValues)
            {
                nDaysChefPrSignedUpDate[name.Key] = newValues[name.Key];
            }
        }

        public bool addDayToSchedule(DayOfWeek weekday,DateTime startDate, DateTime endDate)
        {
            //Check whether day is already added
            if (dinnerDays.All(x => x.dayOfWeek != weekday))
            {
                //If day is not in schedule yet, add it and return true, indicating that day was added.
                dinnerDays.Add(new DinnerDay(weekday,startDate,endDate));
                return true;
            }
            else
            {
                //Return false, indicating that day was not added, since it was already in list
                return false;
            }
        }

        public int getNumberOfTimesPersonIsChef(string name)
        {
            int nDays = 0;
            foreach (DinnerDay weekDay in dinnerDays)
            {
                foreach (CommonDinnerDate date in weekDay.specificDates)
                {
                    if (date.responsiblePerson.Equals(name))
                    {
                        nDays++;
                    }
                }
            }

            return nDays;
        }

        public bool checkIfAnyoneCooks2WeeksAfterAnother()
        {
            //Add all specific dates to one list
            List<CommonDinnerDate> allSpecificDates = new List<CommonDinnerDate>();
            foreach (DinnerDay weekday in dinnerDays)
            {
                foreach (CommonDinnerDate date in weekday.specificDates)
                {
                    allSpecificDates.Add(date);
                }
            }

            //Check for all dates that no dates in the same week or week before or after has the same chef.
            //If they do, return true.
            foreach (CommonDinnerDate Date in allSpecificDates)
            {
                if(allSpecificDates.Any(x => x.responsiblePerson==Date.responsiblePerson && x.date!=Date.date && (x.date-Date.date).TotalDays<14))
                {
                    return true;
                }
            }

            return false;
        }

        public void PrintScheduleToConsole()
        {
            foreach(var dd in dinnerDays)
            {
                Console.WriteLine(dd.dayOfWeek + " - Participants: " + dd.GetNumberOfParticipants());

                foreach(var sd in dd.specificDates)
                {
                    Console.WriteLine(sd.date + " - " + sd.responsiblePerson);
                }
            }
        }

        public void AssignDates()
        {
            //reset responsibility for all dates
            foreach (var dd in dinnerDays)
            {
                foreach(var sd in dd.specificDates)
                {
                    sd.responsiblePerson = "None";
                }
            }

            Random rnd = new Random();
            float margin = 0.5f;
            foreach(var dd in dinnerDays)
            {
                while(dd.getNextUnassignedDate()!=null)
                {
                    //get name of one of the persons that cooked the fewest times compared to how many days they are signed up for
                    Dictionary<string, float> sortedList = nDaysChefPrSignedUpDate.Where(x => dd.Participants.Contains(x.Key)).ToDictionary(dict => dict.Key, dict => dict.Value);
                    Dictionary<string, float> lowNCooked = sortedList.Where(x => x.Value<sortedList.First().Value+margin).ToDictionary(dict => dict.Key, dict => dict.Value);
                    List<string> values = Enumerable.ToList(lowNCooked.Keys);
                    int index = rnd.Next(0, lowNCooked.Count);
                    String name = values[index];
                    dd.setPersonResponsibleForDate(dd.getNextUnassignedDate(), name);
                    update_nTimesChefPrSignedUpDate();
                }
            }
        }

        public float getMaxRatioDifference()
        {
            float max = nDaysChefPrSignedUpDate.Values.Max();
            float min = nDaysChefPrSignedUpDate.Values.Min();
            float dif = max - min;
            return dif;
        }
    }
}
