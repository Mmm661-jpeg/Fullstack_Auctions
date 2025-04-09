import { useContext, useState,useEffect } from "react";
import { useNavigate } from "react-router-dom";

import { UserContext } from "../../context/UserContext/UserContext";
import { AuctionContext } from "../../context/AuctionContext/AuctionContext";

import { deleteautionCall } from "../../service/AuctionService";
import { makeBid } from "../../service/BidsService";


import UpdateAuctionComp from "../UpdateAuctionComp/UpdateAuctionComp";

import "./ItemPage_BtnComp.css"

//Need to be seperated into compenents and return must be madde cleaner.

function ItemPage_BtnComp({isOwner})
{
    const {isLoggedin,loggedInUserID} = useContext(UserContext);
    const{oneAuction} = useContext(AuctionContext);

    const [formIsOpen,setFormIsopen] = useState(false);
    const [bidResult,setBidResult] = useState({ message: "Place your bid above." });
    const [amount,setAmount] = useState(0);
  

    const [currentTime,setCurrentTime] = useState(new Date());

    const navigation = useNavigate();


    useEffect(()=>
    {
        const interval = setInterval(()=>
        {
            setCurrentTime(new Date());

        },5000) //can be more accurate

        return () => clearInterval(interval);
    },[])

    
    const auctionOpeningTime = oneAuction?.openingTime ? new Date(oneAuction.openingTime) : null;

    
        
   
    

    const PlaceBid = async (amount) =>
    {
        let resultObj = await makeBid(parseInt(oneAuction.auctionId),parseInt(amount));
        console.log("Make bid result: " + resultObj.message);
        setBidResult(resultObj);
        setFormIsopen(false);
    }

    const HandleDelete = async () =>
    {
        try
        {
            const response = await deleteautionCall(parseInt(oneAuction.auctionId));

            if(response.result)
            {
                alert(response.message);
                navigation("/")
            }
            else
            {
                alert(response.message);
            }
         
        }
        catch
        {
            console.error("Error deleting auction:", error);
            alert("An error occurred while deleting the auction.");
        }
    }

    return (
        <>

         <div className="itemp-btn-top-con">
            {isOwner ? ( // ✅ Owner can always edit or delete
                <>
                    <button onClick={() => setFormIsopen(prev=>!prev)}>{formIsOpen ?"Cancel":"Edit Auction"}</button>
                    <button onClick={HandleDelete}>Delete</button>
                </>
            ) : (
                oneAuction.isOpen && auctionOpeningTime <= currentTime ? ( // ✅ Only show "Bid" button when auction is open
                    !isLoggedin ? ( 
                        <button>Login to Bid</button>
                    ) : (
                        <button onClick={() => setFormIsopen(prev=>!prev)}>{formIsOpen?"Cancel":"Place a Bid"}</button>
                    )
                ) : null
            )}

            
                {isOwner ? 
                  
                    (
                        formIsOpen &&(
                            <div className="itempage-updauc-cont">
                            <UpdateAuctionComp/>
                            </div>
                        )
                        
                    )
                    :
                 
                    (
                       
                        formIsOpen ? (

                            
                            <div className="bid-form">
                                <input
                                    type="number"
                                    placeholder="Enter bid amount"
                                    min="1"
                                    onChange={(e) => setAmount(e.target.value)} 
                                />
                                <button onClick={() => PlaceBid(amount)}>Submit Bid</button>
                            </div>
                        )
                        :
                        (<p>{bidResult.message}</p>)
                        

                       
                        
                    )
                }
            

        </div>
           
        </>
    );
}


export default ItemPage_BtnComp