import { useEffect, useState } from 'react';
import { Table, Form, Button } from 'react-bootstrap';
import { connectToPlc, deletePlc, getPlcs, getPlcStatuses } from '../../../API/plcs';
import { getSlots, deleteSlot, addSlot, bindSlotWithPlc } from '../../../API/slots';
import './styles.css';
import { PLC, Slot } from './Types';

const PlcTabComponent = () => {

    const plc: PLC = {
        id: "fe123-dbe3-45aa-b973-6977b834826b",
        ip: "127.0.0.1",
        model: 1200,
        isConnected: true,
    }

    const plc2: PLC = {
        id: "fe08d3cd-dbe3-45aa-b973-6977b834826b",
        ip: "127.0.0.2",
        model: 1500,
        isConnected: false,
    }

    const plc3: PLC = {
        id: "f111d3cd-dbe3-45aa-b973-6977b834826b",
        ip: "127.0.0.2",
        model: 1500,
        isConnected: false,
    }

    const slot: Slot = {
        id: "ed43b66b-e5d2-4fa6-94f8-28aa29b50ae7",
        number: 1,
        plcId: "fe08d3c3-dbe3-45aa-b973-6977b834826b",
    }

    const [plcs, setPlcs] = useState<PLC[]>([plc, plc2, plc3]);
    const [slots, setSlots] = useState<Slot[]>([slot]);
    const [hoveredPlc, setHoveredPlc] = useState("");

    const plcChanged = (slotId: string, plcId: string) => {
        if (plcId == "none") {
            console.log("Should remove PLC from slot");
        }
        else {
            bindSlotWithPlc(slotId, plcId);
        }
    }

    const refreshPlcStatusHandler = () => {
        getPlcStatuses().then(response => {
            var newPlcs = plcs;
            response && newPlcs.forEach(plc => {
                plc.isConnected = (response.plcs.find(x => x.plcId === plc.id)?.status ?? false)
            })
            setPlcs(newPlcs);
        })
    }
    
    const removeSlotHandler = (slotId: string) => {
        deleteSlot(slotId).then(r => {
            reloadSlotsRequest();
        });
    }

    const reloadSlotsRequest = () => {
        getSlots().then(response => {
            setSlots(response.slots)
        })
    }

    const reloadSlots = useEffect(() => {
        reloadSlotsRequest();
    }, [])

    const addSlotHandler = () => {
        addSlot().then(r => {
            reloadSlotsRequest();
        });
    }

    const reloadPlcsRequest = () => {
        getPlcs().then(response => {
            setPlcs(response.plcs);
        })
    }

    const reloadPlcs = useEffect(() => {
        reloadPlcsRequest();
    }, [])


    const reloadPlcStatuses = useEffect(() => {
        refreshPlcStatusHandler();
    }, [plcs])





    const connectToPlcHandler = (plcId: string) => {
        connectToPlc(plcId).then(() => {
            refreshPlcStatusHandler();
        })
    };

const removePlcHandler = (plcId: string) => {
    deletePlc(plcId).then(() => {
        reloadPlcsRequest();
    })
}

return (
    <>
        <h3>SLOTS</h3>
        <div className="scrollable-table">
            <Table size="sm" striped>
                <tbody>
                    {
                        slots && slots.map(slot => {
                            return (
                                <tr>
                                    <td className="py-0 text-center number">{slot.number}</td>
                                    <td>
                                        <Form.Control
                                            className="py-0"
                                            as="select"
                                            defaultValue={slot.plcId ? slot.plcId : "none"}
                                            onChange={(e) => plcChanged(slot.id, e.target.value)}
                                            aria-label="Default select example">
                                            <option value="none">Unspecified</option>
                                            {plcs && plcs.map(plc => {
                                                { return (<option value={plc.id}>{plc.ip}</option>) }
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

        <Button
            className="my-3 mx-1 col-5"
            variant="success"
            onClick={() => addSlotHandler()}
        >
            Add Slot
        </Button>

        <h3 className="">PLCs</h3>
        <div className="scrollable-table">
            <Table size="sm" striped>
                <tbody>
                    {
                        plcs && plcs.map(plc => {
                            return (
                                <tr>
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
        </div>






    </>
)
}
export default PlcTabComponent;

function reloadPlcsRequest() {
    throw new Error('Function not implemented.');
}
