using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cours_m2G
{
    class Container<T>
    {
        List<T> components;
        List<List<Id>> parent;
        List<List<Id>> children;

        public int Count { get { return components.Count; } }

        public Container()
        {
            components = new List<T>();
            children = new List<List<Id>>();
            parent = new List<List<Id>>();
        }

        public int Add(T value, Id parent,int k =0, params Id[] children)
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
        public int Add(T value, Id parent,Id parent2,int k = 0, params Id[] children)
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
        public int Add(T value,int k = 0, params Id[] children)
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
                parent.Add(new List<Id>());
                this.children.Add(new List<Id>());
                foreach (Id i in children)
                {
                    this.children[this.children.Count - 1].Add(i);
                }
            }

            return -1;
        }

        public Tuple<List<Id> , List<Id>> Remove(int index)
        {
            components.RemoveAt(index);
            Tuple<List<Id>, List<Id>> r = new Tuple<List<Id>, List<Id>>(parent[index], children[index]);
            parent.RemoveAt(index);
            children.RemoveAt(index);
            return r;
        }

        public List<T> RemoveChildren(Id child)
        {
            List<T> r = new List<T>();
            for(int i = children.Count-1; i>=0;i--)
            {
               foreach(Id id in children[i])
                    if(id == child)
                    {
                        children.RemoveAt(i);
                        parent.RemoveAt(i);
                        r.Add(components[i]);
                        components.RemoveAt(i);
                    }
            }
            return r;
        }

        public List<T> RemoveParent(Id parent)
        {

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
