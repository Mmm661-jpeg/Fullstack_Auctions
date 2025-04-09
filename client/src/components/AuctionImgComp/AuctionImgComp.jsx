import { useContext, useEffect, useState } from "react";



import { GetAuctionPic } from "../../service/ImageService";

import reactImg from "../../assets/react.svg"
import noImgimg from "../../assets/no-img-jpeg.jpg"

function AuctionImgComp({element}) //This is for imageCard
{
    
     const [auctionImgPath,setAuctionImgPath] = useState(noImgimg);
    


        useEffect(()=>
        {
            const callForauctionImg = async () =>
            {
                const path = await GetAuctionPic(element.auctionId);

                if(path)
                {
                    setAuctionImgPath(path);
                }
                else
                {
                    setAuctionImgPath(noImgimg);
                }
               
               
            }

            callForauctionImg();

        },[element])

    return(
        <>
        <img src={auctionImgPath} alt="auctionPic" />
        </>
    )

}

export default AuctionImgComp