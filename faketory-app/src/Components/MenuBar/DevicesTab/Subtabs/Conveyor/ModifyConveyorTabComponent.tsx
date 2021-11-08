import { useEffect, useState } from "react";
import { Button, Row, Form, Col } from "react-bootstrap"
import { useDispatch, useSelector } from "react-redux";
import { deleteConveyor, updateConveyor } from "../../../../../API/Conveyors/conveyors";
import { IState } from "../../../../../States";
import { setUserConveyors } from "../../../../../States/devices/userConveyors/actions";
import { setOpenedDevicesSubtab } from "../../../../../States/menuBar/openedDevicesSubtab/actions";
import Conveyor from "../../../../Devices/ConveyorComponent/Types";
import { Slot } from "../../../PLCTab/Types";
import { DeviceTabState } from "../../EDevicesTabState";



const ModifyConveyorTabComponent = () => {

    const [isVertical, setIsVertical] = useState(false);
    const userSlots = useSelector<IState, Slot[]>(state => state.userSlots);
    const userConveyors = useSelector<IState, Conveyor[]>(state => state.userConveyors);
    const dispatch = useDispatch();    
    const ConveyorToModify = useSelector<IState, Conveyor>(state => state.conveyorToModify);
    const [formData, updateFormData] = useState<any>();
    const [negativeLogic, setNegativeLogic] = useState(ConveyorToModify.negativeLogic);


    const handleChange = (e: any) => {
        const fieldName = e.target.name;
        let value = e.target.value.trim()

        if (["isVertical", "isTurnedDownOrLeft"].includes(fieldName)) {
            value = value > 0;
            if (fieldName === "isVertical") {
                setIsVertical(value);
            }
        }

        else if (fieldName === "slotId") { }

        else if (fieldName === "negativeLogic") {
            setNegativeLogic(e.target.checked);
            value = e.target.checked;
        }

        else {
            value = Number.parseInt(value);
        }

        updateFormData({
            ...formData,
            [fieldName]: value
        });
    };

    useEffect(() => {
        setNegativeLogic(ConveyorToModify.negativeLogic)
        let form = {
            conveyorId: ConveyorToModify.id,
            slotId: ConveyorToModify.slot,
            posX: ConveyorToModify.posX,
            posY: ConveyorToModify.posY,
            length: ConveyorToModify.length,
            isVertical: ConveyorToModify.isVertical,
            isTurnedDownOrLeft: ConveyorToModify.isTurnedDownOrLeft,
            frequency: ConveyorToModify.frequency,
            bit: ConveyorToModify.bit,
            byte: ConveyorToModify.byte,
            negativeLogic: ConveyorToModify.negativeLogic
        }
        updateFormData(form)
    },[ConveyorToModify])

    const handleModify = () => {
        updateConveyor(formData).then(() => {
            var newConveyor : Conveyor = {
                id: ConveyorToModify.id,
                posX: formData.posX,
                posY: formData.posY,
                length: formData.length,
                isRunning: ConveyorToModify.isRunning,
                isTurnedDownOrLeft: formData.isTurnedDownOrLeft,
                isVertical: formData.isVertical,
                frequency: formData.frequency,
                slot: formData.slotId,
                byte: formData.byte,
                bit: formData.bit,
                negativeLogic: formData.negativeLogic
            }
            var conveyorList = userConveyors;
            var index = conveyorList.findIndex(x => x.id === newConveyor.id);
            conveyorList[index] = newConveyor;
            console.log(conveyorList);
            dispatch(setUserConveyors([...conveyorList]));
        });
    }

    const handleRemove = () => {
        deleteConveyor(ConveyorToModify.id).then(() => {
            dispatch(setUserConveyors(userConveyors.filter(x => x.id !== ConveyorToModify.id)));
            dispatch(setOpenedDevicesSubtab(DeviceTabState.list));
        })
    }


    return (
        <>
            <h3 className="text-center">Modify Conveyor</h3>

            <Form className="pt-3">
                <Row className="pb-3">
                    <Col className="mx-3 px-3">
                        <Form.Label>X Position</Form.Label>
                        <Form.Control key={ConveyorToModify.posX} size="sm" name="posX" type="number" min={0} className="m-0 px-2" defaultValue={ConveyorToModify.posX} onChange={handleChange} />
                    </Col>
                    <Col className="mx-3 px-3">
                        <Form.Label>Y Position</Form.Label>
                        <Form.Control size="sm" name="posY" type="number" className="m-0 px-2" min={0} defaultValue={ConveyorToModify.posY} key={ConveyorToModify.posY} onChange={handleChange} />
                    </Col>
                </Row>

                <Row className="pb-3">
                    <Col className="mx-3 px-3">
                        <Form.Label>Frequency</Form.Label>
                        <Form.Control size="sm" name="frequency" type="number" min={0} className="m-0 px-2" key={ConveyorToModify.frequency} defaultValue={ConveyorToModify.frequency} onChange={handleChange} />
                    </Col>
                    <Col className="mx-3 px-3">
                        <Form.Label>Length</Form.Label>
                        <Form.Control size="sm" name="length" type="number" className="m-0 px-2" min={1} key={ConveyorToModify.length} defaultValue={ConveyorToModify.length} onChange={handleChange} />
                    </Col>
                </Row>

                <Row className="pb-3">
                    <Col className="mx-3 px-3">
                        <Form.Label>Orientation</Form.Label>
                        <Form.Select
                            size="sm"
                            name="isVertical"
                            id="inlineFormCustomSelect"
                            defaultValue={ConveyorToModify.isVertical ? 1 : 0}
                            key={ConveyorToModify.isVertical ? 1 : 0}
                            onChange={handleChange}
                        >
                            <option value={0}>Horizontal</option>
                            <option value={1}>Vertical</option>
                        </Form.Select>
                    </Col>
                    <Col className="mx-3 px-3">
                        <Form.Label>Direction</Form.Label>
                        <Form.Select
                            size="sm"
                            name="isTurnedDownOrLeft"
                            id="inlineFormCustomSelect"
                            defaultValue={ConveyorToModify.isTurnedDownOrLeft ? 1 : 0}
                            key={ConveyorToModify.isTurnedDownOrLeft ? 1 : 0}
                            onChange={handleChange}
                        >
                            {isVertical ?
                                <>
                                    <option value={0}>Up</option>
                                    <option value={1}>Down</option>
                                </>
                                :
                                <>
                                    <option value={0}>Right</option>
                                    <option value={1}>Left</option>
                                </>}
                        </Form.Select>
                    </Col>
                </Row>

                <Row className="pb-3">
                    <Col className="mx-3 px-3">
                        <Form.Label>Byte</Form.Label>
                        <Form.Control size="sm" name="byte" type="number" min={0} className="m-0 px-2" key={ConveyorToModify.byte} defaultValue={ConveyorToModify.byte} onChange={handleChange} />
                    </Col>
                    <Col className="mx-3 px-3">
                        <Form.Label>Bit</Form.Label>
                        <Form.Control size="sm" name="bit" type="number" className="m-0 px-2" min={0} key={ConveyorToModify.bit} defaultValue={ConveyorToModify.bit} onChange={handleChange} />
                    </Col>
                </Row>

                <Row>
                    <Col className="mx-3 px-3 mb-3">
                        <Form.Label>Slot</Form.Label>
                        <Form.Select
                            size="sm"
                            name="slotId"
                            id="inlineFormCustomSelect"
                            defaultValue={ConveyorToModify.slot}
                            key={ConveyorToModify.slot}
                            onChange={handleChange}
                        >
                            <option value={"null"}>Select</option>
                            {userSlots && userSlots.map(slot => {
                                return <option value={slot.id}>{slot.number}</option>
                            })}
                        </Form.Select>
                    </Col>
                </Row>


                <Row className="pb-3">
                    <Col className="mx-1 ps-4">
                        <Form.Check
                            name="negativeLogic"
                            type="switch"
                            id="negativeLogic"
                            label="Negative Logic"
                            key={Math.random()}
                            defaultChecked={negativeLogic}
                            onChange={handleChange}
                        />
                    </Col>
                </Row>

                <Button
                    size="sm"
                    variant="secondary"
                    className="float-start add-button px-3 mx-2"
                    onClick={() => dispatch(setOpenedDevicesSubtab(DeviceTabState.list))}
                >
                    Back
                </Button>

                <Button
                    size="sm"
                    variant="warning"
                    className="float-end add-button px-3 mx-2"
                    onClick={() => handleModify()}
                >
                    Modify
                </Button>
                
                <Button
                    size="sm"
                    variant="danger"
                    className="float-end add-button px-3 mx-2"
                    onClick={() => handleRemove()}
                >
                    Remove
                </Button>
            </Form>
        </>
    )
}

export default ModifyConveyorTabComponent;
