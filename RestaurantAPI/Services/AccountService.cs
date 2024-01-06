using AutoMapper;
using Microsoft.AspNetCore.Identity;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDTO dto);
    }

    public class AccountService : IAccountService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher<User> _hasher;

        public AccountService(RestaurantDbContext dbContext, IMapper mapper, IPasswordHasher<User> hasher)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hasher = hasher;
        }

        public void RegisterUser(RegisterUserDTO dto)
        {
            var user = _mapper.Map<User>(dto);
            var passwordHash = _hasher.HashPassword(user, dto.Password);
            user.PasswordHash = passwordHash;   
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }
    }
}
