namespace DataAccess.Data.Entities
{
    public enum Categories
    {
        Electronics = 1,
        Sport,
        Fashion,
        HomeAndGarden,
        Transport,
        ToysAndHobbies,
        MusicalInstruments,
        Art,
        Other
    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product>? Products { get; set; } 
    }
}
