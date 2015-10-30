using System;
using System.Linq;
using System.ServiceModel;

namespace DictonaryService.DbClasses
{
    public class DictonaryRepository : IDictonaryRepository
    {
        private readonly DictonaryContext db;

        public DictonaryRepository()
        {
            db = new DictonaryContext();
        }

        public DictonaryRepository(DictonaryContext _db)
        {
            db = _db;
        }

        public bool AddWordToDb(int roomDictonary, string newWord)
        {
            try
            {
               if (roomDictonary == 0)
                {
                    bool exists = db.RusDictonaries.Select(rd => rd.Word).Any(w => w == newWord);

                    if (!exists)
                    {
                        db.RusDictonaries.Add(new RusDictonary { Word = newWord });
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    bool exists = db.EngDictonaries.Select(rd => rd.Word).Any(w => w == newWord);

                    if (!exists)
                    {
                        db.EngDictonaries.Add(new EngDictonary { Word = newWord });
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw new FaultException("Возникла ошибка ,слово не было добавлено в БД ! ");
            } 
        }

        public bool DeleteWordFromDb(int roomDictonary, string deleteWord)
        {
            try
            {
                if (roomDictonary == 0)
                {
                    var rusWordForDelete =
                        (from a in db.RusDictonaries where a.Word == deleteWord select a).SingleOrDefault();

                    if (rusWordForDelete != null)
                    {
                        db.RusDictonaries.Remove(rusWordForDelete);
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    var engWordForDelete =
                        (from a in db.EngDictonaries where a.Word == deleteWord select a).SingleOrDefault();

                    if (engWordForDelete != null)
                    {
                        db.EngDictonaries.Remove(engWordForDelete);
                    }
                    else
                    {
                        return false;
                    }
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                throw new FaultException("Возникла ошибка ,слово не было удалено из БД ! ");
            }
        }

        public string[] FindTranslateIntoDb(int roomDictonary, string searchWord)
        {
            string[] translateWords;

            if (roomDictonary == 0)
            {
                translateWords = db.RusDictonaries
                    .Where(rd => rd.Word == searchWord)
                    .SelectMany(rd => rd.Words)
                    .Select(ew => ew.Word)
                    .ToArray();
            }
            else
            {
                translateWords = db.EngDictonaries
                    .Where(rd => rd.Word == searchWord)
                    .SelectMany(rd => rd.Words)
                    .Select(ew => ew.Word)
                    .ToArray();
            }
            return translateWords;
        }

        public bool UpdateWordsToDb(int roomDictonary, string updateSearchWord, string[] updateWordsTranslate)
        {
            try
            {
                if (roomDictonary == 0)
                {
                    var rusWordEngTranslate = (from a in db.RusDictonaries where a.Word == updateSearchWord select a).FirstOrDefault(); // SingleOrDefault()

                    if (rusWordEngTranslate != null)
                    {
                        foreach (string engTranslatedWord in updateWordsTranslate)
                        {
                           var englishWord = rusWordEngTranslate.Words.Where(a => a.Word == engTranslatedWord);

                           if (englishWord.FirstOrDefault() == null)
                            {
                                rusWordEngTranslate.Words.Add(new EngDictonary { Word = engTranslatedWord });
                            }
                        }

                        db.SaveChanges();
                    }
                }
                else
                {
                    var engWordRusTranslate = (from a in db.EngDictonaries where a.Word == updateSearchWord select a).FirstOrDefault();

                    if (engWordRusTranslate != null)
                    {
                        foreach (var rusTranslatedWord in updateWordsTranslate)
                        {
                            var russianWord = engWordRusTranslate.Words.Where(a => a.Word == rusTranslatedWord);
                            if (russianWord.FirstOrDefault() == null)
                            {
                                engWordRusTranslate.Words.Add(new RusDictonary { Word = rusTranslatedWord });
                            }
                        }

                        db.SaveChanges();
                    }
                }

                return true;
            }
            catch (Exception)
            {
                throw new FaultException("Возникла ошибка ,обновления не были сделаны в БД ! ");
            } 
        }
    }
}