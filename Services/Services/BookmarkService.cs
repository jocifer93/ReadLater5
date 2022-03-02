using Data;
using Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public class BookmarkService : IBookmarkService
    {
        private ReadLaterDataContext _ReadLaterDataContext;
        public BookmarkService(ReadLaterDataContext readLaterDataContext)
        {
            _ReadLaterDataContext = readLaterDataContext;
        }

        public Bookmark CreateBookmark(Bookmark bookmark)
        {
            _ReadLaterDataContext.Add(bookmark);
            _ReadLaterDataContext.SaveChanges();
            return bookmark;
        }

        public void UpdateBookmark(Bookmark bookmark)
        {
            _ReadLaterDataContext.Update(bookmark);
            _ReadLaterDataContext.SaveChanges();
        }

        public List<Bookmark> GetBookmarks(string username)
        {
            return _ReadLaterDataContext.Bookmarks.Where(b => b.Author == username).Include(b => b.Category).ToList();
        }

        public Bookmark GetBookmark(int id, string username)
        {
            return _ReadLaterDataContext.Bookmarks.Where(b => b.ID == id && b.Author == username).Include(b => b.Category).FirstOrDefault();
        }
        public Bookmark GetBookmarkAsNoTracking(int id, string username)
        {
            return _ReadLaterDataContext.Bookmarks.Where(b => b.ID == id && b.Author == username).Include(b => b.Category).AsNoTracking().FirstOrDefault();
        }

        public void DeleteBookmark(Bookmark bookmark)
        {
            _ReadLaterDataContext.Bookmarks.Remove(bookmark);
            _ReadLaterDataContext.SaveChanges();
        }
    }
}
