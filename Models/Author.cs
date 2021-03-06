﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace LibreriaArqui.Models
{
    public class Author
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Nationallity { get; set; }
        public int Age { get; set; }
        public int Id { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
