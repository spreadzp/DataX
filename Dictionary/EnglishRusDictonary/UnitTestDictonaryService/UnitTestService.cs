using System.Diagnostics;
using DictonaryService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestDictonaryService
{
    [TestClass]
    public class UnitTestService
    {
        [TestMethod]
        public void TestConnectClient()
        {
            ServiceForDictonary testService = new ServiceForDictonary(); 
            var resultMethod = testService.ConnectClient();
            Assert.IsNotNull(resultMethod);
            Assert.IsTrue(resultMethod);
        }

        [TestMethod]
        public void TestFindTranslate()
        {
            int roomDictonary = 0;
            string searchWord = "кот";
            string[] exrectedTranslate = { "cat" };
            ServiceForDictonary testService = new ServiceForDictonary(new FakeServiceDbRepository());

            var actualTranslate = testService.FindTranslate(roomDictonary, searchWord);
            Assert.IsNotNull(actualTranslate);
            CollectionAssert.AreEquivalent(exrectedTranslate, actualTranslate);
        }

        [TestMethod]
        public void TestAddWord()
        {
            ServiceForDictonary testService = new ServiceForDictonary(new FakeServiceDbRepository());
            string testWord = "жук";
            int roomDictonary = 0;

            bool actualAnswer = testService.AddWord(roomDictonary, testWord);

            Assert.IsTrue(actualAnswer);
        }

        [TestMethod]
        public void TestUpdateWords()
        {
            int roomDictonary = 0;
            string searchWord = "кот";
            string[] exrectedTranslate = new string[] { "cat" };
            ServiceForDictonary testService = new ServiceForDictonary(new FakeServiceDbRepository()); 

            bool actualMessage = testService.UpdateWords(roomDictonary, searchWord, exrectedTranslate);

            Assert.IsTrue(actualMessage);
        }

        [TestMethod]
        public void TestDeleteWord()
        {
            int roomDictonary = 0;
            string testWord = "жук";
            ServiceForDictonary testService = new ServiceForDictonary();

            bool actualMessage = testService.DeleteWord(roomDictonary, testWord);

            Assert.IsTrue(actualMessage);
        }
    }
}
