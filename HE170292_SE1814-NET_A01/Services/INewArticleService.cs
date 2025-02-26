using HE170292_SE1814_NET_A01.Models;

namespace HE170292_SE1814_NET_A01.Services
{
    public interface INewArticleService
    {
        List<dynamic> GetNewsArticles();
        bool CreateNewsArticle(NewsArticle article, List<int> selectedTags, int? userId);
        NewsArticle? GetNewsArticleById(string newsArticleId);
        bool UpdateNewsArticle(NewsArticle updatedArticle, List<int> selectedTags, int? userId);
        bool DeleteNewsArticleById(string newsArticleId);
        List<NewsArticle> GetNewsArticlesByUser(short userId);
        List<dynamic> GetNewsArticlesActive();
        List<NewsArticle> GetNewsReport(DateTime startDate, DateTime endDate);
    }
}
