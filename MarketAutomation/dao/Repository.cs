using MarketAutomation.enumaration;
using MarketAutomation.model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MarketAutomation.dao
{
    public class Repository
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;
        int returnValue;
        List<LoginTable> loginTableList;
        public Repository()
        {
            con = new SqlConnection(@"Data Source=DESKTOP-*******\SQLEXPRESS; Initial Catalog=market; User ID=sa; Password=1");
        }

        public void setConnection()
        {
            if(con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            else
            {
                con.Close();
            }
        }

        public User login(string kullaniciAdi, string sifre)
        {
            con.Open();
            cmd = new SqlCommand("select * from loginTable where kullaniciAdi = @kullaniciAdi and sifre = @sifre", con);
            cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
            cmd.Parameters.AddWithValue("@sifre", sifre);
            dr = cmd.ExecuteReader();

            if(dr.Read())
            {
                User user = new User();
                user.id = int.Parse(dr["id"].ToString());
                user.kullaniciAdi = dr["kullaniciAdi"].ToString();
                user.sifre = dr["sifre"].ToString();
                user.yetki = dr["yetki"].ToString();
                user.emailAdres = dr["emailAdres"].ToString();
                user.guvenlikSorusu = dr["guvenlikSorusu"].ToString();
                user.guvenlikCevabi = dr["guvenlikCevabi"].ToString();
                user.status = LoginStatus.basarili;
                return user;
            }
            else
            {
                User user = new User();
                user.status = LoginStatus.basarisiz;
                return user;
            }
        }

        public List<LoginTable> getLoginTable()
        {
            loginTableList = new List<LoginTable>();
            con.Open();
            cmd = new SqlCommand("sp_guvenlikSorusuGetir", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                LoginTable loginTable = new LoginTable();
                loginTable.id = int.Parse(dr["id"].ToString());
                loginTable.kullaniciAdi = dr["kullaniciAdi"].ToString();
                loginTable.sifre = dr["sifre"].ToString();
                loginTable.emailAdres = dr["emailAdres"].ToString();
                loginTable.guvenlikSorusu = dr["guvenlikSorusu"].ToString();
                loginTable.guvenlikCevabi = dr["guvenlikCevabi"].ToString();
                loginTableList.Add(loginTable);
            }
            con.Close();
            return loginTableList;
        }

        public LoginStatus doAuthentication(string kullaniciAdi, string guvenlikSorusu, string guvenlikCevabi)
        {
            con.Open();
            cmd = new SqlCommand("select count(*) from loginTable where kullaniciAdi = @kullaniciAdi and guvenlikSorusu = @guvenlikSorusu and guvenlikCevabi = @guvenlikCevabi", con);
            cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
            cmd.Parameters.AddWithValue("@guvenlikSorusu", guvenlikSorusu);
            cmd.Parameters.AddWithValue("@guvenlikCevabi", guvenlikCevabi);
            returnValue = (int)cmd.ExecuteScalar();
            con.Close();

            if(returnValue == 1)
            {
                return LoginStatus.basarili;
            }
            else
            {
                return LoginStatus.basarisiz;
            }           
        }

        public LoginStatus changePassword(string kullaniciAdi, string sifre)
        {
            con.Open();
            cmd = new SqlCommand("sp_sifreGuncelle", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
            cmd.Parameters.AddWithValue("@sifre", sifre);
            returnValue = cmd.ExecuteNonQuery();
            con.Close();
            return LoginStatus.basarili;
        }

        public string checkEmailAddress(string kullaniciAdi)
        {
            con.Open();
            cmd = new SqlCommand("select emailAdres from loginTable where kullaniciAdi = @kullaniciAdi", con);
            cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
            string emailAdres = (string)cmd.ExecuteScalar();
            con.Close();
            return emailAdres;
        }

        public Urun getProduct(string barkod) 
        {
            con.Open();
            cmd = new SqlCommand("select * from urun where barkod = @barkod", con);
            cmd.Parameters.AddWithValue("@barkod", barkod);
            dr = cmd.ExecuteReader();
            Urun urun = new Urun();

            while (dr.Read())
            {                
                urun.id = dr["id"].ToString();
                urun.qrKod = dr["qrKod"].ToString();
                urun.barkod = dr["barkod"].ToString();
                urun.olusturulmaTarih = DateTime.Parse(dr["olusturulmaTarih"].ToString());                
                urun.urunIsim = dr["urunIsim"].ToString();
                urun.kilo = int.Parse(dr["kilo"].ToString());
                urun.fiyat = int.Parse(dr["fiyat"].ToString());                
            }
            con.Close();
            return urun;
        }

        public List<User> getAllUsers()
        {
            List<User> userList = new List<User>();
            con.Open();
            cmd = new SqlCommand("select * from loginTable", con);
            dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                User user = new User();
                user.id = int.Parse(dr["id"].ToString());
                user.kullaniciAdi = dr["kullaniciAdi"].ToString();
                user.sifre = dr["sifre"].ToString();
                user.yetki = dr["yetki"].ToString();
                user.bolge = dr["bolge"].ToString();
                user.emailAdres = dr["emailAdres"].ToString();
                user.guvenlikSorusu = dr["guvenlikSorusu"].ToString();
                user.guvenlikCevabi = dr["guvenlikCevabi"].ToString();
                userList.Add(user);
            }
            con.Close();
            return userList;
        }

        public LoginStatus addUser(User user)
        {
            con.Open();
            cmd = new SqlCommand("sp_kullaniciEkle", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@kullaniciAdi", user.kullaniciAdi);
            cmd.Parameters.AddWithValue("@sifre", user.sifre);
            cmd.Parameters.AddWithValue("@yetki", user.yetki);
            cmd.Parameters.AddWithValue("@bolge", user.bolge);
            cmd.Parameters.AddWithValue("@emailAdres", user.emailAdres);
            cmd.Parameters.AddWithValue("@guvenlikSorusu", user.guvenlikSorusu);
            cmd.Parameters.AddWithValue("@guvenlikCevabi", user.guvenlikCevabi);
            int returnValue = cmd.ExecuteNonQuery();            
            con.Close();

            if (returnValue == 1)
            {
                return LoginStatus.basarili;
            }
            else
            {
                return LoginStatus.basarisiz;
            }           
        }

        public LoginStatus updateUser(User user)
        {
            con.Open();
            cmd = new SqlCommand("sp_kullaniciGuncelle", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", user.id);
            cmd.Parameters.AddWithValue("@kullaniciAdi", user.kullaniciAdi);
            cmd.Parameters.AddWithValue("@sifre", user.sifre);
            cmd.Parameters.AddWithValue("@yetki", user.yetki);
            cmd.Parameters.AddWithValue("@bolge", user.bolge);
            cmd.Parameters.AddWithValue("@emailAdres", user.emailAdres);
            cmd.Parameters.AddWithValue("@guvenlikSorusu", user.guvenlikSorusu);
            cmd.Parameters.AddWithValue("@guvenlikCevabi", user.guvenlikCevabi);
            int returnValue = cmd.ExecuteNonQuery();
            con.Close();

            if(returnValue == 1)
            {
                return LoginStatus.basarili;
            }
            else
            {
                return LoginStatus.basarisiz;
            }
        }

        public LoginStatus deleteUser(int id)
        {
            con.Open();
            cmd = new SqlCommand("delete from loginTable where id = @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            int returnValue = cmd.ExecuteNonQuery();
            con.Close();

            if(returnValue == 1)
            {
                return LoginStatus.basarili;
            }
            else
            {
                return LoginStatus.basarisiz;
            }
        }

        public List<Urun> getAllProducts()
        {
            List<Urun> urunList = new List<Urun>();
            con.Open();
            cmd = new SqlCommand("select * from urun", con);
            dr = cmd.ExecuteReader();

            while(dr.Read())
            {
                Urun urun = new Urun();
                urun.id = dr["id"].ToString();
                urun.qrKod = dr["qrKod"].ToString();
                urun.barkod = dr["barkod"].ToString();
                urun.olusturulmaTarih = DateTime.Parse(dr["olusturulmaTarih"].ToString());
                if(!string.IsNullOrEmpty(dr["guncellenmeTarih"].ToString()))
                {
                    urun.guncellenmeTarih = DateTime.Parse(dr["guncellenmeTarih"].ToString());
                }                
                urun.urunIsim = dr["urunIsim"].ToString();
                urun.kilo = int.Parse(dr["kilo"].ToString());
                urun.fiyat = int.Parse(dr["fiyat"].ToString());
                urun.ciro = int.Parse(dr["ciro"].ToString());
                urunList.Add(urun);
            }
            con.Close();
            return urunList;
        }

        public LoginStatus addProduct(Urun urun)
        {
            con.Open();
            cmd = new SqlCommand("sp_urunEkle", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", urun.id);
            if(!string.IsNullOrEmpty("@qrKod"))
            {
                cmd.Parameters.AddWithValue("@qrKod", urun.qrKod);
            }            
            cmd.Parameters.AddWithValue("@barkod", urun.barkod);
            cmd.Parameters.AddWithValue("@olusturulmaTarih", urun.olusturulmaTarih);           
            cmd.Parameters.AddWithValue("@guncellenmeTarih", urun.guncellenmeTarih);                        
            cmd.Parameters.AddWithValue("@urunIsim", urun.urunIsim);
            cmd.Parameters.AddWithValue("@kilo", urun.kilo);
            cmd.Parameters.AddWithValue("@fiyat", urun.fiyat);
            cmd.Parameters.AddWithValue("@ciro", urun.ciro);
            int returnValue = cmd.ExecuteNonQuery();
            con.Close();

            if(returnValue == 1)
            {
                return LoginStatus.basarili;
            }
            else
            {
                return LoginStatus.basarisiz;
            }
        }

        public LoginStatus updateProduct(Urun urun)
        {
            con.Open();
            cmd = new SqlCommand("sp_urunGuncelle", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", urun.id);
            if (!string.IsNullOrEmpty("@qrKod"))
            {
                cmd.Parameters.AddWithValue("@qrKod", urun.qrKod);
            }
            cmd.Parameters.AddWithValue("@barkod", urun.barkod);
            cmd.Parameters.AddWithValue("@olusturulmaTarih", urun.olusturulmaTarih);
            cmd.Parameters.AddWithValue("@guncellenmeTarih", urun.guncellenmeTarih);
            cmd.Parameters.AddWithValue("@urunIsim", urun.urunIsim);
            cmd.Parameters.AddWithValue("@kilo", urun.kilo);
            cmd.Parameters.AddWithValue("@fiyat", urun.fiyat);
            cmd.Parameters.AddWithValue("@ciro", urun.ciro);
            int returnValue = cmd.ExecuteNonQuery();
            con.Close();

            if (returnValue == 1)
            {
                return LoginStatus.basarili;
            }
            else
            {
                return LoginStatus.basarisiz;
            }
        }

        public LoginStatus deleteProduct(string id)
        {
            con.Open();
            cmd = new SqlCommand("delete from urun where id = @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            int returnValue = cmd.ExecuteNonQuery();
            con.Close();

            if (returnValue == 1)
            {
                return LoginStatus.basarili;
            }
            else
            {
                return LoginStatus.basarisiz;
            }
        }
    }
}
