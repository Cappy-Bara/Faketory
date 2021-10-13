import { useState } from "react";
import { ToggleButton } from "react-bootstrap";
import { Button,Tab,Tabs } from "react-bootstrap";
import MainTabComponent from "./MainTab/MainTabComponent";
import PlcTabComponent from "./PLCTab/PlcTabComponent";

const MenuBarComponent = ({autoTimestamp,setAutoTimestamp}:any) => {

    const [key, setKey] = useState('home');

    return (
        <div className="menu-bar">
        {console.log("Menu Bar Rendered")}
            <Tabs
                id="controlled-tab-example"
                activeKey={key}
                onSelect={(k) => setKey(k ? k : "")}
                className="mb-3"
            >
                <Tab eventKey="home" title="Home">
                    <MainTabComponent autoTimestamp={autoTimestamp} setAutoTimestamp={setAutoTimestamp}/>
                </Tab>
                <Tab eventKey="plcs" title="PLCs">
                    <PlcTabComponent />
                </Tab>
                <Tab eventKey="utils" title="UTILITIES" disabled>
                    <p>Page 3</p>
                </Tab>
            </Tabs>
        </div>
    )
}

export default MenuBarComponent;