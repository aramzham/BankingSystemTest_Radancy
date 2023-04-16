using Mapster;
using MapsterMapper;
using Radancy.Api.Models;
using Radancy.Api.Repositories.Contracts;
using Radancy.Api.Services.Contracts;

namespace Radancy.Api.Services;

public class UserService : BaseService, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserModel> Create()
    {
        var user = await _userRepository.Create();
        return user.Adapt<UserModel>();
    }
}