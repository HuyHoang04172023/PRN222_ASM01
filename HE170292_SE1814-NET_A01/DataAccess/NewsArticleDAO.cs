using HE170292_SE1814_NET_A01.Models;
using Microsoft.EntityFrameworkCore;

namespace HE170292_SE1814_NET_A01.DataAccess
{
    public class NewsArticleDAO
    {
        private readonly FunewsManagementContext _context;
        public NewsArticleDAO()
        {
            _context = new FunewsManagementContext();
        }

        public List<dynamic> GetNewsArticles()
        {
            var articles = _context.NewsArticles
                .Select(article => new
                {
                    NewsArticleID = article.NewsArticleId,
                    NewsTitle = article.NewsTitle,
                    Headline = article.Headline,
                    CategoryName = article.Category != null ? article.Category.CategoryName : "N/A",
                    CreatedByEmail = article.CreatedBy != null ? article.CreatedBy.AccountEmail : "N/A",
                    UpdatedByEmail = article.UpdatedBy != null ? article.UpdatedBy.AccountEmail : "N/A",
                    CreatedDate = article.CreatedDate,
                    NewsStatus = article.NewsStatus,
                    Tags = article.Tags.Select(tag => tag.TagName).ToList()
                })
                .ToList<dynamic>();

            return articles;
        }

        public bool CreateNewsArticle(NewsArticle article, List<int> selectedTags, int? userId)
        {
            if (article == null)
            {
                return false;
            }

            article.NewsArticleId ??= Guid.NewGuid().ToString();

            article.CreatedDate = DateTime.UtcNow;
            article.CreatedById = userId.HasValue ? (short)userId.Value : null;

            article.UpdatedById = null;
            article.ModifiedDate = null;

            if (selectedTags != null && selectedTags.Any())
            {
                var tags = _context.Tags.Where(t => selectedTags.Contains(t.TagId)).ToList();
                article.Tags = tags;
            }

            _context.NewsArticles.Add(article);

            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database Error: " + ex.Message);
                return false;
            }
        }

        public NewsArticle? GetNewsArticleById(string newsArticleId)
        {
            return _context.NewsArticles
                .Include(na => na.Category)
                .Include(na => na.Tags)
                .Include(na => na.CreatedBy)
                .Include(na => na.UpdatedBy)
                .FirstOrDefault(na => na.NewsArticleId == newsArticleId);
        }

        public bool UpdateNewsArticle(NewsArticle updatedArticle, List<int> selectedTags, int? userId)
        {
            var existingArticle = _context.NewsArticles
                .Include(na => na.Tags)
                .FirstOrDefault(na => na.NewsArticleId == updatedArticle.NewsArticleId);

            if (existingArticle == null)
            {
                return false;
            }

            existingArticle.NewsTitle = updatedArticle.NewsTitle;
            existingArticle.Headline = updatedArticle.Headline;
            existingArticle.NewsContent = updatedArticle.NewsContent;
            existingArticle.NewsSource = updatedArticle.NewsSource;
            existingArticle.CategoryId = updatedArticle.CategoryId;
            existingArticle.NewsStatus = updatedArticle.NewsStatus;
            existingArticle.UpdatedById = userId.HasValue ? (short)userId.Value : null;
            existingArticle.ModifiedDate = DateTime.UtcNow;

            existingArticle.Tags.Clear();
            if (selectedTags != null && selectedTags.Any())
            {
                var tags = _context.Tags.Where(t => selectedTags.Contains(t.TagId)).ToList();
                existingArticle.Tags = tags;
            }

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

        public bool DeleteNewsArticleById(string newsArticleId)
        {
            var article = _context.NewsArticles
                .Include(na => na.Tags)
                .FirstOrDefault(na => na.NewsArticleId == newsArticleId);

            if (article == null)
            {
                return false;
            }

            if (article.Tags.Any())
            {
                article.Tags.Clear();
            }

            _context.NewsArticles.Remove(article);

            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database Error: " + ex.Message);
                return false;
            }
        }

        public List<NewsArticle> GetNewsArticlesByUser(short userId)
        {
            return _context.NewsArticles
                .Include(na => na.Category)
                .Include(na => na.Tags)
                .Where(na => na.CreatedById == userId)
                .ToList();
        }

        public List<dynamic> GetNewsArticlesActive()
        {
            var articles = _context.NewsArticles
                .Select(article => new
                {
                    NewsArticleID = article.NewsArticleId,
                    NewsTitle = article.NewsTitle,
                    Headline = article.Headline,
                    CategoryName = article.Category != null ? article.Category.CategoryName : "N/A",
                    CreatedByEmail = article.CreatedBy != null ? article.CreatedBy.AccountEmail : "N/A",
                    UpdatedByEmail = article.UpdatedBy != null ? article.UpdatedBy.AccountEmail : "N/A",
                    CreatedDate = article.CreatedDate,
                    NewsStatus = article.NewsStatus,
                    Tags = article.Tags.Select(tag => tag.TagName).ToList()
                })
                .Where(article => article.NewsStatus == true)
                .ToList<dynamic>();

            return articles;
        }

        public List<NewsArticle> GetNewsReport(DateTime startDate, DateTime endDate)
        {
            return _context.NewsArticles
                .Where(n => n.CreatedDate >= startDate && n.CreatedDate <= endDate)
                .Include(na => na.Category)
                .Include(na => na.CreatedBy)
                .OrderByDescending(n => n.CreatedDate)
                .ToList();
        }
    }
}
