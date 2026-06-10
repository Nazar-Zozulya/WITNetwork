




using AutoMapper;
using WITnetwork.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Post, PostResponseDto>();
        CreateMap<CreatePostDto, Post>();
        CreateMap<Tag, TagResponseDto>();
        CreateMap<CreateTagDto, Tag>();
        CreateMap<UserProfile, AuthorDto>();
    }
}