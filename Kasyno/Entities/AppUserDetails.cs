namespace Kasyno.Entities
{
    /// <summary>
    /// encja szczegolowych informacji o uzytkowniku
    /// </summary>
    public class AppUserDetails
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public double Balance { get; set; }


        // dwa ponizsze pola tworza relacje z tabela AppUser 1:1 (zobacz AppUser.cs)
        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
