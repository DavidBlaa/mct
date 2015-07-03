namespace MCT.DB.Entities
{
    public class Day
    {
        public virtual long Id { get; set; }
        public virtual int DayInYear { get; set; }
        public virtual int WeekPerYear { get; set; }
    }
}
