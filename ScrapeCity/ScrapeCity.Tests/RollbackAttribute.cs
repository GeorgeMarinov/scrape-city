using System;
using System.Transactions;
using NUnit.Framework;


/// <summary>
/// Rollback Attribute wraps test execution into a transaction and cancels the transaction once the test is finished.
/// You can use this attribute on single test methods or test classes/suites
/// </summary>
namespace ScrapeCity.Tests
{
    public class RollbackAttribute : Attribute
    {
        private TransactionScope transaction;

        public void BeforeTest()
        {
            transaction = new TransactionScope();
        }

        public void AfterTest()
        {
            transaction.Dispose();
        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }
    }
}