
namespace Rajas.Persona.Web.MyBlood4You.Web.ModelBinder
{
    using System;
    using System.Web.Mvc;
    using Rajas.Persona.Domain.Models;
    using Rajas.Persona.Helper.Utility;

    public class DonorModelBinder : IModelBinder
    {
        public enum FormElement
        {
            UserId,
            WebSiteId,
            FirstName,
            MiddelName,
            LastName,
            GenderId,
            DayOfBirth,
            MonthOfBirth,
            YearOfBirth,
            Address,
            PinCode,
            CountryId,
            StateId,
            DistrictId,
            TownId,
            BloodGroupId,
            WeightId,
            MobileNumber1,
            MobileNumber2,
            LandlineNumber,
            FaxNumber,
            Email,
            Webpage,
            UserName,
            Password,
            SecurityQuestionId,
            SecurityQuestionAns,
            RoleId,
            Active,
            IsVolunteer,
            LastUpdatedOn,
            LandlineAreaCode,
            checkbox
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            bindingContext.ModelState.Clear();

            var userId = Parse.ToInt(request.Form.Get(FormElement.UserId.ToString()), 0);
            var webSiteId = Parse.ToInt(request.Form.Get(FormElement.WebSiteId.ToString()), 0);
            var firstName = request.Form.Get(FormElement.FirstName.ToString());
            var lastName = request.Form.Get(FormElement.LastName.ToString());
            var genderId = Parse.ToInt(request.Form.Get(FormElement.GenderId.ToString()), null);
            var dayOfBirth = Parse.ToInt(request.Form.Get(FormElement.DayOfBirth.ToString()), 0);
            var monthOfBirth = Parse.ToInt(request.Form.Get(FormElement.MonthOfBirth.ToString()), 0);
            var yearOfBirth = Parse.ToInt(request.Form.Get(FormElement.YearOfBirth.ToString()), 0);
            var address = request.Form.Get(FormElement.Address.ToString());
            var pinCode = request.Form.Get(FormElement.PinCode.ToString());
            var countryId = Parse.ToInt(request.Form.Get(FormElement.CountryId.ToString()), 0);
            var stateId = Parse.ToInt(request.Form.Get(FormElement.StateId.ToString()), 0);
            var districtId = Parse.ToInt(request.Form.Get(FormElement.DistrictId.ToString()), 0);
            var townId = Parse.ToInt(request.Form.Get(FormElement.TownId.ToString()), 0);
            var bloodGroupId = Parse.ToInt(request.Form.Get(FormElement.BloodGroupId.ToString()), null);
            var weightId = Parse.ToInt(request.Form.Get(FormElement.WeightId.ToString()), null);
            var mobileNumber1 = request.Form.Get(FormElement.MobileNumber1.ToString());
            var mobileNumber2 = request.Form.Get(FormElement.MobileNumber2.ToString());
            var landlineAreaCode = request.Form.Get(FormElement.LandlineAreaCode.ToString());
            var landlineNumber = request.Form.Get(FormElement.LandlineNumber.ToString());
            var email = request.Form.Get(FormElement.Email.ToString());
            var agreed = request.Form.Get(FormElement.checkbox.ToString()) == "checkbox" ? true : false;

            if (string.IsNullOrWhiteSpace(firstName))
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "First Name is required.");
            }

            if (string.IsNullOrWhiteSpace(lastName))
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Last Name is required.");
            }

            if (genderId == null)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Gender is required.");
            }

            var dateOfBirthRaw = string.Format("{0}/{1}/{2}", monthOfBirth.Value, dayOfBirth, yearOfBirth);
            var dateOfBirth = DateTime.MinValue;
            if (DateTime.TryParse(dateOfBirthRaw, out dateOfBirth) && dateOfBirth > DateTime.Now.AddYears(-17))
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Date of Birth is not valid.");
            }

            if (bloodGroupId == null || bloodGroupId == 0)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Blood Group is required");
            }

            if (weightId == null || weightId == 0)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Weight is required");
            }

            if (string.IsNullOrWhiteSpace(mobileNumber1))
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Mobile Number is required");
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Address is required");
            }

            if (countryId == null || countryId == 0)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Country is required.");
            }

            if (stateId == null || stateId == 0)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "State is required");
            }

            if (districtId == null || districtId == 0)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "District is required");
            }

            if (townId == null || townId == 0)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Town is required");
            }

            if (!agreed)
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Please check the check box if you have checked your eligibility and you are willing to donate your blood.");
            }

            UserModel userModel = new UserModel
            {
                UserId = userId != null ? userId.Value : 0,
                WebSiteId = webSiteId != null ? webSiteId.Value : 0,
                FirstName = firstName,
                LastName = lastName,
                GenderId = genderId,
                DayOfBirth = dayOfBirth != null ? dayOfBirth.Value : 0,
                MonthOfBirth = monthOfBirth != null ? monthOfBirth.Value : 0,
                YearOfBirth = yearOfBirth != null ? yearOfBirth.Value : 0,
                DateOfBirth = dateOfBirth,
                LandlineAreaCode = landlineAreaCode,
                Address = address,
                PinCode = pinCode,
                CountryId = countryId != null ? countryId.Value : 0,
                StateId = stateId != null ? stateId.Value : 0,
                DistrictId = districtId != null ? districtId.Value : 0,
                TownId = townId,
                BloodGroupId = bloodGroupId != null ? bloodGroupId.Value : 0,
                WeightId = weightId,
                MobileNumber1 = !string.IsNullOrWhiteSpace(mobileNumber1) ? mobileNumber1 : string.Empty,
                MobileNumber2 = !string.IsNullOrWhiteSpace(mobileNumber2) ? mobileNumber2 : string.Empty,
                LandlineNumber = !string.IsNullOrWhiteSpace(landlineNumber) ? landlineNumber : string.Empty,
                Email = email,
            };

            userModel.LoadDropDowns();
            return userModel;
        }
    }
}