namespace Error
{
    enum SqlErrorCode{
        ERR_CONN,
        ERR_SQLCMD,
    }

    enum RegErrorCode{
        ERR_UEXIST,
        ERR_PHEXIST
    }

    enum LoginErrorCode{
        ERR_UUNEXIST,
        ERR_PWD
    }

    enum StErrorCode{
        ERR_STLEN,
        ERR_STINVCH
    }
    
    enum UErrorCode{
        ERR_PWDLEN,
        ERR_PWDSE,
        ERR_PWDINVCH,
        ERR_NAMELEN,
        ERR_NAMEINVCH,
        ERR_PHLEN,
        ERR_PHINVCH,
        ERR_EMALEN,
        ERR_EMAINVCH,
        ERR_GENINVCH,
        ERR_PIDLEN,
        ERR_PIDINVCH,
        ERR_ADDRLEN,
        ERR_ADDRINVCH,
    }

    
}