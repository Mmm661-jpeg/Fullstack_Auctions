import { useContext, useEffect, useState } from "react"
import { searchAuctionsKeyword } from "../../service/SearchService";
import { AuctionContext } from "../../context/AuctionContext/AuctionContext";

import "./SearchComp.css"

function SearchComp() //Must have a debouncer!
{
    const {setIsSearching,setAllAuctions} = useContext(AuctionContext);
    const [keyWord,setKeyWord] = useState("");
    const [debouncer,setDebouncer] = useState(null);

    
    useEffect(()=>
    {
        if (keyWord.trim() === "")  //when "" make allauctions call and restet page
        {
            //we can search all auctions here and set
            setIsSearching(false);
            return;
        }

        setIsSearching(true);

        if (debouncer) 
        {
            clearTimeout(debouncer);
        }

        const newTimeout = setTimeout(async () => {
            await SearchWithKeyword(keyWord);
            setIsSearching(false);
        }, 500); 

        setDebouncer(newTimeout);
      

    },[keyWord])

    

    const handleKeywordChange = (e) =>
    {
        setKeyWord(e.target.value)
    }

    const SearchWithKeyword = async (param) =>
    {
       

        let result = await searchAuctionsKeyword(param);

        if(Array.isArray(result) && result.length > 0)
        {
            setAllAuctions(result); //set all the auction which will then be made into cards in itemCardComp but only briefly refresh leads to the actuall: allauctions
        }
    }


    return(
        <>
         <div className="searchCon">
            <input className="search-inp" type="text" placeholder="search..." value={keyWord} onChange={handleKeywordChange}/>
        </div>
        </>
    )
}

export default SearchComp