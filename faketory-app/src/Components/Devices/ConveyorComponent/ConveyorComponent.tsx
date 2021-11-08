import Conveyor from "./Types";
import './styles.css';
import { useDispatch } from "react-redux";
import { setConveyorToModify } from "../../../States/menuBar/deviceToModify/conveyorToModify/actions";
import { setOpenedDevicesSubtab } from "../../../States/menuBar/openedDevicesSubtab/actions";
import { setOpenedTab } from "../../../States/menuBar/openedTab/actions";
import { DeviceTabState } from "../../MenuBar/DevicesTab/EDevicesTabState";

interface Props {
    conveyor: Conveyor;
}

const ConveyorComponent = ({ conveyor }: Props) => {
    const tileSize: number = 3.2;
    const dispatch = useDispatch();

    const calculateWidth = () => {
        if (conveyor.isVertical) {
            return tileSize;
        }
        return tileSize * conveyor.length;
    }

    const calculateHeight = () => {
        if (conveyor.isVertical) {
            return tileSize * conveyor.length;
        }
        return tileSize;
    }

    const calculatePosBottom = () => {
        if (conveyor.isVertical) {
            return (conveyor.isTurnedDownOrLeft ? (conveyor.posY + 1 - conveyor.length) * tileSize : conveyor.posY * tileSize);
        }
        return (conveyor.posY * tileSize)
    }

    const calculatePosLeft = () => {
        if (!conveyor.isVertical) {
            return (conveyor.isTurnedDownOrLeft ? (conveyor.posX + 1 - conveyor.length) * tileSize : conveyor.posX * tileSize);
        }
        return (conveyor.posX * tileSize)
    }

    const handleClick = () => {
        dispatch(setConveyorToModify(conveyor));
        dispatch(setOpenedTab("devices"))
        dispatch(setOpenedDevicesSubtab(DeviceTabState.modifyConveyor))
    }

    return (
        <div
            className="conveyor-base"
            style={{
                bottom: `${calculatePosBottom()}vw`,
                left: `${calculatePosLeft()}vw`,
                width: `${calculateWidth()}vw`,
                height: `${calculateHeight()}vw`,
            }}
            onClick={(handleClick)}
        />
    )
}
export default ConveyorComponent;