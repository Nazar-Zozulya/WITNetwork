




using WITnetwork.Dtos;
using WITnetwork.Models;
using WITnetwork.Dtos;
using AutoMapper;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<Post, PostResponseDto>();

        CreateMap<CreatePostDto, Post>()
            .ForMember(x => x.Images, opt => opt.Ignore())
            .ForMember(x => x.Author, opt => opt.Ignore())
            .ForMember(x => x.Tags, opt => opt.Ignore())
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.CreatedAt, opt => opt.Ignore())
            .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
            .ForMember(x => x.Topic, opt => opt.Ignore());

        CreateMap<PostImage, PostImageDto>();
        CreateMap<Tag, TagResponseDto>();

        CreateMap<CreateTagDto, Tag>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.Posts, opt => opt.Ignore());

        CreateMap<UserProfile, AuthorDto>()
            .ForCtorParam(nameof(AuthorDto.Avatar),
                opt => opt.MapFrom(src => src.Profile != null ? src.Profile.Avatar : null));

        CreateMap<Message, MessageDto>();


        CreateMap<UserProfile, UserToPostDto>();


        CreateMap<UserProfile, UserResponseDto>();
        // CreateMap<UserProfile, UserWithoutIncludes>();
        CreateMap<UserProfile, UserWithoutIncludes>()
            .ForCtorParam(nameof(UserWithoutIncludes.Id), opt => opt.MapFrom(s => s.Id))
            .ForCtorParam(nameof(UserWithoutIncludes.UserName), opt => opt.MapFrom(s => s.UserName))
            .ForCtorParam(nameof(UserWithoutIncludes.Email), opt => opt.MapFrom(s => s.Email))
            .ForCtorParam(nameof(UserWithoutIncludes.PasswordHash), opt => opt.MapFrom(s => s.PasswordHash))
            .ForCtorParam(nameof(UserWithoutIncludes.FirstName), opt => opt.MapFrom(s => s.FirstName))
            .ForCtorParam(nameof(UserWithoutIncludes.LastName), opt => opt.MapFrom(s => s.LastName))
            // .ForCtorParam(nameof(UserWithoutIncludes.IsActive), opt => opt.MapFrom(s => s.LockoutEnabled))
            // .ForCtorParam(nameof(UserWithoutIncludes.IsStaff), opt => opt.MapFrom(s => false))
            // .ForCtorParam(nameof(UserWithoutIncludes.IsSuperUser), opt => opt.MapFrom(s => false))
            .ForCtorParam(nameof(UserWithoutIncludes.LastLoginAt), opt => opt.MapFrom(s => s.LastLoginAt))
            .ForCtorParam(nameof(UserWithoutIncludes.DateJoined), opt => opt.MapFrom(s => s.DateJoined));

        CreateMap<Profile, ProfileDto>();
    }
}