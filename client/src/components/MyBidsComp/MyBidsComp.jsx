import { useEffect, useState } from "react";
import { getMyBids } from "../../service/BidsService";
import { formatDates } from "../../service/HelperMethodsService";


function MyBidsComp()
{
    const [myBids,setMyBids] = useState([])
    const [viewBidsIsOn,setViewBidsIsOn] = useState(false);


    useEffect(()=>
    {
        const callGetMyBids = async () =>
        {
            let result = await getMyBids();
            setMyBids(result);
        }

        callGetMyBids();

    },[viewBidsIsOn])

    

    const makeListItems = (bids) => //Make this a global component reusable for other
    {
        if (bids.length === 0) 
        {
            return <p>No bids placed yet.</p>;  
        }

        let result = bids.map((element) =>
        {
            return(
                <li key={element.bidId}>

                    <strong>Bid ID:</strong> {element.bidId} <br />
                    <strong>Amount:</strong> {element.bidAmount} <br />
                    <strong>Time:</strong> {formatDates(element.bidTime)} <br />
                    <strong>User ID:</strong> {element.userId} <br />
                    <strong>Auction ID:</strong> {element.auctionId}

                </li>
            )
        })

        return result;
    }




    return(
        <div className="displayBid-container">
            <h4 className="bids-title">Bids:</h4>
            <button className="viewBid-btn" onClick={()=>setViewBidsIsOn(prev=>!prev)}>{viewBidsIsOn? "X" : "☰" }</button>

            {viewBidsIsOn && <ul className="ul-container">{makeListItems(myBids)}</ul>}

        </div>
    )
}

export default MyBidsComp