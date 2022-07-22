import { useDispatch } from "react-redux";
import Machine from "./types";
import "./styles.css"
import { setOpenedTab } from "../../../States/menuBar/openedTab/actions";
import { setMachineToModify } from "../../../States/menuBar/deviceToModify/machineToModify/actions";
import { DeviceTabState } from "../../MenuBar/DevicesTab/EDevicesTabState";
import { setOpenedDevicesSubtab } from "../../../States/menuBar/openedDevicesSubtab/actions";

interface Props{
    machine :Machine;
}


const MachineComponent = ({machine} : Props) => {

    const dispatch = useDispatch();

    const handleClick = () => {
        dispatch(setMachineToModify(machine));
        dispatch(setOpenedTab("devices"))
        dispatch(setOpenedDevicesSubtab(DeviceTabState.modifyMachine))
    }

    const tileSize: number = 3.2;

    return(
        <div
            key={machine.posX}
            className={machine.isProcessing ? "machine-processing" : "machine-not-processing"}
            onClick={handleClick}
            style={{
                position: `absolute`,
                bottom:`${((machine.posY)*tileSize)}vw`,
                left:`${((machine.posX)*tileSize)}vw`,
                width: `${tileSize}vw`,
                height: `${tileSize}vw`,
            }}
        />
    )
}
export default MachineComponent;