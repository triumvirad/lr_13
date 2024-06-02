using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using ClassLibrary;
using ClassLibrary2;

namespace lr_13
{
    internal class Program 
    {
        static void Main(string[] args)
        {
            // создать две коллекции MyObservableCollection.
            MyObservableCollection<MusicalInstrument> m1 = new MyObservableCollection<MusicalInstrument>();
            MyObservableCollection<MusicalInstrument> m2 = new MyObservableCollection<MusicalInstrument>();

            // создать два объекта типа Journal
            Journal j1 = new Journal();
            Journal j2 = new Journal();

            // один объект Journal подписать на события CollectionCountChanged и CollectionReferenceChanged из первой коллекции, другой объект Journal подписать на события CollectionReferenceChanged из обеих коллекций. 
            m1.CollectionCountChanged += (source, args) => j1.AddEntry(source, args, " 1 ");
            m1.CollectionReferenceChanged += (source, args) => j1.AddEntry(source, args, " 1 ");
            m1.CollectionReferenceChanged += (source, args) => j2.AddEntry(source, args, " 1 ");
            m2.CollectionReferenceChanged += (source, args) => j2.AddEntry(source, args, " 2 ");

            // добавить элементы в коллекции m1 и m2
            MusicalInstrument a1 = new MusicalInstrument(); // первая коллекция добавление
            a1.RandomInit();
            m1.Add(a1);

            MusicalInstrument a2 = new MusicalInstrument(); // первая коллекция добавление
            a2.RandomInit();
            m1.Add(a2);

            MusicalInstrument a3 = new MusicalInstrument(); // первая коллекция добавление
            a3.RandomInit();
            m1.Add(a3);

            MusicalInstrument a4 = new MusicalInstrument(); // первая коллекция добавление
            a4.RandomInit();

            MusicalInstrument a5 = new MusicalInstrument(); // первая коллекция добавление
            a5.RandomInit();

            m1.Add(a4);

            m1.Remove(a4);

            m1.Replace(a3, a5);

            // драмматическая пауза чтобы разграничить добавления элементов в m1 и m2

            MusicalInstrument b1 = new MusicalInstrument(); // вторая коллекция добавление
            b1.RandomInit();
            m2.Add(b1);

            MusicalInstrument b2 = new MusicalInstrument(); // вторая коллекция добавление
            b2.RandomInit();
            m2.Add(b2);

            MusicalInstrument b3 = new MusicalInstrument(); // вторая коллекция добавление
            b3.RandomInit();
            m2.Add(b3);

            MusicalInstrument b4 = new MusicalInstrument(); // вторая коллекция добавление
            b4.RandomInit();
            m2.Add(b4);

            MusicalInstrument b5 = new MusicalInstrument(); // первая коллекция добавление
            b5.RandomInit();

            m2.Add(b4);

            m2.Remove(b4);

            m2.Replace(b3, b5);

            Console.WriteLine("Журнал № 1:");
            Console.WriteLine(j1);
            Console.WriteLine("Журнал № 2:");
            Console.WriteLine(j2);
        }
    }
}
