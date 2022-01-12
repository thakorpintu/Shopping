using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CrudOperationMVC.Models;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace CrudOperationMVC.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        void mycon()
        {
            con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True");
            con.Open();
        }


        List<RegistrationModel> FillGride()
        {
            List<RegistrationModel> tbl = new List<RegistrationModel>();
            mycon();
            cmd = new SqlCommand("Select * FROM Registration", con);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                RegistrationModel model = new RegistrationModel();

                model.Id = Convert.ToInt32(dr["Id"].ToString());
                model.name = dr["name"].ToString();
                model.Email = dr["Email"].ToString();
                model.Password = dr["Password"].ToString();
                model.Gender = dr["Gender"].ToString();
                model.Dob = dr["Dob"].ToString();
                model.showimg = dr["Img"].ToString();
                model.Rdate = dr["Rdare"].ToString();

                tbl.Add(model);
            }
            ViewData["filldata"] = tbl;
            return (tbl);

        }

        public ActionResult Index(int id = 0)
        {
            FillGride();
            RegistrationModel rmd = new RegistrationModel();

            if (id > 0)
            {
                mycon();
                cmd = new SqlCommand("Select * from Registration where Id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    rmd.name = dr["name"].ToString();
                    rmd.Email = dr["Email"].ToString();
                    rmd.Password = dr["Password"].ToString();
                    rmd.Gender = dr["Gender"].ToString();
                    string[] dob = dr["Dob"].ToString().Split('/');
                    rmd.Date = dob[0];
                    rmd.Month = dob[1];
                    rmd.Year = dob[2];
                   
       
                    rmd.showimg = dr["Img"].ToString();
                    ViewBag.showimg = dr["Img"].ToString();

                    rmd.Rdate = dr["Rdare"].ToString();


                }
            }
            return View(rmd);

        }

        public ActionResult Delete(int id=0)
        {
            mycon();
            cmd = new SqlCommand("Delete from Registration where Id=@id", con);
            cmd.Parameters.AddWithValue("@id",id);
            cmd.ExecuteNonQuery();
            con.Close();
            ViewBag.msg = "Succefully Delete Data...";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Index(RegistrationModel Model)
        {
            if (Model.Id > 0)
            {
                mycon();
                cmd = new SqlCommand("Update Registration set name=@name,Email=@email,Password=@password,Gender=@gender,Dob=@dob,Img=@img where Id=@id", con);
                cmd.Parameters.AddWithValue("@name", Model.name);
                cmd.Parameters.AddWithValue("@email", Model.Email);
                cmd.Parameters.AddWithValue("@password", Model.Password);
                cmd.Parameters.AddWithValue("@gender", Model.Gender);
                cmd.Parameters.AddWithValue("@dob", Model.Date + "/" + Model.Month + "/" + Model.Year);
                string Path = "";

                if (Model.img.ContentLength > 0)
                {
                    Model.img.SaveAs(Server.MapPath("~/Content/Image/" + Model.img.FileName));
                    Path = "~/Content/Image/" + Model.img.FileName;
                }
                
                cmd.Parameters.AddWithValue("@img",Path);
                cmd.Parameters.AddWithValue("@id", Model.Id);
                cmd.ExecuteNonQuery();
                ViewBag.msg = "Suceefully Data Update....";
                con.Close();
                FillGride();
                return RedirectToAction("Index");
            }
            else
            {
                
	        mycon();
                cmd = new SqlCommand("Insert into Registration Values(@name,@email,@password,@gender,@dob,@img,@rdate)", con);
                cmd.Parameters.AddWithValue("@name", Model.name);
                cmd.Parameters.AddWithValue("@email", Model.Email);
                cmd.Parameters.AddWithValue("@password", Model.Password);
                cmd.Parameters.AddWithValue("@gender", Model.Gender);
                cmd.Parameters.AddWithValue("@dob", Model.Date + "/" + Model.Month + "/" + Model.Year);
                string Path = "";
                if (Model.img.ContentLength > 0)
                {
                    Model.img.SaveAs(Server.MapPath("~/Content/Image/" + Model.img.FileName));
                    Path = "~/Content/Image/" + Model.img.FileName;
                }
                cmd.Parameters.AddWithValue("img", Model.showimg = Path);
                cmd.Parameters.AddWithValue("@rdate", System.DateTime.Now.ToString());

                cmd.ExecuteNonQuery();
                ViewBag.msg = "Suceefully Save Date....";
                FillGride();
            }
            return View();
        }

    }
}