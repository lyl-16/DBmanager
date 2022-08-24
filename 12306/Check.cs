using System.Data;
using Error;
using Containers;

namespace Check
{
        
    class checkUser
    {
        public static readonly char[] BlackList = {'\'', '\"', '\\'};
        public static int checkUserPWD(string UserPWD)
        {
            //check length 6-20
            if(UserPWD.Length < 6 || UserPWD.Length > 20)
            {
                return (int)UErrorCode.ERR_PWDLEN; 
            }

            //check pwd security(must contain 0-9 && a/A-z/Z && other nums)
            bool containNum = false;
            bool containAlp = false;
            bool containOth = false;
            for(int i = 0; i < UserPWD.Length; i++)
            {
                if (!containNum && UserPWD[i] >= '0' && UserPWD[i] <= '9')
                {
                    containNum = true;
                }
                else if (!containAlp && ((UserPWD[i] >= 'a' && UserPWD[i] <= 'z') || (UserPWD[i] >= 'A' && UserPWD[i] <= 'Z')))
                {
                    containAlp = true;
                }
                else
                {
                    //check BlackList
                    for (int j = 0; j < BlackList.Length; j++)
                    {
                        if (UserPWD[i] == BlackList[j])
                        {
                            return (int)UErrorCode.ERR_PWDINVCH;
                        }
                    }
                    if (!containOth)
                    {
                        containOth = true;
                    }
                }
            }
            if (!containOth || !containNum && !containAlp)
            {
                return (int)UErrorCode.ERR_PWDSE;
            }
            return -1;
        }

        public static int checkUserRName(string UserRName)
        {
            //check length 1-30
            if (UserRName.Length < 1 || UserRName.Length > 30)
            {
                return (int)UErrorCode.ERR_NAMELEN;
            }

            //check Chinese Character
            //TODO

            //check BlackList
            for (int i = 0; i < UserRName.Length; i++)
            {
                for (int j = 0; j < BlackList.Length; j++)
                {
                    if (UserRName[i] == BlackList[j])
                    {
                        return (int)UErrorCode.ERR_NAMEINVCH;
                    }
                }
            }

            return -1;
        }

        public static int checkUserPhone(string UserPhone)
        {
            //check length must be 11
            if (UserPhone.Length != 11)
            {
                return (int)UErrorCode.ERR_PHLEN;
            }

            //check security
            for (int i = 0; i < UserPhone.Length; i++)
            {
                if (UserPhone[i] < '0' || UserPhone[i] > '9')
                {
                    return (int)UErrorCode.ERR_PHINVCH;
                }
            }

            return -1;
        }

        public static int checkUserEmail(string UserEmail)
        {
            //check length 1-50
            if (UserEmail.Length < 1 || UserEmail.Length > 50)
            {
                return (int)UErrorCode.ERR_EMALEN;
            }

            //check security only '@' && '.' is legal
            for (int i = 0; i < UserEmail.Length; i++)
            {
                if (!(UserEmail[i] >= '0' && UserEmail[i] <= '9')) // not number
                {
                    if (!((UserEmail[i] >= 'a' && UserEmail[i] <= 'z') || (UserEmail[i] >= 'A' && UserEmail[i] <= 'Z'))) // mpt alpha
                    {
                        if (!(UserEmail[i] == '@' || UserEmail[i] == '.'))
                        {
                            return (int)UErrorCode.ERR_EMAINVCH;
                        }
                    }
                }
            }

            return -1;
        }

        public static int checkUserGender(string UserGender)
        {
            //check length must be 1(can be designed not filled by User)
            //could only be "0" || "1"
            if (UserGender != "0" && UserGender != "1")
            {
                return (int)UErrorCode.ERR_GENINVCH;
            }

            return -1;
        }

        public static int checkUserPID(string UserPID)
        {
            //check length must be 18
            if (UserPID.Length != 18)
            {
                return (int)UErrorCode.ERR_PIDLEN;
            }

            //check security
            //every char should be '0' - '9' except the last one for it could be 'X'
            for (int i = 0; i < UserPID.Length - 1; i++)
            {
                if (UserPID[i] < '0' || UserPID[i] > '9')
                {
                    return (int)UErrorCode.ERR_PIDINVCH;
                }
            }
            if (UserPID[17] != 'X' && (UserPID[17] < '0' || UserPID[17] > '9'))
            {
                return (int)UErrorCode.ERR_PIDINVCH;
            }

            return -1;
        }

        public static int checkUserAddr(string UserAddr)
        {
            //check length 1-50
            if (UserAddr.Length < 1 || UserAddr.Length > 50)
            {
                return (int)UErrorCode.ERR_ADDRLEN;
            }

            //check BlackList
            for (int i = 0; i < UserAddr.Length; i++)
            {
                for (int j = 0; j < BlackList.Length; j++)
                {
                    if (UserAddr[i] == BlackList[j])
                    {
                        return (int)UErrorCode.ERR_ADDRINVCH;
                    }
                }
            }

            return -1;
        }

        public static int checkRegister(User U)
        {
            int ret = -1;

            //check User_Real_Name
            if ((ret = checkUserRName(U.UserRName)) != -1)
            {
                return ret;
            }

            //check User_Password
            if ((ret = checkUserPWD(U.UserPWD)) != -1)
            {
                return ret;
            }

            //check User_Gender
            if ((ret = checkUserGender(U.UserGender)) != -1)
            {
                return ret;
            }

            //check User_Phone
            if ((ret = checkUserPhone(U.UserPhone)) != -1)
            {
                return ret;
            }

            //check User_Email
           /* if ((ret = checkUserEmail(U.UserEmail)) != -1)
            {
                return ret;
            }*/

            //check User_Address
            /*if ((ret = checkUserAddr(U.UserAddr)) != -1)
            {
                return ret;
            }*/

            //check User_PID
            if ((ret = checkUserPID(U.UserPID)) != -1)
            {
                return ret;
            }

            return ret;
        }

        public static int checkLogin(User U)
        {
            int ret = -1;

            //check User_Password
            if ((ret = checkUserPWD(U.UserPWD)) != -1)
            {
                return ret;
            }

            //check User_PID
            if (U.UserPID != null && (ret = checkUserPID(U.UserPID)) != -1)
            {
                return ret;
            }

            //check User_Phone_Number
            if (U.UserPhone != null && (ret = checkUserPhone(U.UserPhone)) != -1)     
            {
                return ret;
            }


            return ret;
        }
    }

    class checkStation
    {
        public static readonly char[] BlackList = {'\'', '\"', '\\'};

        public static int checkStationName(string StationName)
        {
            //check length 1 - 15
            if(StationName.Length < 1 || StationName.Length > 15)
            {
                return (int)StErrorCode.ERR_STLEN;
            }

            //check security
            for(int i = 0; i < StationName.Length; i++)
            {
                for(int j = 0; j < BlackList.Length; j++)
                {
                    if(StationName[i] == BlackList[j])
                    {
                        return (int)StErrorCode.ERR_STINVCH;
                    }
                }
            }

            return -1;
        }

        public static int checkAddSt(Station S)
        {
            int ret = -1;

            if((ret = checkStationName(S.StationName)) != -1)
            {
                return ret;
            }
            return -1;
        }
    }

    class checkTrain
    {
        public static readonly char[] BlackList = {'\'', '\"', '\\'};


    }
}