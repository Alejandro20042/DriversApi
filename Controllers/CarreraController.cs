using Drivers.Api.Models; // Agrega esta línea si no está presente
using Drivers.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Drivers.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarreraController : ControllerBase
{

    private readonly ILogger<CarreraController> _logger;
    private readonly CarreraServices _carreraServices;

    public CarreraController(ILogger<CarreraController> logger, CarreraServices carreraServices)
    {
        _logger = logger;
        _carreraServices = carreraServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetDrivers()
    {
        var carrera = await _carreraServices.GetAsync();
        return Ok(carrera);
    }

    [HttpPost]
public async Task<IActionResult> InsertCarrera([FromBody] Carrera carreraToInsert)
{
    if (carreraToInsert == null)
        return BadRequest();

    if (string.IsNullOrEmpty(carreraToInsert.Name))
        ModelState.AddModelError("Name", "El driver no debe estar vacío");

    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    await _carreraServices.InsertCarrera(carreraToInsert);

    return Created("Created", true);
}

[HttpDelete("{id}")]
public async Task<IActionResult> DeleteCarrera(string idToDelete)
{
    if (idToDelete == null)
        return BadRequest();

    if (idToDelete == string.Empty)
        ModelState.AddModelError("Id", "No debe dejar el id vacío");

    await _carreraServices.DeleteCarrera(idToDelete);

    return Ok();
}

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCarrera(Carrera carreraToUpdate)
    {
        if (carreraToUpdate == null)
            return BadRequest();

        if (string.IsNullOrEmpty(carreraToUpdate.Id))
            ModelState.AddModelError("Id", "No debe dejar el id vacío");

        if (string.IsNullOrEmpty(carreraToUpdate.Name))
            ModelState.AddModelError("Name", "No debe dejar el nombre vacío");

        await _carreraServices.UpdateCarrera(carreraToUpdate);

        return Ok();
    }
}