using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ETicaretAPI.Application.Abstractions.Storage
{
    public interface IStorageService : IStorage
    {
        public string StorageName { get;}
    }
}