namespace DiplomClient
{
    public partial class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Passportnumber { get; set; }
        public string Passportseries { get; set; }
        public string Login { get; set; }
        public int PasswordHash { get; set; }
        public int? Experience { get; set; }
        public byte[] DocFoto { get; set; }
        public string Room { get; set; }
    }
}
