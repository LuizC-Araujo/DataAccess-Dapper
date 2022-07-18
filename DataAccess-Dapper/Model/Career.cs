using System;
using System.Collections.Generic;

namespace DataAccess_Dapper.Model
{
    public class Career
    {
        public Career()
        {
            Items = new List<CareerItem>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public IList<CareerItem> Items { get; set; }
    }
}
