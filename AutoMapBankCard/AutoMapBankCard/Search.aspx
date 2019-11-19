<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="AutoMapBankCard.Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bank Card List</title>
    <style>
        div {
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-size: 30px;"><b>Bank Card List</b></div>
        <div>
            <div>Total Records:<asp:Label ID="lbCount" runat="server"></asp:Label></div>
            <div>
                Page Size:
                <asp:DropDownList ID="ddlPageSize" runat="server" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="10" Value="10" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div>Page Index:
                <asp:DropDownList ID="ddlPageIndex" runat="server"></asp:DropDownList></div>
            <div>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            </div>
        </div>
        <div>
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
