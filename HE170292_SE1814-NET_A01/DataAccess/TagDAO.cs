using HE170292_SE1814_NET_A01.Models;

namespace HE170292_SE1814_NET_A01.DataAccess
{
    public class TagDAO
    {
        private readonly FunewsManagementContext _context;
        public TagDAO()
        {
            _context = new FunewsManagementContext();
        }
        public List<Tag> GetTags() => _context.Tags.ToList();
    }
}
