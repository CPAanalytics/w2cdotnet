using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using w2cdotnet;

namespace w2cdotnet_test
{
    [TestClass]
    public class EINs
    {
        [TestMethod]
        public void Valid()
        {
            var testvalidein = new Submitter(123456789, "abdc", softwareVendor: "ABCD");
            testvalidein.WriteLine();
            
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.InvalidEin))]
        public void TestTooShort()
        {
            List<int> testlist = new List<int>{123, 123456789, 283456789};
            
            foreach(int field in testlist)
            {
                var einlist = new Submitter(field);
                Assert.ThrowsException<Exceptions.InvalidEin>(() => einlist.WriteLine());
            }
        
            
        }
    }
}