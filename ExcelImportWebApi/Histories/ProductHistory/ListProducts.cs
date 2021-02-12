

using Domain.Models;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Histories.ProductHistory
{
    public class ListProducts
    {
        private readonly ExcelContext _context;

        public ListProducts(ExcelContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> Run()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
