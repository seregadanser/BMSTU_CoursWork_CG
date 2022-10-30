using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cours_m2G
{
    [Serializable]
    class Dict<T>
    {
        private Id key;
        private T value;

        public Id Key { get { return key; } }
        public T Value { get { return value; } }

        public Dict(Id key, T value)
        {
            this.value = value;
            this.key = key;
        }
    }
    [Serializable]
    class HashTable<T>
    {
        List<Dict<T>>[] table;
        List<Dict<int>> buisy;
        const int N = 100000;
        public int Count { get { return buisy.Count; } }
        public HashTable()
        {
            table = new List<Dict<T>>[N];
            buisy = new List<Dict<int>>();
        }

        private int HashFunc(string s, int size)
        {
            int hash_result = 0;
            for (int i = 0; i < s.Length; i++)
                hash_result = ((size - 1) * hash_result + s[i]) % size;
            hash_result = (hash_result * 2 + 1) % size;
            return hash_result;
        }

        public void Add(Dict<T> val)
        {
            int position = HashFunc(val.Key.Description, N);
            if (table[position] == null)
            table[position] = new List<Dict<T>>();
            table[position].Add(val);
            buisy.Add(new Dict<int>(val.Key, position));
        }

        public void Remove(Id key)
        {
            int position = HashFunc(key.Description, N);
            if (table[position] != null)
                foreach (Dict<T> d in table[position])
                    if (d.Key == key)
                    {
                        table[position].Remove(d);
                        buisy.Remove(new Dict<int>(key, position));
                        return;
                    }
        }

        public bool IsIn(Id key)
        {
            int position = HashFunc(key.Description, N);
            if (table[position] != null)
                foreach (Dict<T> d in table[position])
                    if (d.Key == key)
                        return true;
            return false;
        }

        public T this[Id key]
        {
            get
            {
                int position = HashFunc(key.Description, N);
                if (table[position] != null)
                    foreach (Dict<T> d in table[position])
                        if (d.Key == key)
                            return d.Value;
                return default(T);
            }

            set { }
        }
        private Id this[int i]
        {
            get { return buisy[i].Key; }
            set { }
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }
        [Serializable]
        public class Enumerator
        {
            int nIndex;
            HashTable<T> collection;
            public Enumerator(HashTable<T> coll)
            {
                collection = coll;
                nIndex = -1;
            }

            public bool MoveNext()
            {
                nIndex++;
                return (nIndex < collection.Count);
            }

            public T Current => collection[collection[nIndex]];
        }

    }

    class ContainerHash<T>
    {

        HashTable<T> components;
        HashTable<List<Id>> parent;
        HashTable<List<Id>> children;

        public int Count { get { return components.Count; } }

        public ContainerHash()
        {
            components = new HashTable<T>();
            children = new HashTable<List<Id>>();
            parent = new HashTable<List<Id>>();

        }

        public T Add(Dict<T> dict, Id parent, int k = 0, params Id[] children)
        {


            if (components.IsIn(dict.Key))
            {
                this.parent[dict.Key].Add(parent);
                return components[dict.Key];
            }



            components.Add(dict);

            this.parent.Add(new Dict<List<Id>>(dict.Key, new List<Id>()));
            this.parent[dict.Key].Add(parent);
            this.children.Add(new Dict<List<Id>>(dict.Key, new List<Id>()));
            foreach (Id i in children)
            {
                this.children[dict.Key].Add(i);
            }


            return default;

        }

        public T Add(Dict<T> dict, Id parent, Id parent2, int k = 0, params Id[] children)
        {

            if (components.IsIn(dict.Key))
            {
                if (IsparentIn(parent, dict.Key) == 0)
                    this.parent[dict.Key].Add(parent);
                if (IsparentIn(parent2, dict.Key) == 0)
                    this.parent[dict.Key].Add(parent2);
                return components[dict.Key];
            }

            components.Add(dict);
            this.parent.Add(new Dict<List<Id>>(dict.Key, new List<Id>()));
            this.parent[dict.Key].Add(parent);
            this.parent[dict.Key].Add(parent2);
            this.children.Add(new Dict<List<Id>>(dict.Key, new List<Id>()));
            foreach (Id i in children)
            {
                this.children[dict.Key].Add(i);
            }

            return default;

        }

        private int IsparentIn(Id w, Id key)
        {
            foreach (Id i in parent[key])
                if (i == w)
                    return 1;
            return 0;
        }
        public T Add(Dict<T> dict, int k = 0, params Id[] children)
        {
            bool f = true;
            if (components.IsIn(dict.Key))
                return components[dict.Key];


            components.Add(dict);
            parent.Add(new Dict<List<Id>>(dict.Key, new List<Id>()));
            this.children.Add(new Dict<List<Id>>(dict.Key, new List<Id>()));
            foreach (Id i in children)
            {
                this.children[dict.Key].Add(i);
            }


            return default;
        }

        ///// <summary>
        ///// Удаление элемнта из контейнера по индексу
        ///// </summary>
        ///// <param name="index">идекс</param>
        ///// <returns>кортеж из списков Id сначала parent затем children</returns>
        //public Tuple<List<Id>, List<Id>> Remove(int index)
        //{
        //    components.RemoveAt(index);
        //    id.RemoveAt(index);
        //    Tuple<List<Id>, List<Id>> r = new Tuple<List<Id>, List<Id>>(parent[index], children[index]);
        //    parent.RemoveAt(index);
        //    children.RemoveAt(index);
        //    return r;
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //public Tuple<List<Id>, List<Id>> Remove(Id id)
        //{
        //    Tuple<List<Id>, List<Id>> r = null;
        //    for (int i = 0; i < components.Count; i++)
        //    {
        //        if (this.id[i] == id)
        //        {
        //            components.RemoveAt(i);
        //            this.id.RemoveAt(i);
        //            r = new Tuple<List<Id>, List<Id>>(parent[i], children[i]);
        //            parent.RemoveAt(i);
        //            children.RemoveAt(i);
        //            return r;
        //        }
        //    }

        //    return r;
        //}

        ///// <summary>
        ///// Удаление детей оюъекта
        ///// </summary>
        ///// <param name="child">Id ребенка</param>
        ///// <returns>список id контейнера рекомендованых к удалению</returns>
        //public List<Id> RemoveChildren(Id child)
        //{
        //    List<Id> r = new List<Id>();
        //    for (int i = children.Count - 1; i >= 0; i--)
        //    {
        //        for (int j = children[i].Count - 1; j >= 0; j--)
        //            if (children[i][j] == child)
        //            {
        //                children[i].RemoveAt(j);
        //                r.Add(id[i]);
        //            }
        //    }
        //    return r;
        //}
        ///// <summary>
        ///// Удаление родителей объекта
        ///// </summary>
        ///// <param name="parent">Id родителя</param>
        ///// <returns>список id контейнера рекомендованых к удалению</returns>
        //public List<Id> RemoveParent(Id parent)
        //{
        //    List<Id> r = new List<Id>();
        //    for (int i = this.parent.Count - 1; i >= 0; i--)
        //    {
        //        for (int j = this.parent[i].Count - 1; j >= 0; j--)
        //            if (this.parent[i][j] == parent)
        //            {
        //                this.parent[i].RemoveAt(j);
        //            }
        //        if (this.parent[i].Count == 0 && children[i].Count == 0)
        //            r.Add(id[i]);
        //    }
        //    return r;
        //}

        public List<Id> GetConnectionObjects(Id id)
        {
            int ii = 0;
            List<Id> ff = new List<Id>();
            ff.AddRange(parent[id]);
            ff.AddRange(children[id]);
            return ff;
        }
        public List<Id> GetParents(Id id)
        {
            int ii = 0;
            List<Id> ff = new List<Id>();
            ff.AddRange(parent[id]);
            return ff;
        }
        public List<Id> GetChildren(Id id)
        {
            List<Id> ff = new List<Id>();
            ff.AddRange(children[id]);
            return ff;
        }


        //public void Clear()
        //{
        //    children.Clear();
        //    parent.Clear();
        //    components.Clear();
        //}

        //public T this[int i]
        //{
        //    get { return components[i]; }
        //    set { components[i] = value; }
        //}

        public T this[Id j]
        {
            get
            {
                return components[j];
            }
            //set
            //{
            //    for (int i = 0; i < id.Count; i++)
            //        if (id[i] == j)
            //        {
            //            components[i] = value;
            //        };
            //    ;
            //}
        }



        public cours_m2G.HashTable<T>.Enumerator GetEnumerator()
        {
            return components.GetEnumerator();
        }
    }
}
