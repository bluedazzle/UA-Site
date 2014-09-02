using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
//using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;

namespace test2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    //public class ApplicationUser : Microsoft.AspNet.Identity.EntityFramework.IdentityUser
    //{
    //}
    [Table("users")]
    public partial class IdentityUser : IUser<string>
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(32)]
        [MinLength(4)]
        public string UserName { get; set; }
        public int StudentId { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public DateTime RegisterTime { get; set; }
        public bool Enable { get; set; }

        public virtual IdentityGroup Group { get; set; }
        public virtual ICollection<UserClaim> Claims { get; set; }
        public virtual ICollection<IdentityRole> Roles { get; set; }
        public virtual ICollection<Content> PublishedContents { get; set; }

        public IdentityUser()
        {
            Id = Guid.NewGuid().ToString();
            RegisterTime = DateTime.Now;
        }
    }
    [Table("roles")]
    public class IdentityRole : IRole<string>
    {
        [Key]
        public string Id { get; set; }
        [Required]
        [MaxLength(32)]
        [MinLength(4)]
        public string Name { get; set; }
        public bool Enable { get; set; }

        public virtual ICollection<IdentityGroup> Groups { get; set; }
        public virtual ICollection<IdentityUser> Users { get; set; }
        public IdentityRole()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
    [Table("claims")]
    public class UserClaim
    {
        [Key]
        public int Id { get;  set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public virtual IdentityUser User { get; set; }
    }
    [Table("groups")]
    public class IdentityGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<IdentityRole> Roles { get; set; }
        public virtual ICollection<IdentityUser> Members { get; set; }
    }
    public class UserStore :
        IUserStore<IdentityUser>,
        IUserRoleStore<IdentityUser>, 
        IUserClaimStore<IdentityUser>,
        IUserPasswordStore<IdentityUser>,
        IUserSecurityStampStore<IdentityUser>
    {
        public WebsiteDbContext Context { get; private set; }
        public UserStore(WebsiteDbContext context)
        {
            Context = context;
        }
        public Task CreateAsync(IdentityUser user)
        {
            Context.Users.Add(user);
            return Context.SaveChangesAsync();
        }

        public Task DeleteAsync(IdentityUser user)
        {
            Context.Users.Remove(user);
            return Context.SaveChangesAsync();
        }

        public Task<IdentityUser> FindByIdAsync(string userId)
        {
            if (!Context.Users.Any(n => n.Id == userId))
                return Task.FromResult<IdentityUser>(null);
            return Context.Users.FirstAsync(n => n.Id == userId);
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            if (!Context.Users.Any(n => n.UserName == userName))
                return Task.FromResult<IdentityUser>(null);
            return Context.Users.FirstAsync(n => n.UserName == userName);
        }

        public Task UpdateAsync(IdentityUser user)
        {
            Context.Users.Attach(user);
            return Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.SaveChanges();
            if (Context != null)
                Context.Dispose();
            Context = null;
        }
        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            return Task.FromResult(user != null);
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Context.SaveChangesAsync();
        }

        public Task<string> GetSecurityStampAsync(IdentityUser user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task SetSecurityStampAsync(IdentityUser user, string stamp)
        {
            user.SecurityStamp = stamp;
            return Context.SaveChangesAsync();
        }


        public Task AddClaimAsync(IdentityUser user, Claim claim)
        {
            user.Claims.Add(new UserClaim()
            {
                Type = claim.Type,
                Value = claim.Value,
            });
            return Context.SaveChangesAsync();
        }

        public Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            List<Claim> cs = new List<Claim>();
            if(user.Claims!=null)
                foreach (var i in user.Claims)
                {
                    cs.Add(new Claim(i.Type, i.Value));
                }

            return Task.FromResult<IList<Claim>>(cs);
        }

        public Task RemoveClaimAsync(IdentityUser user, Claim claim)
        {
            Context.Claims.Remove(Context.Claims.FirstOrDefaultAsync(c => c.User == user).Result);
            return Context.SaveChangesAsync();
        }

        public Task AddToRoleAsync(IdentityUser user, string roleName)
        {
            user.Roles.Add(Context.Roles.FirstAsync(r => r.Name == roleName).Result);
            return Context.SaveChangesAsync();
        }

        public Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            return Task.FromResult<IList<string>>(user.Roles != null ? user.Roles
                .Select(n => n.Name)
                .ToList() : new List<string>());
        }

        public Task<bool> IsInRoleAsync(IdentityUser user, string roleName)
        {
            return Task.FromResult(user.Roles.Any(u => u.Name == roleName));
        }

        public Task RemoveFromRoleAsync(IdentityUser user, string roleName)
        {
            user.Roles.Remove(Context.Roles.FirstAsync(r => r.Name == roleName).Result);
            return Context.SaveChangesAsync();
        }
    }
    public class RoleStore : 
        IRoleStore<IdentityRole>
    {
        public WebsiteDbContext Context { get; private set; }
        public RoleStore(WebsiteDbContext context)
        {
            Context = context;
        }

        public Task CreateAsync(IdentityRole role)
        {
            Context.Roles.Add(role);
            return Context.SaveChangesAsync();
        }

        public Task DeleteAsync(IdentityRole role)
        {
            Context.Roles.Remove(role);
            return Context.SaveChangesAsync();
        }

        public Task<IdentityRole> FindByIdAsync(string roleId)
        {
            if (!Context.Roles.AnyAsync(n => n.Id == roleId).Result)
                return Task.FromResult<IdentityRole>(null);
            return Context.Roles.FirstAsync(n => n.Id == roleId);
        }

        public Task<IdentityRole> FindByNameAsync(string roleName)
        {
            if (!Context.Roles.AnyAsync(n => n.Name == roleName).Result)
                return Task.FromResult<IdentityRole>(null);
            return Context.Roles.FirstAsync(n => n.Name == roleName);
        }

        public Task UpdateAsync(IdentityRole role)
        {
            Context.Roles.Attach(role);
            return Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.SaveChanges();
            if (Context != null)
                Context.Dispose();
            Context = null;
        }
    }
}