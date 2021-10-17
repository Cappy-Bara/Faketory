import { useState } from "react";
import { Button, Row, Form, Col } from "react-bootstrap"
import { addPallet } from "../../../../../API/Pallets/pallets";
import { DeviceTabState } from "../../EDevicesTabState";



const AddPalletTabComponent = ({ changeActiveTab }: any) => {

    const defaultValue = {
        posX: 0,
        posY: 0,
    }

    const [formData, updateFormData] = useState<any>(defaultValue);

    const handleChange = (e: any) => {
        const fieldName = e.target.name;

        updateFormData({
            ...formData,
            [fieldName]: Number.parseInt(e.target.value.trim())
        });
    };

    const handleCreate = () => {
        console.log(formData);
        addPallet(formData);
    }

    return (
        <>

            <h3 className="text-center">Add Pallet</h3>

            <Form className="pt-3">


                <Row className="pb-4">
                    <Col className="mx-3 px-3">
                        <Form.Label>X Position</Form.Label>
                        <Form.Control size="sm" name="posX" type="number" min={0} className="m-0 px-2" defaultValue={0} onChange={handleChange} />
                    </Col>
                    <Col className="mx-3 px-3">
                        <Form.Label>Y Position</Form.Label>
                        <Form.Control size="sm" name="posY" type="number" className="m-0 px-2" min={0} defaultValue={0} onChange={handleChange} />
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

export default AddPalletTabComponent;
