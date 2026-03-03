using Microsoft.AspNetCore.Mvc;
using OnedayOneDev_Shared.Repository.Interface;
using OnedayOneDev_Shared.Request;
using OnedayOneDev_Shared.Service;
using OnedayOneDev_Shared.Service.Interface;

[ApiController]
[Route("api/auth")]
public class AuthController(JwtTokenService jwt, IUserService userService) : ControllerBase
{
    private readonly JwtTokenService _jwt = jwt;
    private IUserService _userService = userService;


    [HttpPost("login")]
    public IActionResult Login([FromForm] AuthLoginRequest request)
    {
        
        if (_userService.IsUser(request.userName,request.password))
        {

            var token = _jwt.GenerateToken(request.userName, role: "Admin");

            return Ok(new
            {
                access_token = token,
                token_type = "Bearer",
                expires_in = 3600
            });
        }

        return Unauthorized("Bad credentials");
    }
}