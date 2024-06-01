using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseApiController
{
    private readonly StoreContext _context;

    BuggyController(StoreContext context)
    {
        _context = context;
    }

    [HttpGet("notfound")]
    public ActionResult GetNotFoundRequest()
    {
        var thing = _context.Products.Find(42);
        if (thing == null)
        {
            return NotFound(new ApiResponse(404));
        }
        return Ok();
    }
    [HttpGet("servererror")]
    public ActionResult GetServerError()
    {
        try
        {
            var thing = _context.Products.Find(42); // Указан id для Find()
            var thingToReturn = thing.ToString(); // Если thing null, то вызовет исключение
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ApiResponse(500, ex.Message)); // Возвращаем код 500 и сообщение об ошибке
        }
    }
    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest(new ApiResponse(400));
    }
    [HttpGet("badrequest/{id}")]
    public ActionResult GetBadRequest(int id)
    {
        return Ok();
    }

}