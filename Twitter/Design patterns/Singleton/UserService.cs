using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Models;

namespace Twitter.DB
{
    public class UserService
    {
        private readonly UserDataService _userService;

        public UserService(string connectionString)
        {
            _userService = UserDataService.GetInstance(connectionString);
        }

        public string AddUser(User user)
        {
            try
            {
                return _userService.AddUser(user) ? "Amigo Add Successfully" : "Error Adding Amigo";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string AddTweet(Tweet tweet)
        {
            try
            {
                return _userService.AddTweet(tweet) ? "Tweet Add Successfully" : "Error Adding Tweet";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string DeleteFollower(int idfollower, int idfollowing)
        {
            try
            {
                return _userService.DeleteFolllower(idfollower,idfollowing) ? "Follower Removed Successfully" : "Error Removing Follower";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string AddFollower(int idfollower, int idfollowing)
        {
            try
            {
                return _userService.AddFollower(idfollower, idfollowing) ? "Follower Add Successfully" : "Error Adding Follower";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public User GetUser(string mail,string username, string contrasena)
        {
            try
            {
                return _userService.GetUser(mail, username, contrasena);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<Tweet> GetUserTweets(int idUser)
        {
            try
            {
                return _userService.GetUserTweets(idUser);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<int> GetFollowing(int idUser)
        {
            try
            {
                return _userService.GetFollowing(idUser);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<int> GetFollowers(int idUser)
        {
            try
            {
                return _userService.GetFollowers(idUser);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<User> Search(int id, string nombre)
        {
            try
            {
                return _userService.Search(id, nombre);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string Exist(string mail, string username)
        {
            try
            {
                return _userService.Exist(mail, username) ? "Existe" : "No existe";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string ExistMail(string mail)
        {
            try
            {
                return _userService.ExistMail(mail) ? "Existe" : "No existe";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string ExistUsername(string username)
        {
            try
            {
                return _userService.ExistUsername(username) ? "Existe" : "No existe";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string Verify(string mail, string username, string pasword)
        {
            try
            {
                return _userService.Verify(mail, username, pasword) ? "Correcto" : "Incorrecto";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
