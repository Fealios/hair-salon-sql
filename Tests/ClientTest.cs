using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;
using HairSalonApp.Objects;

namespace HairSalonApp
{
    public class ClientTest : IDisposable
    {
        public ClientTest()
        {
          DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=hair_salon_test;Integrated Security=SSPI;";
        }

        [Fact]
        public void ClientHasNameEntered_true()
        {
            Client newClient = new Client("Melvin", 1);

            Assert.Equal("Melvin", newClient.GetName());
        }

        [Fact]
        public void ClientHasStylistIdEntered_true()
        {
            Client newClient = new Client("Melvin", 1);

            Assert.Equal(1, newClient.GetStylistId());
        }

        [Fact]
        public void Save_SavesEnteredClient_true()
        {
            Client newClient = new Client("Melvin", 1);

            List<Client> backendList = new List<Client>{newClient};
            newClient.Save();
            List<Client> sqlList = Client.GetAll();


            Assert.Equal(backendList, sqlList);
        }

        [Fact]
        public void Find_ReturnClientById_true()
        {
            Client newClient = new Client("Melvin", 1);
            newClient.Save();

            int id = newClient.GetId();

            Client foundClient = Client.Find(id);

            Assert.Equal(newClient, foundClient);
        }

        [Fact]
        public void Delete_DeleteClientByIdFromSql_true()
        {
            Client newClient = new Client("Melvin", 1);

            List<Client> beforeDelete = Client.GetAll();
            newClient.Save();
            newClient.Delete();
            List<Client> afterDelete = Client.GetAll();

            Assert.Equal(beforeDelete, afterDelete);
        }

        [Fact]
        public void Update_ChangeClientNameToNewInput_true()
        {
            Client newClient = new Client("Melvin", 1);
            newClient.Save();

            newClient.Update("John");

            Assert.Equal("John", newClient.GetName());
        }

//end tests --------------------------------------------
        public void Dispose()
        {
            Client.DeleteAll();
        }
    }
}
