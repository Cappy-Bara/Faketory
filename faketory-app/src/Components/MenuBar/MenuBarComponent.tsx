import {Tab,Tabs } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { IState } from "../../States";
import { setOpenedDevicesSubtab } from "../../States/menuBar/openedDevicesSubtab/actions";
import { setOpenedTab } from "../../States/menuBar/openedTab/actions";
import DevicesTabComponent from "./DevicesTab/DevicesTabComponent";
import { DeviceTabState } from "./DevicesTab/EDevicesTabState";
import MainTabComponent from "./MainTab/MainTabComponent";
import PlcTabComponent from "./PLCTab/PlcTabComponent";

const MenuBarComponent = ({autoTimestamp,setAutoTimestamp}:any) => {
    
    const openedTab = useSelector<IState,string>(state => state.openedTab);
    const openedDevicesSubtab = useSelector<IState,DeviceTabState>(state => state.openedDevicesSubtab);
    const dispatch = useDispatch();

    return (
        <div className="menu-bar">
            <Tabs
                id="controlled-tab-example"
                activeKey={openedTab}
                onSelect={(k) =>{
                    k === "devices" && 
                    openedTab === "devices" &&
                    dispatch(setOpenedDevicesSubtab(DeviceTabState.list));
                    dispatch(setOpenedTab(k ? k : ""));
                } }
                className="mb-3"
            >
                <Tab eventKey="home" title="Home">
                    <MainTabComponent autoTimestamp={autoTimestamp} setAutoTimestamp={setAutoTimestamp}/>
                </Tab>
                <Tab eventKey="plcs" title="PLCs">
                    <PlcTabComponent />
                </Tab>
                <Tab eventKey="devices" title="Devices">
                    <DevicesTabComponent />
                </Tab>
            </Tabs>
        </div>
    )
}

export default MenuBarComponent;