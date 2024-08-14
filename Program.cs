// See https://aka.ms/new-console-template for more information

int choice = 0;
string customerUsername = "johnson123";
string customerPassword = "hunter2";
string adminUsername = "admin";
string adminPassword = "password";
string inputUsername = "";
string inputPassword = "";

try
{
    while(choice != 3)
    {
        Console.WriteLine("Welcome to THE BANK");
        Console.WriteLine("1. Customer");
        Console.WriteLine("2. Admin");
        Console.WriteLine("3. Exit");
        choice = Convert.ToInt32(Console.ReadLine());
        Console.Clear();
        switch(choice)
        {
            case 1:
                
                Console.WriteLine("Please enter username: ");
                inputUsername = Console.ReadLine();
                Console.WriteLine("Please enter password: ");
                inputPassword = Console.ReadLine();
                if(inputUsername == customerUsername && inputPassword == customerPassword)
                {
                    Console.Clear();
                    int choiceCustomer = 0;
                    while(choiceCustomer != 8)
                    {
                        Console.WriteLine("1. Check Account Details");
                        Console.WriteLine("2. Withdraw");
                        Console.WriteLine("3. Deposit");
                        Console.WriteLine("4. Transfer");
                        Console.WriteLine("5. Last 5 transactions");
                        Console.WriteLine("6. Request Cheque Book");
                        Console.WriteLine("7. Change Password");
                        Console.WriteLine("8. Exit");
                        choiceCustomer = Convert.ToInt32(Console.ReadLine());
                        switch(choiceCustomer)
                        {
                            case 1:
                                Console.WriteLine("Account details");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case 2:
                                Console.WriteLine("Withdrawal successful");
                                Console.ReadLine();
                                Console.Clear();
                            break;
                            
                            case 3:
                                Console.WriteLine("Deposit successful");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case 4:
                                Console.WriteLine("Transfer successful");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case 5:
                                Console.WriteLine("Last 5 transactions:");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case 6:
                                Console.WriteLine("Cheque book requested");
                                Console.ReadLine();
                                Console.Clear();
                            break;
                            
                            case 7:
                                Console.WriteLine("Password change successful");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case 8:
                                Console.WriteLine("Exiting...");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            default:
                                Console.WriteLine("Invalid Choice");
                                Console.ReadLine();
                                Console.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Credentials");
                    Console.ReadLine();
                    Console.Clear();
                }
            break;

            case 2:
                Console.WriteLine("Please enter username: ");
                inputUsername = Console.ReadLine();
                Console.WriteLine("Please enter password: ");
                inputPassword = Console.ReadLine();
                if(inputUsername == adminUsername && inputPassword == adminPassword)
                {
                    Console.Clear();
                    int choiceAdmin = 0;
                    while(choiceAdmin != 7)
                    {
                        Console.WriteLine("1. Create New Account");
                        Console.WriteLine("2. Delete Account");
                        Console.WriteLine("3. Edit Account Details");
                        Console.WriteLine("4. Display Summary");
                        Console.WriteLine("5. Reset Customer Password");
                        Console.WriteLine("6. Approve Cheque Book Request");
                        Console.WriteLine("7. Exit");
                        choiceAdmin = Convert.ToInt32(Console.ReadLine());
                        switch(choiceAdmin)
                        {
                            case 1:
                                Console.WriteLine("New account created");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case 2:
                                Console.WriteLine("Account deleted");
                                Console.ReadLine();
                                Console.Clear();
                            break;
                            
                            case 3:
                                Console.WriteLine("Account details edited");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case 4:
                                Console.WriteLine("Summary displayed");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case 5:
                                Console.WriteLine("Customer password reset");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case 6:
                                Console.WriteLine("Cheque book request approved");
                                Console.ReadLine();
                                Console.Clear();
                            break;
                            
                            case 7:
                                Console.WriteLine("Exiting...");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            default:
                                Console.WriteLine("Invalid Choice");
                                Console.ReadLine();
                                Console.Clear();
                            break;
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Credentials");
                    Console.ReadLine();
                    Console.Clear();
                }
            break;

            case 3:
            Console.Clear();
            Console.WriteLine("Exited Bank");
            break;

            default:
                Console.WriteLine("Invalid Choice, try again.");
            break;
        }
    }
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}










