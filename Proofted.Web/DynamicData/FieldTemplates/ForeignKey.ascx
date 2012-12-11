<%@ Control Language="C#" CodeBehind="ForeignKey.ascx.cs" Inherits="Proofted.Web.DynamicData.FieldTemplates.ForeignKeyField" %>

<asp:HyperLink ID="HyperLink1" runat="server"
    Text="<%# GetDisplayString() %>"
    NavigateUrl="<%# GetNavigateUrl() %>"  />

