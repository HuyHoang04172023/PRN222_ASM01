using HE170292_SE1814_NET_A01.Models;
using HE170292_SE1814_NET_A01.Repositories;

namespace HE170292_SE1814_NET_A01.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;
        public TagService()
        {
            _repository = new TagRepository();
        }
        public List<Tag> GetTags()
        {
            return _repository.GetTags();
        }
    }
}
