<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="AutoMapBankCard.Page.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page</title>
    <script>
        window.onload = function () {
            var iframe = document.getElementById("ifMain");
            var width = (window.innerWidth - 20) + "px";
            var height = (window.innerHeight - iframe.offsetTop - 15) + "px";
            iframe.style.width = width;
            iframe.style.height = height;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:RadioButtonList ID="rblOption" runat="server" OnSelectedIndexChanged="rblOption_SelectedIndexChanged" RepeatDirection="Horizontal" AutoPostBack="true">
                <asp:ListItem Text="Check Bank Card" Value="1" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Upload Sample Data" Value="2"></asp:ListItem>
                <asp:ListItem Text="Search Data" Value="3"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div>
            <iframe id="ifMain" runat="server" style="border: 0px; overflow: scroll" src="~/CheckBankCard"></iframe>
        </div>
    </form>
</body>
</html>
