import { useContext, useEffect, useState } from "react"

//import MainCardComp from "../MainCardComponent/MainCardComp";
import ItemCardComp from "../components/ItemCardComp/ItemCardComp";

import "./Pages.css"


import { AuctionContext } from "../context/AuctionContext/AuctionContext";
import { getAllauctionsCall } from "../service/AuctionService";
import HeaderComp from "../components/HeaderComponent/HeaderComp";
import SidebarComp from "../components/SideBarComp/SidebarComp";
import SearchComp from "../components/SearchComp/Searchcomp";



function MainPage1()
{
    const {allAuctions,setAllAuctions,isSearching} = useContext(AuctionContext);
    const [auctionsLoaded,setAuctionsLoaded] = useState(null)

    const getTheAuctions = async () =>
    {
        if(!isSearching)
        {
            let result = await getAllauctionsCall();

            if(Array.isArray(result) && result.length > 0)
            {
                setAllAuctions(result);
                setAuctionsLoaded(true);
            }
            else
            {
                setAuctionsLoaded(false);
            }
        }
           
    }


  

    useEffect(()=>
    {
        getTheAuctions();
    
    },[])

    return(
    <>
        <HeaderComp/>
        <SidebarComp/>
        <SearchComp/>
        {auctionsLoaded !== null &&(<h3>{auctionsLoaded === true ? `${allAuctions.length} items found`:`loading..`}</h3>)}
        <div className="container">
            <ItemCardComp />
        </div>
    </>
    )
 
}

export default MainPage1