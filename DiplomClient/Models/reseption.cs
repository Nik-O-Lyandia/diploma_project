namespace DiplomClient
{
    public partial class Reseption
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int TimeId { get; set; }
        public string PIB { get; set; }
        public string Login { get; set; }
        public string Status { get; set; } // Прийшов/Не прийшов/Ще не час
        public bool FreeOrNot { get; set; }
    }
}
