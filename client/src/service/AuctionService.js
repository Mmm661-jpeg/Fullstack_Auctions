const baseUrl = "http://localhost:5120/api/Auction"

const apiCall = async (endpoint,method,body=null,auth=false) =>
{
    const headers = {"Accept":"application/json"}

    if(auth)
    {
        const token = localStorage.getItem("token");

        if(!token)
        {
            console.error("No token found!");
            return null;
        }

        headers["Authorization"] = `Bearer ${token}`
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

export const createauctionCall = async (createActionObj) =>
{
    const data = await apiCall("CreateAuction","POST",createActionObj,true);

    console.log(data?.message);

    return data ?? null //Ändra till result false true backend obj används för loggning

}

export const deleteautionCall = async (auctionId) =>
{
    const data = await apiCall("DeleteAuction","DELETE",{auctionId},true);

    console.log(data?.message)

    return data ?? null //Only return result eventually else false
}

export const updateauctionCall = async (updateauctionObj) =>
{
    //nulls taken care of in sql
    //Object needs id,and userid rest can be null
    const data = await apiCall("UpdateAuction","PUT",updateauctionObj,true);

    console.log(data?.message)

    return data.result ?? false

}

export const getAllauctionsCall = async () =>
{
    const data = await apiCall("GetAllAuctions","GET");

    data.result?.sort((a,b) => b.isOpen - a.isOpen); 

    console.log(data?.message)

    return data.result ?? [];

}

export const getMyAuctions = async () =>
{
    const data = await apiCall("GetMyAuctions","GET",null,true);

    data.result?.sort((a,b) => b.isOpen - a.isOpen); //closed ones should be removed in back-end

    console.log(data?.message)

    return data.result ?? []

}


