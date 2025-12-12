using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OutletBillResponse
/// </summary>
public class OutletBillResponse
{
    public byte[] Data { get; set; }
    public string ErrorMessage { get; set; }
    public bool Status { get; set; }
}