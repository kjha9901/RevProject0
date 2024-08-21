// See https://aka.ms/new-console-template for more information

using System.Data.SqlClient;

string choice = "0";
string inputUsername = "";
string inputPassword = "";


AdminOperations admin = new AdminOperations();
CustomerOperations customer = new CustomerOperations();



try
{
    while(choice != "3")
    {
        Console.Clear();
        Console.WriteLine("Welcome to THE BANK");
        Console.WriteLine("1. Customer");
        Console.WriteLine("2. Admin");
        Console.WriteLine("3. Exit");
        choice = Console.ReadLine();
        Console.Clear();
        switch(choice)
        {
            case "1":
                Console.WriteLine("Please enter username: ");
                inputUsername = Console.ReadLine();
                Console.WriteLine("Please enter password: ");
                inputPassword = Console.ReadLine();
                if(customer.CheckCustomerLogin(inputUsername,inputPassword) == true)
                {
                    int customerID = customer.GetID(inputUsername,inputPassword);
                    string choiceCustomer = "0";
                    Console.Clear();
                    
                    while(choiceCustomer != "8")
                    {
                        customer.DisplayCustomerMenu();
                        choiceCustomer = Console.ReadLine();
                        switch(choiceCustomer)
                        {
                            case "1":
                                customer.AccountDetails(customerID);
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case "2":
                                customer.Withdraw(customerID);
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;
                            
                            case "3":
                                customer.Deposit(customerID);
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case "4":
                                customer.Transfer(customerID);
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case "5":
                                customer.LastFiveTransactions(customerID);
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case "6":
                                customer.ChequebookRequest(customerID);
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;
                            
                            case "7":
                                customer.PasswordChangeRequest(customerID);
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case "8":
                                Console.WriteLine("Exiting...");
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
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
                    Console.WriteLine("Invalid Credentials");
                    Console.WriteLine("Press enter to continue");
                    Console.ReadLine();
                    Console.Clear();
                }
            break;

            case "2":
                Console.WriteLine("Please enter username: ");
                inputUsername = Console.ReadLine();
                Console.WriteLine("Please enter password: ");
                inputPassword = Console.ReadLine();
                if(admin.CheckAdminLogin(inputUsername,inputPassword) == true)
                {
                    int adminID = admin.GetID(inputUsername,inputPassword);
                    string choiceAdmin = "0";
                    Console.Clear();
                    while(choiceAdmin != "7")
                    {
                        admin.DisplayAdminMenu();
                        choiceAdmin = Console.ReadLine();
                        switch(choiceAdmin)
                        {
                            case "1":
                                admin.CreateNewAccount();
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case "2":
                                admin.DeleteAccount(inputUsername);
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;
                            
                            case "3":
                                admin.EditAccountDetails();
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case "4":
                                admin.DisplaySummary();
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case "5":
                                admin.ResetCustomerPassword();
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case "6":
                                admin.ApproveCheckbookRequest();
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;
                            
                            case "7":
                                Console.WriteLine("Exiting...");
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
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
                    Console.WriteLine("Invalid Credentials");
                    Console.WriteLine("Press enter to continue");
                    Console.ReadLine();
                    Console.Clear();
                }
            break;

            case "3":
            Console.Clear();
            Console.WriteLine("Exited Bank");
            break;

            default:
                Console.WriteLine("Invalid Choice, try again.");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            break;
        }
    }
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}










