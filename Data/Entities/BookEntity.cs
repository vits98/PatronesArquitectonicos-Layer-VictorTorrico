using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaArqui.Data.Entities
{
    public class BookEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public string Genre { get; set; }

        [ForeignKey("AuthorId")]
        public virtual AuthorEntity Author { get; set; }
    }
}
