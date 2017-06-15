using CsQuery;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FrexT.Controllers
{
    public class CurrencyType
    {
        public string cTitle;
        public int cValue;
        public String s5;
        public String s15;
        public String sHourly;
        public String sDaily;
        public CurrencyType()
        {
            cTitle = "";
            cValue = 0;
            s5 = "";
            s15 = "";
            sHourly = "";
            sDaily = "";
        }
    }
    public class ForexController : Controller
    {
        // GET: Forex
        public ActionResult Index()
        {
            ArrayList data_summery = new ArrayList();
            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "Only a test!");
            const string strUrl = "http://www.investing.com/technical/technical-summary";
            string pageContent = webClient.DownloadString(strUrl);
            CQ dom = pageContent;
            CQ table = dom.Find(".closedTbl").Eq(0);
            CQ summerys = table.Find("tr[data-row-type='summary']");
            CQ averages = table.Find("td[rowspan='3']");
            for ( int i =0;i< summerys.Length; i++)
            {
                CurrencyType data = new CurrencyType();
                CQ summery = summerys.Eq(i);
                CQ average = averages.Eq(i);
                data.s5 = summery.Children().Eq(1).Text();
                data.s15 = summery.Children().Eq(2).Text();
                data.sHourly = summery.Children().Eq(3).Text();
                data.sDaily = summery.Children().Eq(4).Text();
                if (average.Children("a").Length != 0)
                {
                    data.cTitle = average.Children("a").Eq(0).Attr("title").ToString().Replace("/","");
                }
                //data.cValue = average.Children("p").Eq(0).Text();
                data_summery.Add(data);
            }
            ViewBag.data_summery = data_summery;
            return View();
        }
    }
}