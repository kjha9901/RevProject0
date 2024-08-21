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
        Console.WriteLine("6. Approve Checkbook Request");
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
            while(active != "True" && active != "False")
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


    public void EditAccountDetails()
    {
        Console.WriteLine("Do you want to edit a customer or an admin account?");
        Console.WriteLine("1. Customer");
        Console.WriteLine("2. Admin");
        int number;
        string input = Console.ReadLine();
        if(int.TryParse(input,out number) && Convert.ToInt32(input) == 1)   // edit customer account
        {
            Console.WriteLine("Enter the account number for the customer account you wish to edit: ");
            input = Console.ReadLine();
            while(input == "" || !int.TryParse(input,out number))    //user cannot input anything other than an int
            {
                Console.WriteLine("You must enter a valid account number.");
                input = Console.ReadLine();
            }
            int editNumber = Convert.ToInt32(input);
            var customer = (from a in db.CustomerUsers
                        where a.AccNo == editNumber
                        select a).SingleOrDefault();
            if(customer != null)
            {
                string choice = "0";
                while(choice != "7")    // CHOICE
                {
                    Console.Clear();
                    Console.WriteLine("Which part of the account do you want to edit?");
                    Console.WriteLine("1. Name");
                    Console.WriteLine("2. Type of Account");
                    Console.WriteLine("3. Balance");
                    Console.WriteLine("4. Whether Account Is Active");
                    Console.WriteLine("5. Username");
                    Console.WriteLine("6. Password");
                    Console.WriteLine("7. Exit");
                    choice = Console.ReadLine();
                    switch(choice)
                    {
                        case "1":   //NAME
                            Console.WriteLine($"Current name: {customer.AccName}");
                            Console.WriteLine("Enter new name: ");
                            string newName = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9]", "x");
                            Console.WriteLine($"New name has been set to {newName}");
                            customer.AccName = newName;
                            db.SaveChanges();
                            Console.WriteLine("Press enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                        break;

                        case "2":   //TYPE OF ACCOUNT
                            Console.WriteLine($"Current account type: {customer.AccType}");
                            Console.WriteLine("Enter new account type from either Checking or Savings: ");
                            string accType = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9]", "x");
                            while(accType != "Checking" && accType != "Savings")
                            {
                                Console.WriteLine("Account type must be either Checking or Savings:");
                                accType = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9]", "x");
                            }
                            Console.WriteLine($"Account type has been set to {accType}");
                            customer.AccType = accType;
                            db.SaveChanges();
                            Console.WriteLine("Press enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                        break;

                        case "3":   //BALANCE
                            Console.WriteLine($"Current balance: {customer.AccBalance}");
                            Console.WriteLine("Enter new balance: ");
                            double balance;
                            string newBal = Console.ReadLine();
                            while(newBal == "" || !double.TryParse(newBal,out balance))    // user cannot input anything other than a double
                            {
                                Console.WriteLine("You must enter a valid value");
                                newBal = Console.ReadLine();
                            }
                            decimal? amount = Math.Round(Convert.ToDecimal(balance),2);
                            Console.WriteLine($"New balance: {amount}");
                            customer.AccBalance = amount;
                            db.SaveChanges();
                            Console.WriteLine("Press enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                        break;

                        case "4":   //ACCOUNT IS/ISN'T ACTIVE
                            Console.WriteLine($"Account is active: {customer.AccIsActive}");
                            Console.WriteLine("Enter whether account active (True/False): ");
                            string active = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "x");
                            while(active != "True" && active != "False")
                            {
                                Console.WriteLine("Must be either True or False");
                                active = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "x");
                            }
                            Console.WriteLine($"Now Account Is Active: {active}");
                            customer.AccIsActive = Convert.ToBoolean(active);
                            db.SaveChanges();
                            Console.WriteLine("Press enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                        break;

                        case "5":   //USERNAME
                            Console.WriteLine($"Current username: {customer.AccUsername}");
                            Console.WriteLine("Enter new username. Must be unique and can only contain alphanumeric values and/or the special characters !@#$%^&* (other characters will be ignored): ");
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
                            Console.WriteLine($"New username: {username}");
                            customer.AccUsername = username;
                            db.SaveChanges();
                            Console.WriteLine("Press enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                        break;

                        case "6":   //PASSWORD
                            Console.WriteLine($"Current password: {customer.AccPassword}");
                            Console.WriteLine("Enter new password. Must be at least 6 characters long and contain only alphanumeric and/or the special characters !@#$%^&* (other characters will be ignored): ");
                            string password = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "");
                            while(password.Length <= 5)
                            {
                                Console.WriteLine("Password must be at least 6 characters long");
                                password = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "");
                            }
                            Console.WriteLine($"New password: {password}");
                            customer.AccPassword = password;
                            db.SaveChanges();
                            Console.WriteLine("Press enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                        break;

                        case "7":   //EXIT
                            Console.WriteLine("Exiting...");
                        break;

                        default:
                            Console.WriteLine("Invalid Choice");
                            Console.WriteLine("Press enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Account does not exist. Cancelling operation...");
            }
        }
        else if(int.TryParse(input,out number) && Convert.ToInt32(input) == 2)  // edit admin account
        {
            Console.WriteLine("Enter the admin username for the admin account you wish to edit: ");
            input = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "");
            var admin = (from a in db.AdminUsers
                            where a.Username == input
                            select a).SingleOrDefault();
            if(admin != null)
            {
                string choice = "0";
                while(choice != "3")
                {
                    Console.Clear();
                    Console.WriteLine("Which part of the account do you want to edit?");
                    Console.WriteLine("1. Username");
                    Console.WriteLine("2. Password");
                    Console.WriteLine("3. Exit");
                    choice = Console.ReadLine();
                    switch(choice)
                    {
                        case "1":   //USERNAME
                            Console.WriteLine($"Current username: {admin.Username}");
                            Console.WriteLine("Enter new username. Must be unique and can only contain alphanumeric values and/or the special characters !@#$%^&* (other characters will be ignored): ");
                            string username = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "");
                            var userCheck = (from a in db.AdminUsers
                                            where a.Username == username
                                            select a).SingleOrDefault();
                            while(userCheck != null)
                            {
                                Console.WriteLine("Username exists, try again.");
                                Console.WriteLine("Username must be unique and can only contain alphanumeric values and/or the special characters !@#$%^&* (other characters will be ignored): ");
                                username = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "");
                                userCheck = (from a in db.AdminUsers
                                            where a.Username == username
                                            select a).SingleOrDefault();
                            }
                            Console.WriteLine($"New username: {username}");
                            admin.Username = username;
                            db.SaveChanges();
                            Console.WriteLine("Press enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                        break;

                        case "2":   //PASSWORD
                            Console.WriteLine($"Current password: {admin.Password}");
                            Console.WriteLine("Enter new password. Must be at least 6 characters long and contain only alphanumeric and/or the special characters !@#$%^&* (other characters will be ignored): ");
                            string password = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "");
                            while(password.Length <= 5)
                            {
                                Console.WriteLine("Password must be at least 6 characters long");
                                password = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "");
                            }
                            Console.WriteLine($"New password: {password}");
                            admin.Password = password;
                            db.SaveChanges();
                            Console.WriteLine("Press enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                        break;

                        case "3":   //EXIT
                            Console.WriteLine("Exiting...");
                        break;

                        default:
                            Console.WriteLine("Invalid Choice");
                            Console.WriteLine("Press enter to continue");
                            Console.ReadLine();
                            Console.Clear();
                        break;
                    }
                }
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


    public void DisplaySummary()
    {
        string input = "";
        while(input != "4")
        {
            Console.Clear();
            Console.WriteLine("1. View all requests");
            Console.WriteLine("2. View only incomplete requests");
            Console.WriteLine("3. View only completed requests");
            Console.WriteLine("4. Exit");
            input = Console.ReadLine();
            switch(input)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("All admin requests: ");
                    var requestList = db.RequestToAdmins.ToList();
                    int counter = 0;
                    foreach (var request in requestList)
                    {
                        counter++;
                        if(request.RqType == "Checkbook")
                        {
                            Console.WriteLine($"Request #: {request.RqNo}\t|\tAccount #: {request.AccNo}\t|\tRequest Type: {request.RqType}\t\t|\tRequest Is Complete: {request.RqComplete}");
                        }
                        else
                        {
                            Console.WriteLine($"Request #: {request.RqNo}\t|\tAccount #: {request.AccNo}\t|\tRequest Type: {request.RqType}\t|\tRequest Is Complete: {request.RqComplete}\t|\tPassword: {request.RqPassword}");
                        }
                        if(counter == 5)
                        {
                            Console.WriteLine("\nPress enter to view the next 5 requests");
                            Console.ReadLine();
                            Console.Clear();
                            counter = 0;
                        }
                    }
                    Console.WriteLine("End of requests. Press enter to continue");
                    Console.ReadLine();
                    Console.Clear();
                break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("All incomplete admin requests: ");
                    requestList = db.RequestToAdmins.Where(a => a.RqComplete == Convert.ToBoolean("False")).ToList();
                    counter = 0;
                    foreach (var request in requestList)
                    {
                        counter++;
                        if(request.RqType == "Checkbook")
                        {
                            Console.WriteLine($"Request #: {request.RqNo}\t|\tAccount #: {request.AccNo}\t|\tRequest Type: {request.RqType}\t\t|\tRequest Is Complete: {request.RqComplete}");
                        }
                        else
                        {
                            Console.WriteLine($"Request #: {request.RqNo}\t|\tAccount #: {request.AccNo}\t|\tRequest Type: {request.RqType}\t|\tRequest Is Complete: {request.RqComplete}\t|\tPassword: {request.RqPassword}");
                        }
                        if(counter == 5)
                        {
                            Console.WriteLine("\nPress enter to view the next 5 requests");
                            Console.ReadLine();
                            Console.Clear();
                            counter = 0;
                        }
                    }
                    Console.WriteLine("End of requests. Press enter to continue");
                    Console.ReadLine();
                    Console.Clear();
                break;

                case "3":
                    Console.Clear();
                    Console.WriteLine("All completed admin requests: ");
                    requestList = db.RequestToAdmins.Where(a => a.RqComplete == Convert.ToBoolean("True")).ToList();
                    counter = 0;
                    foreach (var request in requestList)
                    {
                        counter++;
                        if(request.RqType == "Checkbook")
                        {
                            Console.WriteLine($"Request #: {request.RqNo}\t|\tAccount #: {request.AccNo}\t|\tRequest Type: {request.RqType}\t\t|\tRequest Is Complete: {request.RqComplete}");
                        }
                        else
                        {
                            Console.WriteLine($"Request #: {request.RqNo}\t|\tAccount #: {request.AccNo}\t|\tRequest Type: {request.RqType}\t|\tRequest Is Complete: {request.RqComplete}\t|\tPassword: {request.RqPassword}");
                        }
                        if(counter == 5)
                        {
                            Console.WriteLine("\nPress enter to view the next 5 requests");
                            Console.ReadLine();
                            Console.Clear();
                            counter = 0;
                        }
                    }
                    Console.WriteLine("End of requests. Press enter to continue");
                    Console.ReadLine();
                    Console.Clear();
                break;

                case "4":
                    Console.WriteLine("Exiting...");
                break;

                default:
                    Console.WriteLine("Invalid input. Press enter to continue");
                    Console.ReadLine();
                    Console.Clear();
                break;
            }
        }
        
    }


    public void ResetCustomerPassword()
    {
        Console.WriteLine("Enter the account number whose password change request you wish to fulfill. Enter 0 to view all standing password requests: ");
        int number;
        string input = Console.ReadLine();
        while(input == "" || !int.TryParse(input,out number))    //user cannot input anything other than an int
        {
            Console.WriteLine("You must enter a valid number.");
            input = Console.ReadLine();
        }
        int custNum = Convert.ToInt32(input);
        var requestFullList = db.RequestToAdmins.Where(a => a.RqComplete == Convert.ToBoolean("False") && a.RqType == "Password Change").ToList();  //All requests
        var requestList = db.RequestToAdmins.Where(a => a.RqComplete == Convert.ToBoolean("False") && a.AccNo == custNum && a.RqType == "Password Change").ToList();    //Requests from a certain account number

        if(requestFullList != null && custNum == 0)
        {
            foreach (var request in requestFullList)
            {
                Console.WriteLine($"Request #: {request.RqNo}\t|\tAccount #: {request.AccNo}\t|\tPassword: {request.RqPassword}");
                Console.WriteLine("Approve Request? (Y/N)");
                input = Console.ReadLine().ToUpper();
                while(input != "Y" && input != "N" )
                {
                    Console.WriteLine("Must input either Y or N");
                    input = Console.ReadLine().ToUpper();
                }
                if(input == "Y")
                {
                    var customer = db.CustomerUsers.Find(request.AccNo);
                    customer.AccPassword = request.RqPassword;
                    request.RqComplete = Convert.ToBoolean("True");
                    db.SaveChanges();
                    Console.WriteLine($"Request approved. Password of customer account #{customer.AccNo} changed to: {customer.AccPassword}");
                }
                else
                {
                    Console.WriteLine("Request NOT approved");
                }
            }
            Console.WriteLine("End of request list");
        }
        else if(requestList != null && custNum != 0)
        {
            foreach (var request in requestList)
            {
                Console.WriteLine($"Request #: {request.RqNo}\t|\tAccount #: {request.AccNo}\t|\tPassword: {request.RqPassword}");
                Console.WriteLine("Approve Request? (Y/N)");
                input = Console.ReadLine().ToUpper();
                while(input!= "Y" && input != "N" )
                {
                    Console.WriteLine("Must input either Y or N");
                    input = Console.ReadLine().ToUpper();
                }
                if(input == "Y")
                {
                    var customer = db.CustomerUsers.Find(request.AccNo);
                    customer.AccPassword = request.RqPassword;
                    request.RqComplete = Convert.ToBoolean("True");
                    db.SaveChanges();
                    Console.WriteLine($"Request approved. Password of customer account #{customer.AccNo} changed to: {customer.AccPassword}");
                }
                else
                {
                    Console.WriteLine("Request NOT approved");
                }
            }
            Console.WriteLine("End of request list");
        }
        else if(requestList == null && custNum != 0)
        {
            Console.WriteLine($"No incomplete password requests from account #{custNum}");
        }
        else
        {
            Console.WriteLine("No incomplete password requests");
        }
    }


    public void ApproveCheckbookRequest()
    {
        Console.WriteLine("Enter the account number whose checkbook request you wish to fulfill. Enter 0 to view all standing checkbook requests: ");
        int number;
        string input = Console.ReadLine();
        while(input == "" || !int.TryParse(input,out number))    //user cannot input anything other than an int
        {
            Console.WriteLine("You must enter a valid number.");
            input = Console.ReadLine();
        }
        int custNum = Convert.ToInt32(input);
        var requestFullList = db.RequestToAdmins.Where(a => a.RqComplete == Convert.ToBoolean("False") && a.RqType == "Checkbook").ToList();  //All requests
        var requestList = db.RequestToAdmins.Where(a => a.RqComplete == Convert.ToBoolean("False") && a.AccNo == custNum && a.RqType == "Checkbook").ToList();    //Requests from a certain account number
        if(requestFullList != null && custNum == 0)
        {
            foreach (var request in requestFullList)
            {
                Console.WriteLine($"Request #: {request.RqNo}\t|\tAccount #: {request.AccNo}\t|\tCheckbook Is Approved: {request.RqComplete}");
                Console.WriteLine("Approve Request? (Y/N)");
                input = Console.ReadLine().ToUpper();
                while(input != "Y" && input != "N" )
                {
                    Console.WriteLine("Must input either Y or N");
                    input = Console.ReadLine().ToUpper();
                }
                if(input == "Y")
                {
                    request.RqComplete = Convert.ToBoolean("True");
                    db.SaveChanges();
                    Console.WriteLine($"Checkbook Request of account #{request.AccNo} approved");
                }
                else
                {
                    Console.WriteLine("Request NOT approved");
                }
            }
            Console.WriteLine("End of request list");
        }
        else if(requestList != null && custNum != 0)
        {
            foreach (var request in requestList)
            {
                Console.WriteLine($"Request #: {request.RqNo}\t|\tAccount #: {request.AccNo}\t|\tCheckbook Is Approved: {request.RqComplete}");
                Console.WriteLine("Approve Request? (Y/N)");
                input = Console.ReadLine().ToUpper();
                while(input != "Y" && input != "N" )
                {
                    Console.WriteLine("Must input either Y or N");
                    input = Console.ReadLine().ToUpper();
                }
                if(input == "Y")
                {
                    request.RqComplete = Convert.ToBoolean("True");
                    db.SaveChanges();
                    Console.WriteLine($"Checkbook Request of account #{request.AccNo} approved");
                }
                else
                {
                    Console.WriteLine("Request NOT approved");
                }
            }
            Console.WriteLine("End of request list");
        }
        else if(requestList == null && custNum != 0)
        {
            Console.WriteLine($"No incomplete password requests from account #{custNum}");
        }
        else
        {
            Console.WriteLine("No incomplete password requests");
        }
    }


}