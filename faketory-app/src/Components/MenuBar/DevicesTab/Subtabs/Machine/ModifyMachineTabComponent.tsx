import { useEffect, useState } from "react";
import { Button, Row, Form, Col } from "react-bootstrap"
import { useDispatch, useSelector } from "react-redux";
import { deleteMachine, updateMachine } from "../../../../../API/Machines/machines";
import { IState } from "../../../../../States";
import { setUserMachines } from "../../../../../States/devices/userMachines/actions";
import { setOpenedDevicesSubtab } from "../../../../../States/menuBar/openedDevicesSubtab/actions";
import Machine from "../../../../Devices/MachineComponent/types";
import { Slot } from "../../../PLCTab/Types";
import { DeviceTabState } from "../../EDevicesTabState";



const ModifyMachineTabComponent = () => {

    const userMachines = useSelector<IState, Machine[]>(state => state.userMachines);

    const dispatch = useDispatch();
    const MachineToModify = useSelector<IState, Machine>(state => state.machineToModify);
    const [formData, updateFormData] = useState<any>();

    const handleChange = (e: any) => {
        const fieldName = e.target.name;
        let value = e.target.value.trim()

        updateFormData({
            ...formData,
            [fieldName]: value
        });
    };

    useEffect(() => {
        let form = {
            id: MachineToModify.id,
            posX: MachineToModify.posX,
            posY: MachineToModify.posY,
            randomFactor : MachineToModify.randomFactor,
        }
        updateFormData(form)
    }, [MachineToModify])

    const handleModify = () => {
        updateMachine(formData).then(() => {
            var MachineList = userMachines;
            var newMachine: Machine = {
                id: MachineToModify.id,
                posX: formData.posX,
                posY: formData.posY,
                randomFactor : formData.randomFactor,
                isProcessing : MachineToModify.isProcessing,
                processingTimestampAmount : formData.processingTimestampAmount
            }
            var MachineList = userMachines;
            var index = MachineList.findIndex(x => x.id === MachineToModify.id)
            MachineList[index] = newMachine;
            dispatch(setUserMachines([...MachineList]));
        });
    }

    const handleRemove = () => {
        deleteMachine(MachineToModify.id).then(() =>{
            dispatch(setUserMachines(userMachines.filter(x => x.id !== MachineToModify.id)));
            dispatch(setOpenedDevicesSubtab(DeviceTabState.list));
        }
        )
    }


    return (
        <>
            <h3 className="text-center">Modify Machine</h3>

            <Form className="pt-3">
                <Row className="pb-3">
                    <Col className="mx-3 px-3">
                        <Form.Label>X Position</Form.Label>
                        <Form.Control key={MachineToModify.posX} size="sm" name="posX" type="number" min={0} className="m-0 px-2" defaultValue={MachineToModify.posX} onChange={handleChange} />
                    </Col>
                    <Col className="mx-3 px-3">
                        <Form.Label>Y Position</Form.Label>
                        <Form.Control size="sm" name="posY" type="number" className="m-0 px-2" min={0} defaultValue={MachineToModify.posY} key={MachineToModify.posY} onChange={handleChange} />
                    </Col>
                </Row>

                <Row className="pb-3">
                    <Col className="mx-3 px-3">
                        <Form.Label>Processing Time in timestamps</Form.Label>
                        <Form.Control size="sm" name="processingTimestampAmount" type="number" min={0} className="m-0 px-2" key={MachineToModify.processingTimestampAmount} defaultValue={MachineToModify.processingTimestampAmount} onChange={handleChange} />
                    </Col>
                    <Col className="mx-3 px-3">
                        <Form.Label>Random Factor</Form.Label>
                        <Form.Control size="sm" name="randomFactor" type="number" className="m-0 px-2" min={0} key={MachineToModify.randomFactor} defaultValue={MachineToModify.randomFactor} onChange={handleChange} />
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

export default ModifyMachineTabComponent;
