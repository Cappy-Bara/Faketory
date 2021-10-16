import { useState } from "react";
import { Button, Row, Form, Col } from "react-bootstrap"
import { addConveyor } from "../../../../../API/Conveyors/conveyors";
import { DeviceTabState } from "../../EDevicesTabState";
import { CreateConveyorForm } from "./types";



const AddConveyorTabComponent = ({ changeActiveTab }: any) => {

    const [isVertical, setIsVertical] = useState(false);

    const defaultValue = {
        slotId: null,
        posX: 0,
        posY: 0,
        length: 1,
        isVertical: false,
        isTurnedDownOrLeft: false,
        frequency: 0,
        bit: 0,
        byte: 0,
        negativeLogic: false
    }

    const [formData, updateFormData] = useState<any>(defaultValue);

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
        addConveyor(formData);
    }

    return (
        <>

            <h3 className="text-center">Add Conveyor</h3>

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
                        <Form.Label>Frequency</Form.Label>
                        <Form.Control size="sm" name="frequency" type="number" min={0} className="m-0 px-2" defaultValue={0} onChange={handleChange} />
                    </Col>
                    <Col className="mx-3 px-3">
                        <Form.Label>Length</Form.Label>
                        <Form.Control size="sm" name="length" type="number" className="m-0 px-2" min={1} defaultValue={1} onChange={handleChange} />
                    </Col>
                </Row>

                <Row className="pb-3">
                    <Col className="mx-3 px-3">
                        <Form.Label>Orientation</Form.Label>
                        <Form.Select
                            size="sm"
                            name="isVertical"
                            id="inlineFormCustomSelect"
                            defaultValue={0}
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
                            defaultValue={0}
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
                            <option value={"127.0.07.74"}>ch</option>
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
                    onClick={() => changeActiveTab(DeviceTabState.list)}
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

export default AddConveyorTabComponent;
