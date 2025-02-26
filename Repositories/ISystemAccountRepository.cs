using HE170292_SE1814_NET_A01.Models;

namespace HE170292_SE1814_NET_A01.Repositories
{
    public interface ISystemAccountRepository
    {
        List<SystemAccount> GetSystemAccounts();
        bool CreateSystemAccount(SystemAccount newAccount);
        SystemAccount GetSystemAccountById(short id);
        bool UpdateSystemAccount(SystemAccount updatedAccount);
        bool DeleteSystemAccountById(short id);
    }
}
