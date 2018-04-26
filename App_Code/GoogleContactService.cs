using Google.Contacts;
using Google.GData.Client;
using Google.GData.Contacts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for GoogleContactService
/// </summary>
public class GoogleContactService
{
    public GoogleContactService()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public GooglePlusAccessToken getAccessToken(string code, string google_client_id,string google_client_sceret, string google_redirect_url)
    {
        /*Get Access Token and Refresh Token*/
        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
        webRequest.Method = "POST";
        string parameters = "code=" + code + "&client_id=" + google_client_id + "&client_secret=" + google_client_sceret + "&redirect_uri=" + google_redirect_url + "&grant_type=authorization_code";
        byte[] byteArray = Encoding.UTF8.GetBytes(parameters);
        webRequest.ContentType = "application/x-www-form-urlencoded";
        webRequest.ContentLength = byteArray.Length;
        Stream postStream = webRequest.GetRequestStream();
        // Add the post data to the web request
        postStream.Write(byteArray, 0, byteArray.Length);
        postStream.Close();
        WebResponse response = webRequest.GetResponse();
        postStream = response.GetResponseStream();
        StreamReader reader = new StreamReader(postStream);
        string responseFromServer = reader.ReadToEnd();
        GooglePlusAccessToken serStatus = JsonConvert.DeserializeObject<GooglePlusAccessToken>(responseFromServer);
        HttpContext.Current.Session["serStatus"] = serStatus;
        return serStatus;
    }
    public List<ContactDetail> GetContacts(GooglePlusAccessToken serStatus)
    {
        string refreshToken = serStatus.refresh_token;
        string accessToken = serStatus.access_token;
        string scopes = "https://www.google.com/m8/feeds/contacts/default/full/";
        OAuth2Parameters oAuthparameters = new OAuth2Parameters()
        {
            RedirectUri = ConfigurationManager.AppSettings["google_redirect_url"].ToString(),
            Scope = scopes,
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };


        RequestSettings settings = new RequestSettings("<var>" + ConfigurationManager.AppSettings["google_app_name"].ToString() + "</var>", oAuthparameters);
        ContactsRequest cr = new ContactsRequest(settings);
        ContactsQuery query = new ContactsQuery(ContactsQuery.CreateContactsUri("default"));
        query.NumberToRetrieve = 5000;
        Feed<Contact> feed = cr.Get<Contact>(query);

        StringBuilder sb = new StringBuilder();
        int i = 1;
        List<ContactDetail> contactDetails = new List<ContactDetail>();
        foreach (Contact entry in feed.Entries)
        {
            ContactDetail contact = new
            ContactDetail
            {
                Name = entry.Title.ToString(),
                EmailAddress1 = entry.Emails.Count >= 1 ? entry.Emails[0].Address : "",
                EmailAddress2 = entry.Emails.Count >= 2 ? entry.Emails[1].Address : "",
                Phone = entry.Phonenumbers.Count >= 1 ? entry.Phonenumbers[0].Value : "",
                Address = entry.PostalAddresses.Count >= 1 ? entry.PostalAddresses[0].FormattedAddress : ""
                //Details = entry.Content.Content
            };

            contactDetails.Add(contact);
        }
        /*End*/
        return contactDetails;
    }
   
}
public class ContactDetail
{
    public string Name { get; set; }
    public string EmailAddress1 { get; set; }
    public string EmailAddress2 { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Details { get; set; }
}