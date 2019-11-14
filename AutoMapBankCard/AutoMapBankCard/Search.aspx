<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="AutoMapBankCard.Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            <asp:GridView ID="gvShow" runat="server" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvShow_PageIndexChanging" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="AccountName" HeaderText="Ac. Name" />
                    <asp:BoundField DataField="AccountNumber" HeaderText="Ac. Number" />
                    <asp:BoundField DataField="IssuingBankAddress" HeaderText="Issuing Bank Address" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
