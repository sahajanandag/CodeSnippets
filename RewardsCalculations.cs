using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RewardsCalculations
{
    class Program
    {
        static void Main(string[] args)
        {

            /*A retailer offers a rewards program to its customers, awarding points based on each recorded purchase.
            A customer receives 2 points for every dollar spent over $100 in each transaction, plus 1 point for every dollar spent over $50 in each transaction
             (e.g.a $120 purchase = 2x$20 + 1x$50 = 90 points).
       
            Given a record of every transaction during a three month period, calculate the reward points earned for each customer per month and total.*/

            var transactionList = new TransList();
            transactionList.transactionAdd(300);
            transactionList.transactionAdd(524);
            transactionList.transactionAdd(201);
            transactionList.transactionAdd(120);
            transactionList.transactionAdd(100);

            var allTransactions = transactionList.getListOfAllTransactions();
            var totalRewards = transactionList.getTotalRewards();
            var threeMonths = transactionList.getLastThreeMonthsList();
            var rewardPerMonth = transactionList.getRewardPerMonth();
           
        }
       

        public class Trans
        {
            public int price { get; set; }
            public int rewards { get; set; }
            public DateTime dateOfTransaction { get; set; }
            public Trans(int itemPrice)
            {
                this.price = itemPrice;
                this.rewards = rewardsCalculation(itemPrice);
                this.dateOfTransaction = DateTime.Today;
            }
            public int rewardsCalculation(int price)
            {
                if (price >= 50 && price < 100)
                {
                    return price - 50;
                }
                else if (price > 100)
                {
                    return (2 * (price - 100) + 50);
                }
                return 0;
            }
        }

        public class TransList
        {
            List<Trans> listOfTransaction;
            public TransList()
            {
                this.listOfTransaction = new List<Trans>();
            }

            public List<Trans> getLastThreeMonthsList()
            {
                var todayDate = DateTime.Today;
                var filteredList = listOfTransaction.FindAll(trans => trans.dateOfTransaction >= todayDate.AddMonths(-3));
                filteredList.Sort((a, b) => b.dateOfTransaction.CompareTo(a.dateOfTransaction));
                return filteredList;
            }
            public List<Trans> getListOfAllTransactions()
            {
                listOfTransaction.Sort((a, b) => a.dateOfTransaction.CompareTo(b.dateOfTransaction));
                return listOfTransaction;
            }

            public void transactionAdd(int itemPrice)
            {
                var transaction = new Trans(itemPrice);
                listOfTransaction.Add(transaction);
            }

            public int getTotalRewards()
            {
                return listOfTransaction.Aggregate(0, (a, b) => b.rewards + a);
            }

            public int[] getRewardPerMonth()
            {
                var threeMonthRewards = new int[3];
                for (var i = 0; i < 3; i++)
                {
                    var listEveryMonth = this.listOfTransaction.FindAll(trans => trans.dateOfTransaction.Month == DateTime.Now.Month - i);
                    threeMonthRewards[i] = listEveryMonth.Aggregate(0, (a, b) => b.rewards + a);
                }
                return threeMonthRewards;
            }
        }
    }
}
