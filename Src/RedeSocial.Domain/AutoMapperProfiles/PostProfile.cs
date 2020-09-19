using AutoMapper;
using RedeSocial.Domain.Post;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedeSocial.Domain.AutoMapperProfiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Postagem, PostagemResponse>();
        }
    }
}
