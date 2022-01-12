using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using CrudOperationMVC.Models;
using System.Web.Mvc;

namespace CrudOperationMVC.Controllers
{
    public class DemoController : Controller
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        void mycon()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");
            con.Open();
        }


        // GET: Demo
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(RegistrationModel model)
        {

            cmd = new SqlCommand("Insert into Registration Values(@name,@email,@password,@Gender,@dob,@img,@rdate)",con);
            cmd.Parameters.AddWithValue("@name",model.name);
            cmd.Parameters.AddWithValue("@email", model.Email);
            cmd.Parameters.AddWithValue("@password", model.Password);
            cmd.Parameters.AddWithValue("@gender", model.Gender);
            cmd.Parameters.AddWithValue("@dob", model.Date+"/"+model.Month+"/"+model.Year);
            string path="";
            if (model.img.ContentLength>0)
            {
                model.img.SaveAs(Server.MapPath("~/Content/Image/" + model.img.FileName));
                path = "~/ Content / Image / " + model.img.FileName;
            }
            cmd.Parameters.AddWithValue("@img", path);
            cmd.Parameters.AddWithValue("@rdate", model.Rdate=System.DateTime.Now.ToString());
            cmd.ExecuteNonQuery();


            return View("Create");
        }




    }
}