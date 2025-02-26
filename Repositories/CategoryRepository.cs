using HE170292_SE1814_NET_A01.DataAccess;
using HE170292_SE1814_NET_A01.Models;

namespace HE170292_SE1814_NET_A01.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDAO _dao;
        public CategoryRepository()
        {
            _dao = new CategoryDAO();
        }

        public List<dynamic> GetCategories()
        {
            return _dao.GetCategories();
        }

        public bool CreateCategory(Category category)
        {
            return _dao.CreateCategory(category);
        }
        public Category GetCategoryById(int id)
        {
            return _dao.GetCategoryById(id);
        }

        public bool UpdateCategory(Category updatedCategory)
        {
            return _dao.UpdateCategory(updatedCategory);
        }

        public bool DeleteCategory(int id)
        {
            return _dao.DeleteCategory(id);
        }
    }
}
