using HE170292_SE1814_NET_A01.DataAccess;
using HE170292_SE1814_NET_A01.Models;

namespace HE170292_SE1814_NET_A01.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly TagDAO _dao;
        public TagRepository()
        {
            _dao = new TagDAO();
        }
        public List<Tag> GetTags()
        {
            return _dao.GetTags();
        }
    }
}
