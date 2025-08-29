using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motorcycles.Domain.Abstractions.Repositories;

public interface IUnitOfWork
{
    Task CommitAsync();
}
