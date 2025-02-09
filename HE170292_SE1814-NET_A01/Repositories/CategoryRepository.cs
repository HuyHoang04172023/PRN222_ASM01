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
        public List<Category> GetCategories()
        {
            return _dao.GetAll().ToList();
        }
    }
}
