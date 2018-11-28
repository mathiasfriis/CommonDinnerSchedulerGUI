using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDinnerScheduler
{
    public class DinnerDay
    {
        public List<String> Participants;
        public DayOfWeek dayOfWeek { get; set; }
        private DateTime startDate { get; set; }
        private DateTime endDate { get; set; }

        public List<CommonDinnerDate> specificDates;
        public Dictionary<String, int> daysResponsibleFor;
        public DinnerDay(DayOfWeek weekDay, DateTime StartDate, DateTime EndDate)
        {
            Participants = new List<string>();
            dayOfWeek = weekDay;
            startDate = StartDate;
            endDate = EndDate;
            specificDates = new List<CommonDinnerDate>();
            daysResponsibleFor= new Dictionary<string, int>();
            
            //Add all dates in accordance to start date and end date

            //Find first "weekday" after the given start date.
            DateTime indexDate = StartDate;
            if (indexDate.DayOfWeek != weekDay)
            {
                int daysUntilWeekday = ((int)weekDay-(int)indexDate.DayOfWeek + 7) % 7;
                indexDate = indexDate.AddDays(daysUntilWeekday);
            }

            do
            {
                specificDates.Add(new CommonDinnerDate(indexDate));
                indexDate=indexDate.AddDays(7);
            } while ((DateTime.Compare(indexDate,endDate)<=0)&& (indexDate.DayOfWeek==dayOfWeek)); //As long as index date is earlier or same as endDate
        }

        public bool AddParticipant(String name)
        {
            if (name == null) throw new ArgumentNullException(nameof(name));
            //If name is not yet on list, add it
            if (!Participants.Contains(name))
            {
                //Add participants name to list of participants, and make key-value pair
                Participants.Add(name);
                daysResponsibleFor.Add(name,0);
                //Indicate that name has been added.
                return true;
            }
            else
            {
                //Indicate that name has not been added, since it's already on the list
                return false;
            }
        }

        public bool RemoveParticipant(String name)
        {
            //Check if name is in list
            if (Participants.Contains(name))
            {
                //Remove from list
                Participants.Remove(name);
                //return true, indicating name was removed
                return true;
            }
            else
            {
                //return false, indicating name was not removed
                return false;
            }
        }

        public int GetNumberOfParticipants()
        {
            return Participants.Count;
        }

        public int GetNumberOfSpecificDate()
        {
            return specificDates.Count;
        }

        public void setPersonResponsibleForDate(CommonDinnerDate date, String name)
        {
            //Check if name is in list of participants
            if (!Participants.Contains(name))
            {
                throw new System.ArgumentException("Name not found in list of participants");
            }
            else
            {
                //If no-one assigned yet, set given name as responsible person, and increment number of days responsible for.
                if (date.responsiblePerson.Equals("None"))
                {
                    date.responsiblePerson = name;
                    if (daysResponsibleFor.ContainsKey(name))
                    {
                        daysResponsibleFor[name]++;
                    }
                }
                else
                {
                    //If a person is already assigned, set given name as responsible person, and increment number of days that given name is responsible for, and decrement for the "old" responsible user.
                    daysResponsibleFor[date.responsiblePerson]--;
                    date.responsiblePerson = name;
                    if (daysResponsibleFor.ContainsKey(name))
                    {
                        daysResponsibleFor[name]++;
                    }
                }
                
            }
        }

        public CommonDinnerDate getNextUnassignedDate()
        {
            return specificDates.Find(x => x.responsiblePerson == "None");
        }
    }
}
