import { useEffect, useState } from "react";
import { Button, Row, Form, Col } from "react-bootstrap"
import { useDispatch, useSelector } from "react-redux";
import { deleteSensor, updateSensor } from "../../../../../API/Sensors/sensors";
import { IState } from "../../../../../States";
import { setUserSensors } from "../../../../../States/devices/userSensors/actions";
import { setOpenedDevicesSubtab } from "../../../../../States/menuBar/openedDevicesSubtab/actions";
import Sensor from "../../../../Devices/SensorComponent/Types";
import { Slot } from "../../../PLCTab/Types";
import { DeviceTabState } from "../../EDevicesTabState";



const ModifySensorTabComponent = () => {

    const userSlots = useSelector<IState, Slot[]>(state => state.userSlots);
    const userSensors = useSelector<IState, Sensor[]>(state => state.userSensors);

    const dispatch = useDispatch();
    const SensorToModify = useSelector<IState, Sensor>(state => state.sensorToModify);
    const [formData, updateFormData] = useState<any>();
    const [negativeLogic, setNegativeLogic] = useState(SensorToModify.negativeLogic);

    const handleChange = (e: any) => {
        const fieldName = e.target.name;
        let value = e.target.value.trim()

        if (fieldName === "slotId") { }

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
        setNegativeLogic(SensorToModify.negativeLogic)
        let form = {
            sensorId: SensorToModify.id,
            slotId: SensorToModify.slotId,
            posX: SensorToModify.posX,
            posY: SensorToModify.posY,
            bit: SensorToModify.bit,
            byte: SensorToModify.byte,
            negativeLogic: SensorToModify.negativeLogic
        }
        updateFormData(form)
    }, [SensorToModify])

    const handleModify = () => {
        updateSensor(formData).then(() => {
            var newSensor: Sensor = {
                id: SensorToModify.id,
                posX: formData.posX,
                posY: formData.posY,
                isSensing: SensorToModify.isSensing,
                negativeLogic: formData.negativeLogic,
                slotId: formData.slotId,
                bit: formData.bit,
                byte: formData.byte
            }
            var sensorList = userSensors;
            var index = sensorList.findIndex(x => x.id === newSensor.id)
            sensorList[index] = newSensor;
            dispatch(setUserSensors([...sensorList]));
        });
    }

    const handleRemove = () => {
        deleteSensor(SensorToModify.id).then(() =>{
            dispatch(setUserSensors(userSensors.filter(x => x.id !== SensorToModify.id)));
            dispatch(setOpenedDevicesSubtab(DeviceTabState.list));
        }
        )
    }


    return (
        <>
            <h3 className="text-center">Modify Sensor</h3>

            <Form className="pt-3">
                <Row className="pb-3">
                    <Col className="mx-3 px-3">
                        <Form.Label>X Position</Form.Label>
                        <Form.Control key={SensorToModify.posX} size="sm" name="posX" type="number" min={0} className="m-0 px-2" defaultValue={SensorToModify.posX} onChange={handleChange} />
                    </Col>
                    <Col className="mx-3 px-3">
                        <Form.Label>Y Position</Form.Label>
                        <Form.Control size="sm" name="posY" type="number" className="m-0 px-2" min={0} defaultValue={SensorToModify.posY} key={SensorToModify.posY} onChange={handleChange} />
                    </Col>
                </Row>

                <Row className="pb-3">
                    <Col className="mx-3 px-3">
                        <Form.Label>Byte</Form.Label>
                        <Form.Control size="sm" name="byte" type="number" min={0} className="m-0 px-2" key={SensorToModify.byte} defaultValue={SensorToModify.byte} onChange={handleChange} />
                    </Col>
                    <Col className="mx-3 px-3">
                        <Form.Label>Bit</Form.Label>
                        <Form.Control size="sm" name="bit" type="number" className="m-0 px-2" min={0} key={SensorToModify.bit} defaultValue={SensorToModify.bit} onChange={handleChange} />
                    </Col>
                </Row>

                <Row>
                    <Col className="mx-3 px-3 mb-3">
                        <Form.Label>Slot</Form.Label>
                        <Form.Select
                            size="sm"
                            name="slotId"
                            id="inlineFormCustomSelect"
                            defaultValue={SensorToModify.slotId}
                            key={SensorToModify.slotId}
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

export default ModifySensorTabComponent;
