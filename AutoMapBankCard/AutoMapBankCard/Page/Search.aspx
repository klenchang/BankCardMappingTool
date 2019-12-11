<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="AutoMapBankCard.Page.Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bank Card List</title>
    <style>
        div {
            margin-top: 10px;
        }

            div > span:first-child {
                margin-right: 50px;
            }

            div > span > span {
                margin-right: 10px;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-size: 30px;"><b>Bank Card List</b></div>
        <div>
            <span>Total Records:<asp:Label ID="lbCount" runat="server"></asp:Label></span>
            <span>
                <span>Page Size:
                <asp:DropDownList ID="ddlPageSize" runat="server" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                    <asp:ListItem Text="10" Value="10" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="20" Value="20"></asp:ListItem>
                    <asp:ListItem Text="50" Value="50"></asp:ListItem>
                </asp:DropDownList>
                </span>
                <span>Page Index:
                <asp:DropDownList ID="ddlPageIndex" runat="server" OnSelectedIndexChanged="ddlPageIndex_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </span>
               <%-- <span>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                </span>--%>
            </span>
        </div>
        <div>
            <asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="false" CellPadding="5" RowStyle-HorizontalAlign="Center" HeaderStyle-BackColor="#ffcc00">
                <Columns>
                    <asp:BoundField DataField="SerialNo" />
                    <asp:BoundField DataField="AccountName" HeaderText="Ac. Name" />
                    <asp:BoundField DataField="AccountNumber" HeaderText="Ac. Number" />
                    <asp:BoundField DataField="IssuingBankAddress" HeaderText="Issuing Bank Address" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
