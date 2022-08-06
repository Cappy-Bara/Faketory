import { useState } from "react";
import { Button, Row, Form, Col } from "react-bootstrap"
import { useDispatch, useSelector } from "react-redux";
import { allElemets } from "../../../../../API/Actions/actions";
import { addMachine } from "../../../../../API/Machines/machines";
import { setUserMachines } from "../../../../../States/devices/userMachines/actions";
import { setOpenedDevicesSubtab } from "../../../../../States/menuBar/openedDevicesSubtab/actions";
import { DeviceTabState } from "../../EDevicesTabState";

const AddMachineTabComponent = () => {

    const defaultValue = {
        posX: 0,
        posY: 0,
        processingTimestampAmount: 0,
        randomFactor: 0,
    }

    const dispatch = useDispatch();
    const [formData, updateFormData] = useState<any>(defaultValue);

    const handleChange = (e: any) => {
        const fieldName = e.target.name;
        let value = e.target.value.trim()
        value = Number.parseInt(value);

        updateFormData({
            ...formData,
            [fieldName]: value
        });
    };

    const handleCreate = () => {
        addMachine(formData).then(() => {
            allElemets().then(response => {
                dispatch(setUserMachines(response.machines));
              });
            dispatch(setOpenedDevicesSubtab(DeviceTabState.list));
        });
    }

    return (
        <>

            <h3 className="text-center">Add Machine</h3>

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
                        <Form.Label>Processing Time in timestamps</Form.Label>
                        <Form.Control size="sm" name="processingTimestampAmount" type="number" min={0} className="m-0 px-2" defaultValue={0} onChange={handleChange} />
                    </Col>
                    <Col className="mx-3 px-3">
                        <Form.Label>Random Factor</Form.Label>
                        <Form.Control size="sm" name="randomFactor" type="number" className="m-0 px-2" min={0} defaultValue={0} onChange={handleChange} />
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

export default AddMachineTabComponent;
