import { useContext, useEffect, useState } from "react"
import { useParams } from "react-router-dom";


import { AuctionContext } from "../context/AuctionContext/AuctionContext";
import { UserContext } from "../context/UserContext/UserContext";
import HeaderComp from "../components/HeaderComponent/HeaderComp";
import SidebarComp from "../components/SideBarComp/SidebarComp";
import DisplayoneItemComp from "../components/DisplayoneItemComp/DisplayoneItemComp";


import { searchAuctionById } from "../service/SearchService";

import "./Pages.css"



function ItemPage1()
{
    //Refresh issue
    //Save id in url and make another api call when we mount?
    //or when userlog status changes refrehs and call the auction again
    const {oneAuction,setOneAuction} = useContext(AuctionContext);
    const {isLoggedin} = useContext(UserContext)
    const {id} = useParams()

    const [isLoading,setIsLoading] = useState(true);

    useEffect(()=>
    {
        const makeAuctionCall = async () => 
        {
            try 
            {
                setIsLoading(true);
                let result = await searchAuctionById(id);
           
                setOneAuction(result);
                console.log("Auction reset");
                
            }   
        
            catch (error) 
            {
                console.error("Error fetching auction:", error);
            }
            finally
            {
                setIsLoading(false);
            }
        }

        makeAuctionCall();

    },[id,isLoggedin])

    if (isLoading) return <p>Loading...</p>;
    if (!oneAuction) return <p>Auction not found.</p>;


  return(
    <>
     <HeaderComp/>
     <SidebarComp/>
    <div className="container">
        <DisplayoneItemComp/> 
    </div>
    </>
  )
}

export default ItemPage1