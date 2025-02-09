using HE170292_SE1814_NET_A01.Models;
using HE170292_SE1814_NET_A01.Repositories;

namespace HE170292_SE1814_NET_A01.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        public CategoryService()
        {
            _repository = new CategoryRepository();
        }
        public List<Category> GetCategories()
        {
            return _repository.GetCategories();
        }
    }
}
