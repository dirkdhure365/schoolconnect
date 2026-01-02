using AutoMapper;
using MediatR;
using SchoolConnect.Enrolment.Application.DTOs;
using SchoolConnect.Enrolment.Application.Queries.Students;
using SchoolConnect.Enrolment.Domain.Interfaces;

namespace SchoolConnect.Enrolment.Application.Handlers.Students;

public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentDto?>
{
    private readonly IStudentRepository _repository;
    private readonly IMapper _mapper;

    public GetStudentByIdQueryHandler(IStudentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<StudentDto?> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
    {
        var student = await _repository.GetByIdAsync(request.Id, cancellationToken);
        return student != null ? _mapper.Map<StudentDto>(student) : null;
    }
}

public class GetStudentsByInstituteQueryHandler : IRequestHandler<GetStudentsByInstituteQuery, IEnumerable<StudentDto>>
{
    private readonly IStudentRepository _repository;
    private readonly IMapper _mapper;

    public GetStudentsByInstituteQueryHandler(IStudentRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StudentDto>> Handle(GetStudentsByInstituteQuery request, CancellationToken cancellationToken)
    {
        var students = await _repository.GetByInstituteAsync(request.InstituteId, cancellationToken);
        return _mapper.Map<IEnumerable<StudentDto>>(students);
    }
}
