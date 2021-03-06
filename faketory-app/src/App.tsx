import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import LoginScreenComponent from './Components/LoginScreen/LoginScreenComponent';
import ContentPageComponent from './Components/ContentPage/ContentPageComponent';
import { useSelector } from 'react-redux';
import { IState } from './States';
function App() {

  const user = useSelector<IState, String|null>(state => state.loggedUser);

  return(
    user ? 
    <ContentPageComponent />
    :
    <LoginScreenComponent />)

}

export default App;
