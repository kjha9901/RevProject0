// See https://aka.ms/new-console-template for more information

using System.Data.SqlClient;

int choice = 0;
string inputUsername = "";
string inputPassword = "";


AdminOperations admin = new AdminOperations();
CustomerOperations customer = new CustomerOperations();



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
                if(customer.CheckCustomerLogin(inputUsername,inputPassword) == true)
                {
                    int customerID = customer.GetID(inputUsername,inputPassword);
                    int choiceCustomer = 0;
                    Console.Clear();
                    
                    while(choiceCustomer != 8)
                    {
                        customer.DisplayCustomerMenu();
                        choiceCustomer = Convert.ToInt32(Console.ReadLine());
                        switch(choiceCustomer)
                        {
                            case 1:
                                customer.AccountDetails(customerID);
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case 2:
                                customer.Withdraw(customerID);
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;
                            
                            case 3:
                                customer.Deposit(customerID);
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case 4:
                                customer.Transfer(customerID);
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case 5:
                                customer.LastFiveTransactions(customerID);
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case 6:
                                Console.WriteLine("Cheque book requested");
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;
                            
                            case 7:
                                Console.WriteLine("Password change successful");
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case 8:
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

            case 2:
                Console.WriteLine("Please enter username: ");
                inputUsername = Console.ReadLine();
                Console.WriteLine("Please enter password: ");
                inputPassword = Console.ReadLine();
                if(admin.CheckAdminLogin(inputUsername,inputPassword) == true)
                {
                    Console.Clear();
                    int choiceAdmin = 0;
                    while(choiceAdmin != 7)
                    {
                        admin.DisplayAdminMenu();
                        choiceAdmin = Convert.ToInt32(Console.ReadLine());
                        switch(choiceAdmin)
                        {
                            case 1:
                                Console.WriteLine("New account created");
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case 2:
                                Console.WriteLine("Account deleted");
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;
                            
                            case 3:
                                Console.WriteLine("Account details edited");
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case 4:
                                Console.WriteLine("Summary displayed");
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case 5:
                                Console.WriteLine("Customer password reset");
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;

                            case 6:
                                Console.WriteLine("Cheque book request approved");
                                Console.WriteLine("Press enter to continue");
                                Console.ReadLine();
                                Console.Clear();
                            break;
                            
                            case 7:
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










