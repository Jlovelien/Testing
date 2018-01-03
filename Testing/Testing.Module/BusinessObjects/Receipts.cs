using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Drawing;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Validation;

namespace Testing.Module.BusinessObjects
{
    [DefaultClassOptions]
    //[DefaultProperty("RetailName")]
    public class Receipts : BaseObject
    {


        string description;

        double receiptTotal;
        double tax;
        DateTime receiptDate;
        private ReceiptType receiptType;
        string retailName;
        DateTime todayDate;




        private User user;

        public Receipts(Session session)
            : base(session)
        {
        }

        private void getData()
        {
            String code = "";
            try
            {
                string dbConn = "Data Source=(local);Initial Catalog=CreditCards;Persist Security Info=false;User ID=ccweb;Password=ccweb123";
                SqlConnection conn = new SqlConnection(dbConn);
                SqlCommand xSqlCommand = new SqlCommand("sp_cc_get_codes_all", conn);
                SqlDataReader dReader = default(SqlDataReader);
                xSqlCommand.CommandType = CommandType.StoredProcedure;
                xSqlCommand.Connection.Open();
                dReader = xSqlCommand.ExecuteReader();
                while (dReader.Read())
                {
                    if (dReader.HasRows)
                    {
                        code = dReader["account_description"].ToString();
                    }
                }
                conn.Close();
                conn.Dispose();
                xSqlCommand.Dispose();
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.todayDate = DateTime.Now.Date;

        }

        //private Job job;
        //[DataSourceProperty("AvailableJob")]
        //public Job Job
        //{

        //    get
        //    {
        //        return job;
        //    }
        //    set
        //    {
        //        SetPropertyValue("Job", ref job, value);
        //    }
        //}

        //private Category category;
        //[DataSourceProperty("AvailableCategory")]
        //public Category Category
        //{

        //    get
        //    {
        //        return category;
        //    }
        //    set
        //    {
        //        SetPropertyValue("Category", ref category, value);
        //    }
        //}


        //[DataSourceProperty("AvailableCodes")]

        //public Code Code
        //{
        //    get
        //    {
        //        return code;
        //    }
        //    set
        //    {
        //        SetPropertyValue("Code", ref code, value);
        //    }
        //}

        [Size(SizeAttribute.Unlimited)]
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                SetPropertyValue("Description", ref description, value);
            }
        }

        //[ImageEditor(DetailViewImageEditorMode = ImageEditorMode.PictureEdit,
        //DetailViewImageEditorFixedWidth = 500)]

        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PopupPictureEdit, DetailViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorFixedWidth = 350)]
        public byte[] Photo
        {
            get
            {
                return GetPropertyValue<byte[]>("Photo");
            }
            set
            {
                SetPropertyValue<byte[]>("Photo", value);
            }
        }

        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PopupPictureEdit, DetailViewImageEditorMode = ImageEditorMode.PopupPictureEdit)]
        public byte[] PhotoResize
        {
            get
            {
                return GetPropertyValue<byte[]>("PhotoResize");
            }
            set
            {
                SetPropertyValue<byte[]>("PhotoResize", value);
            }
        }

        double subTotal;
        public double SubTotal
        {
            get
            {
                return subTotal;
            }
            set
            {
                SetPropertyValue("SubTotal", ref subTotal, value);
            }
        }

        string ocrText;
        [Size(SizeAttribute.Unlimited)]
        public string OcrText
        {
            get
            {
                return ocrText;
            }
            set
            {
                SetPropertyValue("OcrText", ref ocrText, value);
            }
        }


        private FileData file;
        [Aggregated, ExpandObjectMembers(ExpandObjectMembers.Never)]
        public FileData File
        {
            get { return file; }
            set
            {
                SetPropertyValue("File", ref file, value);
            }
        }
        //private double rate;
        //public double Rate
        //{
        //    get
        //    {
        //        return rate;
        //    }
        //    set
        //    {
        //        if (SetPropertyValue("Rate", ref rate, value))
        //            OnChanged("Amount");
        //    }
        //}

        //[Persistent("Amount")]
        //private double amount;
        //[PersistentAlias("amount")]
        //public double Amount
        //{
        //    get
        //    {
        //        if (!IsLoading)
        //        {
        //            amount = Rate * SubTotal;
        //        }
        //        return amount;
        //    }
        //}

        //private double subTotal;
        //public double SubTotal
        //{
        //    get
        //    {
        //        return subTotal;
        //    }
        //    set
        //    {
        //        if (SetPropertyValue("SubTotal", ref subTotal, value))
        //            OnChanged("Amount");
        //    }
        //}

        [RuleRequiredField("RuleRequiredField for Receipts.ReceiptTotal", DefaultContexts.Save,
       "A total must be specified")]

        //[RuleRegularExpression("", DefaultContexts.Save, @"?:\d *\.)?\d+")]

        public double ReceiptTotal
        {
            get
            {
                return receiptTotal;
            }
            set
            {
                SetPropertyValue("ReceiptTotal", ref receiptTotal, value);
            }
        }

        public double Tax
        {
            get
            {
                return tax;
            }
            set
            {
                SetPropertyValue("Tax", ref tax, value);
            }
        }

        [RuleRequiredField("RuleRequiredField for Receipts.ReceiptDate", DefaultContexts.Save,
      "A date must be specified")]
        public DateTime ReceiptDate
        {
            get
            {
                return receiptDate;
            }
            set
            {
                SetPropertyValue("ReceiptDate", ref receiptDate, value);
            }
        }
        public ReceiptType ReceiptType1
        {
            get
            {
                return receiptType;
            }
            set
            {
                SetPropertyValue("ReceiptType", ref receiptType, value);
            }
        }

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string RetailName
        {
            get
            {
                return retailName;
            }
            set
            {
                SetPropertyValue("RetailName", ref retailName, value);
            }
        }
        public DateTime TodayDate
        {
            get
            {
                return todayDate;
            }
            set
            {
                SetPropertyValue("TodayDate", ref todayDate, value);
            }
        }

        [Association("User-Receipts")]
        [RuleRequiredField("RuleRequiredField for Receipts.User", DefaultContexts.Save,
      "A user must be specified")]
        public User User
        {
            get
            {
                return user;
            }
            set
            {
                SetPropertyValue("User", ref user, value);
                //RefreshAvailableCodes();
                //RefreshAvailableJob();
                //RefreshAvailableCategory();
            }
        }
        
        

        bool approval;
        public bool Approval
        {
            get
            {
                return approval;
            }
            set
            {
                SetPropertyValue("Approval", ref approval, value);
            }
        }



        public enum ReceiptType { Credit, Cash }

        
    }
}
