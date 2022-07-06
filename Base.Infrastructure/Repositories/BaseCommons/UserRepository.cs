using Base.Domain.Entities.BaseCommons;
using Base.Domain.Interfaces.BaseCommons.RepositoryInterface;
using Base.Domain.Models.User;
using Base.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Base.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly BaseCommonsContext context;
        public UserRepository(BaseCommonsContext context)
        {
            this.context = context;
        }


        //CRUD


        /// <summary>
        /// Create new record for User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        public async Task<long> Insert(User data)
        {
            try
            {
                context.Set<User>().Add(data);
                await context.SaveChangesAsync();
                return data.UserId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            
        }

        /// <summary>
        /// Select by Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<User> SelectByIdASync(long userId)
        {
            User obj = await context.Set<User>().FindAsync(userId);
            return obj;
        }

        /// <summary>
        /// Delete physical record User
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task DeleteASync(long userId)
        {
            context.Set<User>().Remove(await SelectByIdASync(userId));
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Update User data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<User> UpdateASync(long userId, User obj)
        {
            var objPrev = (from iClass in context.Users where iClass.UserId == userId select iClass).FirstOrDefault();
            context.Entry(objPrev).State = EntityState.Detached;
            context.Entry(obj).State = EntityState.Modified;

            await context.SaveChangesAsync();
            return obj;
        }

        /// <summary>
        /// List All Rols
        /// </summary>
        /// <param name="incidentId"></param>
        /// <returns></returns>
        public async Task<List<User>> SelectAll()
        {
            return await context.Set<User>().ToListAsync();
        }


        //CUSTOM ACTIONS
        public async Task<User> ChangeUserStatusASync(long userId, long userStatusId)
        {
            User objPrev = await context.Set<User>().FindAsync(userId);
            objPrev.UserStatusId = userStatusId;
            context.Entry(objPrev).State = EntityState.Detached;
            context.Entry(objPrev).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return objPrev;
        }

        public Task<User> GetByEmail(string email)
        {
            var objPrev = (from iClass in context.Users where iClass.Email.ToLower() == email.ToLower() select iClass).FirstOrDefaultAsync();
            return objPrev;
        }

        public Task<List<User>> GetByCompanyId(long companyId)
        {
            var objPrev = (from iClass in context.Users where iClass.CompanyId == companyId select iClass).ToListAsync();
            return objPrev;
        }

        public IQueryable<User> GetByFilter(UserFilter filter, long companyId)
        {
            var query = context.Users
                .Where(x => x.CompanyId == companyId);

            if (filter.UserId > 0)
            {
                query = query.Where(x => x.UserId == filter.UserId);
            }

            if (filter.UserStatusId > 0)
            {
                query = query.Where(x => x.UserStatusId == filter.UserStatusId);
            }

            return query;
        }

        public Task<List<User>> GetByIdList(List<long> ids)
        {
            return context.Users.Where(x => ids.Any(y => y == x.UserId)).ToListAsync();
        }

        public async Task UpdatePasswordRecoveryToken(long userId, Guid token)
        {
            var user = await SelectByIdASync(userId);
            if (user != null)
            {
                user.RecoveryToken = token;
                user.TokenExpiration = DateTime.Now.AddHours(1);
                await UpdateASync(userId,user);
            }
        }


        public Task<User> GetByPasswordRecoveryToken(Guid token)
        {
            return context.Users.FirstOrDefaultAsync(x => x.RecoveryToken == token);
        }

        public async Task<User> RejectedEmailInvitation(long userId)
        {
            var user =  context.Users
                .Where(x => x.UserId == userId).FirstOrDefault();
            context.Remove<User>(user);
            await context.SaveChangesAsync();
            return user;
        }

        public async Task<User> AproveEmailIntivations(long userId)
        {
            var data = context.Users
                .Where(x => x.UserId == userId).FirstOrDefault();
            data.UserStatusId = 1;
            context.Entry(data).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return data;
        }

        public async Task InactiveUser(long userId)
        {
            var user = await SelectByIdASync(userId);
            if (user != null)
            {
                user.UserStatusId = 2;
                await UpdateASync(userId, user);
            }
        }

    }
}
