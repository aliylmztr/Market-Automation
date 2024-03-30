using MarketAutomation.dao;
using MarketAutomation.enumaration;
using MarketAutomation.model;
using System.Collections.Generic;

namespace MarketAutomation.controller
{
    public class Controller
    {
        Repository repository;        

        public Controller()
        {
            repository = new Repository();
        }

        public User login(string kullaniciAdi, string sifre)
        {
            User result;
            if (!string.IsNullOrEmpty(kullaniciAdi) && !string.IsNullOrEmpty(sifre))
            {               
                result = repository.login(kullaniciAdi, sifre);
                return result;
            }
            else
            {
                User user = new User();
                user.status = LoginStatus.eksikParametre;
                return user;
            }
        }

        public List<LoginTable> getLoginTable()
        {            
           List<LoginTable> loginTableList = repository.getLoginTable();
           return loginTableList;
        }

        public LoginStatus doAuthentication(string kullaniciAdi, string guvenlikSorusu, string guvenlikCevabi)
        {
            if(!string.IsNullOrEmpty(kullaniciAdi) && !string.IsNullOrEmpty(guvenlikSorusu) && !string.IsNullOrEmpty(guvenlikCevabi))
            {
                LoginStatus result = repository.doAuthentication(kullaniciAdi, guvenlikSorusu, guvenlikCevabi);

                if(result == LoginStatus.basarili)
                {
                    return result;
                }
                else
                {
                    return LoginStatus.basarisiz;
                }
            }
            else
            {
                return LoginStatus.eksikParametre;
            }
        }

        public LoginStatus changePassword(string kullaniciAdi, string sifre)
        {
            if(!string.IsNullOrEmpty(kullaniciAdi) && !string.IsNullOrEmpty(sifre))
            {
                return repository.changePassword(kullaniciAdi, sifre);
            }
            else
            {
                return LoginStatus.eksikParametre;
            }
        }
        public string checkEmailAddress(string kullaniciAdi)
        {
            return repository.checkEmailAddress(kullaniciAdi);
        }

        public Urun getProduct(string barkod)
        {
            if(!string.IsNullOrEmpty(barkod))
            {
                return repository.getProduct(barkod);
            }
            else
            {
                return null;
            }
        }

        public List<User> getAllUsers()
        {           
            return repository.getAllUsers();
        }

        public LoginStatus addUser(User user)
        {
            if(!string.IsNullOrEmpty(user.kullaniciAdi) && !string.IsNullOrEmpty(user.sifre) && !string.IsNullOrEmpty(user.yetki) && !string.IsNullOrEmpty(user.emailAdres) && !string.IsNullOrEmpty(user.guvenlikSorusu) && !string.IsNullOrEmpty(user.guvenlikCevabi))
            {
                LoginStatus result = repository.addUser(user);
                return result;
            }
            else
            {
                return LoginStatus.eksikParametre;
            }
        }

        public LoginStatus updateUser(User user)
        {
            return repository.updateUser(user);
        }

        public LoginStatus deleteUser(int id)
        {
            if(!string.IsNullOrEmpty(id.ToString()))
            {
                LoginStatus result = repository.deleteUser(id);
                return result;
            }
            else
            {
                return LoginStatus.eksikParametre;
            }
        }

        public List<Urun> getAllProducts()
        {
            return repository.getAllProducts();
        }

        public LoginStatus addProduct(Urun urun)
        {
            if(!string.IsNullOrEmpty(urun.id) && !string.IsNullOrEmpty(urun.urunIsim) && !string.IsNullOrEmpty(urun.barkod))
            {
                return repository.addProduct(urun);
            }
            else
            {
                return LoginStatus.eksikParametre;
            }
        }

        public LoginStatus updateProduct(Urun urun)
        {
            if (!string.IsNullOrEmpty(urun.id) && !string.IsNullOrEmpty(urun.urunIsim) && !string.IsNullOrEmpty(urun.barkod))
            {
                return repository.updateProduct(urun);
            }
            else
            {
                return LoginStatus.eksikParametre;
            }
        }

        public LoginStatus deleteProduct(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return repository.deleteProduct(id);
            }
            else
            {
                return LoginStatus.eksikParametre;
            }
        }
    }
}
