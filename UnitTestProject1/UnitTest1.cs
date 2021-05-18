using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static DivisionGrupos.Program;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

   
        [TestMethod]
        public void TestMethod1()
        {
            Division divition = new Division();
            double permu = divition.getPercistanceOfStudents(10,10);
            Console.WriteLine(permu); 
          
            Assert.IsTrue(permu < 0.050);

        }



    }
}
