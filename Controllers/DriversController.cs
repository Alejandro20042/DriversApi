using Drivers.Api.Models; // Agrega esta línea si no está presente
using Drivers.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Drivers.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriversController : ControllerBase
{

    private readonly ILogger<DriversController> _logger;
    private readonly DriverServices _driverServices;

    public DriversController(ILogger<DriversController> logger, DriverServices driverServices)
    {
        _logger = logger;
        _driverServices = driverServices;
    }

    [HttpGet]
    public async Task<IActionResult> GetDrivers()
    {
        var drivers = await _driverServices.GetAsync();
        return Ok(drivers);
    }
    [HttpPost]
public async Task<IActionResult> InsertDriver([FromBody] Driver driverToInsert)
{
    if (driverToInsert == null)
        return BadRequest();

    if (string.IsNullOrEmpty(driverToInsert.Name))
        ModelState.AddModelError("Name", "El driver no debe estar vacío");

    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    await _driverServices.InsertDriver(driverToInsert);

    return Created("Created", true);
}

[HttpDelete("{id}")]
public async Task<IActionResult> DeleteDriver(string idToDelete)
{
    if (idToDelete == null)
        return BadRequest();

    if (idToDelete == string.Empty)
        ModelState.AddModelError("Id", "No debe dejar el id vacío");

    await _driverServices.DeleteDriver(idToDelete);

    return Ok();
}

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDriver(string id, [FromBody] Driver driverToUpdate)
    {
        if (driverToUpdate == null)
            return BadRequest();

        if (string.IsNullOrEmpty(driverToUpdate.Id))
            ModelState.AddModelError("Id", "No debe dejar el id vacío");

        if (string.IsNullOrEmpty(driverToUpdate.Name))
            ModelState.AddModelError("Name", "No debe dejar el nombre vacío");

        if (driverToUpdate.Number <= 0)
            ModelState.AddModelError("Number", "No debe dejar el Number vacío o con un número inválido");

        if (string.IsNullOrEmpty(driverToUpdate.Team))
            ModelState.AddModelError("Team", "No debe dejar el Team vacío");

        await _driverServices.UpdateDriver(driverToUpdate);

        return Ok();
    }
}