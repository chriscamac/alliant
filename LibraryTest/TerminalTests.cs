using NUnit.Framework;
using Library;

namespace LibraryTest
{
    public class TerminalTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestABCDABAA()
        {
            //Scan these items in this order: ABCDABAA; Verify the total price is $32.40.
            var sut = new Terminal();
            sut.Scan("A");
            sut.Scan("B");
            sut.Scan("C");
            sut.Scan("D");
            sut.Scan("A");
            sut.Scan("B");
            sut.Scan("A");
            sut.Scan("A");

            var total = sut.Total();

            Assert.AreEqual(32.40m, total);
        }

        [Test]
        public void TestCCCCCCC()
        {
            //Scan these items in this order: CCCCCCC; Verify the total price is $7.25.
            var sut = new Terminal();
            sut.Scan("C");
            sut.Scan("C");
            sut.Scan("C");
            sut.Scan("C");
            sut.Scan("C");
            sut.Scan("C");
            sut.Scan("C");
 
            var total = sut.Total();

            Assert.AreEqual(7.25m, total);
        }

        [Test]
        public void TestABCD()
        {
            //Scan these items in this order: ABCD; Verify the total price is $15.40.
            var sut = new Terminal();
            sut.Scan("A");
            sut.Scan("B");
            sut.Scan("C");
            sut.Scan("D");

            var total = sut.Total();

            Assert.AreEqual(15.40m, total);
        }

    }
}