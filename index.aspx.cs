using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using Google.GData.Client;
using Google.Contacts;
using Google.GData.Contacts;
using Google.GData.Extensions;
using System.Data;
using System.Configuration;

public partial class TutorialCode_GoogleContactAPI_google_contact_api : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["code"] != null)
            {
               
                    ListAllContacts();
                
            }
        }
    }
    protected void googleButton_Click(object sender, EventArgs e)
    {
        
        if (Session["serStatus"] != null)
        {
            GoogleContactService objService1 = new GoogleContactService();
            List<ContactDetail> contactDetails = objService1.GetContacts((GooglePlusAccessToken)Session["serStatus"]);
            BindDataToDrid(contactDetails);
        }
        else
        {
            RedirectTOGoogle();
        }
    }
    protected void googleButtonNewContact_Click(object sender, EventArgs e)
    {
        Session["serStatus"] = null;
        RedirectTOGoogle();
    }
    public void RedirectTOGoogle()
    {
        string clientId = ConfigurationManager.AppSettings["google_client_id"].ToString();
        string redirectUrl = ConfigurationManager.AppSettings["google_redirect_url"].ToString();
        Response.Redirect("https://accounts.google.com/o/oauth2/auth?redirect_uri=" + redirectUrl + "&response_type=code&client_id=" + clientId + "&scope=https://www.google.com/m8/feeds/&approval_prompt=force&access_type=offline");
    }
    public void BindDataToDrid(List<ContactDetail> contactDetails)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(new DataColumn("Name"));
        dt.Columns.Add(new DataColumn("Email"));
        dt.Columns.Add(new DataColumn("Secondry Email"));
        dt.Columns.Add(new DataColumn("Phone"));
        dt.Columns.Add(new DataColumn("Address"));
        // dt.Columns.Add(new DataColumn("Details"));

        foreach (ContactDetail contact1 in contactDetails)
        {
            dt.Rows.Add(contact1.Name,
                contact1.EmailAddress1,
                contact1.EmailAddress2,
                contact1.Phone,
                contact1.Address);
        }

        contactGrid.DataSource = dt;
        contactGrid.DataBind();
    }
    public void ListAllContacts()
    {
        string code = Request.QueryString["code"];
        string google_client_id = ConfigurationManager.AppSettings["google_client_id"].ToString();
        string google_client_sceret = ConfigurationManager.AppSettings["google_client_sceret"].ToString();
        string google_redirect_url = ConfigurationManager.AppSettings["google_redirect_url"].ToString();
        /*Get Access Token and Refresh Token*/
        GoogleContactService objService = new GoogleContactService();
        GooglePlusAccessToken serStatus = objService.getAccessToken(code, google_client_id, google_client_sceret, google_redirect_url);
        /*End*/
        List<ContactDetail> contactDetails = objService.GetContacts(serStatus);
        BindDataToDrid(contactDetails);

    }
   

}