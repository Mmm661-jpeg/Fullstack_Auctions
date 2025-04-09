import { useContext, useEffect, useState } from "react"
import { useNavigate } from "react-router-dom";

import { UserContext } from "../../context/UserContext/UserContext";

import { loginuserCall,registeruserCall } from "../../service/UsersService";
import "./Login_Register_Page.css"


function Login_Register_Page()
{
    const {username,setUsername} = useContext(UserContext);
    const {setIsLoggedin} = useContext(UserContext)
    const [password,setPassword] = useState("");
    const [loginSuccess,setLoginSuccess] = useState(null);
    const [registerSucces,setRegistersuccess] = useState(null);

    const navigate = useNavigate()


    useEffect(()=>
    {
        if(!username && !password)
        {
            setRegistersuccess(null);
        }
    },[username,password])
    

    const handleLogin = async () =>
    {
        if(!username && !password)
        {
            console.log("Please enter username and password first")
            return;
        }

        let result = await loginuserCall(username,password);

        setLoginSuccess(result); //Sets it locally for message
        if(result)
        {
            setIsLoggedin(true) //set it globaly for other comps to use
            setTimeout(() => {
                navigate("/") //NAv to itemPage 
            }, 500);
        }

    }

    const handleRegister = async () =>
    {
        if(!username && !password)
        {
            console.log("Please enter username and password first")
            return;
        }

        let result = await registeruserCall(username,password);

        setRegistersuccess(result);

    }


    return(
        <>
            <div className="login_register_container"> 
                <h1>Login</h1>
                {registerSucces !== null &&(<h3>{registerSucces === true? "Register succesfull!":"Register failed!"}</h3>)}
                {loginSuccess===false &&(<p>Username or Password incorrect!</p>)}
                <input type="text" placeholder="username.." value={username ?? ""} onChange={(e) =>{setUsername(e.target.value)}}/>
                <input type="text" placeholder="password.." value={password} onChange={(e) =>{setPassword(e.target.value)}}/>
                <button className="login-btn"onClick={()=>{handleLogin()}}>Login</button>
                <button className="register-btn" onClick={()=>{handleRegister()}}>Register</button>
            </div>
        </>
    )
}

export default Login_Register_Page