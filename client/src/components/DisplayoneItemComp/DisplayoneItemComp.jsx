import { useState,useEffect} from "react";
import { useContext } from "react";

import { UserContext } from "../../context/UserContext/UserContext";
import { AuctionContext } from "../../context/AuctionContext/AuctionContext";

import ItemPage_BtnComp from "../ItemPage_BtnComp/ItemPage_BtnComp";
import DisplayBidComp from "../DisplayBidComp/DisplayBidComp";
import ItemPage_ImgComp from "../ItemPage_ImgComp/ItemPage_ImgComp";

import { formatDates } from "../../service/HelperMethodsService";

import "./DisplayoneItemComp.css"



function DisplayoneItemComp() //is loggedin is owner etc should be set here then sent in as props.
{
    const {oneAuction} = useContext(AuctionContext); 
    const {loggedInUserID,isLoggedin} = useContext(UserContext);
    const [isOwner,setIsOwner] = useState(false); //Turn into global prop?

    
    const CheckConditions = () => //Logged in or not, Owner or not
            {
                
                setIsOwner(isLoggedin && oneAuction?.userId && Number(loggedInUserID) === Number(oneAuction.userId))
            }
        
    
    useEffect(()=>
    {
        if (oneAuction) 
        {
            CheckConditions();
        }
                  
        
    },[loggedInUserID, isLoggedin, oneAuction])



   

        
    
        

       
        
    
    return(
    <>
      <div className="img-con">
            <ItemPage_ImgComp isOwner={isOwner}/>
            <p>Auction ID: {oneAuction.auctionId}</p>
        </div>
   
        <div className="item-card"> 

            <div className="item-card-det-1">
                <h2>{oneAuction.auctionName}</h2>
                <p>{oneAuction.highestBidAmount? oneAuction.highestBidAmount : oneAuction.startingPrice}£</p>   
                <ItemPage_BtnComp isOwner={isOwner}/>
                <p>Closing time: {formatDates(oneAuction.closingTime)}</p>
                <br />
                <h3>Description:</h3>
                <p>{oneAuction.auctionDescription}</p>
                <p>Starting Price: {oneAuction.startingPrice}</p>
                <p>Opening time: {formatDates(oneAuction.openingTime)}</p>
                <p>UserID:{oneAuction.userId}</p>
                
            </div>

            <DisplayBidComp/>


            
            
        </div>

    </>
    
)
}

export default DisplayoneItemComp