using SchoolConnect.EducationSystem.Application.Interfaces;
using SchoolConnect.EducationSystem.Domain.Aggregates;

namespace SchoolConnect.EducationSystem.Infrastructure.Repositories;

public interface IRepository<T> : Application.Interfaces.IRepository<T> where T : AggregateRoot
{
}
