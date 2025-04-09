import { useContext, useEffect, useState } from "react"
import { NavLink } from "react-router-dom"


import { UserContext } from "../../context/UserContext/UserContext";

import "./Header.css"

function HeaderComp()
{
   
    const {isLoggedin} = useContext(UserContext)
 
   

   


    
    return(
        <>
            <header>
            <nav>
               <NavLink to="/" className={({isActive})=>(isActive ?"nav-link active-link":"nav-link idle-link")}>
                Main Page
               </NavLink>

               <NavLink to="/loginpage" className={({isActive})=>(isActive ?"nav-link active-link":"nav-link idle-link")}>
                Login/Register
               </NavLink>

               {isLoggedin &&(
                <NavLink to="/userpage" className={({isActive})=>(isActive ?"nav-link active-link":"nav-link idle-link")}>
                My Account
               </NavLink>
               )}

             
            </nav>

            
            </header>

          
        </>
    )
}

export default HeaderComp