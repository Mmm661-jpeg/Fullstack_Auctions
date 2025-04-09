import { useContext, useEffect, useState } from "react"
import { useNavigate } from "react-router-dom";

import "./MyAuctionsComp.css"

import { AuctionContext } from "../../context/AuctionContext/AuctionContext";

import { getMyAuctions } from "../../service/AuctionService";
import { searchAuctionById } from "../../service/SearchService";




function MyAuctionsComp() 
{
    const [myAuctions,setMyAuctions] = useState([]);
    const {setOneAuction} = useContext(AuctionContext);
    const navigation = useNavigate();

    //Set oneComponents nav to itempage and put elemt id in url 

    const CallgetMyAuctions = async () =>
    {
        let result = await getMyAuctions();

        if(result)
        {
            setMyAuctions(result);
        }
    }
    const generateMyCards = () =>
    {
        if (!myAuctions || myAuctions.length === 0) {
            return <p>No Auctions found..</p>;
        }

        else
        {
            return myAuctions.map((element)=>
            {
                return(

                    <div className={element.isOpen?"mycard":"mycard-expi"} key={element.auctionId}>
                        <div className="mycard-text">
                            <h4>{element.auctionName}</h4>
                            <p>{element.auctionDescription}</p>

                        </div>

                        <button className="mycard-btn" onClick={()=>{handleCardClick(element)}}>View</button>

                    </div>
                )
            })
        }
    }

    const SearchOneAuction = async (element) =>
    {
        let result = await searchAuctionById(element.auctionId)

        if(result)
        {
            setOneAuction(result); 
        }
    }


    const handleCardClick = (element) =>
    {
        SearchOneAuction(element); //make api call then set oneAuction to result
        
        setTimeout(() => {
            navigation(`/itempage/${element.auctionId}`)
        }, 1000);
    }

    useEffect(()=>
    {
        CallgetMyAuctions();

    },[])

    return(
        <div className="myAuct-container">
            {generateMyCards()}
        </div>
    )
}

export default MyAuctionsComp