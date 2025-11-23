using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ReignsProject
{
    /// <summary>
    /// контейнер - двусвязный список
    /// </summary>
    /// www - наследуется от class чтобы работало с null
    /// наследуется от IEnumerable<Reignscard> чтобы можно было реализовать перебор элементов
    public class ReignsContainer<WWW> : IEnumerable<WWW> where WWW : class
    {
        #region поля
        private LinkedList<WWW> forContLinkedList;
        /// <summary>
        /// Количество элементов в контейнере
        /// </summary>
        public int Count => forContLinkedList.Count;
        /// <summary>
        /// Пуст ли контейнер
        /// </summary>
        public bool Isnulll => forContLinkedList.Count == 0;
        /// <summary>
        /// Первый элемент контейнера
        /// </summary>
        public WWW First => forContLinkedList.First?.Value;
        /// <summary>
        /// Последний элемент контейнера
        /// </summary>
        public WWW Last => forContLinkedList.Last?.Value;
        #endregion 
        #region конструкторы
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ReignsContainer()
        {
            forContLinkedList = new LinkedList<WWW>();
        }
        /// <summary>
        /// Конструктор с начальной коллекцией
        /// </summary>
        public ReignsContainer(IEnumerable<WWW> coll)
        {
            forContLinkedList = new LinkedList<WWW>(coll);
        }
        #endregion
        #region методы
        /// <summary>
        /// Добавить элемент в начало контейнера
        /// </summary>
        public void AddFirst(WWW card)
        {
            forContLinkedList.AddFirst(card);
        }
        /// <summary>
        /// Добавить элемент в конец контейнера
        /// </summary>
        public void AddLast(WWW card)
        {
            forContLinkedList.AddLast(card);
        }
        /// <summary>
        /// Удалить первый элемент контейнера
        /// </summary>
        public WWW RemoveFirst()
        {
            if (forContLinkedList.Count == 0) return null;
            var first = forContLinkedList.First.Value;
            forContLinkedList.RemoveFirst();
            return first;
        }
        /// <summary>
        /// Удалить последний элемент контейнера
        /// </summary>
        public WWW RemoveLast()
        {
            if (forContLinkedList.Count == 0) return null;
            var last = forContLinkedList.Last.Value;
            forContLinkedList.RemoveLast();
            return last;
        }
        /// <summary>
        /// Добавить коллекцию элементов в контейнер
        /// </summary>
        public void AddRange(IEnumerable<WWW> arr)
        {
            foreach (var card in arr) if (card != null) this.forContLinkedList.AddLast(card);
        }
        /// <summary>
        /// Удалить конкретный элемент из контейнера
        /// </summary>
        public bool Remove(WWW item)
        {
            if (item == null) return false;
            return forContLinkedList.Remove(item);
        }
        /// <summary>
        /// Проверить существует ли элемент в контейнере
        /// </summary>
        public bool Contains(WWW item)
        {
            if (item == null) return false;
            return forContLinkedList.Contains(item);
        }
        /// <summary>
        /// Очистить контейнер
        /// </summary>
        public void Clear()
        {
            forContLinkedList.Clear();
        }
        /// <summary>
        /// Получить все элементы по указанному правилу
        /// </summary>
        /// Func чтобы передевать именно lmnd методом 
        public IEnumerable<WWW> Where(Func<WWW, bool> res)
        {
            List<WWW> results = new List<WWW>();
            foreach (var item in forContLinkedList)
            {
                if (res(item))
                    results.Add(item);
            }
            return results;
        }
        /// <summary>
        /// Найти первый элемент удовлетворяющий условию
        /// </summary>
        public WWW FindFirst(Func<WWW, bool> res)
        {
            foreach (var item in forContLinkedList)
            {
                if (res(item)) return item;
            }
            return null;
        }
        /// <summary>
        /// Найти последний элемент удовлетворяющий условию
        /// </summary>
        public WWW FindLast(Func<WWW, bool> res)
        {
            return forContLinkedList.LastOrDefault(res);
        }
        /// <summary>
        /// Отсортировать контейнер с использованием своего условия
        /// </summary>
        public void Sort(IComparer<WWW> comparer)
        {
            var list = forContLinkedList.ToList();
            list.Sort(comparer);
            forContLinkedList = new LinkedList<WWW>(list);
        }
        /// <summary>
        /// Преобразовать контейнер в список
        /// </summary>
        public List<WWW> ToList()
        {
            return forContLinkedList.ToList();
        }
        /// <summary>
        /// Получить перечислитель
        /// </summary>
        public IEnumerator<WWW> GetEnumerator()
        {
            return forContLinkedList.GetEnumerator();
        }
        /// <summary>
        /// Получить перечислитель
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        /// <summary>
        /// Индексатор для доступа к элементам по индексу вернет элемент по индексу
        /// </summary>
        public WWW this[int index]
        {
            get
            {
                var curr = forContLinkedList.First;
                for (int i = 0; i < index; i++) curr = curr.Next;
                return curr.Value;
            }
        }
        #endregion
    }
}