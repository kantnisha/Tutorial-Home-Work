
namespace Rajas.Persona.Web.MyBlood4You.Web.Controllers
{
    using System.Web.Mvc;
    using Rajas.Persona.Domain.Models;
    using Rajas.Persona.Domain.Utility;
    using Rajas.Persona.Web.MyBlood4You.Web.Models;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(@"~\views\home.cshtml");
        }

        public ActionResult SearchDonor()
        {
            UserModel userModel = new UserModel(true);
            userModel.SearchDonor(model => model.IsVolunteer == false);
            return View(@"~\Views\DonorList.cshtml", userModel);
        }

        [HttpPost]
        public ActionResult SearchDonor(UserModel userModel)
        {
            userModel.SearchDonor(model => model.IsVolunteer == false);
            userModel.LoadDropDowns();
            return View(@"~\Views\DonorList.cshtml", userModel);
        }

        public ActionResult RegisterDonor()
        {
            return View(@"~\Views\RegisterDonor.cshtml", new UserModel(true));
        }

        [HttpPost]
        public ActionResult RegisterDonor(UserModel newDonor)
        {
            if (ViewData.ModelState.IsValid)
            {
                newDonor.Save();
                newDonor.ProcessingStatus = true;
                return View(@"~\Views\RegisterDonor.cshtml", newDonor);
            }

            newDonor.LoadDropDowns();
            return View(@"~\Views\RegisterDonor.cshtml", newDonor);
        }

        public ActionResult SearchBloodBank()
        {
            BloodBankModel bloodBankModel = new BloodBankModel(true);
            bloodBankModel.Search();
            return View(@"~\Views\BloodBankList.cshtml", bloodBankModel);
        }

        [HttpPost]
        public ActionResult SearchBloodBank(BloodBankModel bloodBankModel)
        {
            bloodBankModel.Search();
            bloodBankModel.LoadDropDowns();
            return View(@"~\Views\BloodBankList.cshtml", new UserModel(true));
        }

        public ActionResult RegisterBloodBank()
        {
            return View(@"~\Views\RegisterBloodBank.cshtml", new BloodBankModel(true));
        }

        [HttpPost]
        public ActionResult RegisterBloodBank(BloodBankModel bloodBankModel)
        {
            bloodBankModel.Save();
            return RedirectToAction("MyBlood4YouHome");
        }

        public ActionResult SearchVolunteer()
        {
            UserModel volunteerModel = new UserModel(true);
            volunteerModel.SearchVolunteer(model => model.IsVolunteer == true);
            return View(@"~\Views\VolunteerList.cshtml", volunteerModel);
        }

        [HttpPost]
        public ActionResult SearchVolunteer(UserModel volunteerModel)
        {
            volunteerModel.SearchVolunteer(model => model.IsVolunteer == true);
            volunteerModel.LoadDropDowns();
            return View(@"~\Views\VolunteerList.cshtml", volunteerModel);
        }

        public ActionResult RegisterVolunteer()
        {
            return View(@"~\Views\RegisterVolunteer.cshtml", new UserModel(true));
        }

        [HttpPost]
        public ActionResult RegisterVolunteer(UserModel newDonor)
        {
            if (ViewData.ModelState.IsValid)
            {
                newDonor.IsVolunteer = true;
                newDonor.Save();
                newDonor.ProcessingStatus = true;
                return View(@"~\Views\RegisterVolunteer.cshtml", newDonor);
            }

            newDonor.LoadDropDowns();
            return View(@"~\Views\RegisterVolunteer.cshtml", newDonor);
        }

        public ActionResult WhoWeAre()
        {
            return View(@"~\Views\WhoWeAre.cshtml", new UserModel(true));
        }

        public ActionResult WhatWeDo()
        {
            return View(@"~\Views\WhatWeDo.cshtml", new UserModel(true));
        }

        public ActionResult BloodChart()
        {
            return View(@"~\Views\BloodCompatibilityChart.cshtml", new UserModel(true));
        }

        public ActionResult ContactUs()
        {
            return View(@"~\Views\ContactUs.cshtml");
        }
    }
}
