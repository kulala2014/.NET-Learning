using System;
using System.Collections.Generic;
using System.Text;

namespace Generical
{
    class Node<T>
    {
        public T m_data;
        public Node<T> m_next;

        public Node(T data) : this(data, null)
        {
            
        }

        public Node(T data, Node<T> next) => (m_data, m_next) = (data, next);

        public override string ToString()
        {
            return m_data.ToString() + (m_next != null ? m_next.ToString() : string.Empty);
        }
    }

    internal class Node
    {
        protected Node m_next;
        public Node(Node next) => m_next = next;
    }

    internal class TypeNode<T> : Node
    {
        public T m_data;

        public TypeNode(T data) : this(data, null)
        {
            
        }

        public TypeNode(T data, Node next) : base(next) =>m_data = data;

        public override string ToString()
        {
            return m_data.ToString() + (m_next != null ? m_next.ToString() : string.Empty);
        }
    }
}
