
namespace Rajas.Persona.Web.MyBlood4You.Web.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Rajas.Persona.Entities;
    using Rajas.Persona.Web.MyBlood4You.Web.Models;

    public static class DataRepository
    {
        public static List<BloodBankModel> GetBloodBank(Expression<Func<BloodBankModel, bool>> expression)
        {
            using (PersonaEntities dataContext = new PersonaEntities())
            {
                return (from bank in dataContext.BloodBanks
                        select new BloodBankModel
                        {
                            BloodBankId = bank.BloodBankId,
                            BloodBankName = bank.BloodBankName,
                            Address = bank.Address,
                            TownId = bank.TownId,
                            Town = bank.Town_Lookup.TownValue,
                            DistrictId = bank.Town_Lookup.DistrictId,
                            District = bank.Town_Lookup.District_Lookup.DistrictValue,
                            StateId = bank.Town_Lookup.District_Lookup.StateID,
                            State = bank.Town_Lookup.District_Lookup.State_Lookup.StateValue,
                            CountryId = bank.Town_Lookup.District_Lookup.State_Lookup.CountryId,
                            Country = bank.Town_Lookup.District_Lookup.State_Lookup.Country_Lookup.CountryValue,
                            Pincode = bank.Pincode,
                            MobileNo1 = bank.MobileNo1,
                            MobileNo2 = bank.MobileNo2,
                            LandlineNo = bank.LandlineNo,
                            FaxNumber = bank.FaxNumber,
                            Email = bank.Email,
                            Website = bank.Website,
                            CreatedOn = bank.CreatedOn
                        }).Where(expression).ToList();
            }
        }
    }
}
