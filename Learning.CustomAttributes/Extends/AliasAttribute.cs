using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.CustomAttributes.Extends
{
   public abstract class AliasAbstractAttribute : Attribute
    {
        public abstract string AliasName(string Name);
    }


    public class TableNameAttribute : AliasAbstractAttribute
    {
        private string Name;

        public TableNameAttribute(string name)
        {
            this.Name = name;
        }

        public override string AliasName(string newName)
        {
            return this.Name;
        }
    }

    public class ColumnNameAttribute : AliasAbstractAttribute
    {
        private string Name;

        public ColumnNameAttribute(string name)
        {
            this.Name = name;
        }

        public override string AliasName(string newName)
        {
            return this.Name;
        }
    }
}
