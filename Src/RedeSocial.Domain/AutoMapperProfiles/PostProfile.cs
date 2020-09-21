using AutoMapper;
using RedeSocial.Domain.Account;
using RedeSocial.Domain.Post;

namespace RedeSocial.Domain.AutoMapperProfiles
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Postagem, PostagemResponse>();
            CreateMap<Account.Account, AccountSimplesResponse>();
        }
    }
}
