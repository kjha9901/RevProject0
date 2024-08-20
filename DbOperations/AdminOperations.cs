using Project_0.Models;


public class AdminOperations
{

    P0KennyBankingDbContext db = new P0KennyBankingDbContext();

    public bool CheckAdminLogin(string username, string password)
    {
        var checkAdmin = (from a in db.AdminUsers
                        where a.Username == username && a.Password == password
                        select a).SingleOrDefault();
        if(checkAdmin != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DisplayAdminMenu()
    {
        Console.WriteLine("1. Create New Account");
        Console.WriteLine("2. Delete Account");
        Console.WriteLine("3. Edit Account Details");
        Console.WriteLine("4. Display Summary");
        Console.WriteLine("5. Reset Customer Password");
        Console.WriteLine("6. Approve Cheque Book Request");
        Console.WriteLine("7. Exit");
    }

    public void CreateNewAccount(AdminUser accObj)
    {
        //db.AccountInfos.Add(accObj);
        db.SaveChanges();
    }

    public void DeleteAccount(int accNo)
    {
        //AccountInfo accObj = db.AccountInfos.Find(accNo);
        //db.AccountInfos.Remove(accObj);
        db.SaveChanges();
    }

    public void EditAccountDetails(CustomerUser changes)
    {
        CustomerUser acc = db.CustomerUsers.Find(changes.AccNo);
        if(acc != null)
        {
            acc.AccName = changes.AccName;
            acc.AccType = changes.AccType;
            acc.AccBalance = changes.AccBalance;
            acc.AccIsActive = changes.AccIsActive;
            db.CustomerUsers.Update(acc);
            db.SaveChanges();
        }
    }

}