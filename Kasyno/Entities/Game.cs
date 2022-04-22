using System.Collections.Generic;

namespace Kasyno.Entities
{
    class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // pole ponizej tworzy relacje 1:N History:Game (zobacz sobie History.cs)
        public virtual ICollection<History> History { get; set; }
    }
}
