using HE170292_SE1814_NET_A01.DataAccess;
using HE170292_SE1814_NET_A01.Models;

namespace HE170292_SE1814_NET_A01.Repositories
{
    public class SystemAccountRepository : ISystemAccountRepository
    {
        private readonly SystemAccountDAO _dao;
        public SystemAccountRepository()
        {
            _dao = new SystemAccountDAO();
        }

        public List<SystemAccount> GetSystemAccounts()
        {
            return _dao.GetSystemAccounts();
        }

        public bool CreateSystemAccount(SystemAccount newAccount)
        {
            return _dao.CreateSystemAccount(newAccount);
        }

        public SystemAccount GetSystemAccountById(short id)
        {
            return _dao.GetSystemAccountById(id);
        }

        public bool UpdateSystemAccount(SystemAccount updatedAccount)
        {
            return _dao.UpdateSystemAccount(updatedAccount);
        }

        public bool DeleteSystemAccountById(short id)
        {
            return _dao.DeleteSystemAccountById(id) ;
        }
    }
}
