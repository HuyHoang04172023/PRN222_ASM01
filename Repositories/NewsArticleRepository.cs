using HE170292_SE1814_NET_A01.DataAccess;
using HE170292_SE1814_NET_A01.Models;

namespace HE170292_SE1814_NET_A01.Repositories
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        private readonly NewsArticleDAO _dao;
        public NewsArticleRepository()
        {
            _dao = new NewsArticleDAO();
        }

        public List<dynamic> GetNewsArticles()
        {
            return _dao.GetNewsArticles();
        }

        public bool CreateNewsArticle(NewsArticle article, List<int> selectedTags, int? userId)
        {
            return _dao.CreateNewsArticle(article, selectedTags, userId);
        }

        public NewsArticle? GetNewsArticleById(string newsArticleId)
        {
            return _dao.GetNewsArticleById(newsArticleId);
        }

        public bool UpdateNewsArticle(NewsArticle updatedArticle, List<int> selectedTags, int? userId)
        {
            return _dao.UpdateNewsArticle(updatedArticle, selectedTags, userId);
        }

        public bool DeleteNewsArticleById(string newsArticleId)
        {
            return _dao.DeleteNewsArticleById(newsArticleId);
        }

        public List<NewsArticle> GetNewsArticlesByUser(short userId)
        {
            return _dao.GetNewsArticlesByUser(userId);
        }

        public List<dynamic> GetNewsArticlesActive()
        {
            return _dao.GetNewsArticlesActive();
        }

        public List<NewsArticle> GetNewsReport(DateTime startDate, DateTime endDate)
        {
            return _dao.GetNewsReport(startDate, endDate);
        }
    }
}
