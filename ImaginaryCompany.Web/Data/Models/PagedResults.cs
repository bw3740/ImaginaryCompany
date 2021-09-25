using System;
using System.Collections;
using System.Collections.Generic;

namespace ImaginaryCompany.Web.Data.Models
{
    public class PagedResults<T> : IEnumerable<T>
    {
        public PagedResults(IEnumerable<T> items, int totalCount, int pageSize, int recordStart)
        {
            if (recordStart < 0) throw new ArgumentOutOfRangeException(nameof(recordStart));
            if (pageSize < 1) throw new ArgumentOutOfRangeException(nameof(pageSize));
            this.items = items;
            TotalCount = totalCount;
            PageSize = pageSize;
            RecordStart = recordStart;
        }

        private IEnumerable<T> items;
        public int TotalCount { get; }
        public int PageSize { get;}
        public int RecordStart { get; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);

        public IEnumerator<T> GetEnumerator() => items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
