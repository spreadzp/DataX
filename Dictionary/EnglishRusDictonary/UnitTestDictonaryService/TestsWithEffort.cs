using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using DictonaryService;
using DictonaryService.DbClasses;
using Effort;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestDictonaryService
{
    [TestClass]
    public class TestsWithEffort
    {
        private DictonaryContext context;

        [TestMethod]
        public void TestAddWordWithEffort()
        {
            using (var connection = DbConnectionFactory.CreateTransient())
            {
                context = new DictonaryContext(connection);

                IDictonaryRepository repository = new DictonaryRepository(context);
                string testWord = "кот";
                int roomDictonary = 0;

                bool actualAnswer = repository.AddWordToDb(roomDictonary, testWord);

                Assert.IsTrue(actualAnswer);
            }
        }

        [TestMethod]
        public void TestUpdateWordsWithEffort()
        {
            using (var connection = DbConnectionFactory.CreateTransient())
            {
                context = new DictonaryContext(connection);
                IDictonaryRepository repository = new DictonaryRepository(context);
                int roomDictonary = 0;
                string searchWord = "кот";
                string[] exrectedTranslate = new string[] { "cat" };

                bool actualMessage = repository.UpdateWordsToDb(roomDictonary, searchWord, exrectedTranslate);

                Assert.IsTrue(actualMessage);
            }
        }

        [TestMethod]
        public void TestFindTranslateWithEffort()
        {
            int roomDictonary = 0;
            string searchWord = "кот";
            string[] translate = new string[] { "cat" };
            string[] exrectedTranslate = { "cat" };

            using (var connection = DbConnectionFactory.CreateTransient())
            {
                context = new DictonaryContext(connection);

                IDictonaryRepository repository = new DictonaryRepository(context);

                EngDictonary engWord = new EngDictonary() { Id = 1, Word = "cat", Words = null };
                RusDictonary word = new RusDictonary() { Id = 1, Word = searchWord, Words = new Collection<EngDictonary>() { engWord } };
                context.RusDictonaries.Add(word);
                context.SaveChanges();

                try
                {
                    var actualTranslate = repository.FindTranslateIntoDb(roomDictonary, searchWord);
                    Assert.IsNotNull(actualTranslate);
                    CollectionAssert.AreEquivalent(exrectedTranslate, actualTranslate);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                } 
            }
        }

        [TestMethod]
        public void TestDeleteWordWithEffort()
        {
            using (var connection = DbConnectionFactory.CreateTransient())
            {
                context = new DictonaryContext(connection);
                IDictonaryRepository repository = new DictonaryRepository(context);
                int roomDictonary = 0;
                string testWord = "жук";

                bool actualMessage = repository.DeleteWordFromDb(roomDictonary, testWord);

                Assert.IsTrue(actualMessage);
            }
        }
    }
}
