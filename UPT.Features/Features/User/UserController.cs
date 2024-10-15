using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UPT.Features.Base;

namespace UPT.Features.Features.User;

internal class UserController : BaseController
{
    //[HttpPost]
    //[ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    //public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request,
    //[FromServices] IOperationContextFactory<CreateUserCommand, UserResponse> factory)
    //{
    //    var command = new CreateUserCommand(request: request, HttpContext.User);
    //    var context = factory.Build(command);
    //    var result = await Handle(context);
    //    return result;
    //}
}
