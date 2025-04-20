// using MongoDB.Driver;
// using YourApp.Models;

// namespace YourApp.Services
// {
//     public class UserService
//     {
//         private readonly IMongoCollection<User> _users;

//         public UserService(IConfiguration config)
//         {
//             var client = new MongoClient(config["MongoDB:ConnectionString"]);
//             var database = client.GetDatabase(config["MongoDB:DatabaseName"]);
//             _users = database.GetCollection<User>("Users");
//         }

//         public async Task<User> GetByEmailAsync(string email) =>
//             await _users.Find(u => u.Email == email).FirstOrDefaultAsync();

//         public async Task CreateUserAsync(User user) =>
//             await _users.InsertOneAsync(user);
//     }
// }

// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using Server.Interfaces;
// using Server.Models;
// using Microsoft.IdentityModel.Tokens;

// namespace Server.Service
// {
//     public class TokenService : ITokenService
//     {
//         private readonly IConfiguration _config;
//         private readonly SymmetricSecurityKey _key;
//         public TokenService(IConfiguration config)
//         {
//             _config = config;
//             _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
//             //Console.WriteLine("Signing Key: " + _config["JWT:AppUser1"]);
//         }

//         public string CreateToken(AppUser user)
//         {
//             var claims = new List<Claim>
//             {
//                 new Claim(JwtRegisteredClaimNames.Email,user.Email),
//                 new Claim(JwtRegisteredClaimNames.GivenName,user.UserName),
//             };

//             var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
//             var tokenDescriptor =new SecurityTokenDescriptor
//             {
//                 Subject = new ClaimsIdentity(claims),
//                 Expires = DateTime.Now.AddDays(7),
//                 SigningCredentials = creds,
//                 Issuer = _config["JWT:Issuer"],
//                 Audience = _config["JWT:Audience"]
//             };

//             var tokenHandler = new JwtSecurityTokenHandler();

//             var token = tokenHandler.CreateToken(tokenDescriptor);

//             return tokenHandler.WriteToken(token);
//         }       
//     }
// }