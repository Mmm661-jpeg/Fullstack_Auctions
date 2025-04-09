const baseUrl = "http://localhost:5120/api/Images"

const apiCall = async (endpoint,method,body=null,auth=false,FileUpload=false) =>
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

    let bodyContent = body;

    if(FileUpload)
    {
        bodyContent = new FormData();

        Object.keys(body).forEach((key) => {
            bodyContent.append(key, body[key]);
        });
    }

    else if(body)
    {
        headers["Content-Type"] = "application/json";
        bodyContent = JSON.stringify(body);
    }

    try
    {
        const response = await fetch(`${baseUrl}/${endpoint}`,
            {
                method:method,
                headers:headers,
                body:bodyContent
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

export const AddAuctionPic = async (id,file) =>
{
    const formData = {ID:id,File:file}

    const data = await apiCall("AddAuctionPic","POST",formData,true,true);

    console.log(data?.message);

    return data.result ?? false;
}

export const AddUserProfilePic = async (file) =>
{
    const formData = {ID:0,File:file}

    const data = await apiCall("AddUserProfilePic","POST",formData,true,true);

    console.log(data?.message);

    return data.result ?? false;
}

export const DeleteAuctionPic = async (auctionID) =>
{
    const data = await apiCall(`DeleteAuctionPic?auctionID=${auctionID}`,"DELETE",null,true,false);

    console.log(data?.message);

    return data.result ?? false;
}

export const DeleteUserProfilePic = async () =>
{
    const data = await apiCall("DeleteUserProfilePic","DELETE",null,true,false);

    console.log(data?.message);

    return data?.result ?? false;
}

export const GetAuctionPic = async(auctionID) =>
{
    const data = await apiCall(`GetAuctionPic?auctionID=${auctionID}`, "GET", null, false, false);

    console.log(data?.message);

    if(data.result)
    {
        return `${data.result}`  //"http://localhost:5120/uploads/auctionspics/1fb5b574-cbab-4f91-830b-5afa530b94a3.png"
    }
    else
    {
        console.error("Error getting auction image");
        return null;
    }

} 

export const GetProfilePic = async (userid) =>
{
    const data = await apiCall(`GetProfilePic?userid=${userid}`, "GET", null, false, false);

    console.log(data?.message);

    if(data.result) 
    {
        return `${data.result}` //"result": "/uploads/userpics/50cf887c-3563-4c2e-97af-c3f8c167d09f.jpg"
                                            
    }
    else
    {
        console.error("Error getting auction image");
        return null;
    }

}
