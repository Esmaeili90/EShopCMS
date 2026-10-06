using EShop_CMS.DataBase.Model;

namespace EShop_CMS.DataBase.Contracts
{
    public interface IProductGroupRepository
    {
        int AddGroup(ProductGroup group);
        void UpdateGroup(ProductGroup group);
        void DeleteGroup(int groupId);
        void DeleteGroup(ProductGroup group);
        ProductGroup GetGroupById(int groupId);
        List<ProductGroup> GetAllGroups();
        List<ProductGroup> SearchGroups(string keyword);
        ProductGroup GetGroupByIdNoTracking(int groupId);
    }
}
