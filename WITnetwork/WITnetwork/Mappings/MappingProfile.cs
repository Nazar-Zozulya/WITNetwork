




using AutoMapper;
using WITnetwork.Dtos;
using WITnetwork.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Post, PostResponseDto>();
        CreateMap<CreatePostDto, Post>()
            .ForMember(dest => dest.Images, opt => opt.Ignore());
        CreateMap<PostImage, PostImageDto>();
        CreateMap<Tag, TagResponseDto>();
        CreateMap<CreateTagDto, Tag>();
        CreateMap<UserProfile, AuthorDto>();
    }
}