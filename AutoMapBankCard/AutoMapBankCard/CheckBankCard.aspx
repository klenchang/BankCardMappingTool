<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckBankCard.aspx.cs" Inherits="AutoMapBankCard.CheckBankCard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 30%; display: inline-block;">
            <div>
                <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Height="300px" Width="500px" Style="resize: none;"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnCheck" runat="server" Text="Check" OnClick="btnCheck_Click" />
            </div>
        </div>
        <div style="width: 69%; display: inline-block;">
            <asp:Label ID="lbMsg" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
