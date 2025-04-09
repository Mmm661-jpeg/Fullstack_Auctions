const baseUrl = "http://localhost:5120/api/Search"

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

export const  searchAuctionById = async (auctionID) =>
{
    const data = await apiCall(`SearchAuctionById?auctionID=${encodeURIComponent(auctionID)}`,"GET") 

    console.log(data?.message);

    return data ?? null;
}

export const searchAuctionsKeyword = async (keyword) => //parse as int here better?
{
    const theEndPoint = `SearchAuctionsKeyword?keyword=${encodeURIComponent(keyword)}`
    const data = await apiCall(theEndPoint,"GET");

    return data ?? [] //[{},{}]
}

