using MiniBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniBlog.Interfaces
{
    public interface IUserStore
    {
        List<User> Users { get; }
    }
}
