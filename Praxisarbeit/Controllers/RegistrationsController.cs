using Microsoft.AspNetCore.Mvc;
using Praxisarbeit.Model;
using Praxisarbeit.Dto;
using MongoDB.Driver;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class RegistrationController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public RegistrationController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetAllRegistrations()
    {
        var registrations = _dbContext.Registrations.Find(_ => true).ToList();
        return Ok(registrations);
    }

    [HttpGet("{name}")]
    public IActionResult GetRegistrationByName(string name)
    {
        var registration = _dbContext.Registrations.Find(r => r.Name == name).FirstOrDefault();

        if (registration == null)
        {
            return NotFound("Registration not found");
        }

        return Ok(registration);
    }

    [HttpPost]
    public IActionResult CreateRegistration([FromBody] RegistrationDto registrationDto)
    {
        try
        {
            // Validierung der Eingabedaten
            if (registrationDto == null)
            {
                return BadRequest("Invalid data");
            }

            // Erstellen Sie ein neues Order-Objekt aus den übermittelten Daten
            var registrationModel = new Order
            {
                Name = registrationDto.Name,
                Email = registrationDto.Email,
                Phone = registrationDto.Phone,
                CreateDate = DateTime.Now,
                PickupDate = registrationDto.PickupDate
            };

            // Parsen Sie die PriorityId und ServiceId aus dem DTO und weisen Sie sie zu
            if (int.TryParse(registrationDto.PriorityId, out int priorityId))
            {
                registrationModel.PriorityId = priorityId;
            }
            else
            {
                // Geben Sie eine Fehlermeldung zurück, wenn die PriorityId nicht in ein int umgewandelt werden kann
                return BadRequest("Invalid PriorityId");
            }

            if (int.TryParse(registrationDto.ServiceId, out int serviceId))
            {
                registrationModel.ServiceId = serviceId;
            }
            else
            {
                // Geben Sie eine Fehlermeldung zurück, wenn die ServiceId nicht in ein int umgewandelt werden kann
                return BadRequest("Invalid ServiceId");
            }

            // Fügen Sie das Order-Objekt in die MongoDB-Sammlung ein
            _dbContext.Registrations.InsertOne(registrationModel);

            // Erfolgreiche Antwort an den Client senden
            return Ok("Data received successfully and saved to the database");
        }
        catch (Exception ex)
        {
            // Bei Fehlern eine entsprechende Fehlermeldung an den Client senden
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpPut("{name}")]


    public IActionResult UpdateRegistration(string name, [FromBody] RegistrationDto registrationDto)
    {
        var existingRegistration = _dbContext.Registrations.Find(r => r.Name == name).FirstOrDefault();

        if (existingRegistration == null)
        {
            return NotFound("Registration not found");
        }

        existingRegistration.Name = registrationDto.Name;
        existingRegistration.Email = registrationDto.Email;
        existingRegistration.Phone = registrationDto.Phone;
        existingRegistration.PriorityId = int.Parse(registrationDto.PriorityId);
        existingRegistration.ServiceId = int.Parse(registrationDto.ServiceId);
        existingRegistration.PickupDate = registrationDto.PickupDate;

        // Save the changes to MongoDB
        _dbContext.Registrations.ReplaceOne(r => r.Name == name, existingRegistration);

        return Ok("Registration updated successfully");
    }

    [HttpDelete("{name}")]
    [Authorize]
    public IActionResult DeleteRegistration(string name)
    {
        var registration = _dbContext.Registrations.Find(r => r.Name == name).FirstOrDefault();

        if (registration == null)
        {
            return NotFound("Registration not found");
        }

        // Delete the document from MongoDB
        _dbContext.Registrations.DeleteOne(r => r.Name == name);

        return Ok("Registration deleted successfully");
    }

    [HttpGet("users")]
    public IActionResult GetUsers()
    {
        try
        {
            var users = _dbContext.Users.Find(_ => true).ToList();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
}
