using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace HairSalonApp.Objects
{
    public class Client
    {
        private int _id;
        public string _name;
        public int _stylistId;

        public Client(string name, int stylistId, int id = 0)
        {
            _id = id;
            _name = name;
            _stylistId = stylistId;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetId()
        {
            return _id;
        }

        public int GetStylistId()
        {
            return _stylistId;
        }

        public override bool Equals(System.Object otherClient)
        {
          if (!(otherClient is Client))
          {
            return false;
          }
          else
          {
            Client newClient = (Client) otherClient;
            bool idEquality = (this.GetId() == newClient.GetId());
            bool nameEquality = (this.GetName() == newClient.GetName());
            bool stylistIdEquality = (this.GetStylistId() == newClient.GetStylistId());
            return (idEquality && nameEquality && stylistIdEquality);
          }
        }

        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM clients;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int clientId = rdr.GetInt32(0);
                string clientName = rdr.GetString(1);
                int clientStylistId = rdr.GetInt32(2);

                Client retrievedClient = new Client(clientName, clientStylistId, clientId);
                allClients.Add(retrievedClient);
            }
            if(rdr != null)
            {
              rdr.Close();
            }
            if(conn != null)
            {
              conn.Close();
            }

            return allClients;
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Save()
        {
          SqlConnection conn = DB.Connection();
          conn.Open();

          SqlCommand cmd = new SqlCommand("INSERT INTO clients(name, stylist_id) OUTPUT INSERTED.id VALUES(@ClientName, @Stylist_Id);", conn);

          SqlParameter nameParameter = new SqlParameter("@ClientName", this.GetName());
          SqlParameter stylistIdParameter = new SqlParameter("@Stylist_Id", this.GetStylistId());

          cmd.Parameters.Add(nameParameter);
          cmd.Parameters.Add(stylistIdParameter);
          SqlDataReader rdr = cmd.ExecuteReader();

          while(rdr.Read())
          {
            this._id = rdr.GetInt32(0);
          }
          if (rdr != null)
          {
            rdr.Close();
          }
          if(conn != null)
          {
            conn.Close();
          }
        } // end save

        public static Client Find(int id)
        {
          SqlConnection conn = DB.Connection();
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id = @Client_Id;", conn);

          SqlParameter idParameter = new SqlParameter("@Client_Id", id);
          cmd.Parameters.Add(idParameter);

          SqlDataReader rdr = cmd.ExecuteReader();

          int foundClientId = 0;
          string foundClientName = null;
          int foundStylistId = 0;

          while(rdr.Read())
          {
            foundClientId = rdr.GetInt32(0);
            foundClientName = rdr.GetString(1);
            foundStylistId = rdr.GetInt32(2);
          }

          Client foundClient = new Client(foundClientName, foundStylistId, foundClientId);
          if (rdr != null)
          {
            rdr.Close();
          }
          if (conn != null)
          {
            conn.Close();
          }

          return foundClient;
        }

        public void Delete()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM clients WHERE id= @Client_Id;",conn);

            SqlParameter clientId = new SqlParameter("@Client_Id", this.GetId());
            cmd.Parameters.Add(clientId);

            cmd.ExecuteNonQuery();

            if (conn != null)
            {
                conn.Close();
            }

        }

        public void Update(string newName)
         {
           SqlConnection conn = DB.Connection();
           conn.Open();

           SqlCommand cmd = new SqlCommand("UPDATE clients SET name = @NewName OUTPUT INSERTED.name WHERE id = @ClientId;", conn);

           SqlParameter newNameParameter = new SqlParameter("@NewName", newName);
           cmd.Parameters.Add(newNameParameter);


           SqlParameter clientIdParameter = new SqlParameter();
           clientIdParameter.ParameterName = "@ClientId";
           clientIdParameter.Value = this.GetId();
           cmd.Parameters.Add(clientIdParameter);
           SqlDataReader rdr = cmd.ExecuteReader();

           while(rdr.Read())
           {
             this._name = rdr.GetString(0);
           }

           if (rdr != null)
           {
             rdr.Close();
           }

           if (conn != null)
           {
             conn.Close();
           }
         }

    }
}
