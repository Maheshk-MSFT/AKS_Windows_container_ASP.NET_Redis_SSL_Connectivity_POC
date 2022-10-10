<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  
    CodeBehind="Default.aspx.cs" Inherits="mikkyredis._Default" Async="true" EnableViewState="true"  %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>ASP.NET&nbsp; - Mikky Redis Issue repro - Oct/7</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>

    </div>

    <div class="row">
        <div class="col-md-4">
              <asp:Button ID="Button1" runat="server"  Height="40px" OnClick="Button1_Click" Text="Test Redis connection" Width="526px" Font-Bold="False" Font-Names="Segoe UI" Font-Size="Large" ForeColor="#3366FF" />
        </div>

    </div>

</asp:Content>
