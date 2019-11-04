<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="AutoMapBankCard.Upload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:FileUpload ID="fuData" runat="server" />
            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
            <asp:Label ID="lbMsg" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
