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

        public void Dispose()
        {
            //
        }
    }
}
