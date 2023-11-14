using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interview.Tasks.Refactoring
{
    public class RoleServices : IRoleServices
    {
        readonly EfDbContext _dbContext;
        IMapper _mapper;

        public RoleServices(EfDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleModels>> GetAvailableRoles()
        {

            return await _dbContext.Roles
                .Select(t => _mapper.Map<Role, RoleModels>(t))
                .ToListAsync();

        }

        public async Task<IEnumerable<RoleModels>> AddUserRole(int userId, int roleId)
        {
            var user = await _dbContext.Users
                .Include(t => t.Roles)
                .Where(t => t.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null) return null;

            var role = await _dbContext.Roles
                .Where(t => t.Id == roleId)
                .FirstOrDefaultAsync();

            if (role == null) return null;

            if (!user.Roles.Contains(role))
            {
                user.Roles.Add(role);
                _dbContext.SaveChanges();
                return user.Roles.Select(t => _mapper.Map<Role, RoleModels>(t));
            }
            else
            {
                return user.Roles.Select(t => _mapper.Map<Role, RoleModels>(t));
            }
        }

        public async Task<IEnumerable<RoleModels>> DeleteUserRole(int userId, int roleId)
        {
            var user = await _dbContext.Users
                .Include(t => t.Roles)
                .Where(t => t.Id == userId)
                .FirstOrDefaultAsync();

            if (user == null) return null;

            if (user.Roles.Where(t => t.Id == roleId).FirstOrDefault() != null)
            {
                user.Roles.Remove(user.Roles.First(t => t.Id == roleId));
                _dbContext.SaveChanges();
                return user.Roles.Select(t => _mapper.Map<Role, RoleModels>(t));
            }
            else
            {
                return user.Roles.Select(t => _mapper.Map<Role, RoleModels>(t));
            }
        }
    }
}
