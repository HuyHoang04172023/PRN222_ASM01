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

        public List<dynamic> GetCategories()
        {
            return _repository.GetCategories();
        }

        public bool CreateCategory(Category category)
        {
            return _repository.CreateCategory(category);
        }
        public Category GetCategoryById(int id)
        {
            return _repository.GetCategoryById(id);
        }

        public bool UpdateCategory(Category updatedCategory)
        {
            return _repository.UpdateCategory(updatedCategory);
        }

        public bool DeleteCategory(int id)
        {
            return _repository.DeleteCategory(id);
        }
    }
}
