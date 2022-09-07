using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cours_m2G
{
    /// <summary>
    /// Контейнер состоящий из объекотв, их детей и их родителей
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Container<T>
    {
        List<T> components;
        List<List<Id>> parent;
        List<List<Id>> children;
        List<Id> id;

        public int Count { get { return components.Count; } }

        public Container()
        {
            components = new List<T>();
            children = new List<List<Id>>();
            parent = new List<List<Id>>();
            id = new List<Id>();
        }

        public int Add(T value,Id objec, Id parent,int k =0, params Id[] children)
        {
            bool f = true;
            int j = 0;
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].Equals(value))
                {
                    f = false;
                    this.parent[i].Add(parent);
                    return i;
                }
            }
            if (f)
            {
                j = -1;
                components.Add(value);
                id.Add(objec);
                this.parent.Add(new List<Id>());
                this.parent[this.parent.Count - 1].Add(parent);
                this.children.Add(new List<Id>());
                
                foreach (Id i in children)
                {
                    this.children[this.children.Count - 1].Add(i);
                }
            }

            return j;

        }
        public int Add(T value, Id objec, Id parent,Id parent2,int k = 0, params Id[] children)
        {
            bool f = true;
            int j = 0;
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].Equals(value))
                {
                    f = false;
                    if(IsparentIn(parent, i) == 0)
                        this.parent[i].Add(parent);
                    if (IsparentIn(parent2, i) == 0)
                        this.parent[i].Add(parent2);
                    return i;
                }
                j = i;
            }
            if (f)
            {
                components.Add(value);
                id.Add(objec);
                this.parent.Add(new List<Id>());
                this.parent[this.parent.Count - 1].Add(parent);
                this.children.Add(new List<Id>());
                foreach (Id i in children)
                {
                    this.children[this.children.Count - 1].Add(i);
                }
                j = -1;
            }

            return j;

        }
        private int IsparentIn(Id w, int j)
        {
            foreach (Id i in parent[j])
                if (i == w)
                    return 1;
            return 0;
        }
        public int Add(T value, Id objec, int k = 0, params Id[] children)
        {
            bool f = true;
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].Equals(value))
                {
                    f = false;
                    return i;
                }
            }
            if (f)
            {
                components.Add(value);
                id.Add(objec);
                parent.Add(new List<Id>());
                this.children.Add(new List<Id>());
                foreach (Id i in children)
                {
                    this.children[this.children.Count - 1].Add(i);
                }
            }

            return -1;
        }

        /// <summary>
        /// Удаление элемнта из контейнера по индексу
        /// </summary>
        /// <param name="index">идекс</param>
        /// <returns>кортеж из списков Id сначала parent затем children</returns>
        public Tuple<List<Id> , List<Id>> Remove(int index)
        {
            components.RemoveAt(index);
            id.RemoveAt(index);
            Tuple<List<Id>, List<Id>> r = new Tuple<List<Id>, List<Id>>(parent[index], children[index]);
            parent.RemoveAt(index);
            children.RemoveAt(index);
            return r;
        }
      /// <summary>
      /// 
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
        public Tuple<List<Id>, List<Id>> Remove(Id id)
        {
            Tuple<List<Id>, List<Id>> r = null;
            for (int i = 0; i < components.Count; i++)
            {
                if (this.id[i]==id)
                {
                    components.RemoveAt(i);
                    this.id.RemoveAt(i);
                    r = new Tuple<List<Id>, List<Id>>(parent[i], children[i]);
                    parent.RemoveAt(i);
                    children.RemoveAt(i);
                    return r;
                }
            }
         
            return r;
        }

        /// <summary>
        /// Удаление детей оюъекта
        /// </summary>
        /// <param name="child">Id ребенка</param>
        /// <returns>список id контейнера рекомендованых к удалению</returns>
        public List<Id> RemoveChildren(Id child)
        {
            List<Id> r = new List<Id>();
            for(int i = children.Count-1; i>=0;i--)
            {
                for (int j = children[i].Count-1; j >= 0; j--)
                    if (children[i][j] == child)
                    {
                        children[i].RemoveAt(j);
                        r.Add(id[i]);   
                    }
            }
            return r;
        }
        /// <summary>
        /// Удаление родителей объекта
        /// </summary>
        /// <param name="parent">Id родителя</param>
        /// <returns>список id контейнера рекомендованых к удалению</returns>
        public List<Id> RemoveParent(Id parent)
        {
            List<Id> r = new List<Id>();
            for (int i = this.parent.Count - 1; i >= 0; i--)
            {
                for (int j = this.parent[i].Count - 1; j >= 0; j--)
                    if (this.parent[i][j] == parent)
                    {
                        this.parent[i].RemoveAt(j);
                    }
                if (this.parent[i].Count == 0 && children[i].Count == 0)
                    r.Add(id[i]);
            }
            return r;
        }

        public void Clear()
        {
            children.Clear();
            parent.Clear();
            components.Clear();
        }

        public T this[int i]
        {
            get { return components[i]; }
            set { components[i] = value; }
        }

        public T this[Id j]
        {
            get
            {
                for (int i = 0; i < id.Count; i++)
                    if (id[i] == j)
                    {
                        return components[i];
                    };
                return components[-1];
            }
            set
            {
                for (int i = 0; i < id.Count; i++)
                    if (id[i] == j)
                    {
                        components[i] = value;
                    };
                ;
            }
        }



        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        public class Enumerator
        {
            int nIndex;
            Container<T> collection;
            public Enumerator(Container<T> coll)
            {
                collection = coll;
                nIndex = -1;
            }

            public bool MoveNext()
            {
                nIndex++;
                return (nIndex < collection.Count);
            }

            public T Current => collection[nIndex];
        }
    }



}
