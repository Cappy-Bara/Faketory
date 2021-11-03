import { useState } from "react";
import "./styles.css";
import LoginFormComponent from "./LoginForm/LoginFormComponent";
import RegisterFormComponent from "./RegisterForm/RegisterFormComponent";

const LoginScreenComponent = () => {

    const [isRegistered, setIsRegistered] = useState<boolean>(true);

    return (
        <>

            <div className="form">
                {isRegistered ? <LoginFormComponent setIsRegistered={setIsRegistered} /> : <RegisterFormComponent setIsRegistered={setIsRegistered} />}
            </div>

        </>
    );
}

export default LoginScreenComponent;