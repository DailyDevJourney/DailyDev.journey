using Microsoft.AspNetCore.Mvc;
using OnedayOneDev_Shared.Repository.Interface;
using OnedayOneDev_Shared.Request;
using OnedayOneDev_Shared.Service;
using OnedayOneDev_Shared.Service.Interface;
using System.Security.Claims;

[ApiController]
[Route("api/auth")]
public class AuthController(JwtTokenService jwt,
                            IUserService userService,
                            IConfiguration configuration) : ControllerBase
{
    private readonly JwtTokenService _jwt = jwt;
    private IUserService _userService = userService;
    private readonly IConfiguration _configuration = configuration;


    [HttpPost("login")]
    public IActionResult Login([FromForm] AuthLoginRequest request)
    {
        try
        {
            // Vérifie déjà que tu reçois bien les champs
            if (request is null)
                return BadRequest("request is null");

            // selon tes noms
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest($"Form fields missing: userName='{request.Username}', password length={(request.Password?.Length ?? 0)}");

            var ok = _userService.IsUser(request.Username, request.Password);

            if (!ok)
                return Unauthorized("Bad credentials");

            var user = _userService.GetUsersByUsername(request.Username);
            var token = _jwt.GenerateToken(user.UserName, user.Role.ToString());
            var expireMinutes = _configuration.GetValue<int>("Jwt:ExpireMinutes");

            return Ok(new { access_token = token, token_type = "Bearer", expires_in = expireMinutes * 60 });
        }
        catch (Exception ex)
        {
            // TEMPORAIRE (debug)
            return Problem(ex.ToString());
        }
    }

    [HttpGet("me")]
    public IActionResult Me()
    {
        var username = User.FindFirst(ClaimTypes.Name)?.Value;

        if (string.IsNullOrWhiteSpace(username))
            return Unauthorized();

        var user = _userService.GetUsersByUsername(username);

        if (user == null)
            return NotFound();

        return Ok(user);
    }
}