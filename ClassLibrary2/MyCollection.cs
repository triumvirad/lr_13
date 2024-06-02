using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lab_12_4
{
    public class MyCollection<T> : ICollection<T>, IEnumerable<T> where T : IInit, IComparable, new()
    {
        public T[] table; // таблица
        int count = 0; // количество записей
        double fillRatio; // коэффицент заполняемой таблицы

        public int Capacity => table.Length; // ёмкость, количество выделенной памяти
        private int capacity;
        public int Count => count; // текущее количество элементов

        public bool IsReadOnly => throw new NotImplementedException();

        public MyCollection() // конструктор без параметров
        {
            capacity = 10;
            table = new T[10];
            this.fillRatio = 0.72;
        }

        public MyCollection(int capacity, double fillRatio = 0.72) // конструктор с паметрами
        {
            if (capacity <= 0) throw new Exception("Число не распознано. В хеш-таблице должен быть хотя бы 1 элемент.");
            this.capacity = capacity;
            table = new T[capacity];
            this.fillRatio = fillRatio;
        }

        public MyCollection(MyCollection<T> collection)
        {
            MyCollection<T> temp = (MyCollection<T>)collection.Clone();
            capacity = temp.capacity;
            table = temp.table;
            fillRatio = temp.fillRatio;
            count = temp.count;
        }

        public object? Clone()
        {
            MyCollection<T> clone = new MyCollection<T>(capacity, fillRatio);
            foreach (T item in this)
            {
                if (item is ICloneable cloneable)
                {
                    clone.Add((T)cloneable.Clone());
                }
                else
                {
                    clone.Add(item);
                }
            }
            return clone;
        }

        public bool Contains(T data) // проверка на наличие элемента
        {
            return !(FindItem(data) < 0);
        }

        public void Print() // печать хеш-таблицы
        {
            int i = 0;
            foreach (T item in table)
            {
                Console.WriteLine($"{i} : {item}");
                i++;
            }
        }

        // private
        int GetIndex(T data)
        {
            return Math.Abs(data.GetHashCode()) % Capacity;
        }

        // добавление элемента в таблицу
        void AddData(T data)
        {
            if (data == null) return; // добавляется пустой элемент
            // ищем место
            int index = GetIndex(data);
            int current = index; // запомнили индекс
            // если место уже занято
            if (table[index] != null)
            {
                // идём до конца таблицы или до первого пустого места
                while (current < table.Length && table[current] != null)
                    current++;
                // если таблица закончилась
                if (current == table.Length)
                {
                    // идём от начала таблицы до индекса, который запомнили
                    current = 0;
                    while (current < index && table[current] != null)
                        current++;
                    // места нет
                    if (current == index)
                    {
                        throw new Exception("Нет места в таблице");
                    }
                }
            }
            // место найдено
            table[current] = data;
            count++;
        }

        public int FindItem(T data)
        {
            // находим индекс
            int index = GetIndex(data);
            // нет элемента
            if (table[index] == null) return -1;
            // есть элемент, совпадает
            if (table[index].Equals(data))
                return index;
            else // не совпадает
            {
                int current = index; // идём вниз по таблице
                while (current < table.Length)
                {
                    if (table[current] != null
                       && table[current].Equals(data)) // совпадает 
                        return current;

                    current++;
                }
                current = 0; // идём с начала таблицы
                while (current < index)
                {
                    if (table[current] != null
                        && table[current].Equals(data)) // совпадают
                        return current;
                    current++;
                }
            }
            // не нашли
            return -1;
        }

        public virtual void Add(T item) // добавляет элемент
        {
            if ((((double)Count) / Capacity) > fillRatio) // место в таблице закончилось
            {
                // увеличиваем таблицу в 2 раза и переписываем всю информацию
                T[] temp = (T[])table.Clone();
                table = new T[temp.Length * 2];
                capacity *= 2;
                count = 0;
                for (int i = 0; i < temp.Length; i++)
                    AddData(temp[i]);
            }
            // добавляем новый элемент
            AddData(item);
        }

        public void Clear() // чистит коллекцию
        {
            count = 0;
            table = new T[capacity];
        }

        public void CopyTo(T[] array, int index) // копирует в массив
        {
            if (index >= 0 && index < array.Length)
            {
                foreach (T item in this)
                {
                    if (index >= array.Length)
                    {
                        break;
                    }
                    else
                    {
                        array[index] = item;
                        index++;
                    }
                }
            }
        }

        public virtual bool Remove(T data) // удаляет элемент
        {
            int index = FindItem(data);
            if (index < 0) return false;
            count--;
            table[index] = default;
            return true;
        }

        IEnumerator IEnumerable.GetEnumerator() // прикол, в котором есть штука для перебора 
        {
            return GetEnumerator();
        }

        public IEnumerator<T> GetEnumerator() // эта самая штука
        {
            foreach (T item in table)
            {
                yield return item;
            }
        }
    }
}
