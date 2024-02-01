using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                LoadRecord();
            }

        }
        void clear()
        {

            txt2.Text = "";
            DropDownList1.SelectedIndex = -1;
            txt3.Text = "";
            txt4.Text = "";
            txt5.Text = "";
        }
        SqlConnection con = new SqlConnection(@"Data Source=HARI\MSSQLSERVER04;Initial Catalog=stu_management;Integrated Security=True");
        //string connection = ConfigurationManager.ConnectionStrings["DefaultConn"].ConnectionString;
        protected void Button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand comm = new SqlCommand("insert into stu_info values('" + txt2.Text + "','" + DropDownList1.SelectedValue + "','" + txt3.Text + "','" + txt4.Text + "','" + txt5.Text + "')", con);
            comm.ExecuteNonQuery();
            con.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Inserted');", true);
            LoadRecord();
            clear();
        }
        protected void LoadRecord()
        {

            SqlCommand comm = new SqlCommand("select * from stu_info", con);
            SqlDataAdapter da = new SqlDataAdapter(comm);
            DataTable dt = new DataTable();
            da.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();


        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            txt2.Text = "";
            DropDownList1.ClearSelection();
            txt3.Text = "";
            txt4.Text = "";
            txt5.Text = "";
        }



        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;

            LoadRecord();


        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            string name = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
            string department = ((DropDownList)GridView1.Rows[e.RowIndex].FindControl("DropDownList2")).SelectedValue;
            string mail = ((TextBox)GridView1.Rows[e.RowIndex].Cells[3].Controls[0]).Text;
            string mobile = ((TextBox)GridView1.Rows[e.RowIndex].Cells[4].Controls[0]).Text;
            string address = ((TextBox)GridView1.Rows[e.RowIndex].Cells[5].Controls[0]).Text;


            con.Open();
            SqlCommand comm = new SqlCommand("Update stu_info set s_name = '" + name + "',dep_name = '" + department + "',mail= '" + mail + "',mobile = '" + mobile + "',s_address ='" + address + "' where s_id= '" + id + "'", con);
            comm.ExecuteNonQuery();
            GridView1.EditIndex = -1;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully Updated');", true);
            LoadRecord();
            con.Close();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            LoadRecord();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value.ToString());
            con.Open();
            SqlCommand comm = new SqlCommand("delete stu_info where s_id= '" + id + "'", con);
            comm.ExecuteNonQuery();
            con.Close();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Successfully deleted');", true);
            GridView1.EditIndex = -1;
            LoadRecord();

        }
    
    }
}