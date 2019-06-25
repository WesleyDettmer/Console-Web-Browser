using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ConnectorModel;
using HtmlAgilityPack;

namespace ConectorController
{
    public class ConnectorController
    {

        public static void SelectSearchEngine()
        {

            UrlResultModel urlObject = new UrlResultModel();

            Console.Write("Search:");

            urlObject.content = Console.ReadLine();
            urlObject.engineUrl = "https://www.google.com.br/search?q=";
            ConnectEngine(urlObject);
        }

        public static void ConnectEngine(UrlResultModel url)
        {

            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(url.engineUrl + url.content);

            document.Save(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\ScraperTexto.txt");

            HtmlDocument documentParse = new HtmlDocument();
            documentParse.Load(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\ScraperTexto.txt");

            HtmlNode[] nodes = documentParse.DocumentNode.SelectNodes("//a").ToArray();

            foreach (HtmlNode obj in nodes)
            {
                obj.InnerHtml = Regex.Replace(obj.InnerHtml, @"<[^>]*>", String.Empty);
                
                Console.WriteLine(obj.InnerHtml + " ");
            }

            Console.Read();

        }

    }
}
