import Conveyor from "./Types";
import './styles.css';
import {getConveyor} from "../../API/conveyors"

interface Props{
    conveyor:Conveyor;
}

const ConveyorComponent = ({conveyor}:Props) => {

const tileSize: number = 3.2;

const calculateWidth = () => {
    if(conveyor.isVertical){
        return tileSize;
    }
    return tileSize*conveyor.length;
}

const calculateHeight = () => {
    if(conveyor.isVertical){
        return tileSize*conveyor.length;
    }
    return tileSize;
}

const calculatePosBottom = () => {
    if(conveyor.isVertical){
        return (conveyor.isTurnedDownOrLeft ? (conveyor.posY+1 - conveyor.length)*tileSize : conveyor.posY*tileSize);
    }
    return(conveyor.posY*tileSize)
}

const calculatePosLeft = () => {
    if(!conveyor.isVertical){
        return (conveyor.isTurnedDownOrLeft ? (conveyor.posX+1 - conveyor.length)*tileSize : conveyor.posX*tileSize);
    }
    return(conveyor.posX*tileSize)
}

const handleClick = async () => {
    var d = await getConveyor(conveyor.id);
    console.log(d.id);
}

    return(
        <div
        className="conveyor-base"
            style={{
                bottom:`${calculatePosBottom()}vw`,
                left:`${calculatePosLeft()}vw`,
                width: `${calculateWidth()}vw`,
                height: `${calculateHeight()}vw`,
            }}
            onClick={handleClick}
        />
    )
}
export default ConveyorComponent;