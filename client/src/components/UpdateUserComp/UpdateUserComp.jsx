import { useContext, useState } from "react"
import { UserContext } from "../../context/UserContext/UserContext";

import { updateuserCall } from "../../service/UsersService";

import "./UpdateUserComp.css"

function UpdateUserComp()
{
    
    const {username} = useContext(UserContext);
    const [updateSucces,setupdateSucces] = useState(null);
    const [apiMessage,setApiMessage] = useState(null);

    const [updateObj,setUpdateObj] = useState({
        username: username,
        password: null

    })

    const callUpdateUser = async (username,password) =>
    {
        const result = await updateuserCall(username,password);

        if(result)
        {
            setupdateSucces(true);
            setApiMessage("Succesfull update!");
        }
        else
        {
            setupdateSucces(false);
            setApiMessage("Update failed!");
        }
    }

    const handleInpChange = (event) =>
    {
        setUpdateObj({...updateObj,[event.target.name]:event.target.value})
    }

    const handleSubmit = (event) =>
    {
        event.preventDefault();

        callUpdateUser(updateObj.username,updateObj.password);
    }

    return(
        <>
        <form className="upd-usr-form" onSubmit={handleSubmit}>

            <label>Username: </label>
            <input type="text" name="username" value={updateObj.username} onChange={handleInpChange}/>

            <label>Password: </label>
            <input type="text" name="password" value={updateObj.password} onChange={handleInpChange}/>

            <button type="submit">Update</button>
        </form>

        {apiMessage &&(
            <p style={{color: updateSucces ? "green":"red"}}>
                {apiMessage}
            </p>
        )}
        </>
    )
}

export default UpdateUserComp