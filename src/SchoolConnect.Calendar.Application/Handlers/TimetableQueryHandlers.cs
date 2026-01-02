using MediatR;
using AutoMapper;
using SchoolConnect.Calendar.Application.Queries.Timetables;
using SchoolConnect.Calendar.Application.DTOs;
using SchoolConnect.Calendar.Domain.Interfaces;

namespace SchoolConnect.Calendar.Application.Handlers;

public class GetTimetableByIdQueryHandler : IRequestHandler<GetTimetableByIdQuery, TimetableDto?>
{
    private readonly ITimetableRepository _timetableRepository;
    private readonly IMapper _mapper;

    public GetTimetableByIdQueryHandler(ITimetableRepository timetableRepository, IMapper mapper)
    {
        _timetableRepository = timetableRepository;
        _mapper = mapper;
    }

    public async Task<TimetableDto?> Handle(GetTimetableByIdQuery request, CancellationToken cancellationToken)
    {
        var timetable = await _timetableRepository.GetByIdAsync(request.TimetableId, cancellationToken);
        return timetable == null ? null : _mapper.Map<TimetableDto>(timetable);
    }
}

public class GetTimetablesByInstituteQueryHandler : IRequestHandler<GetTimetablesByInstituteQuery, IEnumerable<TimetableDto>>
{
    private readonly ITimetableRepository _timetableRepository;
    private readonly IMapper _mapper;

    public GetTimetablesByInstituteQueryHandler(ITimetableRepository timetableRepository, IMapper mapper)
    {
        _timetableRepository = timetableRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TimetableDto>> Handle(GetTimetablesByInstituteQuery request, CancellationToken cancellationToken)
    {
        var timetables = await _timetableRepository.GetByInstituteIdAsync(request.InstituteId, cancellationToken);
        return _mapper.Map<IEnumerable<TimetableDto>>(timetables);
    }
}

public class GetTimetableSlotsQueryHandler : IRequestHandler<GetTimetableSlotsQuery, IEnumerable<TimetableSlotDto>>
{
    private readonly ITimetableSlotRepository _slotRepository;
    private readonly IMapper _mapper;

    public GetTimetableSlotsQueryHandler(ITimetableSlotRepository slotRepository, IMapper mapper)
    {
        _slotRepository = slotRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TimetableSlotDto>> Handle(GetTimetableSlotsQuery request, CancellationToken cancellationToken)
    {
        var slots = await _slotRepository.GetByTimetableIdAsync(request.TimetableId, cancellationToken);
        return _mapper.Map<IEnumerable<TimetableSlotDto>>(slots);
    }
}

public class GetTeacherTimetableQueryHandler : IRequestHandler<GetTeacherTimetableQuery, IEnumerable<TimetableSlotDto>>
{
    private readonly ITimetableSlotRepository _slotRepository;
    private readonly IMapper _mapper;

    public GetTeacherTimetableQueryHandler(ITimetableSlotRepository slotRepository, IMapper mapper)
    {
        _slotRepository = slotRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TimetableSlotDto>> Handle(GetTeacherTimetableQuery request, CancellationToken cancellationToken)
    {
        var slots = await _slotRepository.GetByTeacherIdAsync(request.TeacherId, request.TimetableId, cancellationToken);
        return _mapper.Map<IEnumerable<TimetableSlotDto>>(slots);
    }
}

public class GetClassTimetableQueryHandler : IRequestHandler<GetClassTimetableQuery, IEnumerable<TimetableSlotDto>>
{
    private readonly ITimetableSlotRepository _slotRepository;
    private readonly IMapper _mapper;

    public GetClassTimetableQueryHandler(ITimetableSlotRepository slotRepository, IMapper mapper)
    {
        _slotRepository = slotRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TimetableSlotDto>> Handle(GetClassTimetableQuery request, CancellationToken cancellationToken)
    {
        var slots = await _slotRepository.GetByClassIdAsync(request.ClassId, request.TimetableId, cancellationToken);
        return _mapper.Map<IEnumerable<TimetableSlotDto>>(slots);
    }
}
