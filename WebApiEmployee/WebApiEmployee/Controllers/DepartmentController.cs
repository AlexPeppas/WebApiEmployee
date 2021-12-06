using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Mvc;
using WebApiEmployee.Models;
using System.Web.Routing;
using System.Web.UI.WebControls;



namespace WebApiEmployee.Controllers
{
    public class DepartmentController : ApiController
    {
        public HttpResponseMessage Get() {
            string query = @"select * from dbo.Department";
            DataTable tableOutput = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Employee"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(tableOutput);

            }
            return Request.CreateResponse(HttpStatusCode.OK, tableOutput);
        }
        [System.Web.Http.Route("api/Department/MultipleDeps")]
        [System.Web.Http.HttpPost]
        public string Post(List<DepartmentModel> deps)
        {
            int counter = 0;
            foreach (var item in deps)
            {
                try
                {
                    string query = @"insert into dbo.Department(DepartmentId, DepartmentName) 
                                values('" + item.DepartmentId + @"','" + item.DepartmentName + @"')
                                ";

                    DataTable tableOutput = new DataTable();
                    using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Employee"].ConnectionString))
                    using (var cmd = new SqlCommand(query, con))
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.Text;
                        da.Fill(tableOutput);
                    }
                    counter++;

                }
                catch
                {
                    continue;

                }
            }
            return "Successfully added " + counter + " items of the list"; 
        }
        public string Post(DepartmentModel dep)
        {
            try {
                string query = @"insert into dbo.Department(DepartmentId, DepartmentName) 
                                values('" + dep.DepartmentId + @"','" + dep.DepartmentName + @"')
                                ";

                DataTable tableOutput = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Employee"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    try {
                        da.Fill(tableOutput);
                    }
                    catch (Exception ex)
                    {
                        return ex.ToString();
                    }

                }
                return "Added Succesfully";

            }
            catch {
                return "Failed to Add";

            }
        }
        public string Put(DepartmentModel dep)
        {
            try
            {
                string query = @"update dbo.Department
                               set DepartmentName = ' " + dep.DepartmentName + @" ' 
                                where DepartmentId = ' " + dep.DepartmentId + @" '";
                DataTable tableOutput = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Employee"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(tableOutput);
                }
                return "Updated Successfully";
            }
            catch
            {
                return "Did not update Successfully";
            }
        }
        public string Delete(int id )
        {
            try
            {
                string query = @"delete from dbo.Department where 
                                DepartmentId = '" + id + @"' ";
                DataTable outputTable = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Employee"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(outputTable);
                }
                return "Deleted Successfully";
            }
            catch 
            {
                return "Did not Delete Successfully";
            }
        }
    }
}
