using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings;
    public class MappingProfile : AutoMapper.Profile
{
        public MappingProfile()
        { 
            CreateMap<Project, ProjectDto>()
                .ForMember(dest => dest.Priority,
                    opt => opt.MapFrom(src => src.Priority.Name))
                .ForMember(dest => dest.Tasks,
                    opt => opt.MapFrom(src => src.Tasks));

     
            CreateMap<TaskItem, TaskDto>()
                .ForMember(dest => dest.Status,
                    opt => opt.MapFrom(src => src.Status.ToString()));
 
            CreateMap<ProjectCreateDto, Project>()
                .ConstructUsing(src => new Project(
                    src.Name,
                    src.Description,
                    Domain.ValueObjects.Priority.FromName(src.Priority),
                    src.DueDate
                ));

            CreateMap<ProjectUpdateDto, Project>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<TaskCreateDto, TaskItem>()
                .ConstructUsing(src => new TaskItem(
                    src.Title,
                    src.Description,
                    src.ProjectId,
                    src.DueDate
                ));

            CreateMap<TaskUpdateDto, TaskItem>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }