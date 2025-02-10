using HE170292_SE1814_NET_A01.Models;
using HE170292_SE1814_NET_A01.Repositories;

namespace HE170292_SE1814_NET_A01.Services
{
    public class SystemAccountService : ISystemAccountService
    {
        private readonly ISystemAccountRepository _repository;
        public SystemAccountService()
        {
            _repository = new SystemAccountRepository();
        }

        public List<SystemAccount> GetSystemAccounts()
        {
            return _repository.GetSystemAccounts();
        }

        public bool CreateSystemAccount(SystemAccount newAccount)
        {
            return _repository.CreateSystemAccount(newAccount);
        }

        public SystemAccount GetSystemAccountById(short id)
        {
            return _repository.GetSystemAccountById(id);
        }

        public bool UpdateSystemAccount(SystemAccount updatedAccount)
        {
            return _repository.UpdateSystemAccount(updatedAccount);
        }

        public bool DeleteSystemAccountById(short id)
        {
            return _repository.DeleteSystemAccountById(id);
        }
    }
}
