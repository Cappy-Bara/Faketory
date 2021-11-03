import { useState } from "react";
import Board from "../BoardComponent/Board";
import MenuBarComponent from "../MenuBar/MenuBarComponent";

const ContentPageComponent = () => {

    const [autoTimestamp, setAutoTimestamp] = useState<boolean>(true);

    return (
        <div
        style={{
          display: "flex",
          alignContent: "center",
          justifyContent: "right",
        }}
      >
        <MenuBarComponent autoTimestamp={autoTimestamp} setAutoTimestamp={setAutoTimestamp}/>
        <Board autoTimestampOn={autoTimestamp}/>
      </div>
    )
}

export default ContentPageComponent;