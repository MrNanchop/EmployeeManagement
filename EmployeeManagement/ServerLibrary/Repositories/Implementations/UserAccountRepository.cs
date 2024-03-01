using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using ServerLibrary.Data;
using ServerLibrary.Helper;
using ServerLibrary.Repositories.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    public class UserAccountRepository(IOptions<JwtSection> config, AppDbContext context) :IUserAccount
    {
        public async Task<GeneralResponse> CreateAsync(Register user)
        {
            if(user is null)
            { return new GeneralResponse(false, "Model is empty"); }

            var checkUser = await FindUserByEmail(user.Email!);
            if(checkUser is not null)
            { return new GeneralResponse(false, "User is already registered"); }


            var appuser = new ApplicationUser()
            {
                Email = user.Email,
                CreateOn = DateTime.Now,
                Id=Guid.NewGuid(),
                Password=BCrypt.Net.BCrypt.HashPassword(user.Password),
                Name=user.Fullname
            };

            var checkAdminRole = await context.SystemRoles.FirstOrDefaultAsync(_ => _.Name!.Equals(Constants.Admin));
            if(checkAdminRole is null)
            {
                var createAdminRole = await AddToDataBase(new SystemRole() { Name =Constants.Admin ,CreateOn=DateTime.Now});
                await AddToDataBase(new UserRole() { RoleId = createAdminRole.Id, UserId=appuser.Id });
            }

            var checkUserRole = await context.SystemRoles.FirstOrDefaultAsync(r => r.Name!.Equals(Constants.User));

            var response = new SystemRole();
            if(checkUserRole is null)
            {
                response= await AddToDataBase(new SystemRole() { Name =Constants.User,CreateOn=DateTime.Now });
                await AddToDataBase(new UserRole() { RoleId = response.Id, UserId=appuser.Id });
            }
            else
            {
                await AddToDataBase(new UserRole() { RoleId = checkUserRole.Id, UserId=appuser.Id, });
            }
            return new GeneralResponse(true, "Account created!");
        }

        private async Task<ApplicationUser> FindUserByEmail(string email)
        {
            var user = await context.ApplicationUsers.FirstOrDefaultAsync(_ => _.Email!.ToLower()!.Equals(email!.ToLower()));
            return user!;
        }
        
        private async Task<T> AddToDataBase<T>(T model)
        {
            var result = context.Add(model);
            await context.SaveChangesAsync();
            return (T)result.Entity;
        }

        public async Task<LoginResponse> SignInAsync(Login user)
        {

            throw new NotImplementedException();
        }

    }
}
