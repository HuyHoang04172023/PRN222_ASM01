using HE170292_SE1814_NET_A01.Models;
using HE170292_SE1814_NET_A01.Repositories;

namespace HE170292_SE1814_NET_A01.Services
{
    public class NewArticleService : INewArticleService
    {
        private readonly INewsArticleRepository _repository;
        public NewArticleService()
        {
            _repository = new NewsArticleRepository();
        }
        
        public List<dynamic> GetNewsArticles()
        {
            return _repository.GetNewsArticles();
        }

        public bool CreateNewsArticle(NewsArticle article, List<int> selectedTags, int? userId)
        {
            return _repository.CreateNewsArticle(article, selectedTags, userId);
        }

        public NewsArticle? GetNewsArticleById(string newsArticleId)
        {
            return _repository.GetNewsArticleById(newsArticleId);
        }

        public bool UpdateNewsArticle(NewsArticle updatedArticle, List<int> selectedTags, int? userId)
        {
            return _repository.UpdateNewsArticle(updatedArticle, selectedTags, userId);
        }

        public bool DeleteNewsArticleById(string newsArticleId)
        {
            return _repository.DeleteNewsArticleById(newsArticleId);
        }

        public List<NewsArticle> GetNewsArticlesByUser(short userId)
        {
            return _repository.GetNewsArticlesByUser(userId);
        }

        public List<dynamic> GetNewsArticlesActive()
        {
            return _repository.GetNewsArticlesActive();
        }

        public List<NewsArticle> GetNewsReport(DateTime startDate, DateTime endDate)
        {
            return _repository.GetNewsReport(startDate, endDate);
        }
    }
}
