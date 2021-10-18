import Conveyor from "./Types";
import './styles.css';

interface Props {
    conveyor: Conveyor;
}

const ConveyorComponent = ({ conveyor }: Props) => {
    const tileSize: number = 3.2;

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

    return (
        <div
            className="conveyor-base"
            style={{
                bottom: `${calculatePosBottom()}vw`,
                left: `${calculatePosLeft()}vw`,
                width: `${calculateWidth()}vw`,
                height: `${calculateHeight()}vw`,
            }}
        />
    )
}
export default ConveyorComponent;