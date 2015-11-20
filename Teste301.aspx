<%@ Page Language="C#" %>
<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Status = "301 Moved Permanently";
        Response.AddHeader("Location", "http://www.boletoasp.com.br");
    }
    
</script>