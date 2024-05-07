using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PikaShop.Data.Context;
using PikaShop.Data.Context.ContextEntities.Core;
using PikaShop.Data.Contracts.Repositories;

namespace PikaShop.Data.Persistence.Repositories
{
    public class ReviewRepository(ApplicationDbContext _context)
        : Repository<ReviewEntity, int>(_context),

        IReviewRepository
    {

    }
}
