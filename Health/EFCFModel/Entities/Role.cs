using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EFCFModel.Attributes;

namespace EFCFModel.Entities
{
    [Table("Roles"), ScaffoldTable(true), DisplayName("Роль")]
    public class Role : IIdentity
    {
        public Role()
        {
            Users = new List<User>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Hide]
        public int Id { get; set; }

        [DisplayName("Имя")]
        [Required(ErrorMessage = "Имя роли не может быть пустым.")]
        public string Name { get; set; }

        [NotDisplay, DisplayName("Пользователи")]
        public virtual ICollection<User> Users { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}