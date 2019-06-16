using ClippingKKModel;
using kindle_viewer.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kindle_viewer.Repository
{



    class Books : KKHttpClient
    {
        public async Task<List<KKBookItem>> FetchBooks(int userid, int offset, int take)
        {
            List<KKBookItem> books = await this.Get<List<KKBookItem>>(
                String.Format("/clippings/books/{0}?from={1}&take={2}", userid, offset, take)
                );

            
            foreach (var book in books)
            {
                book.image = "https://cdn.annatarhe.com/" + book.image + "-copyrightDB";
            }

            return books;
        }
    }
}
