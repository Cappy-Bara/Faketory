import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import Board from './Components/BoardComponent/Board';
import MenuBarComponent from './Components/MenuBar/MenuBarComponent';
import { useState } from 'react';
function App() {

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
  );
}

export default App;
