using System;
using HtmlAgilityPack;
using System.Net;
using System.IO;
using System.Text;

namespace Bookmakers
{
    class Program
    {
        static void Main(string[] args)
        {
            var pageContent = LoadPage(@"https://1x-bet-ua.com/ru/live/Football/");
            var document = new HtmlDocument();
            document.LoadHtml(pageContent);

            HtmlNodeCollection teams = document.DocumentNode.SelectNodes(".//span[@class='c-events__teams']");
            HtmlNodeCollection bets = document.DocumentNode.SelectNodes(".//div[@class='c-bets']//a");


            foreach (HtmlNode team in teams)
                foreach (HtmlNode bet in bets) { 
                    Console.WriteLine("{0}", team.InnerText, team.GetAttributeValue("title", ""));
                    Console.WriteLine("{0} - {1}", bet.InnerText, bet.GetAttributeValue("data-coef", ""));
                }




            string LoadPage(string url)
            {
                var result = "";
                var request = (HttpWebRequest)WebRequest.Create(url);
                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var receiveStream = response.GetResponseStream();
                    if (receiveStream != null)
                    {
                        StreamReader readStream;
                        if (response.CharacterSet == null)
                            readStream = new StreamReader(receiveStream);
                        else
                            readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                        result = readStream.ReadToEnd();
                        readStream.Close();
                    }
                    response.Close();
                }
                return result;
            }


        }

        
    }
}