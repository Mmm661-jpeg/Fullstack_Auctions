import { useEffect, useState } from "react";
import { UserContext } from "./UserContext";

const UserProvider = ({children}) =>
{
    const [username,setUsername] = useState(null); 
    const [isLoggedin,setIsLoggedin] = useState(false);
    const [loggedInUserID,setLoggedInUserID] = useState(null);

  


    const decodeToken = (token) =>
    {
        try
        {
            if (!token) return null; 

            let ThedecodedToken = JSON.parse(atob(token.split(".")[1]));
            return ThedecodedToken;
        }
        catch(errors)
        {
            console.error("Error decoding token:", errors)
            return null;
        }
    }

 

    const validatetoken = (decodedJWT) =>
    {
        try
        {
            if (!decodedJWT || !decodedJWT.exp) return false;

            const isExpired = decodedJWT.exp * 1000 < Date.now();
            return !isExpired;
     
        }
        catch(errors)
        {
            console.log(errors);
            return false;
        }
    }

    useEffect(()=>
    {
        const token = localStorage.getItem("token");
        if (!token) {
            setIsLoggedin(false);
            setLoggedInUserID(null);
            return;
        }

        let decodedJWT = decodeToken(token);

        if(token && validatetoken(decodedJWT))
        {
            setIsLoggedin(true);
            setLoggedInUserID(decodedJWT?.UserID || null);
        }
        else
        {
            setIsLoggedin(false);
            setLoggedInUserID(null)
            localStorage.removeItem("token"); 
        }

    },[isLoggedin])


    return(
        <UserContext.Provider value={{username,setUsername,isLoggedin,setIsLoggedin,loggedInUserID,setLoggedInUserID}}>
            {children}
        </UserContext.Provider>
    )
}

export default UserProvider