<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="fabricante.aspx.cs" Inherits="Fabrica.fabricante" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h1 class="text-center">FABRICANTES</h1>
    
    <div class="row">
        <div class="col-md-5">
            
            <h3><b>Nuevo Fabricante</b></h3>
            <hr />
            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Nombre fabricante" BackColor="White"></asp:Label>
                <asp:TextBox ID="txt_Nombre" CssClass="form-control" placeholder="Ingrese el nombre" runat="server"></asp:TextBox>
                <%-- Campo Obligatorio: Nombre fabricante --%>
                <asp:RequiredFieldValidator ID="reqNombre" runat="server" ControlToValidate="txt_Nombre"
                    ErrorMessage="El nombre del fabricante es obligatorio." ForeColor="Red" Display="Dynamic" ValidationGroup="NuevoFabricante"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <asp:Label ID="Label2" runat="server" Text="País Origen" BackColor="White" ></asp:Label>
                <asp:TextBox ID="txt_PaisOrigen" CssClass="form-control" placeholder="Ingrese país" runat="server"></asp:TextBox>
                <%-- Campo Obligatorio: País Origen --%>
                <asp:RequiredFieldValidator ID="reqPais" runat="server" ControlToValidate="txt_PaisOrigen"
                    ErrorMessage="El país de origen es obligatorio." ForeColor="Red" Display="Dynamic" ValidationGroup="NuevoFabricante"></asp:RequiredFieldValidator>
            </div>
            <asp:Button ID="btn_guardar" CssClass="btn btn-success mt-2" runat="server" Text="Guardar" OnClick="btn_guardar_Click" ValidationGroup="NuevoFabricante" />
            
            <br /><br />

            <h3><b>Ingresa Id para Buscar</b></h3>
            <hr />
            <div class="form-group">
                <asp:TextBox ID="txt_buscar" runat="server" CssClass="form-control" placeholder="Ingrese ID del fabricante a buscar"></asp:TextBox>
                <%-- Campo Obligatorio: ID Buscar --%>
                <asp:RequiredFieldValidator ID="reqBuscar" runat="server" ControlToValidate="txt_buscar"
                    ErrorMessage="El ID para buscar es obligatorio." ForeColor="Red" Display="Dynamic" ValidationGroup="BuscarFabricante"></asp:RequiredFieldValidator>
            </div>
            <asp:Button ID="btn_buscar" runat="server" Text="Buscar fabricante" CssClass="btn btn-info mt-2" OnClick="btn_buscar_Click" ValidationGroup="BuscarFabricante" />
            
            <br /><br />

            <h3><b>Seleccione el ID para Modificar</b></h3>
            <hr />
            <div class="form-group">
                <asp:Label ID="Label5" runat="server" Text="ID Fabricante a modificar" BackColor="White"></asp:Label>
                <asp:TextBox ID="txt_IdFabricante" CssClass="form-control" placeholder="Ingrese el ID a modificar" runat="server"></asp:TextBox>
                <%-- Campo Obligatorio: ID Fabricante a modificar --%>
                 <asp:RequiredFieldValidator ID="reqIdModificar" runat="server" ControlToValidate="txt_IdFabricante"
                    ErrorMessage="El ID a modificar es obligatorio." ForeColor="Red" Display="Dynamic" ValidationGroup="ModificarFabricante"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <asp:Label ID="Label_ModificarNombre" runat="server" Text="Nuevo Nombre" BackColor="White"></asp:Label>
                <asp:TextBox ID="txt_ModificarNombre" CssClass="form-control" placeholder="Ingrese el nuevo nombre" runat="server"></asp:TextBox>
                <%-- Campo Obligatorio: Nuevo Nombre (Modificar) --%>
                <asp:RequiredFieldValidator ID="reqModNombre" runat="server" ControlToValidate="txt_ModificarNombre"
                    ErrorMessage="El nuevo nombre es obligatorio." ForeColor="Red" Display="Dynamic" ValidationGroup="ModificarFabricante"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <asp:Label ID="Label_ModificarPais" runat="server" Text="Nuevo País Origen" BackColor="White"></asp:Label>
                <asp:TextBox ID="txt_ModificarPaisOrigen" CssClass="form-control" placeholder="Ingrese el nuevo país" runat="server"></asp:TextBox>
                <%-- Campo Obligatorio: Nuevo País Origen (Modificar) --%>
                <asp:RequiredFieldValidator ID="reqModPais" runat="server" ControlToValidate="txt_ModificarPaisOrigen"
                    ErrorMessage="El nuevo país de origen es obligatorio." ForeColor="Red" Display="Dynamic" ValidationGroup="ModificarFabricante"></asp:RequiredFieldValidator>
            </div>
            <asp:Button ID="btn_modificar" CssClass="btn btn-primary mt-2" runat="server" Text="Modificar" OnClick="btn_modificar_Click" ValidationGroup="ModificarFabricante" />
            
            <br /><br />

            <h3><b>Ingrese ID para Eliminar</b></h3>
            <hr />
            <div class="form-group">
                <asp:TextBox ID="txt_eliminar" CssClass="form-control" placeholder="Ingrese el ID de fabricante a eliminar" runat="server"></asp:TextBox>
                <%-- Campo Obligatorio: ID Eliminar --%>
                <asp:RequiredFieldValidator ID="reqEliminar" runat="server" ControlToValidate="txt_eliminar"
                    ErrorMessage="El ID para eliminar es obligatorio." ForeColor="Red" Display="Dynamic" ValidationGroup="EliminarFabricante"></asp:RequiredFieldValidator>
            </div>
            <asp:Button ID="Button1" runat="server" Text="Eliminar" CssClass="btn btn-danger mt-2" OnClick="btn_eliminar_Click" ValidationGroup="EliminarFabricante" />
            
            <br /><br />

            <%-- Resumen de Validación General --%>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Por favor, corrija los siguientes errores:"
                ShowErrors="true" ShowMessageBox="true" ForeColor="Red" DisplayMode="BulletList" />

            <asp:Label ID="lbl_mensajes" runat="server" Text="" ForeColor="Green"></asp:Label>
            
        </div>
        <div class="col-md-7">
            <h3><b>Listado de Fabricantes</b></h3>
            <hr />
            <asp:GridView ID="grid_fabricantes" runat="server" AutoGenerateColumns="False" Width="100%" BorderStyle="Solid" BorderWidth="1px"
                DataKeyNames="IdFabricante" OnSelectedIndexChanged="grid_fabricantes_SelectedIndexChanged" AutoGenerateSelectButton="True"
                CssClass="table table-striped table-hover">
                <Columns>
                    <asp:BoundField DataField="IdFabricante" HeaderText="ID" />
                    <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="PaisOrigen" HeaderText="País de Origen" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>