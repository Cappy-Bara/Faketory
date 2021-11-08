import { useEffect, useState } from "react";
import { Button, Row, Form, Col } from "react-bootstrap"
import { useDispatch, useSelector } from "react-redux";
import { deletePallet, updatePallet } from "../../../../../API/Pallets/pallets";
import { IState } from "../../../../../States";
import { setUserPallets } from "../../../../../States/devices/userPallets/actions";
import { setOpenedDevicesSubtab } from "../../../../../States/menuBar/openedDevicesSubtab/actions";
import Pallet from "../../../../Devices/PalletComponent/Types";
import { DeviceTabState } from "../../EDevicesTabState";



const ModifyPalletTabComponent = () => {

    const dispatch = useDispatch();
    const PalletToModify = useSelector<IState, Pallet>(state => state.palletToModify);
    const UserPallets = useSelector<IState, Pallet[]>(state => state.userPallets);
    
    const [formData, updateFormData] = useState<any>();

    const handleChange = (e: any) => {
        const fieldName = e.target.name;
        let value = e.target.value.trim()
        value = Number.parseInt(value);

        updateFormData({
            ...formData,
            [fieldName]: value
        });
    };

    useEffect(() => {
        let form = {
            palletId: PalletToModify.id,
            posX: PalletToModify.posX,
            posY: PalletToModify.posY,
        }
        updateFormData(form)
    }, [PalletToModify])

    const handleModify = () => {
        updatePallet(formData).then(() => {
            var newPallet : Pallet = {
                id: PalletToModify.id,
                posX: formData.posX,
                posY: formData.posY,
            }
            var palletList = UserPallets;
            var index = palletList.findIndex(x => x.id === newPallet.id);
            palletList[index] = newPallet;
            dispatch(setUserPallets([...palletList]));
        });
    }

    const handleRemove = () => {
        deletePallet(PalletToModify.id).then(() => {
            dispatch(setUserPallets(UserPallets.filter(x => x.id !== PalletToModify.id)));
            dispatch(setOpenedDevicesSubtab(DeviceTabState.list));
        })
    }


    return (
        <>
            <h3 className="text-center">Modify Palllet</h3>

            <Form className="pt-3">
                <Row className="pb-3">
                    <Col className="mx-3 px-3">
                        <Form.Label>X Position</Form.Label>
                        <Form.Control key={PalletToModify.posX} size="sm" name="posX" type="number" min={0} className="m-0 px-2" defaultValue={PalletToModify.posX} onChange={handleChange} />
                    </Col>
                    <Col className="mx-3 px-3">
                        <Form.Label>Y Position</Form.Label>
                        <Form.Control size="sm" name="posY" type="number" className="m-0 px-2" min={0} defaultValue={PalletToModify.posY} key={PalletToModify.posY} onChange={handleChange} />
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

export default ModifyPalletTabComponent;
