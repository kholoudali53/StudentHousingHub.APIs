using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentHousingHub.Core.Entities
{
    public class BaseEntity<TKey>
    {
        public TKey id { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
