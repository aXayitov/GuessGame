using GuessGame1.Entity.Base;

namespace GuessGame1.Entity
{
    public class User : EntityBase
    {
        public string UserName {  get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
