using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Models;

namespace Twitter.DB
{
    public class UserDataService
    {
        private readonly SqlClient _client;
        private static UserDataService _instance;
        private static readonly object _lock = new object();

        public static UserDataService GetInstance(string connectionString)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new UserDataService(connectionString);
                    }
                }
            }
            return _instance;
        }

        private UserDataService(string connectionString)
        {
            _client = new SqlClient(connectionString);
        }

        public bool AddUser(User user)
        {
            var result = false;
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "adduser",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@nombre", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = user.name
                    };

                    var par2 = new SqlParameter("@apellidos", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = user.lastName
                    };

                    var par3 = new SqlParameter("@mail", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = user.mail
                    };

                    var par4 = new SqlParameter("@username", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = user.username
                    };

                    var par5 = new SqlParameter("@contrasena", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = user.pasword
                    };

                    var par6 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);
                    command.Parameters.Add(par4);
                    command.Parameters.Add(par5);
                    command.Parameters.Add(par6);

                    command.ExecuteNonQuery();

                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());
                }
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }


        public bool UpdateUser(string nombre, string apellidos, string contrasena, string username, string email)
        {
            var result = false;
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "updateuser",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@nombre", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = nombre
                    };

                    var par2 = new SqlParameter("@apellidos", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = apellidos
                    };

                    var par3 = new SqlParameter("@contrasena", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = contrasena
                    };

                    var par4 =  new SqlParameter("@username", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = username
                    };

                    var par5 = new SqlParameter("@email", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = email
                    };

                    var par6 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);
                    command.Parameters.Add(par4);
                    command.Parameters.Add(par5);
                    command.Parameters.Add(par6);

                    command.ExecuteNonQuery();

                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());
                }
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }


        public bool AddTweet(Tweet tweet)
        {
            var result = false;
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "addtweet",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@descripcion", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = tweet.text
                    };

                    var par2 = new SqlParameter("@hora", SqlDbType.DateTime)
                    {
                        Direction = ParameterDirection.Input,
                        Value = tweet.date
                    };

                    var par3 = new SqlParameter("@likes", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = tweet.likes
                    };

                    var par4 = new SqlParameter("@idUser", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = tweet.idUser
                    };


                    var par5 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);
                    command.Parameters.Add(par4);
                    command.Parameters.Add(par5);

                    command.ExecuteNonQuery();

                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());
                }
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public bool AddFollower(int idfollower, int idfollowing)
        {
            var result = false;
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "addfollower",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idfollower", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idfollower
                    };

                    var par2 = new SqlParameter("@idfollowing", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idfollowing
                    };

                    var par3 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);

                    command.ExecuteNonQuery();

                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());
                }
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public bool DeleteFolllower(int idfollower, int idfollowing)
        {
            var result = false;
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "deletefollower",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idfollower", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idfollower
                    };

                    var par2 = new SqlParameter("@idfollowing", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idfollowing
                    };

                    var par3 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);

                    command.ExecuteNonQuery();

                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());
                }
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public bool Exist(string mail, string username)
        {
            var result = false;
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "exist",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@username", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = username
                    };

                    var par2 = new SqlParameter("@mail", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = mail
                    };

                    var par3 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);

                    command.ExecuteNonQuery();

                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());

                }
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public bool ExistMail(string mail)
        {
            var result = false;
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "existmail",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@mail", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = mail
                    };

                    var par2 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);

                    command.ExecuteNonQuery();

                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());

                }
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public bool ExistUsername(string username)
        {
            var result = false;
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "existusername",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@username", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = username
                    };

                    var par2 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);

                    command.ExecuteNonQuery();

                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());

                }
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public bool Verify(string mail, string username, string pasword)
        {
            var result = false;
            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "verify",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@mail", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = mail
                    };

                    var par2 = new SqlParameter("@username", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = username
                    };

                    var par3 = new SqlParameter("@contrasena", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = pasword
                    };

                    var par4 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);
                    command.Parameters.Add(par4);

                    command.ExecuteNonQuery();

                    result = !Convert.ToBoolean(command.Parameters["@haserror"].Value.ToString());

                }
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public User GetUser(string mail, string username, string contrasena)
        {
            var result = new User();

            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "getuser",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@mail", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = mail
                    };

                    var par2 = new SqlParameter("@username", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = username
                    };

                    var par3 = new SqlParameter("@contrasena", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = contrasena
                    };

                    var par4 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);
                    command.Parameters.Add(par4);

                    result = null;

                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        var user = new User
                        {
                            id = (int)dataReader["iduser"],
                            name = dataReader["nombre"].ToString(),
                            lastName = dataReader["apellidos"].ToString(),
                            mail = dataReader["mail"].ToString(),
                            username = dataReader["username"].ToString(),
                            pasword = dataReader["contrasena"].ToString()
                            
                        };
                        result = user;
                        break;
                    }
                }
            }
            catch
            {
                result = null;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public List<Tweet> GetUserTweets(int idUser)
        {
            var result = new List<Tweet>();

            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "getusertweets",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idUser", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idUser
                    };

                    var par2 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);

                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        var tweet = new Tweet
                        {
                            text = dataReader["descripcion"].ToString(),
                            date = (DateTime)dataReader["hora"],
                            likes = (int)dataReader["likes"],
                            idUser = (int)dataReader["idUser"]
                        };
                        result.Add(tweet);
                    }
                }
            }
            catch
            {
                result = null;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public List<int> GetFollowing(int idUser)
        {
            var result = new List<int>();

            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "getfollowing",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@idfollower", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idUser
                    };

                    var par2 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);

                    
var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        result.Add((int)dataReader["idfollowing"]);
                    }
                }
            }
            catch
            {
                result = null;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public List<int> GetFollowers(int idUser)
        {
            var result = new List<int>();

            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "getfollowers",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@iduser", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = idUser
                    };

                    var par2 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);


                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        result.Add((int)dataReader["idfollower"]);
                    }
                }
            }
            catch
            {
                result = null;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }

        public List<User> Search(int id, string nombre)
        {
            var result = new List<User>();

            try
            {
                if (_client.Open())
                {
                    var command = new SqlCommand
                    {
                        Connection = _client.Conecction,
                        CommandText = "search",
                        CommandType = CommandType.StoredProcedure
                    };

                    var par1 = new SqlParameter("@iduser", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Input,
                        Value = id
                    };

                    var par2 = new SqlParameter("@nombre", SqlDbType.NVarChar)
                    {
                        Direction = ParameterDirection.Input,
                        Value = nombre
                    };

                    var par3 = new SqlParameter("@haserror", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(par1);
                    command.Parameters.Add(par2);
                    command.Parameters.Add(par3);


                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        var user = new User
                        {
                            id = (int)dataReader["iduser"],
                            name = dataReader["nombre"].ToString(),
                            lastName = dataReader["apellidos"].ToString(),
                            mail = "Sin acceso",
                            username = dataReader["username"].ToString(),
                            pasword = "Sin acceso"

                        };
                        result.Add(user);
                    }
                }
            }
            catch
            {
                result = null;
            }
            finally
            {
                _client.Close();
            }

            return result;
        }


    }
}
