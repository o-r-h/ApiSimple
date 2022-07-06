using Base.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Infrastructure.Helpers
{
    public static class QueryableExtension
    {
        public static async Task<Pagination<T>> ToPaginatedListAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            var paginacion = new Pagination<T>() { TotalRows = query.Count() };
            if (pageNumber < 1 || pageSize < 1)
            {
                paginacion.List = await query.ToListAsync();
            }
            else
            {
                var salto = (pageNumber - 1) * pageSize;
                paginacion.List = await query.Skip(salto).Take(pageSize).ToListAsync();
            }

            return paginacion;
        }
    }
}
