using BuberDinner.Application.Common.Interfaces.Services;

namespace BuberDinner.InfraStructure.Services;
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
