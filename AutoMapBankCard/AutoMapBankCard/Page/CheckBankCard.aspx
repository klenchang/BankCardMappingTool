<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckBankCard.aspx.cs" Inherits="AutoMapBankCard.Page.CheckBankCard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Check Bank Card</title>
    <style>
        div {
            margin-top: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="font-size: 30px;"><b>Please enter the text that you want to verify.</b></div>
        <div>
            <div style="display: inline-block;">
                <div>
                    <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Height="300px" Width="300px" Style="resize: none;"></asp:TextBox>
                </div>
                <div>
                    <asp:Button ID="btnCheck" runat="server" Text="Check" OnClick="btnCheck_Click" />
                </div>
                <div>
                    <asp:Label ID="lbMsg" runat="server" Style="color: forestgreen;"></asp:Label>
                </div>
            </div>
            <div style="display: inline-block; position: absolute;">
                <pre>
    Example:
    hosts.
          4 9830278  黄    南宁市医科大支行,
          2 2609678  农    南宁鲁班支行,
          2 1092977  彭    永州零陵支行营业部,
          1 9043433  宋    深圳国贸支行,
          1 4043971  李   广东省深圳市华联支行,
          1 1097877  尹    永州零陵支行营业部,
    Email

    <label style="color:red;">* The text must start from "hosts." and end with "Email".</label>
                </pre>
            </div>
        </div>
    </form>
</body>
</html>
