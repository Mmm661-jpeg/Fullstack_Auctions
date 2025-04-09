import { useContext, useEffect, useState } from "react"
import { AuctionContext } from "../../context/AuctionContext/AuctionContext"

import { DeleteAuctionPic } from "../../service/ImageService";
import { AddAuctionPic } from "../../service/ImageService";

import { GetAuctionPic } from "../../service/ImageService";

import noImgimg from "../../assets/no-img-jpeg.jpg"



function ItemPage_ImgComp({isOwner})
{
    const {oneAuction} = useContext(AuctionContext);
    const [hasImage,setHasImage] = useState(false);
    const [imgPath,setImgPath] = useState(noImgimg)
    const [selectedFile, setSelectedFile] = useState(null);

    useEffect(()=>
    {
        const callTheImg = async () =>
        {
            const result = await GetAuctionPic(oneAuction.auctionId);

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

        callTheImg();

    },[oneAuction])
    
    const handleFileChange = (event) =>
    {
        setSelectedFile(event.target.files[0]); 
    };

    const handleUpload = async(event) =>
    {
        event.preventDefault();
        if (!selectedFile) return; 

        const result = await AddAuctionPic(oneAuction.auctionId,selectedFile);

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


        const result = await DeleteAuctionPic(oneAuction.auctionId);

        if(result)
        {
            setImgPath(noImgimg);
            setHasImage(false);
        }
       
    }


    return (
        <>
            <img src={imgPath} alt="auctionPic" />
            
            {isOwner && (
                <>
                    {hasImage ? (
                        <button onClick={handleRemove}>Remove Image</button>
                    ) : (
                        <form onSubmit={handleUpload}>
                            <input type="file" accept="image/*" onChange={handleFileChange} />
                            <button type="submit" disabled={!selectedFile}>Upload Image</button>
                        </form>
                    )}
                </>
            )}
        </>
    );
}
export default ItemPage_ImgComp