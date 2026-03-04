using chapelhilldotnet.web.Models;

namespace chapelhilldotnet.web.Services;

public interface IEventService
{
    Task<List<Event>> GetAllEventsAsync();
    Task<Event?> GetEventByIdAsync(int id);
    Task<Event> CreateEventAsync(Event newEvent);
    Task<Event> UpdateEventAsync(Event updatedEvent);
    Task<bool> DeleteEventAsync(int id);
}
