using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Application.Interface.UseCases;
using Pacagroup.Ecommerce.Domain.Entities;
using Pacagroup.Ecommerce.Transversal.Common;
using Pacagroup.Ecommerce.Transversal.Logging;

namespace Pacagroup.Ecommerce.Application.UseCases.Users
{
    public class AuthApplication : IAuthApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly IAppLogger<AuthApplication> _logger;

        public AuthApplication(IUnitOfWork unitOfWork, IJwtService jwtService, IMapper mapper, IAppLogger<AuthApplication> logger)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<TokenDto>> SignInAsync(SignInDto signInDto)
        {
            var response = new Response<TokenDto>();

            try
            {
                var user = await _unitOfWork.Users.GetByEmailAsync(signInDto.Email);
                if (user == null)
                {
                    response.Message = "Email no existe o no se encuentra registrado";
                    _logger.LogError("Failed to validate email. Error: {Message}", response.Message);
                    return response;
                }

                var isValidPassword = await _unitOfWork.Users.CheckPasswordAsync(user, signInDto.Password);
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
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }

        public async Task<Response<bool>> SignUpAsync(SignUpDto signUpDto)
        {
            var response = new Response<bool>();
            try
            {
                var existingUser = await _unitOfWork.Users.GetByEmailAsync(signUpDto.Email);
                if (existingUser != null)
                {
                    response.Message = "El usuario ya existe";
                    return response;
                }

                var user = _mapper.Map<User>(signUpDto);
                response.Data = await _unitOfWork.Users.CreateUserAsync(user, signUpDto.Password);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Usuario creado exitosamente";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }
    }
}
