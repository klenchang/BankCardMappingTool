<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="AutoMapBankCard.Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error</title>
    <style>
        div {
            text-align: center;
            margin-top: 10px;
        }
    </style>
    <script type="text/javascript">
        function Back() {
            window.location.href = "\Index";
        }
        window.onload = function () {
            if (self != top) {
                document.getElementById("btnBack").remove();
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-size: 143px; margin-top: 40px;"><b>Error!!!!</b></div>
        <div>
            <img src="Content/original.gif" />
        </div>
        <div style="color: red; font-size: 25px;">
            <label>Error:</label><asp:Label ID="lbErrorMsg" runat="server"></asp:Label>
        </div>
        <div>
            <input id="btnBack" type="button" onclick="Back();" value="Back to Home Page" />
        </div>
    </form>
</body>
</html>
