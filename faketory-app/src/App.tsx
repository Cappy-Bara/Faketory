import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import { useState } from 'react';
import LoginScreenComponent from './Components/LoginScreen/LoginScreenComponent';
import ContentPageComponent from './Components/ContentPage/ContentPageComponent';
function App() {

  const [token, setToken] = useState<boolean>(true);

  return(
    token ? 
    <ContentPageComponent />
    :
    <LoginScreenComponent />)

}

export default App;
