using Base.Domain.Entities.BaseCommons;
using Base.Domain.Interfaces.BaseCommons.RepositoryInterface;
using Base.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.Infrastructure.Repositories
{

    public class RolRepository : IRolRepository
    {
        public readonly BaseCommonsContext context;
        public RolRepository(BaseCommonsContext context)
        {
            this.context = context;
        }

        //CRUD

        /// <summary>
        /// Create new record for Rol
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Rol> SelectByIdASync(long rolid)
        {
            Rol obj = await context.Set<Rol>().FindAsync(rolid);
            return obj;
        }

        /// <summary>
        /// Delete physical record Rol
        /// </summary>
        /// <param name="rolid"></param>
        /// <returns></returns>
        public async Task DeleteASync(long rolid)
        {
            context.Rols.RemoveRange(context.Rols.Where(x => x.RolId == rolid));
            context.Set<Rol>().Remove(await SelectByIdASync(rolid));
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Update Rol data
        /// </summary>
        /// <param name="rolId"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task UpdateASync(long rolId, Rol obj)
        {
            var objPrev = (from iClass in context.Rols where iClass.RolId == rolId select iClass).FirstOrDefault();
            context.Entry(objPrev).State = EntityState.Detached;
            context.Entry(obj).State = EntityState.Modified;

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// List All Rols
        /// </summary>
        /// <param name="incidentId"></param>
        /// <returns></returns>
        public async Task<List<Rol>> SelectAll()
        {
            return await context.Set<Rol>().ToListAsync();
        }

    }



}
