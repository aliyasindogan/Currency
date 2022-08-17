using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System.Xml;

namespace MVC.Controllers
{
    public class CurrencyController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var currency=GetCurrency();
            if (currency!=null)
            {
                return View(currency);
            }
            return View();
        }


        public Currency GetCurrency()
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load("http://www.tcmb.gov.tr/kurlar/today.xml");
                string usd = xml.SelectSingleNode(string.Format("Tarih_Date/Currency[@Kod='{0}']/ForexSelling", "USD")).InnerText.Replace('.', ',');
                return new Currency() { CurrencyUnit = "USD", Quantity = Convert.ToDecimal(usd),Date=DateTime.Now };
            }
            catch (Exception)
            {
                return null;
            }

        }


    }
}
