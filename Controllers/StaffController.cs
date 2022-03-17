using Hotel.DTOs;
using Hotel.Models;
using Hotel.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Controllers;

[ApiController]
[Route("api/staff")]
public class StaffController : ControllerBase
{
    private readonly ILogger<StaffController> _logger;
    private readonly IStaffRepository _staff;
    private readonly IRoomRepository _room;

    public StaffController(ILogger<StaffController> logger, IStaffRepository staff, IRoomRepository room)
    {
        _logger = logger;
        _staff = staff;
        _room = room;
    }

    [HttpGet]
    public async Task<ActionResult> GetList()
    {
        var res = await _staff.GetList();
        return Ok(res.Select(x => x.asDto));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StaffDTO>> GetById([FromRoute] int id)
    {
        var res = await _staff.GetById(id);
        if (res is null)
            return NotFound();

        var dto = res.asDto;
        dto.Room = (await _room.GetAllForStaff(id)).Select(x => x.asDto).ToList();
        return Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] StaffCreateDTO Data)
    {
        var toCreateStaff = new Staff
        {
            Name = Data.Name,
            DateOfBirth = Data.DateOfBirth.UtcDateTime,
            Mobile = Data.Mobile,
            Shift = Data.Shift

        };
        var res = await _staff.Create(toCreateStaff);

        return StatusCode(StatusCodes.Status201Created, res.asDto);

    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromRoute] int id, [FromBody] StaffUpdateDTO Data)
    {
        var existingStaff = await _staff.GetById(id);

        if (existingStaff == null)
            return NotFound();

        var toUpdateSchedule = existingStaff with
        {
            Mobile = Data.Mobile,
            Shift = Data.Shift

        };

        var didUpdate = await _staff.Update(toUpdateSchedule);

        if (!didUpdate)
            return StatusCode(StatusCodes.Status500InternalServerError);

        return NoContent();
    }





}
