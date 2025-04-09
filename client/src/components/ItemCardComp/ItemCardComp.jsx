import { useContext } from "react";

import { AuctionContext } from "../../context/AuctionContext/AuctionContext";
import "./ItemCardComp.css"
import { useNavigate } from "react-router-dom";

import AuctionImgComp from "../AuctionImgComp/AuctionImgComp";

import { searchAuctionById } from "../../service/SearchService";

function ItemCardComp()
{
   
    const { allAuctions, setOneAuction } = useContext(AuctionContext);

    const navigation = useNavigate();

    const SearchOneAuction = async (element) =>
        {
            let result = await searchAuctionById(element.auctionId)

            if(result)
            {
                setOneAuction(result); 
            }
        }

    const HandleCardClick = async (element) =>
    {
        
        await SearchOneAuction(element); //make api call then set oneAuction to resul
        
        //Take element.auctionId and set it in the url
        setTimeout(() => {
            navigation(`/itempage/${element.auctionId}`)
        }, 1000);
       
    }

    const generateCards =  () => //Make into component for reusability
    {
        if(allAuctions.length > 0)
        {
            return allAuctions.map((element)=>
            {
                
                return(
                    
                    <div className={element.isOpen?"card":"card-expi"} key={element.auctionId}>
                        <div className="card-img">
                        <AuctionImgComp element ={element}/>
                        </div>
                        <div className="card-body">
                            <h4>{element.auctionName}</h4>
                            <p>{element.auctionDescription}</p>
                            <button onClick={()=>{HandleCardClick(element)}}>View</button>

                        </div>

                    </div>
                    
                )
            })
        }

        else
        {
            return(<h1>No auctions available... {allAuctions.length}</h1>)
           
        }

    }


    return(
        <>
        {generateCards()}
        </>
    )
}

export default ItemCardComp