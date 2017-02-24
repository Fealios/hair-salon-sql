using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using HairSalonApp.Objects;

namespace HairSalonApp
{
    public class StylistTest : IDisposable
    {
        public StylistTest()
        {
          DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void StylistHasNameEntered_true()
        {
            Stylist newStylist = new Stylist("Melvin", "phone#");

            Assert.Equal("Melvin", newStylist.GetName());
        }

        [Fact]
        public void StylistHasPhoneEntered_true()
        {
            Stylist newStylist = new Stylist("Melvin", "phone#");

            Assert.Equal("phone#", newStylist.GetPhone());
        }

        [Fact]
        public void Save_SavesEnteredStylist_true()
        {
            Stylist newStylist = new Stylist("Melvin", "phone#");

            List<Stylist> backendList = new List<Stylist>{newStylist};
            newStylist.Save();
            List<Stylist> sqlList = Stylist.GetAll();


            Assert.Equal(backendList, sqlList);
        }

        public void Dispose()
        {
            Stylist.DeleteAll();
        }
    }
}
