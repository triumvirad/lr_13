using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class Journal
    {
        public List<JournalEntry> entries = new(); 

        public void AddEntry(object source, CollectionHandlerEventArgs args, string collectionName) // метод для добавления записи об изменении в журнал
        {
            entries.Add(new JournalEntry(collectionName, args.ChangeType, args.ChangedItem.ToString()));
        }

        public override string ToString() // перегруженная версия метода ToString() 
        {
            string result = string.Empty;
            foreach (var entry in entries)
            {
                result += entry.ToString() + Environment.NewLine;
            }
            return result;
        }
    }
}
