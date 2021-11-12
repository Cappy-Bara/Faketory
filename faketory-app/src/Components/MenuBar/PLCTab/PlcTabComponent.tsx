import { useEffect, useState } from 'react';
import { Table, Form, Button, Row, Col } from 'react-bootstrap';
import { connectToPlc, deletePlc, getPlcs, getPlcStatuses, addPlc } from '../../../API/Plcs/plcs';
import { getSlots, deleteSlot, addSlot, bindSlotWithPlc } from '../../../API/Slots/slots';
import { CreatePlc } from '../../../API/Plcs/types';
import './styles.css';
import { PLC, Slot } from './Types';
import { useDispatch, useSelector } from 'react-redux';
import { IState } from '../../../States';
import { setUserSlots } from '../../../States/userSlots/actions';

const PlcTabComponent = () => {

    const [plcs, setPlcs] = useState<PLC[]>();
    const [hoveredPlc, setHoveredPlc] = useState("");
    const [formData, updateFormData] = useState<any>();
    const userSlots = useSelector<IState, Slot[]>(state => state.userSlots);
    const dispatch = useDispatch();

    const plcChanged = (slotId: string, plcId: string) => {
        if (plcId === "none") {
            console.log("Should remove PLC from slot");
        }
        else {
            bindSlotWithPlc(slotId, plcId).then(() => {
                reloadSlotsRequest();
            }
            );
        }
    }

    const refreshPlcsHandler = () => {
        getPlcStatuses().then(response => {
            getPlcs().then(getPlcResponse => {
                const newPlcs = getPlcResponse.plcs;
                response && newPlcs.forEach(plc => {
                    plc.isConnected = (response.plcs.find(x => x.plcId === plc.id)?.status ?? false)
                })
                setPlcs(newPlcs);
            })
        })
    }

    const removeSlotHandler = (slotId: string) => {
        deleteSlot(slotId).then(r => {
            reloadSlotsRequest();
        });
    }

    const reloadSlotsRequest = () => {
        getSlots().then(response => {
            var value = response.slots ? response.slots : [] as Slot[]; 
            dispatch(setUserSlots(value));
        })
    }

    useEffect(() => {
        reloadSlotsRequest();
    }, [])

    const addSlotHandler = () => {
        addSlot().then(r => {
            reloadSlotsRequest();
        });
    }

    useEffect(() => {
        refreshPlcsHandler();
    }, [])

    const connectToPlcHandler = (plcId: string) => {
        connectToPlc(plcId).then(() => {
            refreshPlcsHandler();
        })
    };

    const removePlcHandler = (plcId: string) => {
        deletePlc(plcId).then(() => {
            refreshPlcsHandler();
        })
    }

    const handleChange = (e: any) => {
        updateFormData({
            ...formData,
            [e.target.name]: e.target.value.trim()
        });
    };

    const addPlcHandler = (e: any) => {
        e.preventDefault()
        addPlc(formData as CreatePlc).then(() => {
            refreshPlcsHandler();
        });
    };


    return (
        <>
            <div
                style={{
                    display: "flex",
                    justifyContent: "space-around",
                    alignItems: "flex-end"
                }}>
                <h3 className="m-0 p-0">
                    Slots
                </h3>
                <Button
                    size="sm"
                    className=""
                    variant="success"
                    onClick={() => addSlotHandler()}
                >
                    Add Slot
                </Button>
            </div>

            <div className="scrollable-table mt-1 mb-4">
                <Table size="sm" striped>
                    <tbody>
                        {
                            userSlots && userSlots.map(slot => {
                                return (
                                    <tr key={slot.id}>
                                        <td className="py-0 text-center number">{slot.number}</td>
                                        <td>
                                            <Form.Control
                                                className="py-0"
                                                as="select"
                                                value={slot.plcId ? slot.plcId : "none"}
                                                onChange={(e) => plcChanged(slot.id, e.target.value)}
                                                aria-label="Default select example">
                                                <option key={0} value="none">Unspecified</option>
                                                {plcs && plcs.map(plc => {
                                                    return <option key={plc.id} value={plc.id}>{plc.ip}</option>
                                                })}
                                            </Form.Control>
                                        </td>
                                        <td className="p-0 ">
                                            <p className="remove-button text-center m-auto p-auto" onClick={() => removeSlotHandler(slot.id)}>
                                                Remove
                                            </p>
                                        </td>
                                    </tr>)
                            })
                        }
                    </tbody>
                </Table>
            </div>

            <h3 className="text-center">PLCs</h3>
            <section className="scrollable-table">
                <Table size="sm" striped>
                    <tbody>
                        {
                            plcs && plcs.map(plc => {
                                return (
                                    <tr key={plc.id}>
                                        <td className="p-0 text-center number">{plc.ip}</td>
                                        {plc.isConnected ?
                                            <td className="py-0 m-0 text-center number">
                                                <p
                                                    onMouseEnter={(e) => setHoveredPlc(plc.id)}
                                                    onMouseLeave={(e) => setHoveredPlc("")}
                                                    className="m-0 connected">
                                                    {hoveredPlc === plc.id ? "Disconnect" : "Connected"}
                                                </p>
                                            </td>
                                            :
                                            <td className="py-0 m-0 text-center number">
                                                <p
                                                    onMouseEnter={(e) => setHoveredPlc(plc.id)}
                                                    onMouseLeave={(e) => setHoveredPlc("")}
                                                    onClick={() => connectToPlcHandler(plc.id)}
                                                    className="m-0 not-connected">
                                                    {hoveredPlc === plc.id ? "Connect" : "Not Connected"}
                                                </p>
                                            </td>}
                                        <td className="p-0 text-center">
                                            <p
                                                className="p-0 remove-button text-center m-0"
                                                onClick={() => removePlcHandler(plc.id)}>
                                                Remove
                                            </p>
                                        </td>
                                    </tr>)
                            })
                        }
                    </tbody>
                </Table>
            </section>

            <div>
                <Form>
                    <Row>
                        <Col>
                            <Form.Control size="sm" name="ip" className="ms-2" placeholder="IP Address" onChange={handleChange} />
                        </Col>
                        <Col className="px-1">
                            <Form.Select
                                size="sm"
                                name="modelId"
                                id="inlineFormCustomSelect"
                                defaultValue={0}
                                onChange={handleChange}
                            >
                                <option key={0} value={0}>Model</option>
                                <option key={300} value={300}>S7-300</option>
                                <option key={400} value={400}>S7-400</option>
                                <option key={1200} value={1200}>S7-1200</option>
                                <option key={1500} value={1500}>S7-1500</option>
                            </Form.Select>
                        </Col>
                        <Col className="px-1">
                            <Button className="m-0" size="sm" variant="primary" onClick={addPlcHandler}>
                                Add Plc
                            </Button>
                        </Col>
                    </Row>
                </Form>
            </div>
        </>
    )
}
export default PlcTabComponent;