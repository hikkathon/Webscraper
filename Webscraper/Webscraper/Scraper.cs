using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Webscraper
{
    public class Scraper
    {
        private ObservableCollection<EntryModel> _entries = new ObservableCollection<EntryModel>();

        public ObservableCollection<EntryModel> Entries
        {
            get { return _entries; }
            set { _entries = value; }
        }

        public void ScrapeData(string page)
        {
            var web = new HtmlWeb();
            var doc = web.Load(page);

            var Articles = doc.DocumentNode.SelectNodes("//*[@class='article-single']");

            foreach(var article in Articles)
            {
                var header = HttpUtility.HtmlDecode(article.SelectSingleNode(".//li[@class='article-header']")?.InnerText);
                var description = HttpUtility.HtmlDecode(article.SelectSingleNode(".//li[@class='article-copy']")?.InnerText);

                _entries.Add(new EntryModel { Title = header, Description = description });
            }
        }
    }
}
