using Business.DB.Model;
using Learning.CustomAttributes.Extends;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reflection
{
    [TableName("House")]
   public class HouseModel: BaseModel
   {
        [ColumnName("House")]
        public string HouseName { get; set; }
        [ColumnName("Sex")]
        public string Gender { get; set; }
    }
}
