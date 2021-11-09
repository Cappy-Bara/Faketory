import { useDispatch, useSelector } from "react-redux";
import { IState } from "../../States";
import { setLogout } from "../../States/userAccount/actions";
import axiosInstance from "../../API/axiosConfig";

import "./styles.css";

const HeaderComponent = () => {

    const dispatch = useDispatch();
    const loggedUser = useSelector<IState, String|null>(state => state.loggedUser);

    const handleClick = () => {
        axiosInstance.logout();
        dispatch(setLogout());
    }

    return (
        <div className="header">
            <div className="button text float-end mx-4" onClick={handleClick}>Log out</div>
            <div className="text float-end">Logged as: {loggedUser}</div>
        </div>
    );
}

export default HeaderComponent;