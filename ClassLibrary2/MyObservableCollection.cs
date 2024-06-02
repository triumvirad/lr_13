using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using ClassLibrary;
using Lab_12_4;

namespace ClassLibrary2
{
    public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args); // для событий, извещающих об изменениях в коллекции, определяется данный делегат 

    public class CollectionHandlerEventArgs : EventArgs
    {
        public string ChangeType { get; set; } // тип изменения
        public object ChangedItem { get; set; } // изменяемый элемент

        public CollectionHandlerEventArgs(string changeType, object changedItem) // конструктор инициализации аргументов
        {
            ChangeType = changeType;
            ChangedItem = changedItem;
        }
    }

    public class MyObservableCollection<T> : MyCollection<T>, ICollection<T>, IEnumerable<T> where T : IInit, ICloneable, IComparable, new()
    {
        public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);
        public event CollectionHandler CollectionCountChanged; // событие, возникающее при изменении количества элементов в коллекции
        public event CollectionHandler CollectionReferenceChanged; // событие, возникающее при изменении ссылки на элемент

        public override void Add(T item) // добавить элемент
        {
            base.Add(item);
            CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs("добавление", item));
        }

        public override bool Remove(T item) // удалить элемент
        {
            bool result = base.Remove(item);
            if (result) CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs("удаление", item));
            return result;
        }

        public virtual void Replace(T oldd, T neww) // замена элемента
        {
            base.Remove(oldd);
            base.Add(neww);
            CollectionReferenceChanged?.Invoke(this, new CollectionHandlerEventArgs("замена", oldd));
        }
    }
}