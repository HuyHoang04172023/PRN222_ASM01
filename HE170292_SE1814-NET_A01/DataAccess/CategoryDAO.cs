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
        public List<dynamic> GetCategories()
        {
            var query = from category in _context.Categories
                        join parent in _context.Categories
                        on category.ParentCategoryId equals parent.CategoryId into parentGroup
                        from parent in parentGroup.DefaultIfEmpty()
                        select new
                        {
                            CategoryId = category.CategoryId,
                            CategoryName = category.CategoryName,
                            CategoryDescription = category.CategoryDesciption,
                            ParentCategoryName = parent != null ? parent.CategoryName : "None",
                            IsActive = category.IsActive
                        };

            return query.ToList<dynamic>();
        }

        public bool CreateCategory(Category category)
        {
            if (category == null)
            {
                return false;
            }

            _context.Categories.Add(category);
            _context.SaveChanges(); 
            return true;
        }
        public Category GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == id);
        }

        public bool UpdateCategory(Category updatedCategory)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == updatedCategory.CategoryId);
            if (category == null)
            {
                return false;
            }

            category.CategoryName = updatedCategory.CategoryName;
            category.CategoryDesciption = updatedCategory.CategoryDesciption;
            category.ParentCategoryId = updatedCategory.ParentCategoryId;
            category.IsActive = updatedCategory.IsActive;

            try
            {
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return false;
            }

            bool isUsedInArticles = _context.NewsArticles.Any(na => na.CategoryId == id);
            if (isUsedInArticles)
            {
                return false;
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return true;
        }
    }
}
