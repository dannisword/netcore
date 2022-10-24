using Microsoft.AspNetCore.Mvc;

using DS.Order.Interface;
using DS.Common.Entities;

namespace DS.Order.API.Controllers;

[ApiController]
[Route("v1/api/[controller]")]
public class UserController : ControllerBase
{
    public IUserService Service { get; private set; }
    public UserController(IUserService service)
    {
        this.Service = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return this.Ok(this.Service.Reads());
    }

    [HttpPost]
    public IActionResult Post(UserAccount user)
    {
        return this.Ok(this.Service.Add(user));
    }
}