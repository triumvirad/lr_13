using ClassLibrary;
using ClassLibrary2;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodAdd()
        {
            var collection = new MyObservableCollection<MusicalInstrument>();
            var journal = new Journal();
            collection.CollectionCountChanged += (source, args) => journal.AddEntry(source, args, "Бибизяна1");
            collection.CollectionReferenceChanged += (source, args) => journal.AddEntry(source, args, "Бибизяна2");

            var item = new MusicalInstrument("Бибизяна", 10);
            collection.Add(item);
            Assert.IsTrue(collection.Contains(item));
        }

        [TestMethod]
        public void TestMethodRemove()
        {
            var collection = new MyObservableCollection<MusicalInstrument>();
            var journal = new Journal();
            collection.CollectionCountChanged += (source, args) => journal.AddEntry(source, args, "Бибизяна1");
            collection.CollectionReferenceChanged += (source, args) => journal.AddEntry(source, args, "Бибизяна2");

            var item = new MusicalInstrument("Бибизяна", 10);
            collection.Add(item);
            collection.Remove(item);
            Assert.IsFalse(collection.Contains(item));
        }

        [TestMethod]
        public void TestMethodCapacityAndCount()
        {
            MyObservableCollection<MusicalInstrument> collection = new();
            Assert.IsTrue(collection.Capacity == 10 && collection.Count == 0);
        }

        [TestMethod]
        public void TestMethodAddMultipleItems()
        {
            var collection = new MyObservableCollection<MusicalInstrument>();
            var journal = new Journal();
            collection.CollectionCountChanged += (source, args) => journal.AddEntry(source, args, "Бибизяна1");
            collection.CollectionReferenceChanged += (source, args) => journal.AddEntry(source, args, "Бибизяна2");

            MusicalInstrument item = null;
            for (int i = 0; i < 15; i++)
            {
                item = new MusicalInstrument();
                item.RandomInit();
                collection.Add(item);
            }
            Assert.IsTrue(collection.Contains(item));
        }

        [TestMethod]
        public void TestMethodFindItem()
        {
            var collection = new MyObservableCollection<MusicalInstrument>();
            var journal = new Journal();
            collection.CollectionCountChanged += (source, args) => journal.AddEntry(source, args, "Бибизяна1");
            collection.CollectionReferenceChanged += (source, args) => journal.AddEntry(source, args, "Бибизяна2");

            var item = new MusicalInstrument("Бибизяна", 10);
            collection.Add(item);
            collection.Remove(item);
            Assert.IsFalse(collection.Contains(item));
        }
        [TestMethod]
        public void TestMethodJournalEntryToString()
        {
            var entry = new JournalEntry("1", "добавление", "1");
            var expected = "Коллекция: 1, Тип операции: добавление, Изменяемый элимент: 1";
            Assert.AreEqual(expected, entry.ToString());
        }
        [TestMethod]
        public void TestJournalToString()
        {
            var collection = new MyObservableCollection<MusicalInstrument>();
            var journal = new Journal();
            collection.CollectionCountChanged += (source, args) => journal.AddEntry(source, args, "Бибизяна1");
            collection.CollectionReferenceChanged += (source, args) => journal.AddEntry(source, args, "Бибизяна2");

            var item1 = new MusicalInstrument("Бибизяна", 10);
            collection.Add(item1);
            collection.Remove(item1);

            var expected = $"Коллекция: Бибизяна1, Тип операции: добавление, Изменяемый элимент: ID: 10, Название: Бибизяна";

            Assert.AreEqual(expected, journal.ToString());
        }
        [TestMethod]
        public void TestJournalToString2()
        {
            var collection = new MyObservableCollection<MusicalInstrument>();
            for (int i = 0; i < 100; i++)
            {
                MusicalInstrument a = new MusicalInstrument();
                a.RandomInit();
                collection.Add(a);
            }
            MusicalInstrument b = new MusicalInstrument("Кукузямба", 11);
            MusicalInstrument c = new MusicalInstrument("FJKFJFJFJFJJF", 12);
            collection.Add(b);
            collection.Replace(b, c);
            Assert.AreEqual(collection.FindItem(c) != -1 && collection.FindItem(b) == -1, true);

        }
    }
}