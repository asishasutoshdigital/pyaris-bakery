using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

public partial class sh : System.Web.UI.Page
{
    /// <summary>
    /// Handles the page load event and processes the query string parameters.
    /// If valid parameters are present (orderNo, outlet, transaction), it triggers the generation of a PDF.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Page_Load(object sender, EventArgs e)
    {
        loader.Style["display"] = "flex";
        string code = Page.RouteData.Values.ContainsKey("code") ? Page.RouteData.Values["code"].ToString() : "";
        bool invalidCode = true;
        if (!string.IsNullOrEmpty(code))
        {
            invalidCode= false;
            string original_url = string.Empty;
            var cn = new SqlConnection(Program.Connection);
            try
            {
                cn.Open();
                var cmd = new SqlCommand("Select [Original Url] from [UrlShortner] where [code]='" + code + "'", cn);
                var getUrl = cmd.ExecuteReader();
                if (getUrl.HasRows)
                {
                    if (getUrl.Read())
                    {
                        original_url = getUrl[0].ToString();
                    }
                }

                if (original_url != "")
                {
                    Response.Redirect(original_url);
                }
                else
                {
                    invalidCode= true;
                }

                cn.Close();
            }

            catch (Exception ex)
            {
                cardtext.InnerHtml = ex.Message;
            }
        }
        if(invalidCode)
        {
            loader.Style["display"] = "none";
            cardtext.InnerText = "The URL appears to be invalid !!";
        }
    }
}