using ArticlesViewer.Application.Commands;
using ArticlesViewer.Application.Commands.Articles;
using ArticlesViewer.Application.DTO;
using ArticlesViewer.Domain;
using AutoMapper;


namespace ArticlesR.MapperProfiles;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<RegisterCommand, ApplicationUser>();
        CreateMap<ApplicationUser, UserUpdateResponse>();   
        CreateMap<ApplicationUser, UserResponse>();
        CreateMap<CreateArticleCommand, Article>();
        CreateMap<Article, ArticleResponse>();
        CreateMap<ApplicationUser, UserUpdateResponse>();
        CreateMap<UserResponse, UserUpdateResponse>();
    }
}
