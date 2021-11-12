import { useDispatch, useSelector } from "react-redux";
import { IState } from "../../States";
import { setLogout } from "../../States/userAccount/actions";
import axiosInstance from "../../API/axiosConfig";

import "./styles.css";

const HeaderComponent = () => {

    const dispatch = useDispatch();
    const loggedUser = useSelector<IState, String|null>(state => state.loggedUser);

    const handleClick = () => {
        dispatch(setLogout());
    }

    return (
        <div className="header">
            <div className="text title ms-3 float-start">F@KETORY v.1.0</div>
            <div className="text me mt-2 ms-1 float-start">by Kacper Pacholczak</div>
            <div className="button text float-end mx-4" onClick={handleClick}>Log out</div>
            <div className="text float-end">Logged as: {loggedUser}</div>
        </div>
    );
}

export default HeaderComponent;