import { useState } from "react";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import "./styles.css";
import { LoginData } from "../../../API/Account/types";
import { useDispatch } from "react-redux";
import { setLoggedUser } from "../../../States/userAccount/actions";
import { login } from "../../../API/Account/actions";

const LoginFormComponent = ({ setIsRegistered }: any) => {

    const dispatch = useDispatch();

    const handleRegister = () => {
        setIsRegistered(false);
    }
    const defaultValue: LoginData = {
        email: "",
        password: ""
    };
    const [formData, updateFormData] = useState<LoginData>(defaultValue);
    const handleChange = (e: any) => {
        const fieldName = e.target.name;
        let value = e.target.value.trim();
        updateFormData({
            ...formData,
            [fieldName]: value
        });
    };

    const handleSubmit = () => {
        login(formData).then(r => {
             dispatch(setLoggedUser({
                 email: formData.email,
                 token: r.toString()}
        ))})};

    return (
            <>
                <h1>Login</h1>
                <Form>
                    <Form.Group className="mb-3" controlId="formBasicEmail">
                        <Form.Label>Email address</Form.Label>
                        <Form.Control name="email" type="email" placeholder="Enter email" onChange={handleChange} />
                    </Form.Group>

                    <Form.Group className="mb-3" controlId="formBasicPassword">
                        <Form.Label>Password</Form.Label>
                        <Form.Control name="password" type="password" placeholder="Password" onChange={handleChange} />
                    </Form.Group>

                    <Button variant="primary" onClick={handleSubmit}>
                        Submit
                    </Button>
                    <span className="float-end pt-2 normal-text">
                        Don't have an account? <span className="text-button" onClick={handleRegister}> Sign up now!</span>
                    </span>
                </Form>
            </>
        );
    }

    export default LoginFormComponent;