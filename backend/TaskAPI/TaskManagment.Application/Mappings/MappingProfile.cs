using AutoMapper;
using TaskManagment.Application.Features.Admin.Query;
using TaskManagment.Application.Features.Mission.Command;
using TaskManagment.Application.Features.User;
using TaskManagment.Domain.Entities;

namespace TaskManagment.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Project, ProjectResponse>()
            .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.Missions, opt => opt.MapFrom(src => src.Missions))
            .ForMember(dest => dest.ProjectUsers, opt => opt.MapFrom(src => src.ProjectUsers));
        
        CreateMap<Customer, CustomerDto>();

        CreateMap<Mission, MissionDto>().ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForPath(dest => dest.Status.Status, opt => opt.MapFrom(src => src.Status))
            .ForPath(dest => dest.Status.StatusName, opt => opt.MapFrom(src => src.Status.ToString()))
            .ForPath(dest => dest.AssignedUser.UserId, opt => opt.MapFrom(src => src.AssignedUserId))
            .ForPath(dest => dest.AssignedUser.Username, opt => opt.MapFrom(src => src.AssignedUser!.Username));

        CreateMap<ProjectUser, ProjectUserDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User != null ? src.User.Username : "[unknown]")) // null kontrol
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
        
        
        CreateMap<Domain.Entities.Mission, MissionResponseDTO>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
            .ForMember(dest => dest.AssignedUserId, opt => opt.MapFrom(src => src.AssignedUserId));

        CreateMap<History, HistoryQueryDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.Username))
            .ForMember(dest => dest.MissionTitle, opt => opt.MapFrom(src => src.Mission.Title));

    }
}