import { useContext, useEffect, useState } from "react"

import { GetProfilePic } from "../../service/ImageService";
import { AddUserProfilePic } from "../../service/ImageService";
import { DeleteUserProfilePic } from "../../service/ImageService";



import noImgimg from "../../assets/no-img-jpeg.jpg"

import "./ProfilePicComp.css"
import { UserContext } from "../../context/UserContext/UserContext";

function ProfilePicComp()
{
    const {loggedInUserID} = useContext(UserContext); //not reliable

    const [imgPath,setImgPath] = useState(noImgimg);
    const [hasImage,setHasImage] = useState(false);
    const [selectedFile,setSelectedFile] = useState(null);

   


   
    useEffect(()=>
    {
        const callTheimg = async() =>
        {
            const result = await GetProfilePic(loggedInUserID); //needs id
          

            if(result)
            {
                setImgPath(result);
                setHasImage(true);
            }
            else
            {
                setImgPath(noImgimg)
                setHasImage(false);
            }
        }

        callTheimg();

    },[loggedInUserID])

    const handleFileChange = (event) =>
    {
        setSelectedFile(event.target.files[0]); 
    };

     const handleUpload = async(event) =>
    {
        event.preventDefault();
        if (!selectedFile) return; 
    
        const result = await AddUserProfilePic(selectedFile);
    
        if(result)
        {
            setHasImage(true)
            //useffect takes care of getting image after a refresh.
        }
        else
        {
            setHasImage(false);
        }
    
    }
    
    const handleRemove = async () =>
    { 
        if(!hasImage) {return} //might aswell return directly saves on api calls
    
    
        const result = await DeleteUserProfilePic();
    
        if(result)
        {
            setImgPath(noImgimg);
            setHasImage(false);
        }
           
    }

        


    return (
        <div className="img-con">
            <img src={imgPath} alt="profilepic" />

            {hasImage ? 
            (
                <button onClick={handleRemove}>Remove Image</button>
                ) : (
                        <form onSubmit={handleUpload}>
                            <input type="file" accept="image/*" onChange={handleFileChange} />
                            <button type="submit" disabled={!selectedFile}>Upload Image</button>
                        </form>
                    )}
          
        </div>
    );
}

export default ProfilePicComp