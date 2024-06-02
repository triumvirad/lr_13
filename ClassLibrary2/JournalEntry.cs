using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2
{
    public class JournalEntry
    {
        public string CollectionName { get; set; } // открытое автореализуемое свойство типа string с названием коллекции, в которой произошло событие
        public string ChangeType { get; set; } // открытое автореализуемое свойство типа string с информацией о типе изменений в коллекции
        public string ChangedItem { get; set; } // открытое автореализуемое свойство типа string c данными объекта, с которым связаны изменения в коллекции

        public JournalEntry(string collectionName, string changeType, string changedItem) // конструктор для инициализации полей класса
        {
            CollectionName = collectionName;
            ChangeType = changeType;
            ChangedItem = changedItem;
        }

        public override string ToString() // перегруженная версия метода ToString() 
        {
            return $"Коллекция: {CollectionName}, Тип операции: {ChangeType}, Изменяемый элимент: {ChangedItem}";
        }
    }
}
