using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{

    private SqlConnection gsqlConn = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Label1.Text = string.Empty;
        try
        {
            if (gsqlConn.State == ConnectionState.Closed)
            {
                gsqlConn.ConnectionString = @"Data Source=localhost;Initial Catalog=ASPState;Integrated Security=True;";
                gsqlConn.Open();


                if (gsqlConn.State == ConnectionState.Open)
                {
                    Label1.Text = "Connection Established";

                    gsqlConn.Close();
                }
            }
        }
        catch (Exception ex)
        {
            Label1.Text = "Connection Failed";
        }
    }


    private DataSet getActionItems(string strKeyID)
    {
        DataSet dsSource = new DataSet();
        SqlCommand sqlCmd = new SqlCommand();

        sqlCmd.Connection = gsqlConn;

        sqlCmd.CommandText = "SELECT TOP 1000 [Id],[Name],[PublishingUserName],[PublishingPassword],[EncryptedUserName],[UserNameHash] FROM [Hosting].[admin].[Users]";
        sqlCmd.CommandType = CommandType.Text;

        SqlDataAdapter sqlDA = new SqlDataAdapter(sqlCmd);

        try
        {
            sqlDA.Fill(dsSource);
        }
        catch (Exception ex)
        {
            if (gsqlConn.State == ConnectionState.Open)
                gsqlConn.Close();
            throw (new ArgumentException(ex.Message));
        }
        return dsSource;
    }

}