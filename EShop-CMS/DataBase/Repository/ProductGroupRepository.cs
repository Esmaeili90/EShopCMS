using EShop_CMS.DataBase.Context;
using EShop_CMS.DataBase.Contracts;
using EShop_CMS.DataBase.Model;
using Microsoft.EntityFrameworkCore;

namespace EShop_CMS.DataBase.Repository
{
    public class ProductGroupRepository:IProductGroupRepository
    {
        DefaultContext _context;
        public ProductGroupRepository(DefaultContext context)
        {
            _context = context;
        }

        public int AddGroup(ProductGroup group)
        {
            _context.Group.Add(group);
            _context.SaveChanges();
            return group.Id;
        }

        public void UpdateGroup(ProductGroup group)
        {
            var tracked = _context.ChangeTracker.Entries<ProductGroup>()
                .FirstOrDefault(e => e.Entity.Id == group.Id);

            if (tracked != null)
            {
                _context.Entry(tracked.Entity).State = EntityState.Detached;
            }

            _context.Group.Update(group);
            _context.SaveChanges();
        }

        public void DeleteGroup(int groupId)
        {
            var group = GetGroupById(groupId);
            DeleteGroup(group);
        }

        public void DeleteGroup(ProductGroup group)
        {
            _context.Group.Remove(group);
            _context.SaveChanges();
        }

        public ProductGroup GetGroupById(int groupId)
        {
            return _context.Group.FirstOrDefault(g => g.Id == groupId);
        }

        

        public List<ProductGroup> GetAllGroups()
        {
            return _context.Group.ToList();
        }

        public List<ProductGroup> SearchGroups(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return GetAllGroups();
            }

            return _context.Group
                .Where(g => g.GroupTitle.Contains(keyword))
                .ToList();
        }
        public ProductGroup GetGroupByIdNoTracking(int groupId)
        {
            return _context.Group.AsNoTracking().FirstOrDefault(g => g.Id == groupId);
        }
    }
}
