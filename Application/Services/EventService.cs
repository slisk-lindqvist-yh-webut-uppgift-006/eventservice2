using System.Runtime.InteropServices.JavaScript;
using Application.Factories;
using Application.Interfaces;
using Application.Models;
using Persistence.Entities;
using Persistence.Repositories;

namespace Application.Services;



public class EventService(IEventRepository eventRepository) : IEventService
{
    private readonly IEventRepository _eventRepository = eventRepository;
    // private readonly IEventLocationRepository _eventLocationRepository;
    
    public async Task<ServiceResult> CreateEventAsync(CreateEventRequest request)
    {
        try
        {
            var eventEntity = new EventEntity
            {
                Image = request.Image,
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                EventDate = request.EventDate
            };

            var result = await _eventRepository.AddAsync(eventEntity);

            return result.Succeeded
                ? ServiceResultFactory.Success(result.Result, 201, "Event created successfully.")
                : ServiceResultFactory.Error(500, $"Failed to create event: {result.Error}");
        }
        catch (Exception ex)
        {
            return ServiceResultFactory.Error(500, $"An unexpected error occurred: {ex.Message}");
        }
    }

    public async Task<ServiceResult<IEnumerable<Event>>> GetEventsAsync()
    {
        var result = await _eventRepository.GetAllAsync();

        if (!result.Succeeded)
        {
            return ServiceResultFactory.Error<IEnumerable<Event>>(500, $"Failed to retrieve events: {result.Error}");
        }

        var events = result.Result?.Select(entity => new Event
        {
            Image = entity.Image,
            Title = entity.Title,
            Description = entity.Description,
            Location = entity.Location,
            EventDate = entity.EventDate
        }) ?? [];

        return ServiceResultFactory.Success(events, 200, "Events retrieved successfully.");
    }
    
    public async Task<ServiceResult<Event>> GetEventAsync(string eventId)
    {
        var result = await _eventRepository.GetAsync(x => x.Id == eventId);

        if (result is { Succeeded: true, Result: not null })
        {
            var currentEvent = new Event
            {
                Image = result.Result.Image,
                Title = result.Result.Title,
                Description = result.Result.Description,
                Location = result.Result.Location,
                EventDate = result.Result.EventDate
            };
            
            return ServiceResultFactory.Success(currentEvent, 200, "Event retrieved successfully.");
        }

        return ServiceResultFactory.Error<Event>(404, "Event not found.");
    }
    
    public async Task<ServiceResult> UpdateEventAsync(string eventId, CreateEventRequest request)
    {
        var existingResult = await _eventRepository.GetAsync(x => x.Id == eventId);

        if (existingResult is not { Succeeded: true, Result: not null })
        {
            return ServiceResultFactory.Error(404, "Event not found.");
        }

        var eventEntity = existingResult.Result;
        eventEntity.Image = request.Image;
        eventEntity.Title = request.Title;
        eventEntity.Description = request.Description;
        eventEntity.Location = request.Location;
        eventEntity.EventDate = request.EventDate;

        var updateResult = await _eventRepository.UpdateAsync(eventEntity);

        return updateResult.Succeeded
            ? ServiceResultFactory.Success<object?>(null, 204, "Event updated successfully.")
            : ServiceResultFactory.Error(500, $"Failed to update event: {updateResult.Error}");
    }
    
    public async Task<ServiceResult> DeleteEventAsync(string eventId)
    {
        var result = await _eventRepository.GetAsync(x => x.Id == eventId);

        if (result is not { Succeeded: true, Result: not null })
        {
            return ServiceResultFactory.Error(404, "Event not found.");
        }

        var deleteResult = await _eventRepository.DeleteAsync(result.Result);

        return deleteResult.Succeeded
            ? ServiceResultFactory.Success<object?>(null, 204, "Event deleted successfully.")
            : ServiceResultFactory.Error(500, $"Failed to delete event: {deleteResult.Error}");
    }
}