using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace Testing.Module.BusinessObjects
{
    [DefaultClassOptions]
    public class User : BaseObject
    {


        public User(Session session)
            : base(session)
        {


        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        private String userName;

        public String UserName
        {
            get
            {
                return userName;
            }
            set
            {
                SetPropertyValue("UserName", ref userName, value);
            }
        }


        

        [Association("User-Receipts")]
        public XPCollection<Receipts> Receipts
        {
            get
            {
                return GetCollection<Receipts>("Receipts");
            }
        }
        

        
        

        
    }
}