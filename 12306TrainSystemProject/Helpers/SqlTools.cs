using System;
using System.Data.Odbc;
using Error;
using Check;
using Crypto;
using _12306TrainSystemProject.Models;

#nullable enable
namespace ServerSqlTools
{
    class OracleSqlTools
    {   
        public static OdbcConnection? conn;
        //static void Main(string[] args)
        //{
        //    //Console.WriteLine(resetUser())
        //    User U = new User();
        //    //U.UserRName  = "小黄";
        //    U.UserPWD    = "Yellowbest132457/";
        //    //U.UserEmail  = "1005631584@qq.com";
        //    //U.UserGender = "1";
        //    //U.UserPhone  = "13795469349";
        //    //U.UserType   = 1;
        //    //U.UserAddr   = "山海市嘉诚区曹阳公路3800号";
        //    U.UserPID    = "311032200110036523";

        //    //Station S = new Station();
        //    //S.StationName = "上海站";

        //    Console.WriteLine(Login(U));
        //}

        public static int Register(UserViewModel U)
        {
            int ret = -1;
            //1. check string security
            if((ret = checkUser.checkRegister(U)) != -1)
            {
                return ret;
            }
            Console.WriteLine("1 Success");

            //2. connect to the database
            if(!Connect())
            {
                Console.WriteLine("Failed to Connect to Oracle");
                return (int)SqlErrorCode.ERR_CONN;
            }
            Console.WriteLine("2 Success");

            //3. do md5 trans
            string md5UserPID   = md5Crypto.MD5Encrypt32(U.UserPID);
            string md5UserPWD   = md5Crypto.MD5Encrypt32(U.UserPWD);
            string md5UserPhone = md5Crypto.MD5Encrypt32(U.UserPhone);
            string md5UserRName = md5Crypto.MD5Encrypt32(U.UserRName);
            string md5UserAddr  = md5Crypto.MD5Encrypt32(U.UserAddr);
            string md5UserEmail = md5Crypto.MD5Encrypt32(U.UserEmail);
            string md5UserID    = md5UserPID;
            Console.WriteLine("3 Success");

            //4. query whether user has been registered(using PID)
            string queryStr = "SELECT count(*) from T_USER where USER_PERSON_ID ='" + md5UserPID + "';";
            Console.WriteLine(queryStr);
            OdbcCommand sqlcmd = new OdbcCommand(queryStr, conn);
            //Execute the DataReader to Access the data
            try
            {
                OdbcDataReader DataReader = sqlcmd.ExecuteReader();
                if(DataReader.Read() && DataReader[0].ToString() != "0")
                {
                    return (int)RegErrorCode.ERR_UEXIST;
                }
                DataReader.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (int)SqlErrorCode.ERR_SQLCMD;
            }

            Console.WriteLine("4 Success");

            //5. query whether phone number has been used
            queryStr = "SELECT count(*) from T_USER where USER_PHONE_NUMBER = '" + md5UserPhone + "';";
            Console.WriteLine(queryStr);
            sqlcmd.CommandText = queryStr;
            //Execute the DataReader to Access the data
            try
            {
                OdbcDataReader DataReader = sqlcmd.ExecuteReader();
                if(DataReader.Read() && DataReader[0].ToString() != "0")
                {
                    return (int)RegErrorCode.ERR_PHEXIST;
                }
                DataReader.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (int)SqlErrorCode.ERR_SQLCMD;
            }

            Console.WriteLine("5 Success");
            //6. insert users
            queryStr = "INSERT INTO T_USER Values('"  + md5UserID    + "','"  
                                                      + md5UserPWD   + "','"  
                                                      + md5UserPhone + "','" 
                                                      + md5UserEmail + "','"
                                                      + md5UserRName + "','"
                                                      + U.UserType   + "','"
                                                      + U.UserGender + "','"
                                                      + md5UserAddr  + "','"
                                                      + md5UserPID   + "');";

            Console.WriteLine(queryStr);
            sqlcmd.CommandText = queryStr;

            try
            {
                sqlcmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (int)SqlErrorCode.ERR_SQLCMD;
            }
            Console.WriteLine("6 Success");

            //close the connection
            Close();
            Console.WriteLine("Connection Closed");
            return -1;
        }

        public static int Login(UserViewModel U)
        {
            int ret = -1;
            //1. check string security
            if((ret = checkUser.checkLogin(U)) != -1)
            {
                return ret;
            }

            //2. connect to the database
            if(!Connect())
            {
                Console.WriteLine("Failed to Connect to Oracle");
                return (int)SqlErrorCode.ERR_CONN;
            }
            Console.WriteLine("2 Success");

            string? md5UserPID;
            string? md5UserPhone;
            string queryStr = "";

            if(U.UserPID != null)
            {
                md5UserPID   = md5Crypto.MD5Encrypt32(U.UserPID);
                queryStr     = "Select USER_PASSWORD from T_USER where USER_PERSON_ID = '" + md5UserPID + "';";
            }
            else if(U.UserPhone != null)
            {
                md5UserPhone   = md5Crypto.MD5Encrypt32(U.UserPhone);
                queryStr       = "Select USER_PASSWORD from T_USER where USER_PHONE_NUMBER = '" + md5UserPhone + "';";
            }

            //3. do md5 trans
            string md5UserPWD   = md5Crypto.MD5Encrypt32(U.UserPWD);
            Console.WriteLine("3 Success");
            
            //4 query whether the User is existed and check the password
            Console.WriteLine(queryStr);
            OdbcCommand sqlcmd = new OdbcCommand(queryStr, conn);
            //Execute the DataReader to Access the data
            try
            {
                OdbcDataReader DataReader = sqlcmd.ExecuteReader();
                if(!DataReader.Read()) // User dosen't exist
                {
                    return (int)LoginErrorCode.ERR_UUNEXIST;
                }
                else // check password
                {
                    if(DataReader[0].ToString() != md5UserPWD)
                    {
                        return (int)LoginErrorCode.ERR_PWD;
                    }
                }
                DataReader.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (int)SqlErrorCode.ERR_SQLCMD;
            }

            Console.WriteLine("4 Success");

            return -1;
        }

        public static bool Connect()
        {
            string connStr = string.Format("DSN=xe;UID=test_Yellowbest;PWD=test_Yellowbest");
            try
            {
                conn = new OdbcConnection(connStr);
                conn.Open();
                Console.WriteLine("Connect Status: " + conn.State);
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static int AddStation(StationViewModel S)
        {
            int ret = -1;
            //1 check security
            if((ret = checkStation.checkAddSt(S)) != -1)
            {
                return ret;
            }
            Console.WriteLine("1 Success");

            //2 connect to the database
            if(!Connect())
            {
                Console.WriteLine("Failed to Connect to Oracle");
                return (int)SqlErrorCode.ERR_CONN;
            }
            Console.WriteLine("2 Success");

            //3 get StationID
            //get total station's number
            string queryStr = "SELECT count(*) from T_STATION_INFO;";
            OdbcCommand sqlcmd = new OdbcCommand(queryStr, conn);
            int No = -1;
            //Execute the DataReader to Access the data
            try
            {
                OdbcDataReader DataReader = sqlcmd.ExecuteReader();
                if(DataReader.Read()) // User dosen't exist
                {
                    int.TryParse(DataReader[0].ToString(), out No);
                    S.StationNo = No + 1;
                }
                DataReader.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (int)SqlErrorCode.ERR_SQLCMD;
            }
            Console.WriteLine("3 Success");

            //4 insert stations 
            queryStr = "INSERT INTO T_STATION_INFO Values('" + S.StationNo.ToString() + "','"
                                                             + S.StationName          + "');";
             
            Console.WriteLine(queryStr);
            sqlcmd.CommandText = queryStr;

            try
            {
                sqlcmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (int)SqlErrorCode.ERR_SQLCMD;
            }
            Console.WriteLine("4 Success");

            //close the connection
            Close();
            Console.WriteLine("Connection Closed");
            return -1;
        }

        //public static int AddTrain(Train T)
        //{
            
        //}

        //just for test
        public static int resetUser()
        {
            if(!Connect())
            {
                Console.WriteLine("Failed to Connect to Oracle");
                return (int)SqlErrorCode.ERR_CONN;
            }

            string queryStr = "Delete from T_USER;";
            OdbcCommand sqlcmd = new OdbcCommand(queryStr, conn);

            try
            {
                sqlcmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return (int)SqlErrorCode.ERR_SQLCMD;
            }

            //close the connection
            Close();
            Console.WriteLine("Connection Closed");
            return -1;
        }

        public static void Close()
        {
            if(conn == null)
            {
                return;
            }
            conn.Close();
        }
    }
}

