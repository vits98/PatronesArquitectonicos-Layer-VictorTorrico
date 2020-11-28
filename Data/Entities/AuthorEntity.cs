using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace LibreriaArqui.Data.Entities
{
    public class AuthorEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Nationallity { get; set; }
        public int Age { get; set; }
        public virtual IEnumerable<BookEntity> Books { get; set; }
    }
}
