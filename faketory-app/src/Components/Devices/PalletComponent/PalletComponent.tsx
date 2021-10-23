import Pallet from "./Types";
import "./styles.css"
import { setPalletToModify } from "../../../States/menuBar/deviceToModify/palletToModify/actions";
import { useDispatch } from "react-redux";
import { setOpenedTab } from "../../../States/menuBar/openedTab/actions";
import { setOpenedDevicesSubtab } from "../../../States/menuBar/openedDevicesSubtab/actions";
import { DeviceTabState } from "../../MenuBar/DevicesTab/EDevicesTabState";

interface Props{
    pallet:Pallet;
}


const PalletComponent = ({pallet} : Props) => {

    const tileSize: number = 3.2;
    const dispatch = useDispatch();

    const handleClick = () => {
        dispatch(setPalletToModify(pallet));
        dispatch(setOpenedTab("devices"))
        dispatch(setOpenedDevicesSubtab(DeviceTabState.modifyPallet))
    }

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
            onClick={handleClick}
        />
    )
}
export default PalletComponent;