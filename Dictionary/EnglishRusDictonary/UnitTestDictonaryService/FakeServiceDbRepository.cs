using DictonaryService;
using DictonaryService.DbClasses;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace UnitTestDictonaryService
{
    public class FakeServiceDbRepository : IDictonaryRepository
    {
        private IList<RusDictonary> rusDictonary = new List<RusDictonary>();  

        public bool AddWordToDb(int roomDictonary, string newWord)
        {
            RusDictonary word = new RusDictonary() { Id = 1, Word = newWord, Words = null };
            rusDictonary.Add(word);

            if (rusDictonary.Contains(word))
            {
                return true;
            }

            return false;
        }

        public bool DeleteWordFromDb(int roomDictonary, string deleteWord)
        {
            RusDictonary word = new RusDictonary() { Id = 1, Word = deleteWord, Words = null };
            rusDictonary.Add(word);
            rusDictonary.Remove(word);

            if (rusDictonary.Contains(word))
            {
                return false;
            }

            return true;
        }

        public string[] FindTranslateIntoDb(int roomDictonary, string searchWord)
        {
            EngDictonary engWord = new EngDictonary() { Id = 1, Word = "cat", Words = null };
            RusDictonary word = new RusDictonary() { Id = 1, Word = searchWord, Words = new Collection<EngDictonary>() { engWord } };
            rusDictonary.Add(word);
            var result = rusDictonary.Where(rd => rd.Word == searchWord)
                    .SelectMany(rd => rd.Words)
                    .Select(ew => ew.Word)
                    .ToArray();
            return result;
        }

        public bool UpdateWordsToDb(int roomDictonary, string updateSearchWord, string[] updateWordsTranslate)
        {
            //return "В базе данных был изменён перевод слова  :" + updateSearchWord +
            //" Для проверки задайте слово :" + updateSearchWord + " в поиске!";
            return true;
        }
    }
}
