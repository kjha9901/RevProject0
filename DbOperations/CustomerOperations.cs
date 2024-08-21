
using Project_0.Models;
using System.Text.RegularExpressions;

public class CustomerOperations
{

    P0KennyBankingDbContext db = new P0KennyBankingDbContext();


    public void DisplayCustomerMenu()
    {
        Console.WriteLine("1. Check Account Details");
        Console.WriteLine("2. Withdraw");
        Console.WriteLine("3. Deposit");
        Console.WriteLine("4. Transfer");
        Console.WriteLine("5. Last 5 transactions");
        Console.WriteLine("6. Request Cheque Book");
        Console.WriteLine("7. Change Password");
        Console.WriteLine("8. Exit");
    }


    public bool CheckCustomerLogin(string username, string password)
    {
        var checkCustomer = (from a in db.CustomerUsers
                            where a.AccUsername == username && a.AccPassword == password
                            select a).SingleOrDefault();
        
        if(checkCustomer != null)
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
        int id = (from a in db.CustomerUsers
                            where a.AccUsername == username && a.AccPassword == password
                            select a.AccNo).SingleOrDefault();
        return id;
    }


    public void AccountDetails(int id)
    {
        CustomerUser customerInfo = db.CustomerUsers.Find(id);
        if(customerInfo != null)
        {
            Console.WriteLine("Account details: ");
            Console.WriteLine("Name: "+customerInfo.AccName);
            Console.WriteLine("Type of account: "+customerInfo.AccType);
            Console.WriteLine("Balance: "+customerInfo.AccBalance);
            Console.WriteLine("Active account: "+customerInfo.AccIsActive);
        }
        else
        {
            Console.WriteLine("Account Not Found");
        }
    }


    public void Withdraw(int id)
    {
        CustomerUser customer = db.CustomerUsers.Find(id);

        double number;
        Console.WriteLine("How much do you want to withdraw?");
        string input = Console.ReadLine();
        while(input == "" || !double.TryParse(input,out number))    //user cannot input anything other than a number
        {
            Console.WriteLine("You must enter a valid value");
            input = Console.ReadLine();
        }
        decimal? amount = Math.Round(Convert.ToDecimal(number),2);
        if(amount > customer.AccBalance)
        {
            Console.WriteLine("Insufficient balance");
        }
        else if(amount < (decimal?).01)
        {
            Console.WriteLine("Must withdraw at least 1 cent");
        }
        else
        {
            CustomerTransaction tr1 = new CustomerTransaction();
            tr1.AccNo = id;
            tr1.TrType = "Withdrawal";
            tr1.TrAmount = amount;
            db.CustomerTransactions.Add(tr1);
            customer.AccBalance -= amount;
            Console.WriteLine("Withdrawal successful. New balance: "+customer.AccBalance);
            db.SaveChanges();
        }
    }


    public void Deposit(int id)
    {
        CustomerUser customer = db.CustomerUsers.Find(id);
        double number;
        Console.WriteLine("How much do you want to deposit?");
        string input = Console.ReadLine();
        while(input == "" || !double.TryParse(input,out number))    //user cannot input anything other than a double
        {
            Console.WriteLine("You must enter a valid value");
            input = Console.ReadLine();
        }
        decimal? amount = Math.Round(Convert.ToDecimal(number),2);
        if(amount < (decimal?).01)
        {
            Console.WriteLine("Must deposit at least 1 cent");
        }
        else
        {
            CustomerTransaction tr1 = new CustomerTransaction();
            tr1.AccNo = id;
            tr1.TrType = "Deposit";
            tr1.TrAmount = amount;
            db.CustomerTransactions.Add(tr1);
            customer.AccBalance += amount;
            Console.WriteLine("Deposit successful. New balance: "+customer.AccBalance);
            db.SaveChanges();
        }
    }


     public void Transfer(int id1)
    {
        CustomerUser customer = db.CustomerUsers.Find(id1);
        
        //set transfer account destination to id2
        Console.WriteLine("Enter the account number to transfer to: ");
        string input = Console.ReadLine();
        int number;
        while(input == "" || !int.TryParse(input,out number))    //user cannot input anything other than an int
        {
            Console.WriteLine("You must enter a valid account number.");
            input = Console.ReadLine();
        }
        int id2 = Convert.ToInt32(number);
        var transferDestination = (from a in db.CustomerUsers
                            where a.AccNo == id2
                            select a).SingleOrDefault();
        // checks for if account with id2 exists, if it doesnt, cancels transfer
        if(transferDestination != null && id1 != id2)
        {
            CustomerUser customer2 = db.CustomerUsers.Find(id2);
            Console.WriteLine("How much do you want to transfer?");
            double number2;
            string input2 = Console.ReadLine();
            while(input2 == "" || !double.TryParse(input2,out number2))    //sets deposit amount, user cannot input anything other than a double
            {
                Console.WriteLine("You must enter a valid value");
                input2 = Console.ReadLine();
            }
            decimal? amount = Math.Round(Convert.ToDecimal(number2),2);
            if(amount > customer.AccBalance)
            {
                Console.WriteLine("Insufficient balance");
            }
            else if(amount < (decimal?).01)
            {
                Console.WriteLine("Must transfer at least 1 cent");
            }
            else
            {
                // transaction of user to id2 is added to transaction table
                CustomerTransaction tr1 = new CustomerTransaction();
                tr1.AccNo = id1;
                tr1.TrType = "Transfer Out";
                tr1.TrAmount = amount;
                tr1.TransferAcc = id2;
                db.CustomerTransactions.Add(tr1);
                //transaction of id2 from user is added to transaction table
                CustomerTransaction tr2 = new CustomerTransaction();
                tr2.AccNo = id2;
                tr2.TrType = "Transfer In";
                tr2.TrAmount = amount;
                tr2.TransferAcc = id1;
                db.CustomerTransactions.Add(tr2);

                customer.AccBalance -= amount;
                customer2.AccBalance += amount;
                Console.WriteLine("Transfer successful. New balance: "+customer.AccBalance);
                db.SaveChanges();
            }
        }
        else if(id1==id2)
        {
            Console.WriteLine("Cannot transfer to self!");
        }
        else
        {
            Console.WriteLine("Account you are transferring to does not exist.");
        }
    }


    public void LastFiveTransactions(int id)
    {
        Console.WriteLine("Last 5 transactions: ");
        var transactionList = db.CustomerTransactions.Where(a => a.AccNo == id)        //gets the last 5 transactions ordered so that the most recent transaction is first
                                                     .OrderByDescending(a => a.TrNo)
                                                     .Take(5)
                                                     .ToList();
        foreach (var transaction in transactionList)
        {
            if(transaction.TrType == "Transfer In")
            {
                Console.WriteLine($"Transaction Type: {transaction.TrType}\t|\tTransaction Amount: {transaction.TrAmount}\t|\tTransferred From Account #: {transaction.TransferAcc}");
            }
            else if(transaction.TrType == "Transfer Out")
            {
                Console.WriteLine($"Transaction Type: {transaction.TrType}\t|\tTransaction Amount: {transaction.TrAmount}\t|\tTransferred To Account #: {transaction.TransferAcc}");
            }
            else if(transaction.TrType == "Withdrawal")
            {
                Console.WriteLine($"Transaction Type: {transaction.TrType}\t|\tTransaction Amount: {transaction.TrAmount}");
            }
            else
            {
                Console.WriteLine($"Transaction Type: {transaction.TrType}\t|\tTransaction Amount: {transaction.TrAmount}");
            }
        }
    }


    public void ChequebookRequest(int id)
    {
        CustomerUser customer = db.CustomerUsers.Find(id);
        RequestToAdmin rq1 = new RequestToAdmin();
        var requestCheck = db.RequestToAdmins.Where(a => a.RqComplete == Convert.ToBoolean("False") && a.RqType == "Checkbook" && a.AccNo == id).SingleOrDefault();
        if(customer.AccType == "Savings")
        {
            Console.WriteLine("You are currently logged into a Savings account and cannot request a checkbook.");
        }
        else if(requestCheck == null && customer.AccType == "Checking")
        {
            rq1.AccNo = id;
            rq1.RqType = "Checkbook";
            db.RequestToAdmins.Add(rq1);
            db.SaveChanges();
            Console.WriteLine("Checkbook Requested");
        }
        else
        {
            Console.WriteLine("You already have a pending checkbook request. Please wait for an admin to approve your previous request.");
        }
    }


    public void PasswordChangeRequest(int id)
    {
        string password = "";
        Console.WriteLine("Enter the new password you want. Passwords must be at least 6 characters long and the only special characters allowed are !@#$%^&*");
        password = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "");
        while(password.Length <= 5)
        {
            Console.WriteLine("Password must be at least 6 characters long");
            password = Regex.Replace(Console.ReadLine(), @"[^a-zA-Z0-9!@#$%^&*]", "");
        }

        CustomerUser customer = db.CustomerUsers.Find(id);
        RequestToAdmin rq1 = new RequestToAdmin();
        var requestCheck = db.RequestToAdmins.Where(a => a.RqComplete == Convert.ToBoolean("False") && a.RqType == "Password Change" && a.AccNo == customer.AccNo).SingleOrDefault();
        if(requestCheck == null)
        {
            rq1.AccNo = id;
            rq1.RqType = "Password Change";
            rq1.RqPassword = password;
            db.RequestToAdmins.Add(rq1);
            db.SaveChanges();
            Console.WriteLine("Password Request Sent");
        }
        else
        {
            string prevRq = requestCheck.RqPassword;
            db.RequestToAdmins.Remove(requestCheck);
            rq1.AccNo = id;
            rq1.RqType = "Password Change";
            rq1.RqPassword = password;
            db.RequestToAdmins.Add(rq1);
            db.SaveChanges();
            Console.WriteLine($"Previous password request of {prevRq} has been removed and a new password request has been sent.");
        }
    }

}


