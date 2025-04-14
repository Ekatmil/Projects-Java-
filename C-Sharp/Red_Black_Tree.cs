﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Red_Black_Tree
{
    class Node
    {
        public int val;
        public Node left;
        public Node right;
        public Node parent;
        public bool color; // true - red; false - black;
        public Node()
        {
            val = 0;
            left = null;
            right = null;
            parent = null;
            color = false;
        }
        public Node(int val)
        {
            this.val = val;
            left = null;
            right = null;
            parent = null;
            color = false;
        }
        public Node(int val, Node left, Node right, Node parent)
        {
            this.left = left;
            this.right = right;
            this.val = val;
            this.parent = parent;
            color = false;
        }

    }
    class RedBlackTree
    {
        public Node root;
        public Node Nil;

        public RedBlackTree()
        {
            root = new Node();
            Nil = new Node();
            Nil.color = false;
            Nil.val = 0;
            root.parent = Nil;
            root = Nil;
        }
        void LeftRotate(Node x)
        {
            Node y = x.right;
            x.right = y.left;
            if (y.left != Nil)
                y.left.parent = x;
            y.parent = x.parent;
            if (x.parent == Nil)
                root = y;
            else if (x == x.parent.left)
                x.parent.left = y;
            else x.parent.right = y;
            y.left = x;
            x.parent = y;
        }
        void RightRotate(Node x)
        {
            Node y = x.left;
            x.left = y.right;
            if (y.right != Nil)
                y.right.parent = x;
            y.parent = x.parent;
            if (x.parent == Nil)
                root = y;
            else if (x == x.parent.right)
                x.parent.right = y;
            else x.parent.left = y;
            y.right = x;
            x.parent = y;
        }
        public bool Insert(int val)
        {
            Node z = new Node(val);
            if (Search(val, root) == true) return false;
            return Insert(ref z);
        }
        public bool Insert(ref Node z)
        {
            Node y = Nil;
            Node x = root;

            while (x != Nil)
            {
                y = x;
                if (z.val < x.val)
                    x = x.left;
                else x = x.right;
            }
            z.parent = y;
            if (y == Nil)
                root = z;
            else if (z.val < y.val)
                y.left = z;
            else y.right = z;
            z.left = Nil;
            z.right = Nil;
            z.color = true;
            InsertFixUp(z);
            return true;

        }
        void InsertFixUp(Node z)
        {
            Node y = Nil;
            while (z.parent.color == true)
            {
                if (z.parent == z.parent.parent.left)
                {
                    y = z.parent.parent.right;
                    if (y.color == true)
                    {
                        z.parent.color = false;
                        y.color = false;
                        z.parent.parent.color = true;
                        z = z.parent.parent;
                    }
                    else
                    {
                        if (z == z.parent.right)
                        {
                            z = z.parent;
                            LeftRotate(z);
                        }
                        z.parent.color = false;
                        z.parent.parent.color = true;
                        RightRotate(z.parent.parent);
                    }
                }
                else
                {
                    y = z.parent.parent.left;
                    if (y.color == true)
                    {
                        z.parent.color = false;
                        y.color = false;
                        z.parent.parent.color = true;
                        z = z.parent.parent;
                    }
                    else
                    {
                        if (z == z.parent.left)
                        {
                            z = z.parent;
                            RightRotate(z);
                        }

                        z.parent.color = false;
                        z.parent.parent.color = true;
                        LeftRotate(z.parent.parent);
                    }
                }
            }
            root.color = false;
        }
        public Node Minimum(Node x)
        {
            while (x.left != Nil)
                x = x.left;
            return x;
        }
        public Node Maximum(Node x)
        {
            while (x.right != Nil)
                x = x.right;
            return x;
        }
        public void Transplant(Node x, Node y)
        {
            if (x.parent == Nil)
                root = y;
            else if (x == x.parent.left)
                x.parent.left = y;
            else x.parent.right = y;
            y.parent = x.parent;
        }
        public bool Delete(int val)
        {
            Node z = SearchNode(val, root);
            if (z == Nil) return false;
            else
            {
               return Delete(ref z);
            }

        }
        public bool Delete(ref Node z)
        {
            Node y = z;
            Node x;
            bool original = y.color;
            if (z.left == Nil)
            {
                x = z.right;
                Transplant(z, z.right);
            }
            else if (z.right == Nil)
            {
                x = z.left;
                Transplant(z, z.left);
            }
            else
            {
                y = Minimum(z.right);
                original = y.color;
                x = y.right;
                if (y.parent == z)
                    x.parent = y;
                else
                {
                    Transplant(y, y.right);
                    y.right = z.right;
                    y.right.parent = y;
                }
                Transplant(z, y);
                y.left = z.left;
                y.left.parent = y;
                y.color = z.color;
            }
                if (original == false)
                    DeleteFixUp(x);
            return true;
            
        }
        void DeleteFixUp(Node x)
        {
            Node y;
            while (x != root && x.color == false)
            {
                if (x == x.parent.left)
                {
                    y = x.parent.right;
                    if (y.color == true)
                    {
                        y.color = false;
                        x.parent.color = true;
                        LeftRotate(x.parent);
                        y = x.parent.right;
                    }
                    if (y.left.color == false && y.right.color == false)
                    {
                        y.color = true;
                        x = x.parent;
                    }
                    else
                    {
                        if (y.right.color == false)
                        {
                            y.left.color = false;
                            y.color = true;
                            RightRotate(y);
                            y = x.parent.right;
                        }
                        y.color = x.parent.color;
                        x.parent.color = false;
                        y.right.color = false;
                        LeftRotate(x.parent);
                        x = root;
                    }
                }
                else
                {
                    y = x.parent.left;
                    if (y.color == true)
                    {
                        y.color = false;
                        x.parent.color = true;
                        RightRotate(x.parent);
                        y = x.parent.left;
                    }
                    if (y.right.color == false && y.left.color == false)
                    {
                        y.color = true;
                        x = x.parent;
                    }
                    else
                    {
                        if (y.left.color == false)
                        {
                            y.right.color = false;
                            y.color = true;
                            LeftRotate(y);
                            y = x.parent.left;
                        }
                        y.color = x.parent.color;
                        x.parent.color = false;
                        y.left.color = false;
                        RightRotate(x.parent);
                        x = root;
                    }
                }
            }
            x.color = false;
        }
        public Node SearchNode(int val, Node x)
        {
            if (x == Nil)
                return Nil;
            if (val < x.val)
            {
                return SearchNode(val, x.left);
            }
            if (val > x.val)
            {
                return SearchNode(val, x.right);
            }
            return x;
        }
        public bool Search(int val, Node x)
        {
            if (x == Nil)
                return false;
            if (val < x.val)
            {
                return Search(val, x.left);
            }
            if (val > x.val)
            {
                return Search(val, x.right);
            }
            return true;
        }
        public void Print(Node root, int count)
        {
            if (root == Nil)
                return;
            Print(root.right, count + 1);
            for (int i = 1; i <= count; i++)
                Write(" ");
            if (root.color == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Write(root.val);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Write(root.val);
            }
            WriteLine();
            Print(root.left, count + 1);
        }
        public bool ColorOfChildren(Node root)
        {
                if (root == Nil)
                    return true;
                if (root.color == true)
                {
                    if (root.left.color == true|| root.right.color == true) return false;
                   
                }
            return (ColorOfChildren(root.left) == true && ColorOfChildren(root.right) == true);
        }
        public int BlackHeight(Node root)
        {
            if (root == Nil)
                return 1;
            int leftSubtree = BlackHeight(root.left);
            if (leftSubtree == 0)
                return leftSubtree;
            int rightSubtree = BlackHeight(root.right);
            if (rightSubtree == 0)
                return rightSubtree;
            if (leftSubtree != rightSubtree)
                return 0;
            else
            {
                if (root.color == false)
                    return leftSubtree + 1;
                else return leftSubtree;
            }
        }
        public bool Check(Node root)
        {
            if (root.color != false) return false;
            if (Nil.color != false) return false;
            if (BlackHeight(root) == 0) return false;
            if (ColorOfChildren(root) == false) return false;
            return true;
        }

    }

    class Program
    {
        static void Main()
        {
            RedBlackTree Tree = new RedBlackTree();
            Random rand = new Random();
            int[] v = new int[1000];
            int dups = 0;
            for (int i = 0; i < v.Length; ++i)
            {
                v[i] = rand.Next(10000);
                WriteLine("inserting " + v[i]);
                if (!Tree.Insert(v[i]))
                    ++dups;
            }
            WriteLine(dups + " duplicate nodes");

            int noDelete = 0;
            foreach (int i in v)
            {
                WriteLine("deleting " + i);
                if (!Tree.Delete(i))
                    ++noDelete;
            }
            WriteLine(noDelete + " values could not be deleted");
            Console.ForegroundColor = ConsoleColor.White;
            WriteLine("Press I to insert integer value into tree");
            WriteLine();
            WriteLine("Press P to print the tree");
            WriteLine();
            WriteLine("Press D to delet value from the tree");
            WriteLine();
            WriteLine("Press S to search for a value in the tree");
            WriteLine();
            WriteLine("Press C to checks that a red-black tree is valid");
            WriteLine();
            WriteLine("Press H to check only black height of the tree");
            WriteLine();
            while (true)
            {
                string s = Console.ReadLine();
                if (s == null) break;
                string[] a = s.Split(' ');
                if (s == "P")
                {
                    WriteLine("P");
                    Tree.Print(Tree.root, 0);
                    WriteLine();
                }
                if (s == "H")
                {
                    WriteLine("H "+ Tree.BlackHeight(Tree.root));
                    WriteLine();
                }
                if (s == "C")
                {
                    if (Tree.Check(Tree.root) == true)
                    {
                        WriteLine("C");
                        WriteLine("Red-Black tree is valid");
                        WriteLine();
                    }
                    else
                    {
                        WriteLine("C");
                        WriteLine("Red-Black tree is not valid");
                        WriteLine();
                    }
                }
                if (a[0] == "I")
                {
                    int y = int.Parse(a[1]);
                    if (Tree.Insert(y) == true)
                    {
                        WriteLine("I " + y);
                        WriteLine("Element inserted");
                        WriteLine();
                    }
                    else
                    {
                        WriteLine("I " + y);
                        WriteLine("Element was already in the tree");
                        WriteLine();
                    }
                }
                if (a[0] == "D")
                {
                    int y = int.Parse(a[1]);
                    if (Tree.Delete(y) == true)
                    {
                        WriteLine("D " + y);
                        WriteLine("Element deleted");
                        WriteLine();
                    }
                    else
                    {
                        WriteLine("D " + y);
                        WriteLine("Element was not in the tree");
                        WriteLine();
                    }
                }
                if (a[0] == "S")
                {
                    int y = int.Parse(a[1]);
                    if (Tree.Search(y, Tree.root) == true)
                    {
                        WriteLine("S " + y);
                        WriteLine("Present");
                        WriteLine();
                    }
                    else
                    {
                        WriteLine("S " + y);
                        WriteLine("Absent");
                        WriteLine();
                    }
                }
          
            }
        }
    }
}
