using ETicaretAPI.Application.Abstractions.Token;
using ETicaretAPI.Application.DTOs;
using ETicaretAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETicaretAPI.Application.Features.Commands.AppUser.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest,LoginUserCommandResponse>
{
    public LoginUserCommandHandler(UserManager<Domain.Entities.Identity.AppUser> userManager, SignInManager<Domain.Entities.Identity.AppUser> signInManager, ITokenHandler tokenHandler)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHandler = tokenHandler;
    }

    private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
    private readonly SignInManager<Domain.Entities.Identity.AppUser> _signInManager;
    private readonly ITokenHandler _tokenHandler;

    

    public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
    {
        Domain.Entities.Identity.AppUser appuser =await _userManager.FindByNameAsync(request.UserNameOrEmail);
        if (appuser == null)
            appuser = await _userManager.FindByEmailAsync(request.UserNameOrEmail);

        if (appuser == null)
            throw new NotFoundUserException();

        SignInResult result = await _signInManager.CheckPasswordSignInAsync(appuser, request.Password, false);
        if (result.Succeeded) // Authentication Başarılı
        {
            Token token =_tokenHandler.CreateAccessToken(5);
            return new LoginUserSuccessCommandResponse()
            {
                Token = token
            };
        }

        throw new AuthenticationErrorException();
        


    }
}