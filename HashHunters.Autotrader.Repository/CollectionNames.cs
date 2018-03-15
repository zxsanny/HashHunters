using HashHunters.Autotrader.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace HashHunters.Autotrader.Repository
{
    //Names here should be THE SAME as collection names in database
    public enum CollectionName
    {
        Users
    }

    public static class RepositoryHelper
    {
        public static Dictionary<Type, CollectionName> CollectionTypes = new Dictionary<Type, CollectionName>
        {
            { typeof(User), CollectionName.Users }
        };

        public static IMongoCollection<T> GetTypedCollection<T>(this IMongoDatabase mongoDatabase)
        {
            if (!CollectionTypes.ContainsKey(typeof(T)))
            {
                throw new Exception($"Type {typeof(T).Name} is not registered in CollectionTypes!");
            }
            var name = CollectionTypes[typeof(T)].ToString();
            return mongoDatabase.GetCollection<T>(name);
        }
    }
}
