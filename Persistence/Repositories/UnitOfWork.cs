using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
