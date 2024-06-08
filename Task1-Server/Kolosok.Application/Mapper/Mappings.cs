using AutoMapper;
using Kolosok.Application.Contracts;
using Kolosok.Application.Contracts.Action;
using Kolosok.Application.Contracts.Brigade;
using Kolosok.Application.Contracts.BrigadeRescuer;
using Kolosok.Application.Contracts.Contacts;
using Kolosok.Application.Contracts.Diagnosis;
using Kolosok.Application.Contracts.Victim;
using Kolosok.Application.Features.Action.Commands;
using Kolosok.Application.Features.Auth.Commands;
using Kolosok.Application.Features.Brigade.Commands;
using Kolosok.Application.Features.BrigadeRescuer.Commands;
using Kolosok.Application.Features.Diagnoses.Commands;
using Kolosok.Application.Features.Victim.Commands;
using Kolosok.Domain.Entities;
using Action = Kolosok.Domain.Entities.Action;

namespace Kolosok.Application.Mapper;

public class Mappings : Profile
{
    public Mappings()
    {
        CreateMap<BaseEntity, BaseResponse>();

        CreateMap<RegisterUserCommand, Contact>()
            .ForMember(c => c.Password, opt => opt.Ignore());
        CreateMap<ContactRequest, Contact>();
        CreateMap<Contact, ContactResponse>();
        
        CreateMap<CreateBrigadeCommand, Brigade>()
            .ForMember(c => c.BrigadeRescuers, opt => opt.Ignore())
            .ForMember(c => c.BrigadeSize, opt => opt.MapFrom(c => c.BrigadeRescuers.Count));
        CreateMap<UpdateBrigadeCommand, Brigade>().ForMember(c => c.BrigadeRescuers, opt => opt.Ignore());
        CreateMap<Brigade, BrigadeResponse>();
        CreateMap<Brigade, BrigadeLookupResponse>();

        CreateMap<CreateBrigadeRescuerCommand, BrigadeRescuer>();
        CreateMap<UpdateBrigadeRescuerRequestCommand, BrigadeRescuer>();
        CreateMap<BrigadeRescuer, BrigadeRescuerResponse>();
        
        CreateMap<CreateVictimCommand, Victim>();
        CreateMap<Victim, VictimResponse>();
        CreateMap<Victim, VictimLookupResponse>();

        CreateMap<CreateDiagnosisCommand, Diagnosis>();
        CreateMap<Diagnosis, DiagnosisResponse>();

        CreateMap<CreateActionCommand, Action>();
        CreateMap<Action, ActionResponse>();
        CreateMap<Action, ActionLookupResponse>();
        CreateMap<Action, VictimActionLookupResponse>();
    }
}