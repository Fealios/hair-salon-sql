using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace HairSalonApp.Objects
{
    public class Stylist
    {
        private int _id;
        private string _name;
        private string _phone;

        public Stylist(string name, string phone, int id = 0)
        {
            _id = id;
            _name = name;
            _phone = phone;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetPhone()
        {
            return _phone;
        }

        public override bool Equals(System.Object otherStylist)
        {
          if (!(otherStylist is Stylist))
          {
            return false;
          }
          else
          {
            Stylist newStylist = (Stylist) otherStylist;
            bool idEquality = (this.GetId() == newStylist.GetId());
            bool nameEquality = (this.GetName() == newStylist.GetName());
            bool phoneEquality = (this.GetPhone() == newStylist.GetPhone());
            return (idEquality && nameEquality && phoneEquality);
          }
        }

        public static List<Stylist> GetAll()
        {
            List<Stylist> allStylists = new List<Stylist>{};

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                int stylistId = rdr.GetInt32(0);
                string stylistName = rdr.GetString(1);
                string stylistPhone = rdr.GetString(2);

                Stylist retrievedStylist = new Stylist(stylistName, stylistPhone, stylistId);
                allStylists.Add(retrievedStylist);
            }
            if(rdr != null)
            {
              rdr.Close();
            }
            if(conn != null)
            {
              conn.Close();
            }

            return allStylists;
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM stylists;", conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO stylists(name, number) OUTPUT INSERTED.id VALUES (@StylistName, @StylistNumber);", conn);

            SqlParameter nameParameter = new SqlParameter("@StylistName", this.GetName());
            SqlParameter phoneParameter = new SqlParameter("@StylistNumber", this.GetPhone());

            cmd.Parameters.Add(nameParameter);
            cmd.Parameters.Add(phoneParameter);

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
        }

        public static Stylist Find(int id)
        {
          SqlConnection conn = DB.Connection();
          conn.Open();
          SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE id = @Stylist_Id;", conn);

          SqlParameter stylistIdParameter = new SqlParameter("@Stylist_Id", id);
          cmd.Parameters.Add(stylistIdParameter);

          SqlDataReader rdr = cmd.ExecuteReader();

          int foundStylistId = 0;
          string foundStylistName = null;
          string foundStylistPhone = null;

          while(rdr.Read())
          {
            foundStylistId = rdr.GetInt32(0);
            foundStylistName = rdr.GetString(1);
            foundStylistPhone = rdr.GetString(2);
          }

          Stylist foundStylist = new Stylist(foundStylistName, foundStylistPhone, foundStylistId);
          if (rdr != null)
          {
            rdr.Close();
          }
          if (conn != null)
          {
            conn.Close();
          }

          return foundStylist;

        }








    } //end class
}
