import { NavLink } from "react-router-dom";
import { useContext, useEffect, useState } from "react";
import { UserContext } from "../../context/UserContext/UserContext";


import { getUserWithJwt } from "../../service/UsersService";

import "./SidebarComp.css"



function SidebarComp()
{
    const [sidebarOpen,setSideBarOpen] = useState(false);
    const {isLoggedin,setIsLoggedin,username,setUsername} = useContext(UserContext);

    useEffect(()=>
    {
        //reason for useEffect: on refresh username was dissapearing.

        const CallForUsername = async ()=>
        {
            let result = await getUserWithJwt();
            if(result)
            {
                setUsername(result.userName); //does id need to be set? hmm
            }
            else
            {
                console.log("Result in call for username was falsly");
            }
        }

        CallForUsername();

    },[])

    const handleSidebarClick = () =>
    {
        setSideBarOpen(prev=>!prev)
    }

    const handleLogout = () =>
    {
        setIsLoggedin(false);
        localStorage.removeItem("token");
    }

    return (
        <>
            {/* Sidebar Toggle Button */}
            <button className="sidebar-btn" onClick={handleSidebarClick}>☰</button>

            {/* Sidebar Menu */}
            {sidebarOpen && (
                <div className="sidebar">

                    <button className="close-btn" onClick={handleSidebarClick}>X</button>
                   

                    {isLoggedin ? (
                        <div className="sidebar-content">
                            <img src="" alt="ProfilePic" />
                            <p>@{username}</p>
                            <NavLink to="/userpage">My Account</NavLink>
                            <button className="logout-btn" onClick={handleLogout}>Logout</button>
                        </div>
                    ) : (
                        <div className="sidebar-content">
                            <NavLink to="/loginpage">Login or Register</NavLink>
                        </div>
                    )}
                </div>
            )}
        </>
    );


}

export default SidebarComp