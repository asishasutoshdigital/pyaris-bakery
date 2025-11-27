using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

public partial class GenerateOutletBill : System.Web.UI.Page
{
    /// <summary>
    /// Handles the page load event and processes the query string parameters.
    /// If valid parameters are present (orderNo, outlet, transaction), it triggers the generation of a PDF.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.Count == 3)
            {
                var orderNo = Request.QueryString["orderNo"].ToString();
                var outlet = Request.QueryString["outlet"].ToString();
                var transaction = Request.QueryString["transaction"].ToString();
                if (orderNo != null && outlet != null && transaction != null)
                {
                    GeneratePdf(orderNo, outlet, transaction);
                }
                else
                {
                    cardText.InnerText = "The URL appears to be invalid !!";
                }
            }
            else
            {
                cardText.InnerText = "The URL appears to be invalid !!";
            }
        }
        catch(Exception ex)
        {
           cardText.InnerText = "Failed to download the Invoice. Please try again later.";
        }
    }
    /// <summary>
    /// Retrieves data from the API, converts the received memory stream to a PDF.
    /// </summary>
    /// <param name="orderNo"></param>
    /// <param name="outlet"></param>
    /// <param name="transaction"></param>
    private void GeneratePdf(string orderNo, string outlet, string transaction)
    {
        try
        {
            RestClient restClient = new RestClient();
            var req = new RestRequest(Program.ParisApiUrl, Method.Get);
            req.AddHeader("Key", Program.ParisApiKey);
            req.AddParameter("orderNo", orderNo);
            req.AddParameter("outlet", outlet);
            req.AddParameter("transaction", transaction);
            var res = restClient.ExecuteAsync(req).Result;
            if(res.StatusCode == HttpStatusCode.OK)
            {
                OutletBillResponse response = JsonConvert.DeserializeObject<OutletBillResponse>(res.Content.ToString());
                if (Convert.ToBoolean(response.Status))
                {
                    byte[] fileBytes = response.Data;
                    string filename = orderNo + ".pdf";
                    HttpContext.Current.Response.ContentType = "application/pdf";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", String.Format("attachment; filename={0}", filename));
                    HttpContext.Current.Response.BinaryWrite(fileBytes);
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    cardText.InnerText = response.ErrorMessage.ToString();
                }
            }
            else
            {
                cardText.InnerText = "Failed to download the Invoice. Please try again later.";
            }
        }
        catch
        {
            cardText.InnerText = "Failed to download the Invoice. Please try again later.";
        }

    }
}