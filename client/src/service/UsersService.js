const baseUrl = "http://localhost:5120/api/Users"

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





export const loginuserCall = async (username,password) =>
    {
        if (!username || !password) {
            console.error("Null values not acceptable when logging in!");
            return false;
        }

        const data = await apiCall("Login","POST",{username,password});
    
        if(data.token)
        {
            localStorage.setItem("token",data.token);
            console.log("Login succesfull")
            return true;
        }
        else
        {
            return false;
        }
    }
    
export const registeruserCall = async (username,password) =>
    {
        if (!username || !password) {
            console.error("Null values not acceptable!");
            return false;
        }

        const data = await apiCall("registerUser", "POST", { username, password });

        console.log(data?.message);
        return data?.result || false;

      
    }
    
export const deleteuserCall = async (id) =>
    {
        if (!id || isNaN(id)) {
            console.error("Invalid ID provided for deletion.");
            return;
        }
    
       const data = await apiCall(`DeleteUser?id=${encodeURIComponent(id)}`, "DELETE");
    
        console.log(data?.message)
    
        return data?.result || false
    
    }
    
export const updateuserCall = async (username,password) =>
    {
        if (!username && !password) {
            console.error("Both Username and Password cant be null");
            return;
        }
        const data = await apiCall("UpdateUser","PUT",{username,password},true);

        console.log(data?.message)
    
        return data?.result || false
    
    }
    
export const getUserWithJwt = async () =>
    {
        const data = await apiCall("GetUserWithJwt","GET",null,true);

        console.log(data?.message);

        return data?.result || null;
    }