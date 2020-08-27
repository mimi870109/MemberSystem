using Dapper;
using MemberSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace MemberSystem.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult selmember(string txtname)
        {

            string query = "SELECT * FROM member";
            SqlConnection db = new SqlConnection();
            db.ConnectionString = WebConfigurationManager.ConnectionStrings["membersorstem"].ConnectionString;
            var x = db.Query<sql>(query).ToList();
          // var y = db.Query(query).ToList();

            return PartialView("_pritalviews", x);
        }

        [HttpPost]
        public JsonResult insmember(string txtname, string gender,string txttel,string txtaddress)
        {

            try
            {
                //string inner = "INSERT INTO member (name,gender,tel,adress)" +
                //    "VALUES('" + txtname + "','" + gender + "','" + txttel + "','" + txtaddress + "')";
                //基本上用這個來控制資料
                using (SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["membersorstem"].ConnectionString))
                {

                    string strsql = "INSERT INTO member (name,gender,tel,adress) VALUES(@txtname,@gender,@txttel,@txtaddress);";

                    Member ins = new Member
                    {
                        txtname = txtname,
                        gender = gender,
                        txttel = txttel,
                        txtaddress = txtaddress
                    };
                    //dynamic ins = new[] { @txtname = txtname, @gender = gender, @txttel = txttel, @txtaddress = txtaddress };
                    conn.Execute(strsql, ins);
                    return Json("會員新增成功");
                }
            }
            catch (Exception)
            {
                return Json("會員新增失敗");
                throw;
            }
            
           // string query = "select * from member";
           // SqlConnection db = new SqlConnection();
           // db.ConnectionString = WebConfigurationManager.ConnectionStrings["membersorstem"].ConnectionString;
           //var x = db.Query(inner).ToList();
      
      
        }
        [HttpGet]
        public ActionResult delmember(string id)
        {
            try
            {
                string delete = "DELETE FROM member " +
                    "WHERE id ='" + id + "'";
                SqlConnection db = new SqlConnection();
                db.ConnectionString = WebConfigurationManager.ConnectionStrings["membersorstem"].ConnectionString;
                var x = db.Query(delete).ToList();




                return View();
            }
            catch (Exception)
            {
                return Json("會員刪除失敗");
                throw;
            }
        }
        [HttpGet]
        public ActionResult editmember(int id)
        {

            //string delete = "UPDATE member" +
            //    "WHERE id ='" + id + "'";
            //SqlConnection db = new SqlConnection();
            //db.ConnectionString = WebConfigurationManager.ConnectionStrings["membersorstem"].ConnectionString;
            //var x = db.Query(delete).ToList();

            //var httpContext = new AuthenticationContext();
            try
            {
                var txtname = Request.QueryString["txtname"];
                var txttel = Request.QueryString["txttel"];
                var txtaddress = Request.QueryString["txtaddress"];
                Member ins = new Member
                {
                    id = id,
                    txtname = txtname,
                    txttel = txttel,
                    txtaddress = txtaddress
                };

                return View("../Member/edit", ins);
            }
            catch (Exception)
            {
                return Json("會員修改失敗");
                throw;
            }
        }
        [HttpPost]
        public JsonResult saveeditmember(int id,string txtname,string txttel,string txtaddress)
        {
            try
            {
                string edit = "UPDATE member " +
                    "SET name='" + txtname + "',tel='" + txttel + "',adress='" + txtaddress + "'" +
                    "WHERE id =" + id + "";
                SqlConnection db = new SqlConnection();
                db.ConnectionString = WebConfigurationManager.ConnectionStrings["membersorstem"].ConnectionString;
                var x = db.Query(edit).ToList();

                return Json("編輯成功");
            }
            catch (Exception)
            {
                return Json("會員修改失敗");
                throw;
            }
        }
    }
}