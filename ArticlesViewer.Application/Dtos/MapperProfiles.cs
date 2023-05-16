using ArticlesViewer.Application.Commands;
using ArticlesViewer.Application.DTO;
using ArticlesViewer.Domain;
using AutoMapper;


namespace ArticlesViewer.Application.MapperProfiles;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<RegisterCommand, User>();
        CreateMap<UpdateUserCommand, UserResponse>();
        CreateMap<User, UserSettingResponse>();
        CreateMap<User, UserResponse>();
        CreateMap<CreateArticleCommand, Article>()
            .ForMember(dest => dest.TagId, opt => opt.MapFrom(src => src.TopicTag.Id));
        CreateMap<Article, ArticleResponse>()
             .ForMember(dest => dest.TopicTag, opt => opt.MapFrom(src => src.TopicTag.Tag));
        CreateMap<User, UserSettingResponse>();
        CreateMap<UserResponse, UserSettingResponse>();
    }
}
