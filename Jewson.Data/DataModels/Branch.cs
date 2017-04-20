namespace Jewson.Data.DataModels
{
    public class Branch
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Town { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Telephone1 { get; set; }
        public string Email { get; set; }
        public string BranchManager{ get; set; }
        public string OpeningHoursWeekend { get; set; }
        public string OpeningHoursMonToFri { get; set; }
        public string ClosingHoursWeekend { get; set; }
        public string ClosingHoursMonToFri { get; set; }
        public string ClosingHoursWeekDay { get; set; }
        public Specialism[] Specialisms { get; set; }
    }
}
