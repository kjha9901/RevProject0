using System.Text.RegularExpressions;
using Project_0.Models;


public class AdminOperations
{

    P0KennyBankingDbContext db = new P0KennyBankingDbContext();


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


    public int GetID(string username, string password)
    {
        int id = (from a in db.AdminUsers
                            where a.Username == username && a.Password == password
                            select a.AdminAccNo).SingleOrDefault();
        return id;
    }


    public void CreateNewAccount()
    {
        Console.WriteLine("Do you want to create a customer or an admin account?");
        Console.WriteLine("1. Customer");
        Console.WriteLine("2. Admin");
        int number;
        string input = Console.ReadLine();
        if(int.TryParse(input,out number) && Convert.ToInt32(input) == 1)   // create customer account
        {
            Console.WriteLine("Enter name: ");
            string name = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9]", "x");
            Console.WriteLine("Enter account type: ");
            string accType = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9]", "x");
            while(accType != "Checking" && accType != "Savings")
            {
                Console.WriteLine("Account type must be either Checking or Savings:");
                accType = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9]", "x");
            }
            Console.WriteLine("Enter whether account is active or not: ");
            string active = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "x");
            while(active != "True" && accType != "False")
            {
                Console.WriteLine("Must be either True or False");
                active = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "x");
            }
            Console.WriteLine("Enter account username. Must be unique and can only contain alphanumeric values and/or the special characters !@#$%^&* (other characters will be ignored): ");
            string username = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "");
            var userCheck = (from a in db.CustomerUsers
                            where a.AccUsername == username
                            select a).SingleOrDefault();
            while(userCheck != null)
            {
                Console.WriteLine("Username exists, try again.");
                Console.WriteLine("Username must be unique and can only contain alphanumeric values and/or the special characters !@#$%^&* (other characters will be ignored): ");
                username = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "");
                userCheck = (from a in db.CustomerUsers
                            where a.AccUsername == username
                            select a).SingleOrDefault();
            }
            Console.WriteLine("Enter account password. Must be at least 6 characters long and contain only alphanumeric and/or the special characters !@#$%^&* (other characters will be ignored): ");
            string password = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "");
            while(password.Length <= 5)
            {
                Console.WriteLine("Password must be at least 6 characters long");
                password = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "");
            }
            CustomerUser newCust = new CustomerUser();
            newCust.AccName = name;
            newCust.AccType = accType;
            newCust.AccBalance = 0;
            newCust.AccIsActive = Convert.ToBoolean(active);
            newCust.AccUsername = username;
            newCust.AccPassword = password;
            db.CustomerUsers.Add(newCust);
            db.SaveChanges();
            Console.WriteLine($"New customer account created with username: {username} and password: {password}");
        }
        else if(int.TryParse(input,out number) && Convert.ToInt32(input) == 2)  // create admin account
        {
           Console.WriteLine("Enter account username. Must be unique and can only contain alphanumeric values and/or the special characters !@#$%^&*: ");
            string username = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "");
            var userCheck = (from a in db.AdminUsers
                            where a.Username == username
                            select a).SingleOrDefault();
            while(userCheck != null)
            {
                Console.WriteLine("Username exists, try again.");
                Console.WriteLine("Username must be unique and can only contain alphanumeric values and/or the special characters !@#$%^&*: ");
                username = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "");
                userCheck = (from a in db.AdminUsers
                            where a.Username == username
                            select a).SingleOrDefault();
            }
            Console.WriteLine("Enter account password. Must be at least 6 characters long and contain only alphanumeric and/or the special characters !@#$%^&*: ");
            string password = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "");
            while(password.Length <= 5)
            {
                Console.WriteLine("Password must be at least 6 characters long");
                password = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "");
            }
            AdminUser newAdmin = new AdminUser();
            newAdmin.Username = username;
            newAdmin.Password = password;
            db.AdminUsers.Add(newAdmin);
            db.SaveChanges();
            Console.WriteLine($"New admin account created with username: {username} and password: {password}");
        }
        else
        {
            Console.WriteLine("Invalid input, cancelling...");
        }
    }

    public void DeleteAccount(string currentUsername)
    {
        Console.WriteLine("Do you want to delete a customer or an admin account?");
        Console.WriteLine("1. Customer");
        Console.WriteLine("2. Admin");
        int number;
        string input = Console.ReadLine();
        if(int.TryParse(input,out number) && Convert.ToInt32(input) == 1)   // delete customer account
        {
            Console.WriteLine("Enter the account number for the customer account you wish to delete: ");
            input = Console.ReadLine();
            while(input == "" || !int.TryParse(input,out number))    //user cannot input anything other than an int
            {
                Console.WriteLine("You must enter a valid account number.");
                input = Console.ReadLine();
            }
            int deleteNumber = Convert.ToInt32(input);
            var deleteCustomer = (from a in db.CustomerUsers
                        where a.AccNo == deleteNumber
                        select a).SingleOrDefault();
            if(deleteCustomer != null)
            {
                CustomerUser removeCustomer = db.CustomerUsers.Find(deleteNumber);
                db.CustomerUsers.Remove(removeCustomer);
                Console.WriteLine($"Customer account #{deleteNumber} deleted.");
                db.SaveChanges();
            }
            else
            {
                Console.WriteLine("Account does not exist. Cancelling operation...");
            }
        }
        else if(int.TryParse(input,out number) && Convert.ToInt32(input) == 2)  //delete admin account
        {
            Console.WriteLine("Enter the username for the admin account you wish to delete: ");
            string deleteUsername = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "");
            while(deleteUsername == currentUsername)
            {
                 Console.WriteLine("You are currently using that account. Enter another username");
                 deleteUsername = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "");
            }
            var deleteAdmin = (from a in db.AdminUsers
                        where a.Username == deleteUsername
                        select a).SingleOrDefault();
            if(deleteAdmin != null)
            {
                AdminUser removeAdmin = db.AdminUsers.Find(deleteAdmin.AdminAccNo);
                db.AdminUsers.Remove(removeAdmin);
                Console.WriteLine($"Admin account {deleteAdmin.Username} deleted.");
                db.SaveChanges();
            }
            else
            {
                Console.WriteLine("Account does not exist. Cancelling operation...");
            }
        }
        else
        {
            Console.WriteLine("Invalid input, cancelling...");
        }
    }

    public void EditAccountDetails(CustomerUser changes)
    {
        
    }

}