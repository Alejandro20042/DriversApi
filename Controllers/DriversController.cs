using Drivers.Api.Models;
using Drivers.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

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
        if(driverToInsert == null)
            return BadRequest();
        if(driverToInsert.Name == string.Empty)
            ModelState.AddModelError("Name","El driver no debe estar vacio");

        await _driverServices.InsertDriver(driverToInsert);

        return Created("Created",true);
    }

    [HttpDelete("ID")]
    public async Task<IActionResult> DeleteDriver(string idToDelete)
    {
        if(idToDelete == null)
            return BadRequest();
        if(idToDelete == string.Empty)
            ModelState.AddModelError("Id","No debe dejar el id vacio");

        await _driverServices.DeleteDriver(idToDelete);

        return Ok();
    }

    [HttpPut("DriverToUpdate")]
    public async Task<IActionResult> UpdateDriver(Driver driverToUpdate)
    {
        if(driverToUpdate == null)
            return BadRequest();
        if(driverToUpdate.Id == string.Empty)
            ModelState.AddModelError("Id","No debe dejar el id vacio");
        if(driverToUpdate.Name == string.Empty)
            ModelState.AddModelError("Name","No debe dejar el nombre vacio");
        if(driverToUpdate.Number <= 0 )
            ModelState.AddModelError("Numbre","No debe dejar el numero vacio o con un nuemro invalido");
        if(driverToUpdate.Team == string.Empty)
            ModelState.AddModelError("Team","No debe dejar el Team vacio");

        await _driverServices.UpdateDriver(driverToUpdate);

        return Ok();
    }

    [HttpGet("ID")]
    public async Task<IActionResult> GetDriverById(string idToSearch)
    {
        var drivers = await _driverServices.GetDriverById(idToSearch);
        return Ok(drivers);
    }

}