using HE170292_SE1814_NET_A01.Models;

namespace HE170292_SE1814_NET_A01.DataAccess
{
    public class SystemAccountDAO
    {
        private readonly FunewsManagementContext _context;
        public SystemAccountDAO()
        {
            _context = new FunewsManagementContext();
        }

        public List<SystemAccount> GetSystemAccounts() => _context.SystemAccounts.ToList();

        public bool CreateSystemAccount(SystemAccount newAccount)
        {
            if (newAccount == null)
            {
                return false;
            }

            bool emailExists = _context.SystemAccounts.Any(a => a.AccountEmail == newAccount.AccountEmail);
            if (emailExists)
            {
                return false;
            }

            var account = new SystemAccount {
                AccountId = 0,
                AccountName = newAccount.AccountName,
                AccountEmail = newAccount.AccountEmail,
                AccountRole = newAccount.AccountRole,
                AccountPassword = newAccount.AccountPassword,
            };
            _context.SystemAccounts.Add(account);
            _context.SaveChanges();

            return true;
        }


        public SystemAccount GetSystemAccountById(short id)
        {
            return _context.SystemAccounts.FirstOrDefault(a => a.AccountId == id);
        }

        public bool UpdateSystemAccount(SystemAccount updatedAccount)
        {
            var account = _context.SystemAccounts.Find(updatedAccount.AccountId);

            if (account == null)
            {
                return false;
            }

            account.AccountName = updatedAccount.AccountName;
            account.AccountEmail = updatedAccount.AccountEmail;
            account.AccountRole = updatedAccount.AccountRole;
            account.AccountPassword = updatedAccount.AccountPassword;

            _context.Update(account);
            _context.SaveChanges();

            return true;
        }

        public bool DeleteSystemAccountById(short id)
        {
            var account = _context.SystemAccounts.Find(id);

            if (account == null)
            {
                return false;
            }

            _context.SystemAccounts.Remove(account);
            _context.SaveChanges();

            return true;
        }

    }
}
