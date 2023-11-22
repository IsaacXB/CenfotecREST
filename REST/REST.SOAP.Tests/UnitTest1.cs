using Microsoft.VisualStudio.TestTools.UnitTesting;
using REST.SOAP.Tests.ServiceSOAP;
using System;
using System.Collections.Generic;
using System.Linq;

namespace REST.SOAP.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CanTestSumList()
        {
            // Create a new instance of the SOAP Service
            WebService1 service1 = new WebService1();

            // Create a list of values to be validated by the SumList (method)
            List<int> list = new List<int>() { 10, 20, 30, 40, 50 } ;

            // Use service to sum the list of values
            var result = service1.SumList(list);
            
            // Tests or validations to be performed by this unit test
            Assert.IsNotNull(result);
            Assert.AreEqual(150, result);
        }
    }
}
