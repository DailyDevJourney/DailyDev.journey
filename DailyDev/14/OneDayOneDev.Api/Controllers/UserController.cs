using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnedayOneDev_Shared.Identification;
using OnedayOneDev_Shared.Request;
using OnedayOneDev_Shared.ResultData;
using OnedayOneDev_Shared.Service;
using OnedayOneDev_Shared.Service.Interface;

namespace OneDayOneDev.Api.Controllers
{
    [Authorize(Roles = nameof(UserRole.ADMIN))]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IUserService userService) : Controller
    {

        private readonly IUserService _userService = userService;


        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUser([FromQuery] GetUserRequest request)
        {
            var Users = _userService.GetUsersList();

            return Users is null ? NotFound() : Ok(Users?.ConvertToPageResult(page: request.PageIndex, pageSize: request.PageSize));
        }

        [HttpDelete("DeleteAUser")]
        public IActionResult DeleteUser(int identifiant)
        {
            var result = _userService.DeleteUser(identifiant);

            return result.Success
                ? Ok(result)
                : BadRequest(result.Message);
        }

        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser([FromBody] UserUpdateRequest request)
        {
            var result = _userService.UpdateUser(request.identifiant, request.NewName, request.NewPassword, request.NewRole);

            return result.Success
                ? Ok(result)
                : BadRequest(result.Message);
        }
        [HttpPost("CreateUser")]
        public IActionResult CreateUser([FromQuery] UserCreationRequest request)
        {
            var result = _userService.CreateNewUser(request.UserName,request.Password,request.Role);

            return result.Success
                ? Ok(result)
                : BadRequest(result.Message);
        }
    }
}
