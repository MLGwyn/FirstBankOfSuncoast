using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstBankOfSuncoast
{
    class Transactions
    {
        public string Account { get; set; }
        public string Action { get; set; }
        public decimal Amount { get; set; }
        private DateTime TransTime { get; } = DateTime.Now;
        public void CompletedTrans()
        {
            Console.WriteLine($"{TransTime} {Action} in the amount of {Amount} dollars in your {Account} account. ");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            static void DisplayGreeting()
            {
                Console.WriteLine("__________________________________________________");
                Console.WriteLine();
                Console.WriteLine("                  WELCOME TO");
                Console.WriteLine("            FIRST BANK OF SUNCOAST");
                Console.WriteLine("__________________________________________________");
            }
            static string PromptForString(string prompt)
            {
                Console.WriteLine(prompt);
                var userInput = Console.ReadLine().ToUpper();
                return userInput;
            }
            static string PromptForAccount()
            {
                string userInput = "";
                while (userInput != "S" && userInput != "C")
                {
                    Console.WriteLine("Which account would you like to access?\n(S)avings or (C)hecking? ");
                    userInput = Console.ReadLine().ToUpper();
                    if (userInput == "S")
                        return "Savings";
                    else if (userInput == "C")
                        return "Checking";
                    else
                    { Console.WriteLine($"I'm sorry {userInput} is not valid. "); }
                }
                return "ERROR";
            }
            static decimal PromptForAmount(string prompt)
            {
                var isThisGoodInput = false;
                while (isThisGoodInput == false)
                {
                    Console.Write(prompt);
                    decimal userInput;
                    isThisGoodInput = Decimal.TryParse(Console.ReadLine(), out userInput);
                    if (isThisGoodInput)
                    {
                        return userInput;
                    }
                    else
                    {
                        Console.WriteLine("I'm sorry that is not a valid response. ");
                    }

                }
                return 0;
            }
            static decimal ComputeBalance(List<Transactions> trans, string account)
            {
                var depTot = trans.Where(t => t.Action == "Deposit" && t.Account == account).Sum(t => t.Amount);
                var withTot = trans.Where(t => t.Action == "Withdrawal" && t.Account == account).Sum(t => t.Amount);
                return depTot - withTot;
            }
            var keepGoing = true;
            var transactions = new List<Transactions>();
            DisplayGreeting();
            while (keepGoing)
            {
                var account = PromptForAccount();
                var choice = PromptForString("What would you like to do?\n(D)eposit\n(W)ithdraw\n(S)how Transactions\n(V)iew Balance ");
                switch (choice)
                {
                    case "D":
                        var depAmount = PromptForAmount("How much would you like to Deposit? ");
                        var transact = new Transactions();
                        transact.Account = account;
                        transact.Action = "Deposit";
                        transact.Amount = depAmount;
                        transactions.Add(transact);
                        transact.CompletedTrans();
                        var balance = ComputeBalance(transactions, account);
                        Console.WriteLine($"Your {account} balance is {balance} dollars. ");
                        break;
                    case "W":
                        balance = ComputeBalance(transactions, account);
                        var withAmount = PromptForAmount("How much would you like to Withdraw? ");
                        if (balance >= withAmount)
                        {
                            transact = new Transactions();
                            transact.Account = account;
                            transact.Action = "Withdrawal";
                            transact.Amount = withAmount;
                            transactions.Add(transact);
                            transact.CompletedTrans();
                        }
                        else
                        {
                            Console.WriteLine($"I'm sorry. {withAmount} exceeds your {account} balance of {balance} dollars. ");
                        }
                        break;
                    case "S":

                        if (account == "Checking")
                        {
                            Console.WriteLine($"-{account} Transactions- ");
                            var cheTrans = transactions.Where(t => t.Account == "Checking");
                            foreach (var trans in cheTrans)
                            {
                                trans.CompletedTrans();
                            }
                        }
                        if (account == "Savings")
                        {
                            Console.WriteLine($"-{account} Transactions- ");
                            var savTrans = transactions.Where(t => t.Account == "Savings");
                            foreach (var trans in savTrans)
                            {
                                trans.CompletedTrans();
                            }
                        }
                        break;
                    case "V":
                        if (account == "Checking")
                        {
                            var cheBal = ComputeBalance(transactions, account);
                            Console.WriteLine($"Your {account} balance is {cheBal} ");
                        }
                        else if (account == "Savings")
                        {
                            var savBal = ComputeBalance(transactions, account);
                            Console.WriteLine($"Your {account} balance is {savBal} ");
                        }
                        break;
                    default:
                        Console.WriteLine($"Error. {choice} is not a valid option ☠️");
                        break;



                }


            }
        }
    }
}
