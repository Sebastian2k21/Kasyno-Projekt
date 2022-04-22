namespace Kasyno.Entities
{
    public class History
    {
        public int Id { get; set; }
        public double BetAmount { get; set; }
        public bool IsWon { get; set; }

        // dwa pola ponizej tworza relacje 1:N History:Game (zobacz Game.cs)
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        // dwa pola ponizej tworza relacje 1:N History:AppUser (zobacz AppUser.cs)
        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
