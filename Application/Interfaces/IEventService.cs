using Application.Models;

namespace Application.Interfaces;

public interface IEventService
{
    Task<ServiceResult> CreateEventAsync(CreateEventRequest request);
    Task<ServiceResult<IEnumerable<Event>>> GetEventsAsync();
    Task<ServiceResult<Event>> GetEventAsync(string eventId);
    Task<ServiceResult> UpdateEventAsync(string eventId, CreateEventRequest request);
    Task<ServiceResult> DeleteEventAsync(string eventId);
}