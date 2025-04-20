using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.DTOs.Account
{
    public class UserDto
    {
        [BsonElement("Username")]
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}