
namespace Rajas.Persona.Web.MyBlood4You.Web.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;
    using Rajas.Persona.Domain.Utility;
    using Rajas.Persona.Entities;
    using Rajas.Persona.Web.MyBlood4You.Web.Utility;

    public class BloodBankModel : BloodBank
    {
        [Display(Name = "Blood Bank Id")]
        public override int BloodBankId { get; set; }

        [Display(Name = "Blood Bank Name")]
        public override string BloodBankName { get; set; }

        [Display(Name = "Address")]
        public override string Address { get; set; }

        public override int TownId { get; set; }

        [Display(Name = "District")]
        public int DistrictId { get; set; }

        [Display(Name = "State")]
        public int StateId { get; set; }

        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Display(Name = "Town")]
        public string Town { get; set; }

        [Display(Name = "District")]
        public string District { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Pincode")]
        public override string Pincode { get; set; }

        [Display(Name = "Mobile No 1")]
        public override string MobileNo1 { get; set; }

        [Display(Name = "Mobile No 2")]
        public override string MobileNo2 { get; set; }

        [Display(Name = "Landline No")]
        public override string LandlineNo { get; set; }

        [Display(Name = "Fax Number")]
        public override string FaxNumber { get; set; }

        [Display(Name = "Email")]
        public override string Email { get; set; }

        [Display(Name = "WebSite")]
        public override string Website { get; set; }

        [Display(Name = "WebSite ID")]
        public override System.DateTime CreatedOn { get; set; }

        public List<SelectListItem> CountrySource { get; set; }

        public List<SelectListItem> StateSource { get; set; }

        public List<SelectListItem> DistrictSource { get; set; }

        public List<SelectListItem> TownSource { get; set; }

        public IEnumerable<BloodBankModel> BloodBankList { get; set; }

        public BloodBankModel()
        {
        }

        public BloodBankModel(bool loadDropDown)
        {
            if (loadDropDown)
            {
                LoadDropDowns();
            }
        }

        public BloodBankModel(int bloodBankModelId)
        {
            this.BloodBankId = bloodBankModelId;
            this.Initialize(true);
        }

        private void Initialize(bool loadDropDown)
        {
            if (this.BloodBankId > 0)
            {
                BloodBankModel bloodBankModel = Rajas.Persona.Web.MyBlood4You.Web.Utility.DataRepository.GetBloodBank(w => w.BloodBankId == this.BloodBankId).SingleOrDefault();
                if (bloodBankModel != null)
                {
                    BloodBankId = bloodBankModel.BloodBankId;
                    BloodBankName = bloodBankModel.BloodBankName;
                    Address = bloodBankModel.Address;
                    TownId = bloodBankModel.TownId;
                    Town = bloodBankModel.Town;
                    District = bloodBankModel.District;
                    DistrictId = bloodBankModel.DistrictId;
                    State = bloodBankModel.State;
                    StateId = bloodBankModel.StateId;
                    Country = bloodBankModel.Country;
                    CountryId = bloodBankModel.CountryId;
                    Pincode = bloodBankModel.Pincode;
                    MobileNo1 = bloodBankModel.MobileNo1;
                    MobileNo2 = bloodBankModel.MobileNo2;
                    LandlineNo = bloodBankModel.LandlineNo;
                    FaxNumber = bloodBankModel.FaxNumber;
                    Email = bloodBankModel.Email;
                    Website = bloodBankModel.Website;
                    CreatedOn = bloodBankModel.CreatedOn;
                }
            }

            if (loadDropDown)
            {
                LoadDropDowns();
            }
        }

        public void LoadDropDowns()
        {
            this.CountryId = 101;
            this.CountrySource = DropDownSource.CountrySource();
            this.StateSource = DropDownSource.StateSource(this.CountryId);
            this.DistrictSource = DropDownSource.DistrictSource(this.StateId);
            this.TownSource = DropDownSource.TownSource(this.DistrictId);

            this.CountrySource.InsertFirstItem("-- Select Country--");
            this.StateSource.InsertFirstItem("-- Select State --");
            this.DistrictSource.InsertFirstItem("-- Select District --");
            this.TownSource.InsertFirstItem("-- Select Town --");
        }

        public void Save()
        {
            using (PersonaEntities dataContext = new PersonaEntities())
            {
                BloodBank bloodBank = null;
                if (this.BloodBankId == 0)
                {
                    bloodBank = new BloodBank
                    {
                        BloodBankId = this.BloodBankId,
                        BloodBankName = this.BloodBankName,
                        Address = this.Address,
                        TownId = this.TownId,
                        Pincode = this.Pincode,
                        MobileNo1 = this.MobileNo1,
                        MobileNo2 = this.MobileNo2,
                        LandlineNo = this.LandlineNo,
                        FaxNumber = this.FaxNumber,
                        Email = this.Email,
                        Website = this.Website,
                        CreatedOn = this.CreatedOn
                    };

                    dataContext.BloodBanks.AddObject(bloodBank);
                }
                else
                {
                    bloodBank = (from blank in dataContext.BloodBanks where blank.BloodBankId == this.BloodBankId select blank).SingleOrDefault();
                    bloodBank.BloodBankId = this.BloodBankId;
                    bloodBank.BloodBankName = this.BloodBankName;
                    bloodBank.Address = this.Address;
                    bloodBank.TownId = this.TownId;
                    bloodBank.Pincode = this.Pincode;
                    bloodBank.MobileNo1 = this.MobileNo1;
                    bloodBank.MobileNo2 = this.MobileNo2;
                    bloodBank.LandlineNo = this.LandlineNo;
                    bloodBank.FaxNumber = this.FaxNumber;
                    bloodBank.Email = this.Email;
                    bloodBank.Website = this.Website;
                    bloodBank.CreatedOn = this.CreatedOn;

                    dataContext.BloodBanks.ApplyCurrentValues(bloodBank);
                }

                dataContext.SaveChanges();
            }
        }

        internal void Search()
        {
            this.BloodBankList = Rajas.Persona.Web.MyBlood4You.Web.Utility.DataRepository.GetBloodBank(u =>
                    u.CountryId == (this.CountryId != 0 ? this.CountryId : u.CountryId) &&
                    u.StateId == (this.StateId != 0 ? this.StateId : u.StateId) &&
                    u.DistrictId == (this.DistrictId != 0 ? this.DistrictId : u.DistrictId) &&
                    u.TownId == (this.TownId != 0 ? this.TownId : u.TownId))
                    .OrderByDescending(u => u.CreatedOn).Take(10);
        }
    }
}
