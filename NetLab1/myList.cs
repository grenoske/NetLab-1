using System;
using System.Collections;
using System.Collections.Generic;


namespace myList
{
    public class myList<T>:IList<T>
    {
        private myNode<T> head;                 //head node;last node connected to the head(head.prev = lastnode; lastnode.next
        private int count;
        public event myListEventHandler Notify;
        public delegate void myListEventHandler(string methodName); //my field for my event handler(string)

        public T this[int index] 
        { 
            get { NotifyMethod("indexGet");  return FindNodeByIndex(index).Value; }
            set { FindNodeByIndex(index).Value = value; NotifyMethod("indexSet"); }
        }

        public int Count
        { get { NotifyMethod("Count"); return count; }  }

        public bool IsReadOnly
        { get { NotifyMethod("IsReadOnly");  return false; } }

        public myList()
        {
            head = null;
            count = 0;
            Notify = null;
        }
        public myList(IEnumerable<T> Collection)
        {
            if (Collection == null)
                throw new ArgumentNullException("null collection");
            else
                foreach (T item in Collection)
                    this.Add(item);
        }

/*        public bool WeakCheck()
        {
            IEnumerable ie = (ICollection)(this);
            foreach (T item in ie)
                break;
            return true;
        }*/

        // add node to the end of the list (head.prev)
        private void AddAfter(myNode<T> newNode)
        {
            if (head == null)       //if no nodes in list
            {
                head = newNode;     //add head
                head.next = head;   //lok head on itself
                head.prev = head;
            }
            else                                  
            {
                newNode.prev = head.prev;       //|newLast| <- |oldLast|
                newNode.prev.next = newNode;    //|oldLast| -> |newLast|
                newNode.next = head;            //|newLast| -> |head|
                head.prev = newNode;            //|head|    <- |newLast|
            }
        }

        // delete concrete node
        private void DeleteNode(myNode<T> node)
        {
            if(node == head)
            {
                if (head.next == head)
                    head = null;
                else
                {
                    node.prev.next = node.next; //|last| -> |nextToHead|
                    node.next.prev = node.prev; //|nextToHead| -> |last|
                    head = node.next;            //|head| = |nextToHead|
                }
            }
            else
            {
                node.prev.next = node.next; //|prevToNode| -> |nextToNode|
                node.next.prev = node.prev; //|nextToNode| -> |prevToNode|
            }
            node.Clear();
        }

        // find node by Value
        private myNode<T> FindNodeByValue(T value)
        {
            if (head == null)                           //if no nodes in list
                throw new Exception("list is empty");

            myNode<T> temp = head;
            do
            {
                if (EqualityComparer<T>.Default.Equals(temp.Value, value))
                    return temp;
                else
                    temp = temp.next;
            } 
            while (temp != head);
            return null;
        }

        private myNode<T> FindNodeByIndex(int index)
        {
            if (head == null)
                throw new ArgumentNullException("list is empty");
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException("index");

            myNode<T> temp = head;
            while (index > 0)
            {
                temp = temp.next;
                index--;
            }
            return temp;
        }

        // add to the end of the list
        public void Add(T item)
        {
            NotifyMethod("Add");
            myNode<T> newNode = new myNode<T>(item);
            AddAfter(newNode);
            count++;
        }

        //clear list
        public void  Clear()
        {
            NotifyMethod("Clear");
            while (count > 0)
            {
                DeleteNode(head.next);
                count--;
            }
        }

        public bool Contains(T item)
        {
            NotifyMethod("Contains");
            return FindNodeByValue(item) == null? false: true;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            NotifyMethod("CopyTo");
            if (array == null)
                throw new ArgumentNullException("array");
            if (arrayIndex < 0 || arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException("index");
            if (array.Length - arrayIndex < Count)
                throw new ArgumentException("not enough space");
            myNode<T> temp = head;
            for (int i = arrayIndex; i < Count + arrayIndex; i++)
            {
                array[i] = temp.item;
                temp = temp.next;
            }
        }

        // back enumerator for enumeration
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new ListEnumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new ListEnumerator(this);
        }

        public int IndexOf(T item)
        {
            NotifyMethod("IndexOf");
            if (head == null)
                throw new Exception("list is empty");

            myNode<T> temp = head;
            int index = 0;
            do
            {
                if (EqualityComparer<T>.Default.Equals(temp.Value, item))
                    return index;
                else
                {
                    temp = temp.next;
                    index++;
                }
            }
            while (temp.next != head);
            return -1;
        }

        public void Insert(int index, T item)
        {
            NotifyMethod("Insert");
            if (head == null)
                if (index == 0)
                    this.Add(item);
                else
                    throw new IndexOutOfRangeException("index");
            else
            {
                myNode<T> newNode = new myNode<T>(item);
                myNode<T> temp = FindNodeByIndex(index);

                newNode.next = temp;        //|newNode|-> |nextNode|
                newNode.prev = temp.prev;   //|newNode|<- |prevNode|
                temp.prev.next = newNode;   //|prevNode|->|newNode|
                newNode.next.prev = newNode;//|nextNode|<-|newNode|
                if (head == temp)
                    head = newNode;
            }
        }

        public bool Remove(T item)
        {
            NotifyMethod("Remove");
            myNode<T> node = FindNodeByValue(item);
            if (node == null)
                return false;
            else
            {
                DeleteNode(node);
                count--;
                return true;
            }

        }

        public void RemoveAt(int index)
        {
            NotifyMethod("RemoveAt");
            myNode<T> node = FindNodeByIndex(index);
            DeleteNode(node); 
            count--;
        }

        private void NotifyMethod(string methodName)
        {
            if (this.Notify != null)
                this.Notify(methodName + " is executing; ");
        }

        // enumerator for list
        internal class ListEnumerator : IEnumerator<T>, System.Collections.IEnumerator
        {
            private myList<T> myList;
            private int index;
            T current;

            public ListEnumerator(myList<T> myList)
            {
                this.myList = myList;
                index = 0;
                current = default(T); 
            }
            public T Current
            {
                get { return current; }
            }

            object System.Collections.IEnumerator.Current
            {
                get 
                {
                    if (index == 0 || (index == myList.Count + 1))
                    {
                        throw new ArgumentException("Invalid_Operation");
                    }
                    return current;
                }
            }

            public void Dispose()
            {
                
            }

            public bool MoveNext()
            {
                if (index >= myList.Count)      //if out of range
                {
                    index = myList.Count + 1;
                    return false;               //return false
                }
                current = myList[index];        //set current object
                index++;
                return true;
 
            }

            public void Reset()
            {
                index = 0;
                current = default(T);
            }
        }

        // use node to save data and link to prev/ next
        internal class myNode<T2>
        {
            internal myNode<T2> next;
            internal myNode<T2> prev;
            internal T2 item;
            public myNode(T2 item)
            {
                this.item = item;
                this.next = null;
                this.prev = null;
            }
            public T2 Value
            {
                get { return this.item; }
                set { this.item = value; }
            }

            public void Clear()
            {
                this.item = default(T2);
                this.next = null;
                this.prev = null;
            }

        }
    }
}
