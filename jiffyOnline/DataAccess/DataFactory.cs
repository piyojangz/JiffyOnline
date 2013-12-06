using jiffyOnline.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using jiffyOnline.Entity;
using jiffyOnline.Models;

namespace jiffyOnline.DataAccess
{
    public class DataFactory
    {
        private JIFFY_DBEntities db;

        public DataFactory()
        {
            db = new JIFFY_DBEntities();
        }

        #region Get Data

        public List<DropdownEntity> GetItemProperty(int ProductId)
        {
            var query = from a in db.V_PRODUCT_CATAGORIES
                        where a.PRODUCT_ID == ProductId
                        select new DropdownEntity
                        {
                            Text = a.NAME_EN,
                            Value = a.P_ID
                        };
            return query.ToList();
        }


        public T_CUSTOMER GetAuthenCustomer(LoginModel models)
        {
            var query = from a in db.T_CUSTOMER
                        where a.AUTH_USER == models.AUTH_USER && a.AUTH_PASS == models.AUTH_PASS
                        select a;
            return query.FirstOrDefault();
        }

        public List<MenuViewEntity> CreateVM(int parentid, List<CategoriesEntity> source)
        {
            var query = from men in source
                        where men.PARENT_ID == parentid
                        select new MenuViewEntity
                        {
                            MenuId = men.ID,
                            Name = men.NAME_EN,
                            // other properties
                            Children = CreateVM(men.ID, source)
                        };
            return query.ToList();
        }
        public List<CategoriesEntity> GetCategories()
        {
            var query = from a in db.T_CATEGORIES
                        select new CategoriesEntity
                        {
                            ID = a.ID,
                            NAME_TH = a.NAME_EN,
                            NAME_EN = a.NAME_EN,
                            PARENT_ID = a.PARENT_ID
                        };
            return query.ToList();
        }

        public List<BannerEntity> GetBanner()
        {
            var query = from a in db.T_BANNER
                        join b in db.T_IMAGE
                        on a.ID equals b.REF_ID
                        where a.ACTIVE == 1
                        select new BannerEntity
                        {
                            ID = a.ID,
                            Img = b.IMG,
                            Position = a.POSITION,
                            Url = a.URL,
                            Type = b.TYPE,
                            RefId = b.REF_ID,
                            Priorrity = b.PRIORITY

                        };
            return query.ToList();
        }
        public List<BannerEntity> GetBannerS()
        {
            var query = from a in db.V_BANNER
                        where a.ACTIVE == 1
                        select new BannerEntity
                        {
                            ID = a.ID,
                            Img = a.IMG,
                            Position = a.POSITION,
                            Url = a.URL,
                            Type = a.BANNER_TYPE,
                            RefId = a.REF_ID
                        };
            return query.ToList();
        }
        public List<ItemImagesEntity> GetItemImgs(int Id)
        {
            var query = from a in db.T_IMAGE
                        where a.REF_ID == Id
                        orderby a.PRIORITY
                        select new ItemImagesEntity
                        {
                            ID = a.ID,
                            Img = a.IMG,
                            RefId = a.REF_ID
                        };
            return query.ToList();
        }

        public List<ItemDetailEntity> GetItemDetail()
        {
            var query = from a in db.T_PRODUCTS
                        join b in db.T_IMAGE
                        on a.ID equals b.REF_ID
                        select new ItemDetailEntity
                        {
                            ID = a.ID,
                            ACTIVE = a.ACTIVE,
                            BRAND_ID = a.BRAND_ID,
                            BUY_FLAG = a.BUY_FLAG,
                            CALL_PRICE_FLAG = a.CALL_PRICE_FLAG,
                            CATEGORIES_ID = a.CATEGORIES_ID,
                            COST = a.COST,
                            CREATE_BY = a.CREATE_BY,
                            CREATE_DATE = a.CREATE_DATE,
                            DELETE_BY = a.DELETE_BY,
                            DELETE_DATE = a.DELETE_DATE,
                            DESCR_EN = a.DESCR_EN,
                            DESCR_TH = a.DESCR_TH,
                            DISPLAY_QTY_FLAG = a.DISPLAY_QTY_FLAG,
                            MAX_QTY = a.MAX_QTY,
                            MIN_QTY = a.MIN_QTY,
                            MIN_STOCK_QTY = a.MIN_STOCK_QTY,
                            NAME_EN = a.NAME_EN,
                            NAME_TH = a.NAME_TH,
                            NEW_ARRIVAL_FLAG = a.NEW_ARRIVAL_FLAG,
                            OLD_PRICE = a.OLD_PRICE,
                            PRICE = a.PRICE,
                            PRODUCT_SKU = a.PRODUCT_SKU,
                            RECOMMEND_FLAG = a.RECOMMEND_FLAG,
                            SHIPPING_CHARGE = a.SHIPPING_CHARGE,
                            SHIPPING_FLAG = a.SHIPPING_FLAG,
                            SHORT_DESCR_EN = a.SHORT_DESCR_EN,
                            SHORT_DESCR_TH = a.SHORT_DESCR_TH,
                            STOCK_QTY = a.STOCK_QTY,
                            TAX_TYPE = a.TAX_TYPE,
                            TAX_VALUE = a.TAX_VALUE,
                            UPDATE_BY = a.UPDATE_BY,
                            UPDATE_DATE = a.UPDATE_DATE,
                            VENDOR_ID = a.VENDOR_ID,
                            WEIGTH = a.WEIGTH,
                            WISH_LIST = a.WISH_LIST,
                            IMAGES = b.IMG
                        };
            return query.ToList();
        }

        public List<ItemDetailEntity> GetProductRelation()
        {
            var query = from a in db.T_PRODUCTS
                        join b in db.T_IMAGE
                         on a.ID equals b.REF_ID
                        join c in db.T_PRODUCT_RELATION
                        on a.ID equals c.PRODUCT_ID
                        select new ItemDetailEntity
                        {
                            ID = a.ID,
                            ACTIVE = a.ACTIVE,
                            BRAND_ID = a.BRAND_ID,
                            BUY_FLAG = a.BUY_FLAG,
                            CALL_PRICE_FLAG = a.CALL_PRICE_FLAG,
                            CATEGORIES_ID = a.CATEGORIES_ID,
                            COST = a.COST,
                            CREATE_BY = a.CREATE_BY,
                            CREATE_DATE = a.CREATE_DATE,
                            DELETE_BY = a.DELETE_BY,
                            DELETE_DATE = a.DELETE_DATE,
                            DESCR_EN = a.DESCR_EN,
                            DESCR_TH = a.DESCR_TH,
                            DISPLAY_QTY_FLAG = a.DISPLAY_QTY_FLAG,
                            MAX_QTY = a.MAX_QTY,
                            MIN_QTY = a.MIN_QTY,
                            MIN_STOCK_QTY = a.MIN_STOCK_QTY,
                            NAME_EN = a.NAME_EN,
                            NAME_TH = a.NAME_TH,
                            NEW_ARRIVAL_FLAG = a.NEW_ARRIVAL_FLAG,
                            OLD_PRICE = a.OLD_PRICE,
                            PRICE = a.PRICE,
                            PRODUCT_SKU = a.PRODUCT_SKU,
                            RECOMMEND_FLAG = a.RECOMMEND_FLAG,
                            SHIPPING_CHARGE = a.SHIPPING_CHARGE,
                            SHIPPING_FLAG = a.SHIPPING_FLAG,
                            SHORT_DESCR_EN = a.SHORT_DESCR_EN,
                            SHORT_DESCR_TH = a.SHORT_DESCR_TH,
                            STOCK_QTY = a.STOCK_QTY,
                            TAX_TYPE = a.TAX_TYPE,
                            TAX_VALUE = a.TAX_VALUE,
                            UPDATE_BY = a.UPDATE_BY,
                            UPDATE_DATE = a.UPDATE_DATE,
                            VENDOR_ID = a.VENDOR_ID,
                            WEIGTH = a.WEIGTH,
                            WISH_LIST = a.WISH_LIST,
                            IMAGES = b.IMG
                        };
            return query.ToList();
        }

        public List<ItemDetailEntity> GetProductRelate()
        {
            var query = from a in db.T_PRODUCTS
                        join b in db.T_IMAGE
                         on a.ID equals b.REF_ID
                        join c in db.V_PRODUCT_RELATION
                        on a.ID equals c.PRODUCT_ID
                        select new ItemDetailEntity
                        {
                            ID = a.ID,
                            ACTIVE = a.ACTIVE,
                            BRAND_ID = a.BRAND_ID,
                            BUY_FLAG = a.BUY_FLAG,
                            CALL_PRICE_FLAG = a.CALL_PRICE_FLAG,
                            CATEGORIES_ID = a.CATEGORIES_ID,
                            COST = a.COST,
                            CREATE_BY = a.CREATE_BY,
                            CREATE_DATE = a.CREATE_DATE,
                            DELETE_BY = a.DELETE_BY,
                            DELETE_DATE = a.DELETE_DATE,
                            DESCR_EN = a.DESCR_EN,
                            DESCR_TH = a.DESCR_TH,
                            DISPLAY_QTY_FLAG = a.DISPLAY_QTY_FLAG,
                            MAX_QTY = a.MAX_QTY,
                            MIN_QTY = a.MIN_QTY,
                            MIN_STOCK_QTY = a.MIN_STOCK_QTY,
                            NAME_EN = a.NAME_EN,
                            NAME_TH = a.NAME_TH,
                            NEW_ARRIVAL_FLAG = a.NEW_ARRIVAL_FLAG,
                            OLD_PRICE = a.OLD_PRICE,
                            PRICE = a.PRICE,
                            PRODUCT_SKU = a.PRODUCT_SKU,
                            RECOMMEND_FLAG = a.RECOMMEND_FLAG,
                            SHIPPING_CHARGE = a.SHIPPING_CHARGE,
                            SHIPPING_FLAG = a.SHIPPING_FLAG,
                            SHORT_DESCR_EN = a.SHORT_DESCR_EN,
                            SHORT_DESCR_TH = a.SHORT_DESCR_TH,
                            STOCK_QTY = a.STOCK_QTY,
                            TAX_TYPE = a.TAX_TYPE,
                            TAX_VALUE = a.TAX_VALUE,
                            UPDATE_BY = a.UPDATE_BY,
                            UPDATE_DATE = a.UPDATE_DATE,
                            VENDOR_ID = a.VENDOR_ID,
                            WEIGTH = a.WEIGTH,
                            WISH_LIST = a.WISH_LIST,
                            IMAGES = b.IMG
                        };
            return query.ToList();
        }
        public List<ProductsEntity> GetProductRecommend()
        {
            var query = from a in db.T_PRODUCTS
                        join b in db.T_IMAGE
                       on a.ID equals b.REF_ID
                        join c in db.T_WISHLIST
                        on a.ID equals c.PRODUCT_ID
                        where a.RECOMMEND_FLAG == 1
                        select new ProductsEntity
                        {
                            ID = a.ID,
                            NAME_TH = a.NAME_EN,
                            NAME_EN = a.NAME_EN,
                            BRAND_ID = a.BRAND_ID,
                            CATEGORIES_ID = a.CATEGORIES_ID,
                            MAX_QTY = a.MAX_QTY,
                            PRICE = a.PRICE,
                            SHIPPING_FLAG = a.SHIPPING_FLAG,
                            STOCK_QTY = a.STOCK_QTY,
                            OLD_PRICE = a.OLD_PRICE,
                            SHIPPING_CHARGE = a.SHIPPING_CHARGE,
                            VENDOR_ID = a.VENDOR_ID,
                            WEIGTH = a.WEIGTH,
                            WISH_LIST = a.WISH_LIST,
                            TAX_VALUE = a.TAX_VALUE,
                            TAX_TYPE = a.TAX_TYPE,
                            SHORT_DESCR_EN = a.SHORT_DESCR_EN,
                            SHORT_DESCR_TH = a.SHORT_DESCR_TH,
                            RECOMMEND_FLAG = a.RECOMMEND_FLAG,
                            NEW_ARRIVAL_FLAG = a.NEW_ARRIVAL_FLAG,
                            DISPLAY_QTY_FLAG = a.DISPLAY_QTY_FLAG,
                            COST = a.COST,
                            IMAGES = b.IMG
                        };
            return query.ToList();
        }

        public CategoriesEntity GetCategoriesById(int Id)
        {
            var query = from a in db.T_CATEGORIES
                        where a.ID == Id
                        select new CategoriesEntity
                        {
                            ID = a.ID,
                            NAME_TH = a.NAME_EN,
                            NAME_EN = a.NAME_EN
                        };
            return query.FirstOrDefault();
        }
        public List<ProductsEntity> GetProduct()
        {
            var query = from a in db.T_PRODUCTS
                        select new ProductsEntity
                        {
                            ID = a.ID,
                            NAME_TH = a.NAME_EN,
                            NAME_EN = a.NAME_EN,
                            BRAND_ID = a.BRAND_ID,
                            CATEGORIES_ID = a.CATEGORIES_ID,
                            MAX_QTY = a.MAX_QTY,
                            PRICE = a.PRICE,
                            SHIPPING_FLAG = a.SHIPPING_FLAG,
                            STOCK_QTY = a.STOCK_QTY,
                            OLD_PRICE = a.OLD_PRICE,
                            SHIPPING_CHARGE = a.SHIPPING_CHARGE,
                            VENDOR_ID = a.VENDOR_ID,
                            WEIGTH = a.WEIGTH,
                            WISH_LIST = a.WISH_LIST,
                            TAX_VALUE = a.TAX_VALUE,
                            TAX_TYPE = a.TAX_TYPE,
                            SHORT_DESCR_EN = a.SHORT_DESCR_EN,
                            SHORT_DESCR_TH = a.SHORT_DESCR_TH,
                            RECOMMEND_FLAG = a.RECOMMEND_FLAG,
                            NEW_ARRIVAL_FLAG = a.NEW_ARRIVAL_FLAG,
                            DISPLAY_QTY_FLAG = a.DISPLAY_QTY_FLAG,
                            COST = a.COST,
                            IMAGES = (from b in db.T_IMAGE where b.REF_ID == a.ID select b).FirstOrDefault().IMG
                        };
            return query.ToList();
        }

        public List<ProductsEntity> GetProductByCateId(int CateId)
        {
            List<int> _MenuId = new List<int>();
            _MenuId.Add(CateId);
            List<MenuViewEntity> _CreateVM = CreateVM(CateId, GetCategories());
            foreach (var item in _CreateVM)
            {
                _MenuId.Add(item.MenuId);
            }
            var query = from a in db.T_PRODUCTS
                        where _MenuId.Contains(a.CATEGORIES_ID)
                        select new ProductsEntity
                        {
                            ID = a.ID,
                            NAME_TH = a.NAME_EN,
                            NAME_EN = a.NAME_EN,
                            BRAND_ID = a.BRAND_ID,
                            CATEGORIES_ID = a.CATEGORIES_ID,
                            MAX_QTY = a.MAX_QTY,
                            PRICE = a.PRICE,
                            SHIPPING_FLAG = a.SHIPPING_FLAG,
                            STOCK_QTY = a.STOCK_QTY,
                            OLD_PRICE = a.OLD_PRICE,
                            SHIPPING_CHARGE = a.SHIPPING_CHARGE,
                            VENDOR_ID = a.VENDOR_ID,
                            WEIGTH = a.WEIGTH,
                            WISH_LIST = a.WISH_LIST,
                            TAX_VALUE = a.TAX_VALUE,
                            TAX_TYPE = a.TAX_TYPE,
                            SHORT_DESCR_EN = a.SHORT_DESCR_EN,
                            SHORT_DESCR_TH = a.SHORT_DESCR_TH,
                            RECOMMEND_FLAG = a.RECOMMEND_FLAG,
                            NEW_ARRIVAL_FLAG = a.NEW_ARRIVAL_FLAG,
                            DISPLAY_QTY_FLAG = a.DISPLAY_QTY_FLAG,
                            COST = a.COST,
                            IMAGES = (from b in db.T_IMAGE where b.REF_ID == a.ID select b).FirstOrDefault().IMG
                        };
            return query.ToList();
        }

        public List<ProductsEntity> GetWishList(int CustomerId)
        {
            var query = from a in db.T_PRODUCTS
                        join b in db.T_IMAGE
                        on a.ID equals b.REF_ID
                        join c in db.T_WISHLIST
                        on a.ID equals c.PRODUCT_ID
                        where c.CUSTOMER_ID == CustomerId
                        select new ProductsEntity
                        {
                            ID = a.ID,
                            NAME_TH = a.NAME_EN,
                            NAME_EN = a.NAME_EN,
                            BRAND_ID = a.BRAND_ID,
                            CATEGORIES_ID = a.CATEGORIES_ID,
                            MAX_QTY = a.MAX_QTY,
                            PRICE = a.PRICE,
                            SHIPPING_FLAG = a.SHIPPING_FLAG,
                            STOCK_QTY = a.STOCK_QTY,
                            OLD_PRICE = a.OLD_PRICE,
                            SHIPPING_CHARGE = a.SHIPPING_CHARGE,
                            VENDOR_ID = a.VENDOR_ID,
                            WEIGTH = a.WEIGTH,
                            WISH_LIST = a.WISH_LIST,
                            TAX_VALUE = a.TAX_VALUE,
                            TAX_TYPE = a.TAX_TYPE,
                            SHORT_DESCR_EN = a.SHORT_DESCR_EN,
                            SHORT_DESCR_TH = a.SHORT_DESCR_TH,
                            RECOMMEND_FLAG = a.RECOMMEND_FLAG,
                            NEW_ARRIVAL_FLAG = a.NEW_ARRIVAL_FLAG,
                            DISPLAY_QTY_FLAG = a.DISPLAY_QTY_FLAG,
                            COST = a.COST,
                            IMAGES = b.IMG
                        };
            return query.ToList();
        }


        public List<ProvinceEntity> GetProvince()
        {
            var query = from a in db.T_PROVINCE
                        select new ProvinceEntity
                        {
                            Text = a.NAME_TH,
                            Value = a.ID
                        };
            return query.ToList();
        }
        public List<DistrictEntity> GetDistrict(int aumphuId)
        {
            var query = from a in db.T_DISTRICT
                        where a.AMPHUR_ID == aumphuId
                        select new DistrictEntity
                        {
                            Text = a.NAME_TH,
                            Value = a.ID
                        };
            return query.ToList();
        }
        public List<AumphurEntity> GetAumphur(int provinceId)
        {
            var query = from a in db.T_AMPHUR
                        where a.PROVINCE_ID == provinceId
                        select new AumphurEntity
                        {
                            Text = a.NAME_TH,
                            Value = a.ID
                        };
            return query.ToList();
        }
        public List<PostcodeEntity> GetPostcode(int aumphurId)
        {
            var query = from a in db.T_AMPHUR_POSTCODE
                        where a.AMPHUR_ID == aumphurId
                        select new PostcodeEntity
                        {
                            Value = a.POST_CODE
                        };
            return query.ToList();
        }

        public List<T_CONTENT> GetContents()
        {
            var query = from a in db.T_CONTENT
                        select a;
            return query.ToList();
        }
        #endregion


        #region Set Data
        public bool SetCustomer(RegisterModel model)
        {
            bool _IsSave = true;
            try
            {
                T_CUSTOMER objCustomer = new T_CUSTOMER();
                objCustomer.ID = GetNextID("T_CUSTOMER");
                objCustomer.AUTH_USER = model.AUTH_USER;
                objCustomer.AUTH_PASS = model.AUTH_PASS;
                objCustomer.COMPANYNAME = model.COMPANYNAME;
                objCustomer.CREATE_BY = "";
                objCustomer.TYPE = "";
                objCustomer.CUSTOMER_GROUP = null;
                objCustomer.DATE_OF_BIRTH = Convert.ToDateTime(model.DATE_OF_BIRTH);
                objCustomer.AMPHUR_ID = model.AMPHUR_ID;
                objCustomer.DISTRICT_ID = model.DISTRICT_ID == null ? 0 : model.DISTRICT_ID;
                objCustomer.PROVINCE_ID = model.PROVINCE_ID;
                objCustomer.EMAIL = model.EMAIL;
                objCustomer.FAX = model.FAX;
                objCustomer.GENDER = model.GENDER;
                objCustomer.FNAME = model.FNAME;
                objCustomer.LNAME = model.LNAME;
                objCustomer.MOBILE = model.MOBILE;
                objCustomer.TEL = model.TEL;
                objCustomer.FAX = model.FAX;
                objCustomer.POSTCODE = model.POSTCODE;
                objCustomer.SEND_NEWS = model.SEND_NEWS == true ? (short)1 : (short)0;
                db.T_CUSTOMER.Add(objCustomer);
                Save();
            }
            catch (Exception ex)
            {
                _IsSave = false;
            }

            return _IsSave;
        }

        public bool SetWishlist(int ProductId, int CustomerId)
        {
            bool _IsSave = true;
            try
            {
                if (db.T_WISHLIST.Where(p => p.PRODUCT_ID == ProductId && p.CUSTOMER_ID == CustomerId).Select(p => p.ID).ToList().Count() > 0)
                {
                    _IsSave = false;
                }
                else
                {
                    T_WISHLIST objWishlist = new T_WISHLIST();
                    objWishlist.ID = GetNextID("T_WISHLIST");
                    objWishlist.CUSTOMER_ID = CustomerId;
                    objWishlist.PRODUCT_ID = ProductId;
                    db.T_WISHLIST.Add(objWishlist);
                    Save();
                }
            }
            catch (Exception ex)
            {
                _IsSave = false;
            }

            return _IsSave;
        }
        #endregion

        #region "Persistence methods"

        public void Save()
        {
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        #endregion


        private int GetNextID(string tableName)
        {
            int retval;
            RUNNING objRunning = db.RUNNINGs.Where(p => p.TABLENAME == tableName && p.ITEM == "ID").FirstOrDefault();
            string jurientDate = "";
          
            if (objRunning != null)
            {
                retval = Convert.ToInt32(objRunning.LASTID);
                if (retval.ToString().Length > 6) jurientDate = retval.ToString().Substring(0, 6);
                if (jurientDate == ApplicationHelpers.GetJDate())
                {
                    retval += 1;
                }
                else
                {
                    retval = Convert.ToInt32(ApplicationHelpers.GetJDate() + "001");
                }
                objRunning.LASTID = retval.ToString();
                objRunning.TABLENAME = tableName;
            }
            else
            {
                retval = Convert.ToInt32(ApplicationHelpers.GetJDate() + "001");
                objRunning = new RUNNING();
                objRunning.LASTID = retval.ToString();
                objRunning.TABLENAME = tableName;
                objRunning.ITEM = "ID";
                db.RUNNINGs.Add(objRunning);
               
            }
            Save();
            //double retval;
            //string sql = "";

            //SqlDataReader dataReader;
            //if (trans != null)
            //    dataReader = ExecuteReader("SELECT LASTID FROM RUNNING WHERE TABLENAME = '" + tableName + "' AND ITEM ='ID'", trans);
            //else
            //    dataReader = ExecuteReader("SELECT LASTID FROM RUNNING WHERE TABLENAME = '" + tableName + "' AND ITEM ='ID'", conn);
            //if (dataReader.Read())
            //{
            //    string jurientDate = "";
            //    retval = Convert.ToDouble(dataReader["LASTID"]);

            //    if (retval.ToString().Length > 6) jurientDate = retval.ToString().Substring(0, 6);
            //    if (jurientDate == GetJDate())
            //    {
            //        retval += 1;
            //    }
            //    else
            //    {
            //        retval = Convert.ToDouble(GetJDate() + "001");
            //    }
            //    sql = "UPDATE RUNNING SET LASTID = " + retval.ToString() + " WHERE TABLENAME = '" + tableName + "' AND ITEM = 'ID'";
            //}
            //else
            //{
            //    retval = Convert.ToDouble(GetJDate() + "001");
            //    sql = "INSERT INTO RUNNING (TABLENAME, LASTID, ITEM) VALUES ('" + tableName + "', " + retval.ToString() + ", 'ID' ) ";
            //}
            //dataReader.Close();
            //if (trans != null)
            //    ExecuteNonQuery(sql, trans);
            //else
            //    ExecuteNonQuery(sql, conn);
            return retval;
        }
    }
}