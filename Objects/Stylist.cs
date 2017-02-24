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

        
    } //end class
}
