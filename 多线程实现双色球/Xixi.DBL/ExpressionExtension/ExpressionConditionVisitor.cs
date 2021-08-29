using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Xixi.DBL
{
    class ExpressionConditionVisitor : ExpressionVisitor
    {
        private Stack<string> _stack = new Stack<string>();


        public string Condition()
        {
            string condition = string.Concat(this._stack.ToArray());
            this._stack.Clear();
            return condition;
        }
        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node == null) throw new ArgumentNullException("BinaryExpression");
            _stack.Push(")");
            this.Visit(node.Right);
            _stack.Push($" {node.NodeType.ToSqlOpearation()} ");
            this.Visit(node.Left);
            _stack.Push("(");
            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if (node == null) throw new ArgumentNullException("ConstantExpression");
            _stack.Push($" '{node.Value}' ");
            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node == null) throw new ArgumentNullException("MemberExpression");
            _stack.Push($" [{node.Member.Name}] ");
            return node;
        }
        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node == null) throw new ArgumentNullException("MethodCallExpression");
            string format;
            switch (node.Method.Name.ToLower())
            {
                case "startwith":
                    format = "{0} LIKES '{1}%'";
                    break;
                case "endwith":
                    format = "{0} LIKES '%{1}'";
                    break;
                case "contains":
                    format = "{0} LIKES '%{1}%'";
                    break;
                default: throw new NotSupportedException("Not Supported Method");
            }

            this.Visit(node.Object);
            this.Visit(node.Arguments[0]);

            var left = _stack.Pop();
            var right = _stack.Pop();
           _stack.Push(string.Format(format, left, right));
            return node;
        }
    }
}
