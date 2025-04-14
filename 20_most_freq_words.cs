using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;
interface Map<K,V>
{
    V this[K key]
    {
        get;
        set;
    }
    bool containsKey(K key);
    Pair<K, V>[] allKeyValues();
}
class Pair<K, V>
{
    public K key;
    public V val;
    public Pair(K key, V val)
    {
        this.key = key;
        this.val = val;
    }
}
class Node<K, V>
{
    public K key;
    public V val;
    public Node<K, V> next;  
    public Node(K key, V val)
    {
        this.key = key;
        this.val = val;
    }
 }
class HashMap<K, V> : Map<K, V>
{
    Node<K, V>[] a = new Node<K, V>[5];
    int count = 0;
    int hash(K key)
    {
        return Math.Abs(key.GetHashCode() % a.Length);
    }
    Node<K, V> find(K key)
    {
        int h = hash(key);
        Node<K, V> p = a[h];
        while (p != null && !p.key.Equals(key))
            p = p.next;
        return p;
    }
    void grow()
    {
        Node<K, V>[] b = a;
        a = new Node<K, V>[2 * a.Length];
        for (int k = 0; k< b.Length; k++)
        {
            if (b[k] != null)
            {
                Node<K, V> p = b[k];
                while (p != null)
                {
                    int i = hash(p.key);
                    Node<K, V> n = new Node<K, V>(p.key, p.val);
                    n.next = a[i];
                    a[i] = n;
                    p = p.next;
                }
            }
        }
    }
    public bool containsKey(K key)
    {
        return find(key) != null;
    }
    public V this[K key]
    {
        get
        {
            Node<K, V> p = find(key);
            if (p == null)
                return default(V);
            return p.val;
        }
        set
        {
            Node<K, V> p = find(key);
            if (p != null)
            {
                p.val = value;
                return;
            }
                count++;
                int h = hash(key);
                Node<K, V> n = new Node<K, V>(key, value);
                n.next = a[h];
                a[h] = n;
                if (1.0 * count / a.Length > 4.0)
                    grow();
        }
    }
    public Pair<K, V>[] allKeyValues()
    {
        Pair<K, V>[] l = new Pair<K, V>[count];
        int j = 0;
        for (int i = 0; i < a.Length; i++)
        {
            Node<K, V> c = a[i];
            while (c != null)
            {
                l[j] = new Pair<K, V>(c.key, c.val);
                c = c.next;
                j++;
            }
        }
        return (l);
    }

}
class Programm
{
    static string readWord(StreamReader r)
    {
        string s = "";
        while (true)
        {
            int i = r.Read();
            if (i == -1)
                return null;
            char c = (char)i;
            if (char.IsLetter(c))
                s += c;
            else if (s.Length > 0)
                return s;
        }
    }
    static void bubbleSort(Pair<string, int>[] a)
    {
        int i, j;
        for (i = a.Length - 1; i >= 0; i--)
            for (j = 0; j < i; j++)
            {
                if (a[j] != null)
                {
                    int k = j + 1;
                    if (a[k] != null)
                    {
                        if (a[j].val > a[k].val)
                        {
                            Pair<string, int> t;
                            t = a[j];
                            a[j] = a[k];
                            a[k] = t;
                        }
                    }
                    else
                        k++;
                }
            }
    }
     
    static void Main()
    {
        HashMap<string, int> m = new HashMap<string, int>();
        StreamReader r = new StreamReader("text.in");
        while (true)
        {
            int count = 1;
            string s = readWord(r);
            if (s == null) break;
            if (m.containsKey(s))
            {
                m[s]++;
            }
            else m[s] = count;
        }
        Pair<string, int>[] p = m.allKeyValues();
        bubbleSort(p);
        if (p.Length >= 21)
        {
            for (int i = p.Length - 1; i >= p.Length - 20; i--)
                WriteLine(p[i].key + " " + p[i].val);
        } 
        else
            for (int i = p.Length -1; i >=0; i --)
                WriteLine(p[i].key + " " + p[i].val);
        r.Close();
    }
