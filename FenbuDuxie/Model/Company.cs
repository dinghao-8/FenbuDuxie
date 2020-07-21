using System;
using System.Collections.Generic;
using System.Text;

namespace FenbuDuxie.Model
{
  public   class Company :BaseModel
    {
        public string  Name { get; set; }
        public DateTime  CreaterTime { get; set; }
        public int  CreatorId { get; set; }
        public Nullable<int>  LastModifierId { get; set; }  
        public DateTime? LastModifyTime { get; set; }
    }
}
