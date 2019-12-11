<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="AutoMapBankCard.Page.Upload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Upload</title>
    <style>
        div {
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="font-size: 30px"><b>Please upload excel file</b></div>
            <div>
                <asp:FileUpload ID="fuData" runat="server" />
                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                <a href="Content/BankCardListSample.xlsx"><b>Download Sample</b></a>
            </div>
        </div>
        <div>
            <asp:Label ID="lbMsg" runat="server" Style="color: red;"></asp:Label>
        </div>
    </form>
</body>
</html>
