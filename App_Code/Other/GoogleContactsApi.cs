using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GoogleContactsApi
/// </summary>
public class GoogleContactsApi
{
    public GoogleContactsApi()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}
public class GooglePlusAccessToken
{
    public string access_token { get; set; }
    public string token_type { get; set; }
    public int expires_in { get; set; }
    public string refresh_token { get; set; }
}
//public class GoogleUserOutputData
//{
//    public string id { get; set; }
//    public string name { get; set; }
//    public string given_name { get; set; }
//    public string email { get; set; }
//    public string picture { get; set; }
//}