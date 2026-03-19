using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Workshop.DataTransferObjects;
using Workshop.DomainModels;

namespace Workshop.BusinessLogicLayer.Mappings;

public class MappingProfile : Profile  
{

  public MappingProfile()
  {
    CreateMap<Project, ProjectDto>();

    CreateMap<ProjectNewDto, Project>();
    CreateMap<ProjectUpdateDto, Project>();
  }
}
