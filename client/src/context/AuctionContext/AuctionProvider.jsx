import { useState } from "react"
import { AuctionContext } from "./AuctionContext";


const AuctionProvider = ({children}) =>
{
    const [allAuctions,setAllAuctions] = useState([]);
    const [oneAuction,setOneAuction] = useState(null);
    const [isSearching,setIsSearching] = useState(false);


    return(
        <AuctionContext.Provider value = {{allAuctions,setAllAuctions,oneAuction,setOneAuction,isSearching,setIsSearching}}>
            {children}
        </AuctionContext.Provider>
    )
}

export default AuctionProvider