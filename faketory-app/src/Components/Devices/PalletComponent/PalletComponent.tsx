import Pallet from "./Types";
import "./styles.css"

interface Props{
    pallet:Pallet;
}


const PalletComponent = ({pallet} : Props) => {

    const tileSize: number = 3.2;



    return(
        <div
            className="pallet-base"
            style={{
                position: `absolute`,
                bottom:`${((pallet.posY+0.125)*tileSize)}vw`,
                left:`${((pallet.posX+0.125)*tileSize)}vw`,
                width: `${tileSize*0.75}vw`,
                height: `${tileSize*0.75}vw`,
            }}
        />
    )
}
export default PalletComponent;