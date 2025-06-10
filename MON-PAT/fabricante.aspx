<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="fabricante.aspx.cs" Inherits="MON_PAT.fabricante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <br />
    <h1 class="text-center">REGISTRO DEL FABRICANTE</h1>
    <div class="row">
        <div class="col-md-5">
            

            <h3><b>Gestión de fabricante</b></h3>
            <hr />
            <asp:Label ID="Label1" runat="server" Text="Nombre fabricante" BackColor="White"></asp:Label>
            <asp:TextBox ID="txt_Nombre" CssClass="form-control" placeholder="Ingrese el nombre" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Area en metros" BackColor="White" ></asp:Label>
            <asp:TextBox ID="txt_PaisOrigen" CssClass="form-control" placeholder="Ingrese pais" runat="server"></asp:TextBox>
            <br />
            
            

           
        </div>

       
    </div>
</asp:Content>
