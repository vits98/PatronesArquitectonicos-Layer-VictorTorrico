using AutoMapper;
using LibreriaArqui.Data.Entities;
using LibreriaArqui.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibreriaArqui.Data
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile()
        {
            this.CreateMap<AuthorEntity, Author>()
                .ReverseMap();

            this.CreateMap<BookEntity, Book>()
                .ReverseMap();
        }
    }
}
