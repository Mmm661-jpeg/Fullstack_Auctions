const baseUrl = "http://localhost:5120/api/Bids"

const apiCall = async (endpoint,method,body=null,auth=false) =>
{
    const headers = {"Accept":"application/json"};

    if(auth)
    {
        const token = localStorage.getItem("token");
        if(!token)
        {
            console.error("No token found!");
            return null;
        }
        
        headers["Authorization"] = `Bearer ${token}`;
    }

    if(body)
    {
        headers["Content-Type"] = "application/json";
    }

    try
    {
        const response = await fetch(`${baseUrl}/${endpoint}`,
            {
                method:method,
                headers:headers,
                body:body ? JSON.stringify(body) : null
            }
        );

        if(!response.ok)
        {
            const errorMessage = await response.text();
            console.error(`HTTP error: ${response.status}: ${errorMessage}`);
            return null;
        }
        else
        {
            return await response.json();
        }
    }

    catch(errors)
    {
        console.error(`Network error before making api call: ${errors.message}`);
        return null;
    }
}

export const makeBid = async (auctionId,amount) =>
    {
        const data = await apiCall("MakeBid","POST",{auctionId,amount},true);

        console.log(data?.message)
        return data ?? false
    }

export const RemoveBid = async (bidID) =>
    {
        const data = await apiCall("RemoveBid","DELETE",{bidID},true);

        console.log(data?.message);

        return data?.result ?? false;
    }

export const getMyBids = async () =>
    {
        const data = await apiCall("GetMyBids","GET",null,true);

        console.log(data?.message);

        return data.result ?? [] //if null = error return empty array

    }
        
export const viewBidsOnAuction = async (auctionID) =>
    {
        const data = await apiCall(`ViewBidsOnAuction?auctionID=${encodeURIComponent(auctionID)}`,"GET");

        console.log(data?.message)
        return data?.result ?? [] //if null = error return empty array

    }
        
        
    

