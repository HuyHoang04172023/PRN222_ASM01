using HE170292_SE1814_NET_A01.Models;

namespace HE170292_SE1814_NET_A01.DataAccess
{
    public class CategoryDAO
    {
        private readonly FunewsManagementContext _context;
        public CategoryDAO()
        {
            _context = new FunewsManagementContext();
        }
        public IQueryable<Category> GetAll() => _context.Categories;
    }
}
