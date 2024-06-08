using AutoMapper;
using Kolosok.Application.Contracts.Contacts;
using Kolosok.Application.Contracts.Victim;
using Kolosok.Application.Interfaces.Infrastructure;
using Kolosok.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolosok.Application.Features.User.Queries
{
    public class GetUsersPageQuery : IRequest<IEnumerable<ContactResponse>>
    {
        public ISpecification<Domain.Entities.Contact>[] Specifications { get; private set; }

        public SearchFilter Filters { get; set; }

        public GetUsersPageQuery(SearchFilter filter)
        {
            Filters = filter;
        }

        public void AddSpecification(params ISpecification<Domain.Entities.Contact>[] specifications)
        {
            Specifications = specifications;
        }
    }

    public class GetUsersPageQueryHandler : IRequestHandler<GetUsersPageQuery, IEnumerable<ContactResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetUsersPageQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ContactResponse>> Handle(GetUsersPageQuery request, CancellationToken cancellationToken)
        {
            var users = await _unitOfWork.ContactRepository
                .GetAllByFiltersAsync(
                    request.Filters,
                    request.Specifications
                );
            var response = _mapper.Map<IEnumerable<ContactResponse>>(users);
            return response;
        }
    }
}
