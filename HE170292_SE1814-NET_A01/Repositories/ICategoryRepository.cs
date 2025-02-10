using HE170292_SE1814_NET_A01.Models;

namespace HE170292_SE1814_NET_A01.Repositories
{
    public interface ICategoryRepository
    {
        List<dynamic> GetCategories();
        bool CreateCategory(Category category);
        Category GetCategoryById(int id);
        bool UpdateCategory(Category updatedCategory);
        bool DeleteCategory(int id);
    }
}
