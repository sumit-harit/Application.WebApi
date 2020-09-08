using Application.WebApi.Application.DTOs.Account;
using Application.WebApi.Application.Wrappers;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.WebApi.Application.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request, string origin);
        Task<Response<string>> ConfirmEmailAsync(string userId, string code);
        
        Task ForgotPassword(ForgotPasswordRequest model, string origin);
        Task<Response<string>> ResetPassword(ResetPasswordRequest model);

        Task<Response<AuthenticationResponse>> TwoFactorAuthentication(string userId, string code, string ipAddress);
        Task<Response<QrCode>> GenerateQRCode(string userId, string code);
    }
}
