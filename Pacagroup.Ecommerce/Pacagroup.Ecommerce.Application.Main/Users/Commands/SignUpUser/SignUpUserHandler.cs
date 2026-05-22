using AutoMapper;
using MediatR;
using Pacagroup.Ecommerce.Application.Interface.Persistence;
using Pacagroup.Ecommerce.Domain.Entities;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.UseCases.Users.Commands.SignUpUser
{
    public class SignUpUserHandler : IRequestHandler<SignUpUserCommand, Response<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public SignUpUserHandler(IUnitOfWork unitOfWork, IMapper mapper, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<Response<bool>> Handle(SignUpUserCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>();
            var existingUser = await _unitOfWork.Users.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                response.Message = "El usuario ya existe";
                return response;
            }

            var user = _mapper.Map<User>(request);
            response.Data = await _unitOfWork.Users.CreateUserAsync(user, request.Password);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Usuario creado exitosamente";
            }

            return response;
        }
    }
}
