using Data;
using Entity;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class WidgetService : IWidgetService
    {
        private ReadLaterDataContext _ReadLaterDataContext;
        public WidgetService(ReadLaterDataContext readLaterDataContext)
        {
            _ReadLaterDataContext = readLaterDataContext;
        }

        public List<Bookmark> GetMostRecentBookmarks(int number, string username)
        {
            return _ReadLaterDataContext.Bookmarks
                .Where(b=>b.Author == username)
                .OrderByDescending(b=>b.CreateDate)
                .Take(number)
                .ToList();
        }
    }
}
