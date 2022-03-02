using Entity;
using System.Collections.Generic;

namespace Services
{
    public interface IWidgetService
    {
        List<Bookmark> GetMostRecentBookmarks(int number, string username);
    }
}
