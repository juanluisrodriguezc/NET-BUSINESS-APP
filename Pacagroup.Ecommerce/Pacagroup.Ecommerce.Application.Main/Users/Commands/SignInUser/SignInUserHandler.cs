using AutoMapper;
using MediatR;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Transversal.Common;
using Pacagroup.Ecommerce.Transversal.Logging;

namespace Pacagroup.Ecommerce.Application.UseCases.Users.Commands.SignInUser
{
    public class SignInUserHandler : IRequestHandler<SignInUserCommand, Response<TokenDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IAppLogger<SignInUserHandler> _logger;

        public SignInUserHandler(IUnitOfWork unitOfWork, IMapper mapper, IJwtService jwtService, IAppLogger<SignInUserHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task<Response<TokenDto>> Handle(SignInUserCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<TokenDto>();
            var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);
            if (user == null)
            {
                response.Message = "Email no existe o no se encuentra registrado";
                _logger.LogError("Failed to validate email. Error: {Message}", response.Message);
                return response;
            }

            var isValidPassword = await _unitOfWork.Users.CheckPasswordAsync(user, request.Password);
            if (!isValidPassword)
            {
                response.Message = "Credenciales inválidas";
                return response;
            }

            var token = _jwtService.GenerateToken(user);
            response.Data = new TokenDto
            {
                AccessToken = token,
                ExpiresIn = 3600
            };

            response.IsSuccess = true;
            response.Message = "Autenticación exitosa";

            return response;
        }
    }
}
