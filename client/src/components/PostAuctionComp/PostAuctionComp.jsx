import { useState } from "react"
import { createauctionCall } from "../../service/AuctionService";

import "./PostAuctionComp.css"

function PostAuctionComp()
{
    const [postMessage,setPostMessage] = useState(null);
    const [succesfullPost,setSuccesfullPost] = useState(null);
    const [auctionObj,setAuctionObj] = useState({
        auctionName:"",
        auctionDescription:"",
        startingPrice:"",
        openingTime:null,
        closingTime:null
    })

    const HandleChange = (e) =>
    {
        setAuctionObj({...auctionObj,[e.target.name]:e.target.value})
    }

    const CallPostAuction = async (auction) =>
    {
        let result = await createauctionCall(auction);
        let message = result.result > 0 ? `Message: ${result.message} ${result.result}` : `Message: ${result.message}`;
        setPostMessage(message);
        setSuccesfullPost(result.result > 0)
      
    }

    const HandleSubmit = (e) =>
    {
        e.preventDefault();
        console.log("Post auction Sending this to requst: " + auctionObj )

        CallPostAuction(auctionObj)
    }


    return(

        <>

        <form className="postform-con" onSubmit={HandleSubmit}>
            <label >Auction Name:</label>
            <input type="text" name="auctionName" value={auctionObj.auctionName} onChange={HandleChange} required/>

            <label>Auction Description:</label>
            <input type="text" name="auctionDescription" value={auctionObj.auctionDescription} onChange={HandleChange} required/>

            <label>Starting Price:</label>
            <input type="text" name="startingPrice" value={auctionObj.startingPrice} onChange={HandleChange} required/>

            <label>Opening Time:</label>
            <input type="datetime-local" name="openingTime" value={auctionObj.openingTime} onChange={HandleChange} required min={new Date().toISOString().slice(0, 16)}/>

            <label>Closing Time:</label>
            <input type="datetime-local" name="closingTime" value={auctionObj.closingTime} onChange={HandleChange} required min={new Date().toISOString().slice(0, 16)}/>

            <button type="submit">Post</button>
        </form>

        {postMessage && (
            <p style={{ color: succesfullPost ? "green" : "red" }}>
                {postMessage}
            </p>
        )}
        </>
    )
}

export default PostAuctionComp