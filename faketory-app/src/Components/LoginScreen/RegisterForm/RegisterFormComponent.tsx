import { useState } from "react";
import { Form,Button } from "react-bootstrap";
import { useDispatch } from "react-redux";
import { register } from "../../../API/Account/actions";
import { RegisterAccountData } from "../../../API/Account/types";
import { setLoggedUser } from "../../../States/userAccount/actions";
import "./styles.css";


const RegisterFormComponent = ({setIsRegistered}:any) => {

    const dispatch = useDispatch();

    const handleLogin = () => {
        setIsRegistered(true);
    }

    const defaultValue : RegisterAccountData = {
        email:"",
        password:"",
        repeatPassword:"",
    };

    const [formData, updateFormData] = useState<RegisterAccountData>(defaultValue);
    
    const handleChange = (e: any) => {
        const fieldName = e.target.name;
        let value = e.target.value.trim();
        updateFormData({
            ...formData,
            [fieldName]: value
        });
    };

    const handleSubmit = () => {
        register(formData).then(r => {
             dispatch(setLoggedUser({
                 email: formData.email,
                 token: r.toString()}
        ))})};

    return (
        <>
        <h1>Register</h1>
        <Form>
            <Form.Group className="mb-3" controlId="formBasicEmail">
                <Form.Label>Email address</Form.Label>
                <Form.Control name="email" type="email" placeholder="Enter email" onChange={handleChange} />
            </Form.Group>

            <Form.Group className="mb-3" controlId="formBasicPassword">
                <Form.Label>Password</Form.Label>
                <Form.Control name="password" type="password" placeholder="Password" onChange={handleChange}/>
            </Form.Group>

            <Form.Group className="mb-3" controlId="formBasicPassword">
                <Form.Label>Repeat Password</Form.Label>
                <Form.Control name="repeatPassword" type="password" placeholder="Repeat Password" onChange={handleChange}/>
            </Form.Group>

            <Button variant="primary" onClick={handleSubmit}>
                Submit
            </Button>
            <span className="float-end pt-2 normal-text">
                Already have an account? <span className="text-button" onClick={handleLogin}> Login!</span>
            </span>
        </Form>
    </>
    );
}

export default RegisterFormComponent;