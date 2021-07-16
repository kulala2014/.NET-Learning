using System;
using System.Collections.Generic;

namespace Generical
{
    class Program
    {
        static void Main(string[] args)
        {
            //开放类型：具有泛型类型参数的类型
            //封闭类型：所有类型参数都传递了屎级的数据类型
            OpenAndClosedClass.show();


            //性能测试
            OperatorTime.show();


            //泛型类型和继承
            SameDataLinkedList();

        }

        private static void SameDataLinkedList()
        {
            Node<Char> head = new Node<char>('A');
            head = new Node<char>('B', head);
            head = new Node<char>('C', head);
            Console.WriteLine(head.ToString());


            Node headNode = new TypeNode<Char>('A');
            headNode = new TypeNode<DateTime>(DateTime.Now, headNode);
            headNode = new TypeNode<string>("Today is ", headNode);
            Console.WriteLine(headNode.ToString());


        }
    }
 }
