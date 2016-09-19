using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.ViewModels
{
    public class IndexViewModel<T>
    {
        public IndexViewModel()
        {
        }

        public IndexViewModel(int page, int pageSize, IEnumerable<T> items)
        {
            Items = items.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = items.Count() };
        }

        public IEnumerable<T> Items { get; set; }
        public PageInfo PageInfo { get; set; }

    }
}