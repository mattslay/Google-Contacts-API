<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="TutorialCode_GoogleContactAPI_google_contact_api" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Contacts API</title> 
</head>
<body>
    <h1>Google Contacts</h1>    
    <form id="form1" runat="server">
        <div class="content">
            <asp:Button ID="googleButton" Text="Get All Google Contacts" runat="server" OnClick="googleButton_Click" />
            <asp:Button ID="googleButtonNewContact" Text="Get from New account/ Sign in with Different account" runat="server" OnClick="googleButtonNewContact_Click" />
              <asp:GridView ID="contactGrid" runat="server" CellPadding="4" 
                                                ForeColor="#333333" GridLines="None" Width="100%">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
            <div id="dataDiv" runat="server"></div>
        </div>
    </form>
</body>
</html>
