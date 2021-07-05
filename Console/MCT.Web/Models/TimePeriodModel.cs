using MCT.DB.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MCT.Web.Models
{
    public class TimePeriodModel
    {
        public long Id { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public TimePeriodArea StartArea { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TimePeriodMonth StartMonth { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public TimePeriodArea EndArea { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
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
            //this.AssignedTo = SubjectModel.Convert(tp.AssignedTo);
            this.Type = tp.GetType().Name;
            if (tp.Next != null)
                this.Next = new TimePeriodModel(tp.Next);
        }

        public TimePeriod ConvertToTimePeriod(Subject subject)
        {
            switch (this.Type)
            {
                case "Cultivate": return setTimePeriod(new Cultivate(), this, subject);
                case "Bloom": return setTimePeriod(new Bloom(), this, subject);
                case "Harvest": return setTimePeriod(new Harvest(), this, subject);
                case "Implant": return setTimePeriod(new Implant(), this, subject);
                case "LifeTime": return setTimePeriod(new LifeTime(), this, subject);
                case "Sowing": return setTimePeriod(new Sowing(), this, subject);
                case "SeedMaturity": return setTimePeriod(new SeedMaturity(), this, subject);
                default: return null;
            }

            return null;
        }

        private TimePeriod setTimePeriod(TimePeriod target, TimePeriodModel source, Subject subject)
        {
            target.Id = source.Id;
            target.Start = source.Start;
            target.StartArea = source.StartArea;
            target.StartMonth = source.StartMonth;
            target.EndArea = source.EndArea;
            target.EndMonth = source.EndMonth;
            target.AssignedTo = subject;

            return target;
        }
    }
}