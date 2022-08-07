import { useState } from "react";
import Board from "../BoardComponent/Board";
import HeaderComponent from "../Header/HeaderComponent";
import MenuBarComponent from "../MenuBar/MenuBarComponent";
import "./styles.css";

const ContentPageComponent = () => {

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
        <MenuBarComponent />
        <Board />
      </div>
    </>
  )
}

export default ContentPageComponent;