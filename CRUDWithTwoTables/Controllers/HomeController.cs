using CRUDWithTwoTables.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Configuration;
using System.IO;
using System.Data.Entity.Infrastructure;

namespace CRUDWithTwoTables.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        ExampleDbModel db = new ExampleDbModel();
        public ActionResult Index(int Page = 1,int PageSize=10)
        {

            List<UserDetail> USER = db.UserDetails.ToList();
            List<CarDetail> Car = db.CarDetails.ToList();
            List<MultipleClass> result = new List<MultipleClass>();

            foreach (var user in USER)
            {
                var data = new MultipleClass
                {
                    UserId = user.UserId,
                    FullName = user.FullName,
                    UserEmail = user.UserEmail,
                    CivilIdNumber = user.CivilIdNumber,
                    
                    
                };

                //var multipletable = from c in USER
                //                    join st in Car on c.UserId equals st.Id

                //                    select new MultipleClass { UserDetails = c, CarDetails = st };



                var cardetails = string.Join(",", Car.Where(x => x.UserId == user.UserId).Select(y => y.CarLicense).ToList());
                result.Add(data);
                data.CarLicense = cardetails;
                

               
            }

            //ViewBag.TotalPages = Math.Ceiling(result.Count() / 20.0);
            //result = result.Skip((PageNumber - 1) * 20).Take(20).ToList();
            PagedList<MultipleClass> result1 = new PagedList<MultipleClass>(result, Page, PageSize);
            return View(result1);
        }


            public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateTable viewModel)
        {
            string fileName = Path.GetFileNameWithoutExtension(viewModel.ImageFile.FileName);
            string extension = Path.GetExtension(viewModel.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + "-" + fileName.Trim() + extension;
            string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();
            viewModel.ProfilePic = UploadPath + fileName;
            viewModel.ImageFile.SaveAs(viewModel.ProfilePic);


            var user = new UserDetail()
            {
                FullName = viewModel.FullName,
                UserEmail = viewModel.UserEmail,
                PasswordHash = viewModel.PasswordHash,
                CivilIdNumber = viewModel.CivilIdNumber,
                DOB = viewModel.DOB,
                MobileNo = viewModel.MobileNo,
                Address = viewModel.Address,
                RoleId = viewModel.RoleId,
                ProfilePic = viewModel.ProfilePic,
                CreatedDate = viewModel.CreatedDate,
                ModifiedDate = viewModel.ModifiedDate,
                IsNotificationActive = viewModel.IsNotificationActive,
                IsActive = viewModel.IsActive,
                DeviceId = viewModel.DeviceId,
                DeviceType = viewModel.DeviceType,
                FcmToken = viewModel.FcmToken,
                verify = viewModel.verify,
                VerifiedBy = viewModel.VerifiedBy,
                Area = viewModel.Area,
                Block = viewModel.Block,
                Street = viewModel.Street,
                Housing = viewModel.Housing,
                Floor = viewModel.Floor,
                NewPass = viewModel.NewPass,
                ConPass = viewModel.ConPass,
                Jadda = viewModel.Jadda,
                Reason = viewModel.Reason,
                ActivatedBy = viewModel.ActivatedBy,
                ActivatedDate = viewModel.ActivatedDate

            };

            var car = new CarDetail()
            {
                CarLicense = viewModel.CarLicense

            };
            db.UserDetails.Add(user);
            db.CarDetails.Add(car);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }


        public ActionResult Edit(int? id)
        {

            var viewModel = (from a in db.UserDetails
                             join c in db.CarDetails on a.UserId equals c.Id
                             where a.UserId == id
                             select new Models.MultipleClass
                             {
                                 UserId = a.UserId,
                                 FullName = a.FullName,
                                 UserEmail = a.UserEmail,
                                 CivilIdNumber = a.CivilIdNumber,
                                 CarLicense = c.CarLicense
                             }).FirstOrDefault();
            return View(viewModel);

        }


        [HttpPost]
        public ActionResult Edit(MultipleClass insert)
        {
            var newuserdetail = db.UserDetails.Where(x => x.UserId == insert.UserId).FirstOrDefault();
            var newcardetail = db.CarDetails.Where(x => x.UserId == insert.UserId).FirstOrDefault();


            newuserdetail.UserId = insert.UserId;
            newuserdetail.FullName = insert.FullName;
            newuserdetail.UserEmail = insert.UserEmail;
            newuserdetail.CivilIdNumber = insert.CivilIdNumber;
            newcardetail.CarLicense = insert.CarLicense;

            db.Entry(newuserdetail).State = EntityState.Modified;
            db.Entry(newcardetail).State = EntityState.Modified;
            db.SaveChanges();
            return View();
        }




    }
}

 