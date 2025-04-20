using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Identity.Mongo.Model;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Models
{
    public class AppRole : MongoIdentityRole
    {
        public string? Role { get; set; }
    }

    public class MongoIdentityRole
    {
    }
}