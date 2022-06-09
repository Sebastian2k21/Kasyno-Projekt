using System.Collections.Generic;

namespace Kasyno.Entities
{
    /// <summary>
    /// encja uzytkownika
    /// </summary>
    public class AppUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Passsword { get; set; }


        // ponizsze pole tworzy  relacje z tabela AppUserDetails 1:1 (zobacz AppUserDetails.cs)
        public virtual AppUserDetails AppUserDetails { get; set; }

        // pole ponizej tworzy relacje 1:N History:AppUser (zobacz sobie History.cs)
        public virtual ICollection<History> History { get; set; }
    }
}
