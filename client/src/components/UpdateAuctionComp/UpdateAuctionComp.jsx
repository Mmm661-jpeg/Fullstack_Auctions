import { useState } from "react"
import { updateauctionCall } from "../../service/AuctionService";
import { AuctionContext } from "../../context/AuctionContext/AuctionContext";
import { useContext } from "react";

import "./UpdateAuctionComp.css"

function UpdateAuctionComp()
{
   
    const {oneAuction} = useContext(AuctionContext);

    const [apiMessage,setApiMessage] = useState(null);
    const [updateSuccess,setUpdateSuccess] = useState(null);
    const [updateObj,setUpdateObj] = useState({
        auctionId: parseFloat(oneAuction.auctionId),
        auctionName: null,
        auctionDescription: null,
        startingPrice:null,
        closingTime:null

    })

    const CallUpdate = async (theUpdateObj) =>
    {
        try
        {
            let response = await updateauctionCall(theUpdateObj);
            if(response === true)
            {
                setApiMessage("Succesfull update!");
                setUpdateSuccess(true);
            }
            else
            {
                setApiMessage("Update failed!");
                setUpdateSuccess(false);
            }
        }
        catch(errors)
        {
            console.error(errors);
            alert("Error updating auction!");
        }
    }

    const HandleInputChange = (e) =>
    {
        setUpdateObj({...updateObj,[e.target.name]:e.target.value})
    }

    const HandleUpdateSubmit = (e) =>
    {
        e.preventDefault();
        console.log(oneAuction.auctionId)
        CallUpdate(updateObj);
    }

    return(
        <>

        <form className="updateform-con" onSubmit={HandleUpdateSubmit}>
            <label> Auction Name: </label>
            <input type="text" name="auctionName" value={updateObj.auctionName} onChange={HandleInputChange}/>

            <label> Auction Description: </label>
            <input type="text" name="auctionDescription" value={updateObj.auctionDescription} onChange={HandleInputChange}/>

            <label> Starting Price: </label>
            <input type="number" name="startingPrice" value={updateObj.startingPrice} onChange={HandleInputChange}/>

            <label> Closing Time </label>
            <input type="datetime-local" name="closingTime" value={updateObj.closingTime} onChange={HandleInputChange} min={new Date().toISOString().slice(0, 16)}/>

            <button type="submit">Update</button>
            
        </form>

        {apiMessage &&(
            <p style={{color: updateSuccess ? "green":"red"}}>
                {apiMessage}
            </p>
        )}
        </>
    )
}

export default UpdateAuctionComp