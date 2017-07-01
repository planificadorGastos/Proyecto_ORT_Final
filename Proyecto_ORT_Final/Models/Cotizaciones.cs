using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace Proyecto_ORT_Final.Models
{
    public class Cotizaciones
    {
        public static void Main(string[] args)
        {
            try
            {
                var url = "https://web-services.oanda.com/rates/api/v1/currencies.json";
                var request = (HttpWebRequest)WebRequest.Create(url);

                string json = "";
                string credentialHeader = String.Format("Bearer <MOFcZGbPJVPLNITBSjcnydND>");
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers.Add("Authorization", credentialHeader);

                HttpWebResponse webresponse = (HttpWebResponse)request.GetResponse();

                var sw = new StreamReader(webresponse.GetResponseStream(), System.Text.Encoding.ASCII);
                json = sw.ReadToEnd();
                sw.Close();

                var response = (new JavaScriptSerializer()).Deserialize<Dictionary<string, List<object>>>(json);

                foreach (Dictionary<string, object> currency in response["currencies"])
                {
                    Console.WriteLine("{0} : {1}", currency["code"], currency["description"]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}