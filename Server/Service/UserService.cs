using MongoDB.Driver;
using Server.Data;
using Server.Models;


namespace Server.Service
{
    public class UserService
    {
        private readonly MongoDbContext _context;

        public UserService(MongoDbContext context)
        {
             _context = context;
        }

        public async Task<AppUser?> GetByEmailAsync(string email)
        {
            var filter = Builders<AppUser>.Filter.Eq(u => u.Email, email.ToLower());
            return await _context.Users.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateUserAsync(AppUser user) =>
            await _context.Users.InsertOneAsync(user);
    }
}
