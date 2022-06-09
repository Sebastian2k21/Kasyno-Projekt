using System.Collections.Generic;

namespace Kasyno.Entities
{
    /// <summary>
    /// encja gry
    /// </summary>
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // pole ponizej tworzy relacje 1:N History:Game (zobacz sobie History.cs)
        public virtual ICollection<History> History { get; set; }
    }
}
