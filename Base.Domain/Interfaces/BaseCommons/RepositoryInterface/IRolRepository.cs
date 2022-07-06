using Base.Domain.Entities.BaseCommons;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Base.Domain.Interfaces.BaseCommons.RepositoryInterface

{
    public interface IRolRepository
    {
        Task DeleteASync(long rolid);
        Task<List<Rol>> SelectAll();
        Task<Rol> SelectByIdASync(long rolid);
        Task UpdateASync(long rolId, Rol obj);
    }
}