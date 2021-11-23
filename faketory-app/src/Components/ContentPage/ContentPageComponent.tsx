import { useState } from "react";
import Board from "../BoardComponent/Board";
import HeaderComponent from "../Header/HeaderComponent";
import MenuBarComponent from "../MenuBar/MenuBarComponent";
import "./styles.css";

const ContentPageComponent = () => {

  const [autoTimestamp, setAutoTimestamp] = useState<boolean>(true);



  

  return (
    <>
      <HeaderComponent />
      <div
        style={{
          display: "flex",
          alignContent: "center",
          justifyContent: "right",
        }}
      >
        <MenuBarComponent autoTimestamp={autoTimestamp} setAutoTimestamp={setAutoTimestamp} />
        <Board autoTimestampOn={autoTimestamp} />
      </div>
    </>
  )
}

export default ContentPageComponent;