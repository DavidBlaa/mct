using MCT.DB.Entities;

namespace MCT.Web.Models
{
    public class TimePeriodModel
    {
        public long Id { get; set; }

        public TimePeriodArea StartArea { get; set; }

        public TimePeriodMonth StartMonth { get; set; }

        public TimePeriodArea EndArea { get; set; }

        public TimePeriodMonth EndMonth { get; set; }

        public SubjectModel AssignedTo { get; set; }

        public bool Start { get; set; }

        public TimePeriodModel Next { get; set; }

        public string Type { get; set; }

        public TimePeriodModel()
        {
            this.Id = 0;
            this.Start = false;
            this.StartArea = TimePeriodArea.Anfang;
            this.StartMonth = TimePeriodMonth.Januar;
            this.EndArea = TimePeriodArea.Anfang;
            this.EndMonth = TimePeriodMonth.Dezember;
            this.AssignedTo = null;
            this.Type = "";
        }

        public TimePeriodModel(TimePeriod tp)
        {
            this.Id = tp.Id;
            this.Start = tp.Start;
            this.StartArea = tp.StartArea;
            this.StartMonth = tp.StartMonth;
            this.EndArea = tp.EndArea;
            this.EndMonth = tp.EndMonth;
            this.AssignedTo = SubjectModel.Convert(tp.AssignedTo);
            this.Type = tp.GetType().Name;
            if (tp.Next != null)
                this.Next = new TimePeriodModel(tp.Next);
        }
    }
}

