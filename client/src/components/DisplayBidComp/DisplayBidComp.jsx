import { useContext, useEffect, useState } from "react"


import { AuctionContext } from "../../context/AuctionContext/AuctionContext"
import { viewBidsOnAuction } from "../../service/BidsService";

import { formatDates } from "../../service/HelperMethodsService";

import "./DisplayBidComp.css"

function DisplayBidComp()
{
    const [BidsOnItem,SetBidsOnItem] = useState([]);
    const [isViewing,setIsViewing] = useState(false);
    const {oneAuction} = useContext(AuctionContext); //can also send auctionID as prop instead

    useEffect(()=>
    {
        const CallbidsOnItem = async () =>
        {
            if (!oneAuction?.auctionId) return;
            try
            {
                let result = await viewBidsOnAuction(parseInt(oneAuction.auctionId));

                if(Array.isArray(result) && result.length > 0)
                {
                    console.log("Calling bids on item succesfull in useeffect");
                    SetBidsOnItem(result);
                }
                else
                {
                    console.log("CallbidsOnItem failed either is not array or no bids found")
                }
            }
            catch(errors)
            {
                console.error(errors);
            }
        }

        CallbidsOnItem();

    },[oneAuction])


  
    const makeListItems = (bids) => //Make this a global component reusable for other
    {
        if (bids.length === 0) 
        {
            return <p>No bids placed yet.</p>;  
        }

        let result = bids.map((element) =>
        {
            return(
                <li className="li-item" key={element.bidId}>

                    <strong className="li-row">Bid ID:</strong> {element.bidId} <br />
                    <strong className="li-row">Amount:</strong> {element.bidAmount} <br />
                    <strong  className="li-row">Time:</strong> {formatDates(element.bidTime)} <br />
                    <strong  className="li-row">User ID:</strong> {element.userId} <br />
                    <strong  className="li-row">Auction ID:</strong> {element.auctionId}

                </li>
            )
        })

        return result;
    }

    return(
        <>
            {oneAuction.isOpen &&
            (
                <div className="displayBid-container">
                     <h4 className="bids-title">Bids:</h4>
                     <button className="viewBid-btn" onClick={()=>setIsViewing(prev=>!prev)}>{isViewing? "X" : "☰" }</button>

                     {isViewing && <ul className="ul-container">{makeListItems(BidsOnItem)}</ul>}
                </div>
               

            )}
        </>
    )

}

export default DisplayBidComp