using System;
using System.Threading.Tasks;
using Dapper;
using PointOfSales.Web.Database;
using PointOfSales.Web.Dtos;
using PointOfSales.Web.Services.Contracts;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace PointOfSales.Web.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IDatabaseConnectionFactory _databaseConnectionFactory;
        private readonly IConfiguration _config;
        public UserService(IConfiguration config, IDatabaseConnectionFactory databaseConnectionFactory)
        {
            _databaseConnectionFactory = databaseConnectionFactory;
            _config = config;
        }

        public async Task<ResponseDto<UserLoginResultDto>> Login(UserLoginDto userLoginDto)
        {
            using var db = _databaseConnectionFactory.GetDbConnection();
            var jwtConfig = _config.GetSection("JWTConfig");
            var secret = jwtConfig.GetValue<string>("secret");
            var user = await db.QueryFirstOrDefaultAsync<UserDto>(
		        @"select name, password, email, id 
                  from users where name = @Name", new { Name = userLoginDto.Name });

            if (user == null) 
				return new ResponseDto<UserLoginResultDto> { Success = false, Message = "User not found." };

            if (!BCrypt.Net.BCrypt.Verify(userLoginDto.Password + secret, user.Password)) 
				return new ResponseDto<UserLoginResultDto> { Success = false, Message = "Password does not match." };

            var nextMinutes = jwtConfig.GetValue<double>("expirationInMinutes");
            var expiryDate = DateTime.Now.AddMinutes(nextMinutes);
            var issuer = jwtConfig.GetValue<string>("issuer");
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.Id.ToString()) }),
                Expires = expiryDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);


            var data = new UserLoginResultDto { Token = tokenHandler.WriteToken(token) };
			return new ResponseDto<UserLoginResultDto> { Success = true, Message = "Login succes.", Data = data };

        }

        public async Task<ResponseDto<UserRegisterResultDto>> Register(UserRegisterDto userRegisterDto)
        {
         
            using var db = _databaseConnectionFactory.GetDbConnection();

            var checkUser = await db.ExecuteScalarAsync<bool>(
		        @"select count(1) 
                  from users 
                  where email = @Email 
                  or name = @Name",  userRegisterDto);
            if (checkUser)
            {
                return new ResponseDto<UserRegisterResultDto> { Success = false, Message = "User already exist." };
            }

            var secret = _config.GetValue<string>("JWTConfig:secret");
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegisterDto.Password + secret);
            userRegisterDto.Password = hashedPassword;

            var lastInsertId = await db.QueryFirstOrDefaultAsync<long>(
                    @"insert into users
                    (name, email, password)
                    values
                    (@Name, @Email, @Password);
                    select last_insert_id()", userRegisterDto);

            var data = await db.QueryFirstOrDefaultAsync<UserRegisterResultDto>(
                @"select name, email
                from users
                where id = @LastInsertId", new { LastInsertId = lastInsertId });
            return new ResponseDto<UserRegisterResultDto> { Data = data,Success = true, Message = "Success." };

        }
    }
}
