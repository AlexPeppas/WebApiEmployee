using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.UI.WebControls;
using WebApiEmployee.Models;

namespace WebApiEmployee.Controllers
{
    public class EmployeeController : ApiController
    {
        public HttpResponseMessage Get() 
        {
            try
            {
                string query = @" select EmployeeName,Department from dbo.Employees";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Employee"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return Request.CreateResponse(HttpStatusCode.OK, table);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        public string Post(EmployeeModel emp)
        {
            try
            {
                string query = @"insert into dbo.Employees
                                (EmployeeId,EmployeeName,Department,DateOfJoining,PhotoFileName)
                                values ('"+emp.EmployeeId+@"',
                                        '"+emp.EmployeeName+ @"',
                                        '" + emp.Department + @"',
                                        '" + emp.DateOfJoining + @"',
                                        '" + emp.PhotoFileName + @"')";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Employee"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Added Successfully";
            }
            catch
            {
                return "Failed to Add values";
            }
        }
        public string Put(EmployeeModel emp)
        {
            try
            {
                string query = @"update dbo.Employees 
                                set Department = '" + emp.EmployeeName + @"',DateOfJoining='" + emp.DateOfJoining + @"'
                                where EmployeeId='"+emp.EmployeeId+@"'";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Employee"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Updated Successfully";
            }
            catch
            {
                return "Failed to Update values";
            }
        }
        public string Delete(int id)
        {
            try
            {
                string query = @"delete from dbo.Employees where EmployeeId='"+id+@"'";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Employee"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Deleted Successfully";
            }
            catch
            {
                return "Failed to Delete entry";
            }
        }
        [Route("api/Employee/GetAllEmployees")]
        [HttpGet]
        public HttpResponseMessage GetAllEmployees() 
        {
            try
            {
                string query = @"select * from dbo.Employees";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Employee"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return Request.CreateResponse(HttpStatusCode.OK,table);
            }
            catch
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
        [Route("api/Employee/SaveFile")]
        public string SaveFile()
        {
            try 
            {
                var httpRequest = HttpContext.Current.Request;

                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + fileName);
                postedFile.SaveAs(physicalPath);

                var postedFile2 = httpRequest.Files[1];
                string fileName2 = postedFile2.FileName;
                var physicalPath2 = HttpContext.Current.Server.MapPath("~/Photos/" + fileName2);
                postedFile2.SaveAs(physicalPath2);
                
                return ("This is the fileName : " + fileName + " The fileName2 : " + fileName2);
            }
            catch
            {
                return ("Anonymous .png");
            }
        }
        [Route("api/Employee/FetchEveryoneButTheNewBies")]
        [HttpGet]
        public HttpResponseMessage FetchEveryoneButTheNewBies() 
        {
            try
            {
                
                string Threshold = "2021-01-01";
                string query = @"select * from dbo.Employees 
                                where DateOfJoining<'" + Threshold + @"'";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["Employee"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return Request.CreateResponse(HttpStatusCode.OK, table);
            }
            catch (Exception Ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, Ex);
            }
        }
    }
}
