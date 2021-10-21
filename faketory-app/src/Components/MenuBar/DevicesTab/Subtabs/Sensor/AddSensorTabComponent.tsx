import { useState } from "react";
import { Button, Row, Form, Col } from "react-bootstrap"
import { useDispatch } from "react-redux";
import { addSensor } from "../../../../../API/Sensors/sensors";
import { setOpenedDevicesSubtab } from "../../../../../States/menuBar/openedDevicesSubtab/actions";
import { DeviceTabState } from "../../EDevicesTabState";



const AddSensorTabComponent = () => {

    const defaultValue = {
        slotId: null,
        posX: 0,
        posY: 0,
        bit: 0,
        byte: 0,
        negativeLogic: false
    }

    const dispatch = useDispatch();
    const [formData, updateFormData] = useState<any>(defaultValue);

    const handleChange = (e: any) => {
        const fieldName = e.target.name;
        let value = e.target.value.trim()

        if (fieldName === "slotId") { }

        else if (fieldName === "negativeLogic") {
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

    const handleCreate = () => {
        console.log(formData);
        addSensor(formData);
    }

    return (
        <>

            <h3 className="text-center">Add Sensor</h3>

            <Form className="pt-3">


                <Row className="pb-3">
                    <Col className="mx-3 px-3">
                        <Form.Label>X Position</Form.Label>
                        <Form.Control size="sm" name="posX" type="number" min={0} className="m-0 px-2" defaultValue={0} onChange={handleChange} />
                    </Col>
                    <Col className="mx-3 px-3">
                        <Form.Label>Y Position</Form.Label>
                        <Form.Control size="sm" name="posY" type="number" className="m-0 px-2" min={0} defaultValue={0} onChange={handleChange} />
                    </Col>
                </Row>

                <Row className="pb-3">
                    <Col className="mx-3 px-3">
                        <Form.Label>Byte</Form.Label>
                        <Form.Control size="sm" name="byte" type="number" min={0} className="m-0 px-2" defaultValue={0} onChange={handleChange} />
                    </Col>
                    <Col className="mx-3 px-3">
                        <Form.Label>Bit</Form.Label>
                        <Form.Control size="sm" name="bit" type="number" className="m-0 px-2" min={0} defaultValue={0} onChange={handleChange} />
                    </Col>
                </Row>

                <Row>
                    <Col className="mx-3 px-3 mb-3">
                        <Form.Label>Slot</Form.Label>
                        <Form.Select
                            size="sm"
                            name="slotId"
                            id="inlineFormCustomSelect"
                            defaultValue={"null"}
                            onChange={handleChange}
                        >
                            <option value={"null"}>Select</option>
                            <option value={"4bb433f1-4a4a-43f1-1732-08d95d116f0d"}>1</option>
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
                    variant="success"
                    className="float-end add-button px-3 mx-2"
                    onClick={() => handleCreate()}
                >
                    Create
                </Button>
            </Form>
        </>
    )
}

export default AddSensorTabComponent;
