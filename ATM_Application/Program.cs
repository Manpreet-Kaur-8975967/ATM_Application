using System;
using System.Collections.Generic;

public class Account   // Creation of Account Class
{
    public int AccountNumber { get; set; }
    public double BankBalance { get; set; }
      public double InterestRate { get; set; }
    public string AccountHolderName { get; set; }
    public List<string> Transactions { get; set; }

    public Account(int accountNumber, double initialBalance, double interestRate, string accountHolderName)
    {
        AccountNumber = accountNumber;  // Declaration of variables in account class
         BankBalance = initialBalance;
         InterestRate = interestRate;
        AccountHolderName = accountHolderName;
        Transactions = new List<string>();
    }

    public void Deposit(double amount)   // Method for depositing Money into the Bank
    {
        BankBalance += amount;
        Transactions.Add($"Deposited: {amount:C}");
    }

    public bool Withdraw(double amount)   // Method for withdrawing money from the Bank
    {
        if (amount <= BankBalance)
        {
            BankBalance -= amount;
            Transactions.Add($"Withdrew: {amount:C}");
            return true;
        }
        else
        {
            Transactions.Add($"Failed Withdrawal Attempt: {amount:C}");
            return false;
        }
    }

    public void DisplayTransactions()  // Method for displaying the transactions done
    {
        Console.WriteLine("Transaction History:");
        foreach (var transaction in Transactions)
        {
            Console.WriteLine(transaction);
        }
    }
}

public class Bank  // Creation of Bank Class
{
    private List<Account> accounts; // List for holding all the accounts

    public Bank()
    {
        accounts = new List<Account>();
        for (int i = 100; i <= 109; i++)
        {
            accounts.Add(new Account(i, 100, 0.03, "Default User"));
        }
    }

    public void AddAccount(Account account)
    {
        accounts.Add(account);
    }

    public Account RetrieveAccount(int accountNumber)  // method for finding the account registered
    {
        return accounts.Find(account => account.AccountNumber == accountNumber);
    }
}

public class AtmApplication
{
    private Bank bank;

    public AtmApplication()
    {
        bank = new Bank();
    }

    public void Run()
    {
        while (true)
        {
            DisplayMainMenu();
            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid choice. Please enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    CreateAccount(); // Calling Acoount Creation method
                    break;
                case 2:
                    SelectAccount(); // Calling select account method for displaying the account
                    break;
                case 3:
                    Console.WriteLine("Exiting application. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private void DisplayMainMenu()  // Method for displaying Main Menu 
    {
        Console.WriteLine("\nATM Main Menu:");
        Console.WriteLine("1. Create Account");
          Console.WriteLine("2. Select Account");
        Console.WriteLine("3. Exit");
        Console.Write("Enter your choice: ");
    }

    private void CreateAccount()  // Method for Creating the account
    {
        Console.Write("Enter Account Number: ");
        int accountNumber = int.Parse(Console.ReadLine());
          Console.Write("Enter Initial Balance: ");
        double initialBalance = double.Parse(Console.ReadLine());
        Console.Write("Enter Interest Rate: ");
        double interestRate = double.Parse(Console.ReadLine());
        Console.Write("Enter Account Holder's Name: ");
        string accountHolderName = Console.ReadLine();

        Account account = new Account(accountNumber, initialBalance, interestRate, accountHolderName);
        bank.AddAccount(account);

        Console.WriteLine($"Account created successfully for {accountHolderName} with Account Number: {accountNumber}");
    }

    private void SelectAccount()
    {
        Console.Write("Enter Account Number: ");
        int accountNumber = int.Parse(Console.ReadLine());
        Account account = bank.RetrieveAccount(accountNumber);

        if (account != null)
        {
            Console.WriteLine($"Account selected: {account.AccountHolderName} (Account Number: {account.AccountNumber})");
            AccountMenu(account);
        }
        else
        {
            Console.WriteLine("Account not found. Please try again.");
        }
    }

    private void AccountMenu(Account account)
    {
        while (true)
        {
            Console.WriteLine("\nAccount Menu:");
              Console.WriteLine("1. Check Balance");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
               Console.WriteLine("4. Display Transactions");
            Console.WriteLine("5. Exit Account");
            Console.Write("Enter your choice: ");
            int choice;
              if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid choice. Please enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.WriteLine($"Balance: {account.BankBalance:C}");
                    break;
                case 2:
                    Console.Write("Enter amount to deposit: ");
                    double depositAmount = double.Parse(Console.ReadLine());
                      account.Deposit(depositAmount);
                    Console.WriteLine($"Deposited: {depositAmount:C}");
                    break;
                case 3:
                    Console.Write("Enter amount to withdraw: ");
                    double withdrawAmount = double.Parse(Console.ReadLine());
                    if (account.Withdraw(withdrawAmount))
                    {
                        Console.WriteLine($"Withdrew: {withdrawAmount:C}");
                    }
                    else
                    {
                        Console.WriteLine("Insufficient funds for withdrawal.");
                       }
                    break;
                case 4:
                      account.DisplayTransactions();
                    break;
                   case 5:
                      Console.WriteLine("Exiting account menu.");
                    return;
                default:
                      Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}

class Program
{
    static void Main(string[] args)  // starting of main function
    {
        AtmApplication app = new AtmApplication(); //creation of object for accessing the methods 
        app.Run(); //calling of run method
    }
}
