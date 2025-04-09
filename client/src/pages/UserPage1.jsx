
import { useState,useContext,useEffect } from "react"
import HeaderComp from "../components/HeaderComponent/HeaderComp";
import SidebarComp from "../components/SideBarComp/SidebarComp";

import PostAuctionComp from "../components/PostAuctionComp/postAuctionComp";
import MyAuctionsComp from "../components/MyAuctionsComp/MyAuctionsComp";
import MyBidsComp from "../components/MyBidsComp/MyBidsComp";
import ProfilePicComp from "../components/ProfilePicComp/ProfilePicComp";
import UpdateUserComp from "../components/UpdateUserComp/UpdateUserComp";

import { getUserWithJwt } from "../service/UsersService";
import { UserContext } from "../context/UserContext/UserContext";

import "./Pages.css"

function UserPage1()
{
    const {setUsername,setLoggedInUserID,username,loggedInUserID} = useContext(UserContext); //not reliable

    const [postFormIsOn,setPostFormIsOn] = useState(false);
    const [myAuctionsIsOn,setmyAuctionsIsOn] = useState(false);
    const [updateuserIsOn,setUpdateUserIsOn] = useState(false);

      useEffect(()=>
        {
                //reason for useEffect: on refresh username was dissapearing.
        
            const CallForUsername = async ()=>
            {
                let result = await getUserWithJwt();
                if(result)
                {
                    setUsername(result.userName); //recall username on refresh
                    setLoggedInUserID(result.userId); //recall userId on refresh

                }
                else
                {
                    console.log("Result in call for username was falsly");
                }
            }
        
            CallForUsername();
        
        },[])
    

    const handlePostFormBtn = () =>
    {
        setPostFormIsOn(prev=>!prev);
    }

    const handleMyAuctionsBtn = () =>
    {
        setmyAuctionsIsOn(prev=>!prev);
    }

    const handleUpdateUserBtn = () =>
    {
        setUpdateUserIsOn(prev=>!prev)
    }

    return(
        <>
        <HeaderComp/>
        <SidebarComp/>
        <div className="container">
            <div className="userP-upperleft">
                <ProfilePicComp/>
                <div className="user-info">
                <h3>Username: @{username}</h3>
                <button onClick={handleUpdateUserBtn}>{updateuserIsOn?"Cancel":"Update Account"}</button>
                </div>
                {updateuserIsOn &&(<UpdateUserComp/>)}
            </div>

            <div>
                <div className="userp-postauc-cont">
                    <button onClick={handlePostFormBtn}>{postFormIsOn?"Cancel":"Post Auction"}</button>
                    {postFormIsOn &&(<PostAuctionComp/>)}
                </div>

              
            </div>

            <div>
              <MyBidsComp/>
            </div>

            <div>
            <p>My Auctions:</p>
            <button onClick={handleMyAuctionsBtn}>{myAuctionsIsOn?"X":"☰"}</button>
            {myAuctionsIsOn &&(<MyAuctionsComp/>)}
            </div>
        </div>
        </>
    )

}

export default UserPage1