namespace KP
{
    public class House
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int HouseNum { get; set; }
        public int ApartmentNum { get; set; }
        public int Area { get; set; }
        public int PeopleNum { get; set; }
        public int RoomsNum { get; set; }
        public int HeatMeter { get; set; }


        public virtual User User { get; set; }
    }
}
